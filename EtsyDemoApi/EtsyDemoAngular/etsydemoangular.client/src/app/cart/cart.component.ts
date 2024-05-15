import { Component, OnInit } from '@angular/core';
import { CartService } from '../Services/cart.service';
import { UserAuthService } from '../Services/user-auth.service';
import { Router } from '@angular/router';
import { ChangeDetectorRef } from '@angular/core';
import { CartItem } from '../Models/CartItem';


@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  cart: any[] = []; 
  displayedColumns: string[] = ['image', 'title', 'quantity', 'price', 'delete'];

  constructor(private cartService: CartService,
              private authService: UserAuthService,
              private router: Router,
              private cd: ChangeDetectorRef,) { }

  ngOnInit(): void {
    this.loadCart();
  }

  loadCart() {
    if (this.authService.isLoggedIn()) {  
      const storedCart = localStorage.getItem('cart');
      if (storedCart) {
        this.cart = JSON.parse(storedCart);
      } else {
        this.cart = []; // Si no hay nada en el almacenamiento local, inicializa con un arreglo vacío
      }
    } else {
      console.error('Usuario no logueado');
      this.router.navigate(['/login']);
    }
  }

  updateQuantity(item: any, change: number) {
    let cart: any[] = JSON.parse(localStorage.getItem('cart') || '[]');
    const index = cart.findIndex(p => p.productId === item.productId);
    if (index !== -1) {
      if (cart[index].quantity + change > 0) {
        cart[index].quantity += change;
      } else {
        cart.splice(index, 1); 
      }
      localStorage.setItem('cart', JSON.stringify(cart));
      this.cart = cart; 
    }
  }

  deleteItem(productId: string) {
    let cart: CartItem[] = JSON.parse(localStorage.getItem('cart') || '[]');
    console.log('Cart before deletion:', JSON.stringify(cart));
    cart = cart.filter(item => item.productId !== productId);
    console.log('Cart after deletion:', JSON.stringify(cart));
    localStorage.setItem('cart', JSON.stringify(cart));
    this.cart = cart; 
    this.cd.detectChanges(); 
  }


  getTotal(): number {
    return this.cart.reduce((acc, item) => acc + (item.quantity * item.price), 0);
  }

  proceedToCheckout(): void {
    const userId = this.authService.getUserId();
    console.log('userId : ', userId);
    if (userId === null) {
      console.error('No hay usuario logueado');
      this.router.navigate(['/login']);
      return;
    }
    const cartData = {
      UserId: userId,
      Products: this.cart.map(item => ({
        productId: item.productId,
        quantity: item.quantity
      }))
    };
    console.log('Datos del carrito a enviar:', cartData);
    this.cartService.createCart(cartData).subscribe({
      next: (response) => {
        console.log('Compra completada:', response);
        localStorage.removeItem('cart');
        this.router.navigate(['/checkout']);
      },
      error: (error) => console.error('Error al registrar el carrito:', error)
    });
  }
}
