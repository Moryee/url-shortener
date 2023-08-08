import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ShortUrlsComponent } from './components/short-urls/short-urls.component';
import { ShortUrlsDetailComponent } from './components/short-urls-detail/short-urls-detail.component';
import { ShortUrlsCreateComponent } from './components/short-urls-create/short-urls-create.component';
import { LoginComponent } from './components/login/login.component';
import { ShortUrlsRedirectComponent } from './components/short-urls-redirect/short-urls-redirect.component';

const routes: Routes = [
  { path: '', component: ShortUrlsComponent },
  { path: 'short-urls', component: ShortUrlsComponent },
  { path: 'short-urls/detail/:id', component: ShortUrlsDetailComponent },
  { path: 'short-urls/create', component: ShortUrlsCreateComponent },
  { path: 'short', component: ShortUrlsRedirectComponent },
  { path: 'short/:shortUrl', component: ShortUrlsRedirectComponent },
  { path: 'login', component: LoginComponent },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
