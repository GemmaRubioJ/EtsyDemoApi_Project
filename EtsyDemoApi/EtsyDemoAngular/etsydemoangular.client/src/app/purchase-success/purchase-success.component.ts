import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-purchase-success',
  templateUrl: './purchase-success.component.html',
  styleUrl: './purchase-success.component.css'
})
export class PurchaseSuccessComponent {
  constructor(private router: Router) { }

  navigateHome() {
    this.router.navigate(['/']);
  }
}
