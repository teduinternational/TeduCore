import { Component, AfterViewChecked, ElementRef } from '@angular/core';

@Component({
  template: '<router-outlet></router-outlet>',
})
export class AppComponent implements AfterViewChecked {
  constructor(private elementRef: ElementRef) {

  }
  ngAfterViewChecked() {

  }
}
