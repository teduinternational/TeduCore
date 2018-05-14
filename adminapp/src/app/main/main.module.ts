import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MainComponent } from './main.component';
import { mainRoutes } from './main.routes';
import { RouterModule, Routes } from '@angular/router';
import { UserModule } from './user/user.module';
import { HomeModule } from './home/home.module';
import { UtilityService } from '@shared/services/utility.service';
import { AuthenService } from '@shared/services/authen.service';
import { SignalrService } from '@shared/services/signalr.service';
import { SidebarMenuComponent } from '@shared/layouts/sidebar-menu/sidebar-menu.component';
import { TopMenuComponent } from '@shared/layouts/top-menu/top-menu.component';

@NgModule({
  imports: [
    CommonModule,
    UserModule,
    HomeModule,
    RouterModule.forChild(mainRoutes)
  ],
  declarations: [MainComponent, SidebarMenuComponent, TopMenuComponent],
  providers: [UtilityService, AuthenService, SignalrService]
})
export class MainModule { }
