import { Pipe, PipeTransform } from '@angular/core';
import * as moment from 'moment';

@Pipe({
  name: 'formatDate'
})
export class FormatDatePipe implements PipeTransform {

  transform(value) {
    if (value) {
      moment.locale(localStorage.getItem('oldLanguage'));
      return moment(value).format('DD MMMM YYYY');
    }
    return "";
  }

}
