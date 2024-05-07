import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { Product } from '../NewFolder/Product';
import { ApiResponse } from '../NewFolder/ApiResponse';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private apiUrl = 'https://localhost:7088/api/Etsy/products';  // Ajusta esto según sea necesario

  private searchUrl = 'https://localhost:7088/api/Etsy/products/search/';

  private searchResults = new BehaviorSubject<Product[]>([]);

  currentSearchResults = this.searchResults.asObservable();

  constructor(private http: HttpClient, private router: Router) {
    this.loadAllProducts();  // Carga inicial de productos al crear el servicio
  }

  // Método para cargar todos los productos al inicializar el servicio
  private loadAllProducts(): void {
    this.getAllProducts().subscribe({
      next: (response) => {
        if (response && response.data) {
          this.searchResults.next(response.data);  // Emitir los productos cargados
        }
      },
      error: (error) => {
        console.error('Failed to fetch initial products:', error);
      }
    });
  }

  // Método para obtener todos los productos
  getAllProducts(): Observable<ApiResponse<Product[]>> {
    return this.http.get<ApiResponse<Product[]>>(`${this.apiUrl}/all`);
  }

  // Método para buscar productos por término
  searchProducts(searchTerm: string): void {
    if (searchTerm) {
      this.http.get<ApiResponse<Product[]>>(`${this.searchUrl}${searchTerm}`).subscribe({
        next: (response) => {
          this.searchResults.next(response.data);
        },
        error: (error) => {
          console.error('Error fetching search results:', error);
        }
      });
    } else {
      this.loadAllProducts();  // Recargar todos los productos si no hay término de búsqueda
    }
  }

    getProductById(productId: string): Observable<ApiResponse<Product>> {
    return this.http.get<ApiResponse<Product>>(`${this.apiUrl}/${productId}`);
  }

  showProducts(): void {
    this.router.navigate([`${this.apiUrl}/all`]);
  }

}
