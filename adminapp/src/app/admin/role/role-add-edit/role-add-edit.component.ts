import { Component, OnInit } from '@angular/core';
import { DataService } from '@shared/services/data.service';
import { NotificationService } from '@shared/services/notification.service';
import { MessageContstants } from '@shared/common/message.constants';

@Component({
  selector: 'app-role-add-edit',
  templateUrl: './role-add-edit.component.html',
  styleUrls: ['./role-add-edit.component.css']
})
export class RoleAddEditComponent implements OnInit {
  public entity: any;
  
  constructor(private _dataService : DataService,
    private _notificationService: NotificationService) { }

  ngOnInit() {
  }
  loadRole(id: any) {
    this._dataService.get('/api/Role/detail/' + id)
      .subscribe((response: any) => {
        this.entity = response;
        console.log(this.entity);
      });
  }

  saveChange(valid: boolean) {
    if (valid) {
      if (this.entity.Id == undefined) {
        this._dataService.post('/api/Role/add', JSON.stringify(this.entity))
          .subscribe((response: any) => {
            this._notificationService.printSuccessMessage(MessageContstants.CREATED_OK_MSG);
          }, error => this._dataService.handleError(error));
      }
      else {
        this._dataService.put('/api/Role/update', JSON.stringify(this.entity))
          .subscribe((response: any) => {
            this._notificationService.printSuccessMessage(MessageContstants.UPDATED_OK_MSG);
          }, error => this._dataService.handleError(error));
      }
    }
  }
}
