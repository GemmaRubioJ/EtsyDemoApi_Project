import { Component, OnInit, ChangeDetectorRef, Inject } from '@angular/core';
import { ProductService } from '../Services/product.service';
import { Product } from '../Models/Product';
import { MatDialog, MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CartService } from '../Services/cart.service';
import { UserAuthService } from '../Services/user-auth.service';
import { CartItem } from '../Models/CartItem';
import { AuthDialogComponent } from '../auth-dialog/auth-dialog.component';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls:[ './product-detail.component.css']
})
export class ProductDetailComponent implements OnInit {
  product: Product | undefined;
  

  constructor(
    private productService: ProductService,
    private cartService: CartService,
    private cd: ChangeDetectorRef,
    private authService: UserAuthService, 
    private dialog: MatDialog,
    public dialogRef: MatDialogRef<ProductDetailComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) { }

  ngOnInit(): void {
    if (this.data.productId) {
      this.fetchProduct(this.data.productId);
    }
  }

  fetchProduct(productId: string): void {
    console.log('Llamando a fetchProduct con ID:', productId); 
    this.productService.getProductById(productId).subscribe(
      response => {
        console.log('Respuesta recibida:', response);  
        if (response && response.data) {
          console.log('Producto cargado:', response.data);  
          this.product = response.data;
          this.cd.detectChanges();  
        } else {
          console.log('La respuesta no contiene datos.');  
        }
      },
      error => {
        console.error('Error fetching product:', error);  
      }
    );
  }

  closeDialog(): void {
    this.dialogRef.close(); 
  }

  addToCart(product: Product): void {
    if (!this.authService.isLoggedIn()) {
      // Usuario no logueado, mostrar modal de autenticación
      this.dialog.open(AuthDialogComponent, {
        width: '300px',
        height:'200px',
        data: { message: 'Por favor, inicie sesión o registrese para añadir productos al carrito.' }
      });
    } else {
      // Usuario logueado, añadir producto al carrito
      let cart: CartItem[] = JSON.parse(localStorage.getItem('cart') || '[]');
      const index = cart.findIndex(item => item.productId === product.productId);

      if (index === -1) {
        const newItem: CartItem = { ...product, quantity: 1 };
        cart.push(newItem);
      } else {
        cart[index].quantity += 1;
      }

      localStorage.setItem('cart', JSON.stringify(cart));
      this.closeDialog();
    }
  }
  
}
