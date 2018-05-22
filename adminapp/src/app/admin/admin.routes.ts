import { Routes } from '@angular/router';
import { AdminComponent } from './admin.component';

export const adminRoutes: Routes = [
    {
        path: '', component: AdminComponent, children: [
            { path: 'user', loadChildren: './user/user.module#UserModule' },

            { path: 'role', loadChildren: './role/role.module#RoleModule' },

            { path: 'function', loadChildren: './function/function.module#FunctionModule' },

            { path: 'announcement', loadChildren: './announcement/announcement.module#AnnouncementModule' },
        ]
    }

]