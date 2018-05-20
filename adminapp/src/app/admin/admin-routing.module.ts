import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FunctionComponent } from './function/function.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            { path: 'function', component: FunctionComponent, data: { permission: 'Pages.App.Admin.Function' } },
        ])
    ],
    exports: [
        RouterModule
    ]
})
export class AdminRoutingModule { }