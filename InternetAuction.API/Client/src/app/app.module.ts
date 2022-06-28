import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppComponent} from './app.component';
import {SignInComponent} from "./core/components/sign-in/sign-in.component";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import { SignUpComponent } from './core/components/sign-up/sign-up.component';
import {HTTP_INTERCEPTORS, HttpClientModule} from "@angular/common/http";
import { NavBarComponent } from './core/components/nav-bar/nav-bar.component';
import { LotsListComponent } from './core/components/lots-list/lots-list.component';
import { UserDetailComponent } from './core/components/user-detail/user-detail.component';
import {AppRoutingModule} from "./core/app-routing/app-routing.module";
import { UserEditComponent } from './core/components/user-edit/user-edit.component';
import {ToastrModule} from "ngx-toastr";
import {ErrorInterceptor} from "./core/interceptors/error.interceptor";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {TokenInterceptor} from "./core/interceptors/token.interceptor";
import {NgxPaginationModule} from "ngx-pagination";
import { LotDetailComponent } from './core/components/lot-detail/lot-detail.component';
import { PageNotFoundComponent } from './core/components/page-not-found/page-not-found.component';
import { LotCreateComponent } from './core/components/lot-create/lot-create.component';
import { SideBarComponent } from './core/components/side-bar/side-bar.component';

@NgModule({
    declarations: [
        AppComponent,
        SignInComponent,
        SignUpComponent,
        NavBarComponent,
        LotsListComponent,
        UserDetailComponent,
        UserEditComponent,
        LotDetailComponent,
        PageNotFoundComponent,
        LotCreateComponent,
        SideBarComponent,
    ],
    imports: [
        BrowserModule,
        NgxPaginationModule,
        BrowserAnimationsModule,
        ReactiveFormsModule,
        HttpClientModule,
        AppRoutingModule,
        ToastrModule.forRoot({positionClass: 'toast-bottom-right'}),
        FormsModule
    ],
    providers: [
        {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true},
        {provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true},
    ],
    bootstrap: [AppComponent]
})
export class AppModule {
}
