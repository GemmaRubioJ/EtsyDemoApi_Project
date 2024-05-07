import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { ProductService } from '../Services/product.service';
import { Product } from '../NewFolder/Product';
import { ProductDetailComponent } from '../product-detail/product-detail.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrl: './products.component.css'
})
export class ProductsComponent implements OnInit, OnDestroy {
  products: Product[] = [];
  filteredProducts: Product[] = [];
  private subscription!: Subscription;

  constructor(private productService: ProductService, public dialog: MatDialog) { }

  ngOnInit() {
    this.subscription = this.productService.currentSearchResults.subscribe(
      products => {
        console.log('Productos cargados:', products);
        this.products = products;
        this.filteredProducts = [...this.products];
      },
      error => {
        console.error('There was an error!', error);
      }
    );
  }

  openDialog(product: Product): void {
    this.dialog.open(ProductDetailComponent, {
      width: '500px',
      data: { productId: product.productId }
    });
  }


  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  filterByCategory(category: string): void {
    if (category) {
      console.log('Filtrando por categoría:', category);
      this.filteredProducts = this.products.filter(product => product.category === category);
    } else {
      // Restablece la vista para mostrar todos los productos si no se proporciona categoría
      this.filteredProducts = [...this.products];
    }
    console.log('Productos filtrados:', this.filteredProducts);
  
  }
}
