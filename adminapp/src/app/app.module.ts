import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule, ChildrenOutletContexts } from '@angular/router';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';

import { SidebarMenuComponent } from '@shared/layouts/sidebar-menu/sidebar-menu.component';
import { TopMenuComponent } from '@shared/layouts/top-menu/top-menu.component';
import { AuthGuard } from '@shared/guards/auth.guard';
import { PaginationModule } from 'ngx-bootstrap/pagination';

import { AuthenService } from '@shared/services/authen.service';
import { NotificationService } from '@shared/services/notification.service';
import { DataService } from '@shared/services/data.service';
import { UtilityService } from '@shared/services/utility.service';

@NgModule({
  declarations: [
    AppComponent,
    SidebarMenuComponent, 
    TopMenuComponent,
    
  ],
  imports: [
    BrowserModule,
    RouterModule,
    FormsModule,
    HttpModule,
    AppRoutingModule,
    PaginationModule.forRoot()
  ],
  providers: [AuthGuard,
    DataService,
    AuthenService,
    NotificationService,
    UtilityService
  ],
})
export class AppModule { }
