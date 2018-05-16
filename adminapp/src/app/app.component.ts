import { Component, AfterViewChecked, ElementRef } from '@angular/core';

@Component({
  template: '<router-outlet></router-outlet>',
})
export class AppComponent implements AfterViewChecked {
  constructor(private elementRef: ElementRef) {

  }
  ngAfterViewChecked() {
    var s = document.createElement("script");
    s.type = "text/javascript";
    s.src = "../assets/js/custom.js";
    this.elementRef.nativeElement.appendChild(s);
  }
}
