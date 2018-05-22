import { Component, OnInit, ViewChild } from '@angular/core';
import { DataService } from '@shared/services/data.service';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { NotificationService } from '@shared/services/notification.service';
import { MessageContstants } from '@shared/common/message.constants';
@Component({
  selector: 'app-role',
  templateUrl: './role.component.html',
  styleUrls: ['./role.component.css']
})
export class RoleComponent implements OnInit {
  public pageIndex: number = 1;
  public pageSize: number = 20;
  public pageDisplay: number = 10;
  public totalRow: number;
  public filter: string = '';
  public roles: any[];


  constructor(private _dataService: DataService,
    private _notificationService: NotificationService) { }

  ngOnInit() {
    this.loadData();
  }

  loadData() {
    this._dataService.get('/api/Role/getAllPaging?page=' + this.pageIndex
      + '&pageSize=' + this.pageSize + '&filter=' + this.filter)
      .subscribe((response: any) => {
        this.roles = response.Results;
        this.pageIndex = response.CurrentPage;
        this.pageSize = response.PageSize;
        this.totalRow = response.RowCount;
      });
  }
  
  pageChanged(event: any): void {
    this.pageIndex = event.page;
    this.loadData();
  }
  showAddModal() {
  }
  showEditModal(id: any) {
  }
  
  deleteItem(id: any) {
    this._notificationService.printConfirmationDialog(MessageContstants.CONFIRM_DELETE_MSG, () =>
      this.deleteItemConfirm(id)
    );
  }
  deleteItemConfirm(id: any) {
    this._dataService.delete('/api/Role/delete', 'id', id).subscribe((response: Response) => {
      this._notificationService.printSuccessMessage(MessageContstants.DELETED_OK_MSG);
      this.loadData();
    });
  }
}
