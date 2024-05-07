import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { filter } from 'rxjs';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent { 
  showBanner: boolean = true;  // Controla la visibilidad del banner
  showProducts: boolean = false;  // Controla la visibilidad de los productos

  constructor(private router: Router) {
    // Escucha los eventos de cambio de ruta
    this.router.events.pipe(
      filter((event): event is NavigationEnd => event instanceof NavigationEnd)
    ).subscribe((event) => {
      // Tu código aquí
      if (event.url === '/register') {
        this.showBanner = false;
        this.showProducts = false;
      } else {
        this.showBanner = true;
        this.showProducts = false;
      }
    });
  }

  toggleDisplay(): void {
    this.showBanner = !this.showBanner;
    this.showProducts = !this.showProducts;
  }
  
}
