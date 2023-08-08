import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { ShortUrlsComponent } from './components/short-urls/short-urls.component';
import { AppRoutingModule } from './app-routing.module';
import { ShortUrlsDetailComponent } from './components/short-urls-detail/short-urls-detail.component';
import { FormsModule } from '@angular/forms';
import { ShortUrlsCreateComponent } from './components/short-urls-create/short-urls-create.component';
import { HeaderComponent } from './components/header/header.component';
import { LoginComponent } from './components/login/login.component';
import { ShortUrlsRedirectComponent } from './components/short-urls-redirect/short-urls-redirect.component';

@NgModule({
  declarations: [
    AppComponent,
    ShortUrlsComponent,
    ShortUrlsDetailComponent,
    ShortUrlsCreateComponent,
    HeaderComponent,
    LoginComponent,
    ShortUrlsRedirectComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
