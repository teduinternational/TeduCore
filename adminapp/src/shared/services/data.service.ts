import { Injectable } from '@angular/core';
import { Http, Response, Headers } from '@angular/http';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { SystemConstants } from '@shared/common/system.constants';
import { AuthenService } from '@shared/services/authen.service';
import { NotificationService } from '@shared/services/notification.service';
import { UtilityService } from '@shared/services/utility.service';
import { Observable, throwError } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { MessageContstants } from '@shared/common/message.constants';
import { environment } from '@environments/environment';
import { HttpErrorResponse } from '@angular/common/http';

@Injectable()
export class DataService {
  private headers: HttpHeaders;
  constructor(private _http: HttpClient,
    private _router: Router,
    private _authenService: AuthenService,
    private _notificationService: NotificationService,
    private _utilityService: UtilityService) {

    this.headers = new HttpHeaders();
    this.headers.delete('Authorization');
    this.headers.append('Content-Type', 'application/json');
  }
  get(uri: string) {
    return this._http.get(environment.API_URL + uri, { headers: this.headers });
  }
  post(uri: string, data?: any) {
    return this._http.post(environment.API_URL + uri, data, {
      headers: this.headers
    });
  }
  put(uri: string, data?: any) {

    return this._http.put(environment.API_URL + uri, data, {
      headers: this.headers
    });
  }
  delete(uri: string, key: string, id: string) {
    return this._http.delete(environment.API_URL + uri + "/?" + key + "=" + id, {
      headers: this.headers
    });
  }
  deleteWithMultiParams(uri: string, params) {
    var paramStr: string = '';
    for (let param in params) {
      paramStr += param + "=" + params[param] + '&';
    }
    return this._http.delete(environment.API_URL + uri + "/?" + paramStr, {
      headers: this.headers
    });

  }
  postFile(uri: string, data?: any) {
    this.headers.delete('Content-Type');
    return this._http.post(environment.API_URL + uri, data, {
      headers: this.headers
    });
  }
  private extractData(res: Response) {
    let body = res.json();
    return body || {};
  }
  public handleError(error: any) {

    if (error.status == 401) {
      localStorage.removeItem(SystemConstants.CURRENT_USER);
      this._notificationService.printErrorMessage(MessageContstants.LOGIN_AGAIN_MSG);
      this._utilityService.navigateToLogin();
    }
    else if (error.status == 403) {
      localStorage.removeItem(SystemConstants.CURRENT_USER);
      this._notificationService.printErrorMessage(MessageContstants.FORBIDDEN);
      this._utilityService.navigateToLogin();
    }
    else {
      let errMsg = JSON.parse(error._body).Message;
      this._notificationService.printErrorMessage(errMsg);
      return Observable.throw(errMsg);
    }
  }
}
