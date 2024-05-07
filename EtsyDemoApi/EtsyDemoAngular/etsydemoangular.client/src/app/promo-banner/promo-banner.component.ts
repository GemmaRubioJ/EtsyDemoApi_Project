import { Component } from '@angular/core';
import { ProductService } from '../Services/product.service';
import { EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-promo-banner',
  templateUrl: './promo-banner.component.html',
  styleUrl: './promo-banner.component.css'
})
export class PromoBannerComponent {

  @Output() buyClicked: EventEmitter<void> = new EventEmitter<void>();
  constructor(private productService: ProductService) { }

  showProducts(): void {
    this.buyClicked.emit();
  }
}
