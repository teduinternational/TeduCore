import { Component, OnInit } from '@angular/core';
import { SystemConstants } from '@shared/common/system.constants';
import { UrlConstants } from '@shared/common/url.constants';
import { UtilityService } from '@shared/services/utility.service';
import { AuthenService } from '@shared/services/authen.service';
import { LoggedInUser } from '@shared/domain/loggedin.user';
import { environment } from '@environments/environment';

@Component({
  templateUrl: './app.component.html',
})
export class AppComponent {
  public user: LoggedInUser;
  public baseFolder: string = environment.API_URL;

  constructor(private utilityService: UtilityService,
    private authenService: AuthenService) { }

  ngOnInit() {
    $('body').attr('class', 'nav-md');
    this.user = JSON.parse(localStorage.getItem(SystemConstants.CURRENT_USER));
  }
  logout() {
    localStorage.removeItem(SystemConstants.CURRENT_USER);
    this.utilityService.navigate(UrlConstants.LOGIN);
  }
}
