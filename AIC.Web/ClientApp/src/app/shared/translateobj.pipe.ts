import { Pipe, PipeTransform } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Pipe({
  name: 'translateobj'
})
export class Translateobj implements PipeTransform {

  constructor(private translateService: TranslateService) { }
  transform(value) {
    if (!value) return "";
    return this.translateService.currentLang == "en" ? value.Title : value.TitleAr;
  }

}
