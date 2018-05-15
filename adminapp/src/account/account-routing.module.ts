import { NgModule } from '@angular/core';
import { RouterModule, Router, NavigationEnd } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { AccountComponent } from './account.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                component: AccountComponent,
                children: [
                    { path: 'login', component: LoginComponent },
                ]
            }
        ])
    ],
    exports: [
        RouterModule
    ]
})
export class AccountRoutingModule {
    constructor(private router: Router) {
        router.events.subscribe((event: NavigationEnd) => {
            setTimeout(() => {
                this.toggleBodyCssClass(event.url);
            }, 0);
        });
    }

    toggleBodyCssClass(url: string): void {
    }
}