import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';


@Component({
  selector: 'app-checkout-form',
  templateUrl: './checkout-form.component.html',
  styleUrl: './checkout-form.component.css'
})
export class CheckoutFormComponent {
  checkoutForm = new FormGroup({
    personalInfo: new FormGroup({
      fullName: new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.required, Validators.email]),
      phone: new FormControl('', [Validators.required])
    }),
    shippingAddress: new FormGroup({
      street: new FormControl('', [Validators.required]),
      city: new FormControl('', [Validators.required]),
      postalCode: new FormControl('', [Validators.required]),
      state: new FormControl('', [Validators.required]),
      country: new FormControl('', [Validators.required])
    }),
    optional: new FormGroup({
      companyName: new FormControl(''),
      specialInstructions: new FormControl('')
    })
  });

  onSubmit() {
    console.log('Form Data:', this.checkoutForm.value);
    // Implement your submission logic here
  }

}
