import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CartRequest } from '../Models/CartRequest';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private apiUrl = 'https://localhost:7088/api/Cart'; 

  constructor(private http: HttpClient) { }

 
  getCart(userId: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/${userId}`);
  }


  createCart(cartData: any): Observable<any> {
    console.log('Enviando datos al servidor:', cartData);
    return this.http.post(`${this.apiUrl}/register`, cartData, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    });
  }


  updateCart(id: number, cartData: any): Observable<any> {
    return this.http.put(`${this.apiUrl}/update/${id}`, cartData);
  }


  deleteCart(idCart: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/delete/${idCart}`);
  }

  addToCart(productId: string, quantity: number = 1, image: string): Observable<any> {
    const cartData = {
      productId: productId,
      quantity: quantity,
      image: image
    };
    return this.http.post(`${this.apiUrl}/register`, cartData);
  }

  checkout(cartRequest: CartRequest): Observable<any> {
    return this.http.post(`${this.apiUrl}/checkout`, cartRequest);
  }

}

