import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminRoutingModule } from './admin-routing.module';
import { FunctionComponent } from './function/function.component'
import { FormsModule } from '@angular/forms';
import { ModalModule } from 'ngx-bootstrap';
import { TreeModule } from 'angular-tree-component';
import { UserModule } from './user/user.module';

@NgModule({
  imports: [
    CommonModule,
    AdminRoutingModule,
    FormsModule,
    ModalModule,
    TreeModule,
    UserModule
  ],
  declarations: [FunctionComponent],
  providers: []
})
export class AdminModule { }
