import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule, Injector, APP_INITIALIZER, LOCALE_ID } from '@angular/core';
import { AppModule } from './app/app.module';
import { RootRoutingModule } from './root-routing.module';
import { RootComponent } from './root.component';

@NgModule({
    imports: [
        BrowserModule,
        BrowserAnimationsModule,
        AppModule,
        RootRoutingModule
    ],
    declarations: [
        RootComponent
    ],
    providers: [
    ],
    bootstrap: [RootComponent]
})
export class RootModule {

}