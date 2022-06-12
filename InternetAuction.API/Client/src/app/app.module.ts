import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppComponent} from './app.component';
import {SignInComponent} from "./core/components/sign-in/sign-in.component";
import {ReactiveFormsModule} from "@angular/forms";
import { SignUpComponent } from './core/components/sign-up/sign-up.component';

@NgModule({
    declarations: [
        AppComponent,
        SignInComponent,
        SignUpComponent
    ],
    imports: [
        BrowserModule,
        ReactiveFormsModule
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule {
}
