import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';

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
    UserExistsDialogComponent
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule, ReactiveFormsModule, ReactiveFormsModule,
    FormsModule
  ],
  providers: [
    provideAnimationsAsync()
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
