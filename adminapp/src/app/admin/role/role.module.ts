import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RoleComponent } from './role.component';
import { Routes, RouterModule } from '@angular/router';
import { DataService } from '@shared/services/data.service';
import { NotificationService } from '@shared/services/notification.service';
import { PaginationModule  } from 'ngx-bootstrap/pagination';
import {FormsModule} from '@angular/forms';
import { ModalModule } from 'ngx-bootstrap/modal';
import { RoleAddEditComponent } from './role-add-edit/role-add-edit.component';

const roleRoutes: Routes = [
  { path: '', redirectTo: 'index', pathMatch: 'full' },
  { path: 'index', component: RoleComponent },
  { path: 'add', component: RoleAddEditComponent },
  { path: 'edit/:id', component: RoleAddEditComponent },
]

@NgModule({
  imports: [
    CommonModule,
    PaginationModule,
    FormsModule,
    ModalModule.forRoot(),
    RouterModule.forChild(roleRoutes)
  ],
  declarations: [RoleComponent, RoleAddEditComponent],
  providers:[DataService,NotificationService]
})
export class RoleModule { }
