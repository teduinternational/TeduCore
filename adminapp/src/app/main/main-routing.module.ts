import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            { path: 'home', component: HomeComponent, data: { permission: 'Pages.Tenant.Dashboard' } },
        ])
    ],
    exports: [
        RouterModule
    ]
})
export class MainRoutingModule { }