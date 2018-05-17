import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { FunctionComponent } from './function/function.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            { path: 'home', component: HomeComponent, data: { permission: 'Pages.Tenant.Dashboard' } },
            { path: 'function', component: FunctionComponent, data: { permission: 'Pages.Tenant.Dashboard' } },
        ])
    ],
    exports: [
        RouterModule
    ]
})
export class MainRoutingModule { }