import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MainComponent } from './main.component';
import { MainRoutingModule } from './main-routing.module';
import { RouterModule, Routes } from '@angular/router';
import { UtilityService } from '@shared/services/utility.service';
import { AuthenService } from '@shared/services/authen.service';
import { SignalrService } from '@shared/services/signalr.service';
import { TreeModule } from 'angular-tree-component';
import { ModalModule } from 'ngx-bootstrap';
import { HomeComponent } from './home/home.component';

@NgModule({
  imports: [
    CommonModule,
    TreeModule,
    ModalModule,
    FormsModule,
    MainRoutingModule
  ],
  declarations: [MainComponent, HomeComponent],
  providers: [UtilityService, AuthenService]
})
export class MainModule { }
