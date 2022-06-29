import {Component, OnInit} from '@angular/core';
import {AbstractControl, FormControl, FormGroup, Validators} from "@angular/forms";
import {AccountService} from "../../services/account.service";
import {Router} from "@angular/router";
import {AppValidators} from "../../validators/app-validators";
import {take} from "rxjs";

@Component({
    selector: 'app-sign-up',
    templateUrl: './sign-up.component.html',
    styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent {
    form: FormGroup = new FormGroup({
        "userName": new FormControl("", AppValidators.validateLength(5, 20)),
        "email": new FormControl("", [Validators.required, Validators.email]),
        "firstName": new FormControl("", AppValidators.validateLength(2, 30)),
        "lastName": new FormControl("", AppValidators.validateLength(2, 30)),
        "passwords": new FormGroup({
            "password": new FormControl("", AppValidators.validateLength(5, 20)),
            "confirmPassword": new FormControl("", [Validators.required, this.confirmPassword])
        }, {validators: this.confirmPassword})

    });

    confirmPassword(control: AbstractControl): { passwordsDontMatch: boolean } | null {
        if (!(control.get('confirmPassword')?.value === control.get('password')?.value)) {
            return {passwordsDontMatch: true};
        }

        return null;
    }

    constructor(private accountService: AccountService, private router: Router) {
    }

    register() {
        this.accountService.signUp({
            userName: this.form.get('userName')?.value.trim(),
            firstName: this.form.get('firstName')?.value.trim(),
            lastName: this.form.get('lastName')?.value.trim(),
            email: this.form.get('email')?.value.trim(),
            password: this.form.get('passwords.password')?.value.trim(),
        }).pipe(take(1)).subscribe(
            {
                complete: () => this.router.navigateByUrl("users/edit")
            });
    }
}
