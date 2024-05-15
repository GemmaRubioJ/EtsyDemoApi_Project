import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ProductsComponent } from './Products/products.component';
import { ProductSearchComponent } from './product-search/product-search.component';
import { FooterComponent } from './footer/footer.component';
import { ProductDetailComponent } from './product-detail/product-detail.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { PromoBannerComponent } from './promo-banner/promo-banner.component';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { ReactiveFormsModule } from '@angular/forms';
import { SuccessDialogComponent } from './success-dialog-component/success-dialog-component.component';
import { UserExistsDialogComponent } from './user-exist-dialog/user-exist-dialog.component';
import { CartComponent } from './cart/cart.component';
import { CheckoutFormComponent } from './checkout-form/checkout-form.component';

@NgModule({
  declarations: [
    AppComponent,
    ProductsComponent,
    ProductSearchComponent,
    FooterComponent,
    ProductDetailComponent,
    PromoBannerComponent,
    RegisterComponent,
    LoginComponent,
    SuccessDialogComponent,
    RegisterComponent,
    UserExistsDialogComponent,
    CartComponent,
    CheckoutFormComponent
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule, ReactiveFormsModule, ReactiveFormsModule,
    FormsModule, MatIconModule, BrowserAnimationsModule, MatTableModule
  ],
  providers: [
    provideAnimationsAsync()
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
