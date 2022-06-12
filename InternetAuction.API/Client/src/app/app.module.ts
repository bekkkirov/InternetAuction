import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppComponent} from './app.component';
import {SignInComponent} from "./core/components/sign-in/sign-in.component";
import {ReactiveFormsModule} from "@angular/forms";
import { SignUpComponent } from './core/components/sign-up/sign-up.component';
import {HttpClientModule} from "@angular/common/http";

@NgModule({
    declarations: [
        AppComponent,
        SignInComponent,
        SignUpComponent,
    ],
    imports: [
        BrowserModule,
        ReactiveFormsModule,
        HttpClientModule,
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule {
}
