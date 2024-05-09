import { Component, OnInit } from '@angular/core';
import { ProductService } from '../Services/product.service';
import { Subject, debounceTime, distinctUntilChanged } from 'rxjs';
import { Router } from '@angular/router';
import { UserAuthService } from '../Services/user-auth.service';

@Component({
  selector: 'app-product-search',
  templateUrl: './product-search.component.html',
  styleUrls: ['./product-search.component.css']
})
export class ProductSearchComponent implements OnInit {

  private searchTerms = new Subject<string>();

  constructor(public productService: ProductService,
    private router: Router,
    public authService: UserAuthService) {
    this.searchTerms.pipe(
      debounceTime(300),
      distinctUntilChanged()
    ).subscribe(term => this.productService.searchProducts(term));
  }

  ngOnInit(): void {
    // No es necesario manejar suscripciones aquí porque se hace en la plantilla con async pipe
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/']); // Navegar al inicio o a la página de login
  }

  searchProducts(term: string): void {
    this.searchTerms.next(term);
  }

  ngOnDestroy() {
    this.searchTerms.unsubscribe();
  }

  goToRegister() {
    this.router.navigate(['/register']);
  }

  goToLogin() {
    this.router.navigate(['/login']);
  }
}
