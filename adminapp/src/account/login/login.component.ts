import { Component, OnInit } from '@angular/core';
import { NotificationService } from '@shared/services/notification.service';
import { AuthenService } from '@shared/services/authen.service';
import { MessageContstants } from '@shared/common/message.constants';
import { UrlConstants } from '@shared/common/url.constants';
import { Router } from '@angular/router';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { SystemConstants } from '@shared/common/system.constants';
import { LoggedInUser } from '@shared/domain/loggedin.user';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loading = false;
  model: any = {};
  returnUrl: string;
  constructor(private authenService: AuthenService,
    private notificationService: NotificationService,
    private router: Router) { }

  ngOnInit() {  
    $('body').attr('class', 'login');
  }
  login() {
    this.loading = true;
    this.authenService.login(this.model.username, this.model.password)
      .subscribe(data => {
        let result = data.json();
        var base64Url = result.token.split('.')[1];
        var base64 = base64Url.replace('-', '+').replace('_', '/');
        var user: LoggedInUser = JSON.parse(window.atob(base64));
        user.access_token = result.token;
        if (user && result.token) {
          localStorage.removeItem(SystemConstants.CURRENT_USER);
          localStorage.setItem(SystemConstants.CURRENT_USER, JSON.stringify(user));
        }
        this.router.navigate([UrlConstants.HOME]);
      }, error => {
        this.notificationService.printErrorMessage(MessageContstants.SYSTEM_ERROR_MSG);
        this.loading = false;
        console.log(error);
      });
  }

}
