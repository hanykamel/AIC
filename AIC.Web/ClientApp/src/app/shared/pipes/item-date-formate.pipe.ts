import { Pipe, PipeTransform } from '@angular/core';
import * as moment from 'moment';

@Pipe({
  name: 'itemDateFormate'
})
export class ItemDateFormatePipe implements PipeTransform {

  transform(value) {
    if (value.Lang) {
      moment.locale(value.Lang);
      if (value.Date)
        return moment(value.Date).format('DD MMMM YYYY');
      else
        return "";
    }
    return "";
  }
}
