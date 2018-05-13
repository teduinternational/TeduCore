import { Component, OnInit } from '@angular/core';
import { SystemConstants } from '../shared/common/system.constants';
import { UrlConstants } from '../shared/common/url.constants';
import { UtilityService } from '../shared/services/utility.service';
import { AuthenService } from '../shared/services/authen.service';
import { LoggedInUser } from '../shared/domain/loggedin.user';
import { environment } from '../../environments/environment';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit {
  public user: LoggedInUser;
  public baseFolder: string = environment.API_URL;
  constructor(private utilityService: UtilityService, private authenService: AuthenService) { }

  ngOnInit() {
    $('body').attr('class', 'nav-md');

    this.user = JSON.parse(localStorage.getItem(SystemConstants.CURRENT_USER));
    console.log(this.user);
  }
  logout() {
    localStorage.removeItem(SystemConstants.CURRENT_USER);
    this.utilityService.navigate(UrlConstants.LOGIN);
  }
}
