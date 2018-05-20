import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { AuthGuard } from '@shared/guards/auth.guard';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: 'app',
                component: AppComponent,
                canActivate: [AuthGuard],
                // canActivateChild: [AuthGuard],
                children: [
                    {
                        path: '',
                        children: [
                            { path: '', redirectTo: 'main/home', pathMatch: 'full' }
                        ]
                    },
                    {
                        path: 'main',
                        loadChildren: './main/main.module#MainModule', //Lazy load main module
                        data: { preload: true }
                    },
                    {
                        path: 'admin',
                        loadChildren: './admin/admin.module#AdminModule', //Lazy load admin module
                        data: { preload: true }
                    }
                ]
            }
        ])
    ],
    exports: [RouterModule]
})
export class AppRoutingModule { }