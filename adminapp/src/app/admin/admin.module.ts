import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { adminRoutes } from './admin.routes';
import { FunctionModule } from './function/function.module';
import { FormsModule } from '@angular/forms';
import { ModalModule } from 'ngx-bootstrap';
import { TreeModule } from 'angular-tree-component';
import { Router, RouterModule } from '@angular/router';
import { AdminComponent } from './admin.component';

import { UtilityService } from '@shared/services/utility.service';
import { AuthenService } from '@shared/services/authen.service';
import { SignalrService } from '@shared/services/signalr.service';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ModalModule,
    TreeModule,
    FunctionModule,
    RouterModule.forChild(adminRoutes)
  ],
  declarations: [AdminComponent],
  providers: [UtilityService, AuthenService, SignalrService]
})
export class AdminModule { }
