import { Component } from '@angular/core';
import { ProductService } from '../Services/product.service';
import { Subject, debounceTime, distinctUntilChanged } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-product-search',
  templateUrl: './product-search.component.html',
  styleUrls: ['./product-search.component.css']
})
export class ProductSearchComponent {
  private searchTerms = new Subject<string>();
  constructor(private productService: ProductService, private router: Router) {
    this.searchTerms.pipe(
      debounceTime(300),       // Espera 300ms después de cada pulsación antes de considerar el término
      distinctUntilChanged()  // Solo busca si el término ha cambiado
    ).subscribe(term => this.productService.searchProducts(term));
  }

  searchProducts(term: string): void {
    this.searchTerms.next(term);
  }

  ngOnDestroy() {
    this.searchTerms.unsubscribe();
  }

  goToRegister() {
    this.router.navigate(['/register']);  // Navega a la ruta de registro
  }

  goToLogin() {
    this.router.navigate(['/login']);
  }


}
