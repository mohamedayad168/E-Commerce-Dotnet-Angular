import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";

import {HTTP_INTERCEPTORS, HttpClientModule} from "@angular/common/http";
import {CoreModule} from "./core/core.module";
import {ShopModule} from "./shop/shop.module";
import {PaginationModule} from "ngx-bootstrap/pagination";
import {HomeModule} from "./home/home.module";
import {ErrorInterceptor} from "./core/interceptors/error.interceptor";

@NgModule({
  declarations: [
    AppComponent,

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    TooltipModule.forRoot(),
    BrowserAnimationsModule,
    HttpClientModule,
    CoreModule,

    HomeModule

  ],
  providers: [
    provideClientHydration(),
    {provide:HTTP_INTERCEPTORS,useClass:ErrorInterceptor,multi:true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
