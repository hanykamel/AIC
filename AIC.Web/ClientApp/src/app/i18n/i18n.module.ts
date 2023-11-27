import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  TranslateCacheModule,
  TranslateCacheService,
  TranslateCacheSettings,
} from 'ngx-translate-cache';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import * as moment from 'moment';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import {
  TranslateLoader,
  TranslateModule,
  TranslateService,
} from '@ngx-translate/core';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    HttpClientModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: httpTranslateLoader,
        deps: [HttpClient],
      },
    }),
    TranslateCacheModule.forRoot({
      cacheService: {
        provide: TranslateCacheService,
        useFactory: translateCacheFactory,
        deps: [TranslateService, TranslateCacheSettings],
      },
    }),
  ],
  exports: [TranslateModule],
})
export class I18nModule {
  constructor(
    translate: TranslateService,
    translateCacheService: TranslateCacheService
  ) {
    translateCacheService.init();
    translate.addLangs(['ar', 'en']);
    const browserLang = translateCacheService.getCachedLanguage() || 'ar';
    const currentLang = localStorage.getItem('oldLanguage')
      ? localStorage.getItem('oldLanguage')
      : 'en';
    translate.use(currentLang);
    translate.currentLang = currentLang;
    moment.locale(translate.currentLang);
  }
}

export function httpTranslateLoader(http: HttpClient) {
  return new TranslateHttpLoader(http, './assets/i18n/', '.json');
}
export function translateCacheFactory(
  translateService: TranslateService,
  translateCacheSettings: TranslateCacheSettings
) {
  return new TranslateCacheService(translateService, translateCacheSettings);
}
