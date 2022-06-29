import {Component} from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {AccountService} from "../../services/account.service";
import {Router} from "@angular/router";
import {take} from "rxjs";

@Component({
    selector: 'app-sign-in',
    templateUrl: './sign-in.component.html',
    styleUrls: ['./sign-in.component.css']
})
export class SignInComponent {
    form: FormGroup = new FormGroup({
        "userName": new FormControl("", [Validators.required]),
        "password": new FormControl("", [Validators.required]),
    })

    constructor(private accountService: AccountService, private router: Router) {
    }

    signIn() {
        this.accountService.signIn(this.form.value)
                           .pipe(take(1))
                           .subscribe({
                                complete: () => this.router.navigateByUrl("/")
        });
    }
}
