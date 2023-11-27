import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SharedModule } from './shared/shared.module';
import { CoreModule } from './core/core.module';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { Globalinterceptor } from './interceptors/Globalinterceptor.interceptor'
import {DialogModule} from 'primeng/dialog';
import { ButtonModule } from 'primeng/button';
import { MessageService } from 'primeng/api';
import { PageLoaderService } from './shared/page-loader/page-loader.service';
import { LoaderInterceptor } from './interceptors/loader-interceptor.interceptor';
import { I18nModule } from './i18n/i18n.module';
import { UnsubscribeComponent } from './newsletter/components/unsubscribe/unsubscribe.component';
import { AdvancedSearchComponent } from './advanced-search/advanced-search/advanced-search.component';
import { HttpErrorInterceptor } from './interceptors/http-error.interceptor';


@NgModule({
  declarations: [
    AppComponent,
    UnsubscribeComponent,
    AdvancedSearchComponent,
  ],
  imports: [
    BrowserModule,
    I18nModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    SharedModule,
    CoreModule,
    HttpClientModule,
    DialogModule,
    ButtonModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: Globalinterceptor, multi: true },
    PageLoaderService, MessageService,
    { provide: HTTP_INTERCEPTORS, useClass: LoaderInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: HttpErrorInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
