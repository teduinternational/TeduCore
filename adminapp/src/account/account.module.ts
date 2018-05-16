import * as ngCommon from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule, JsonpModule } from '@angular/http';
import { ModalModule } from 'ngx-bootstrap/modal';
import { AccountRoutingModule } from './account-routing.module';
import { AccountComponent } from './account.component';
import { LoginComponent } from './login/login.component';
import { NotificationService } from '@shared/services/notification.service';
import { AuthenService } from '@shared/services/authen.service';
@NgModule({
    imports: [
        ngCommon.CommonModule,
        FormsModule,
        HttpModule,
        JsonpModule,
        ModalModule.forRoot(),
        AccountRoutingModule
    ],
    declarations: [
        AccountComponent,
        LoginComponent
    ],
    providers: [AuthenService, NotificationService]
})
export class AccountModule {

}