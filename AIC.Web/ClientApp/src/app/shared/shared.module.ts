import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MomentModule } from 'ngx-moment';

import { HeaderComponent } from '../core/components/header/header.component';
import { FooterComponent } from '../core/components/footer/footer.component';
import { MenubarModule } from 'primeng/menubar';
import { MenuModule } from 'primeng/menu';

import { DialogModule } from 'primeng/dialog';

import { GenericContentComponent } from './generic-content/generic-content.component';
import { ListComponent } from './generic-our-people/list/list.component';
import { DetailsComponent } from './generic-our-people/details/details.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CarouselModule } from 'ngx-owl-carousel-o';
import { SidebarModule } from 'primeng/sidebar';
import { GenericSliderComponent } from './generic-slider/generic-slider.component';
import { RelatedNewsComponent } from './related-news/related-news.component';

import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { HttpClient } from '@angular/common/http';
import { GenericListComponent } from './generic-list/generic-list.component';
import { GenericDetailsComponent } from './generic-details/generic-details.component';
import { GenericSocialIconsComponent } from './generic-social-icons/generic-social-icons.component';
import { GenericGalleryComponent } from './generic-gallery/generic-gallery.component';
import { SafeUrlPipe } from './pipes/safe-url.pipe';
import { Translateobj } from './translateobj.pipe';
import { GenericGalleryViewComponent } from './generic-gallery-view/generic-gallery-view.component';
import { GalleriaModule } from 'primeng/galleria';
import { NoDataFoundComponent } from './no-data-found/no-data-found.component';

import { InfiniteScrollModule } from "ngx-infinite-scroll";
import { PageLoaderComponent } from './page-loader/page-loader.component';
import { DataLoaderComponent } from './data-loader/data-loader.component';
import { SharepointPaginationComponent } from './sharepoint-pagination/sharepoint-pagination.component';
import { ButtonModule } from 'primeng/button';

import { DropdownModule } from 'primeng/dropdown';
import { TabViewModule } from 'primeng/tabview';
import { AccordionModule } from 'primeng/accordion';
import { FormatDatePipe } from './pipes/formate-date.pipe';
import { ItemDateFormatePipe } from './pipes/item-date-formate.pipe';
import { GenericEmailInputComponent } from './generic-email-input/generic-email-input.component';
import { RxReactiveFormsModule } from '@rxweb/reactive-form-validators';
import { CalendarModule } from 'primeng/calendar';
import { ToastModule } from 'primeng/toast';
import { RedirectSwitcherComponent } from './redirect-switcher/redirect-switcher.component';
import { TrimwordsPipe } from './pipes/trimwords.pipe';
import { QRCodeModule } from 'angularx-qrcode';
import { SafeHtmlPipe } from './pipes/safe-html.pipe';

export function createTranslateLoader(http: HttpClient) {
  return new TranslateHttpLoader(http, './assets/i18n/', '.json');
}

@NgModule({
  declarations: [
    GenericContentComponent,
    ListComponent,
    DetailsComponent,
    GenericSliderComponent, HeaderComponent, FooterComponent,
    GenericListComponent, GenericDetailsComponent, GenericSocialIconsComponent, GenericGalleryComponent,
    SafeUrlPipe, PageLoaderComponent, DataLoaderComponent, SharepointPaginationComponent
    , GenericGalleryComponent, GenericGalleryViewComponent, Translateobj, FormatDatePipe, ItemDateFormatePipe
    , RelatedNewsComponent, NoDataFoundComponent, GenericEmailInputComponent, RedirectSwitcherComponent,
    TrimwordsPipe, SafeHtmlPipe, ItemDateFormatePipe

  ],
  imports: [
    CommonModule,
    MomentModule,
    FormsModule, TranslateModule,
    ReactiveFormsModule,
    CarouselModule, SidebarModule, HttpClientModule, MenubarModule, MenuModule, InfiniteScrollModule,
    DialogModule, ButtonModule, 
      GalleriaModule, TabViewModule, AccordionModule, DropdownModule, RxReactiveFormsModule, CalendarModule, ToastModule
  ],
  exports: [
    GenericContentComponent,
    MomentModule,
    TranslateModule,
    ListComponent,
    DetailsComponent, GenericSliderComponent,
    FormsModule, ReactiveFormsModule, FormatDatePipe, ItemDateFormatePipe,
    CarouselModule, SidebarModule, HttpClientModule, HeaderComponent, FooterComponent,
    MenubarModule, MenuModule, DialogModule,
    GenericListComponent, GenericDetailsComponent, GenericSocialIconsComponent, GenericGalleryComponent,
    SafeUrlPipe, InfiniteScrollModule, PageLoaderComponent, DataLoaderComponent, SharepointPaginationComponent
    , GenericGalleryComponent, GenericGalleryViewComponent, TabViewModule, AccordionModule, TrimwordsPipe,
    Translateobj, DropdownModule, RelatedNewsComponent, NoDataFoundComponent, GenericEmailInputComponent,
    CalendarModule, ToastModule
    , QRCodeModule, SafeHtmlPipe
  ],
})
export class SharedModule {}
