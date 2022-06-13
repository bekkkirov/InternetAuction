import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {LotsListComponent} from "../components/lots-list/lots-list.component";
import {UserDetailComponent} from "../components/user-detail/user-detail.component";
import {SignInComponent} from "../components/sign-in/sign-in.component";
import {SignUpComponent} from "../components/sign-up/sign-up.component";
import {UserEditComponent} from "../components/nav-bar/user-edit/user-edit.component";

const routes: Routes = [
    {path: '', component: LotsListComponent},
    {path: 'auth/sign-in', component: SignInComponent},
    {path: 'auth/sign-up', component: SignUpComponent},
    {path: 'users/:userName', component: UserDetailComponent},
    {path: 'users/edit', component: UserEditComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
