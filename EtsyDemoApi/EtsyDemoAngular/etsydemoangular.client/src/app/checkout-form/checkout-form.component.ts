import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { CartRequest } from '../Models/CartRequest';
import { CartItemDto } from '../Models/CartItemDto';
import { CartService } from '../Services/cart.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-checkout-form',
  templateUrl: './checkout-form.component.html',
  styleUrl: './checkout-form.component.css'
})
export class CheckoutFormComponent {
  checkoutForm = new FormGroup({
    personalInfo: new FormGroup({
      fullName: new FormControl('', [Validators.required]),
      lastName: new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.required, Validators.email]),
      phone: new FormControl('', [Validators.required])
    }),
    shippingAddress: new FormGroup({
      street: new FormControl('', [Validators.required]),
      door: new FormControl('', [Validators.required]),
      city: new FormControl('', [Validators.required]),
      postalCode: new FormControl('', [Validators.required]),
      state: new FormControl('', [Validators.required])
    }),
    optional: new FormGroup({
      companyName: new FormControl(''),
      specialInstructions: new FormControl('')
    })
  });

  constructor(private cartService: CartService, private router: Router) { }


  onSubmit() {
    if (this.checkoutForm.valid) {
      const cartItems: CartItemDto[] = JSON.parse(localStorage.getItem('cart') || '[]');
      const userId = JSON.parse(localStorage.getItem('userId') || '0'); 


      const cartRequest: CartRequest = {
        userId: userId,
        products: cartItems
      };

      this.cartService.checkout(cartRequest).subscribe(
        response => {
          console.log('Purchase finalized', response);
          this.router.navigate(['/purchase-success']);
        },
        error => {
          console.error('Error finalizing purchase', error);
        }
      );
      localStorage.removeItem('cart');
    } else {
      console.log('Form Data is invalid:', this.checkoutForm.value);
    }
  }
    

}
