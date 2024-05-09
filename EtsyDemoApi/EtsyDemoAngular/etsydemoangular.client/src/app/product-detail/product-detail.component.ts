import { Component, OnInit, ChangeDetectorRef, Inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductService } from '../Services/product.service';
import { Product } from '../Models/Product';
import { ApiResponse } from '../Models/ApiResponse';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls:[ './product-detail.component.css']
})
export class ProductDetailComponent implements OnInit {
  product: Product | undefined;
  

  constructor(
    private productService: ProductService,
    private cd: ChangeDetectorRef,
    public dialogRef: MatDialogRef<ProductDetailComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) { }

  ngOnInit(): void {
    if (this.data.productId) {
      this.fetchProduct(this.data.productId);
    }
  }

  fetchProduct(productId: string): void {
    console.log('Llamando a fetchProduct con ID:', productId);  // Log para ver si el método se llama
    this.productService.getProductById(productId).subscribe(
      response => {
        console.log('Respuesta recibida:', response);  // Log para ver la respuesta completa
        if (response && response.data) {
          console.log('Producto cargado:', response.data);  // Confirmar que se recibe el producto correctamente
          this.product = response.data;
          this.cd.detectChanges();  // Forzar la detección de cambios
        } else {
          console.log('La respuesta no contiene datos.');  // Si no hay datos en la respuesta
        }
      },
      error => {
        console.error('Error fetching product:', error);  // Log de cualquier error recibido
      }
    );
  }

  closeDialog(): void {
    this.dialogRef.close(); // Cerrar el modal
  }
}
