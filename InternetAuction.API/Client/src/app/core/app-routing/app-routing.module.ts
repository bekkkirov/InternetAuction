import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {LotsListComponent} from "../components/lots-list/lots-list.component";
import {UserDetailComponent} from "../components/user-detail/user-detail.component";
import {SignInComponent} from "../components/sign-in/sign-in.component";
import {SignUpComponent} from "../components/sign-up/sign-up.component";
import {UserEditComponent} from "../components/user-edit/user-edit.component";
import {AuthGuard} from "../guards/auth.guard";
import {LoggedInGuard} from "../guards/logged-in.guard";

const routes: Routes = [
    {path: '', component: LotsListComponent},
    {
        path: '',
        runGuardsAndResolvers: "always",
        canActivate: [AuthGuard],
        children: [
            {path: 'users/profile/:userName', component: UserDetailComponent},
            {path: 'users/edit', component: UserEditComponent}
        ]
    },
    {path: 'auth/sign-in', component: SignInComponent, canActivate: [LoggedInGuard]},
    {path: 'auth/sign-up', component: SignUpComponent, canActivate: [LoggedInGuard]},
    {path: 'categories/:categoryId', component: LotsListComponent, canActivate: [AuthGuard]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
