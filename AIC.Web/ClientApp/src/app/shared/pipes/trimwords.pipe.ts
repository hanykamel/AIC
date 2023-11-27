import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'trimwords'
})
export class TrimwordsPipe implements PipeTransform {

  transform(value: string, maxLen: number, separator?: string): string {
    if (value.length <= maxLen) return value;
    separator = (separator) ? separator : ' ';
    return value.substr(0, value.lastIndexOf(separator, maxLen));
  }

}
