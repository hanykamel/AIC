import { Pipe, PipeTransform } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';

@Pipe({
  name: 'safeUrl'
})
export class SafeUrlPipe implements PipeTransform {

  constructor(private sanitized: DomSanitizer) { }

  transform(value) {
    if (value.includes("src")) {
      let src = this.getIframeSrc(value)
      return this.sanitized.bypassSecurityTrustResourceUrl(src);
    }
    else {
      return this.sanitized.bypassSecurityTrustResourceUrl(value);
    }
  }

  private getIframeSrc(iframe: any) {
    var parser = new DOMParser();
    var htmlDoc = parser.parseFromString(iframe, 'text/html');
    return htmlDoc.getElementsByTagName('iframe')[0].src;
  }

}
