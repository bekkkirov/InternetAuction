import {Component, OnInit} from '@angular/core';
import {AbstractControl, FormControl, FormGroup, Validators} from "@angular/forms";

@Component({
    selector: 'app-sign-up',
    templateUrl: './sign-up.component.html',
    styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {
    form: FormGroup = new FormGroup({
        "userName": new FormControl("", this.validateLength(5, 20)),
        "email": new FormControl("", [Validators.required, Validators.email]),
        "firstName": new FormControl("", this.validateLength(2, 30)),
        "lastName": new FormControl("", this.validateLength(2, 30)),
        "passwords": new FormGroup({
            "password": new FormControl("", this.validateLength(5, 20)),
            "confirmPassword": new FormControl("", [Validators.required, this.confirmPassword])
        }, {validators: this.confirmPassword})

    });

    confirmPassword(control: AbstractControl): { passwordsDontMatch: boolean } | null {
        if (!(control.get('confirmPassword')?.value === control.get('password')?.value)) {
            return {passwordsDontMatch: true};
        }

        return null;
    }

    validateLength(minLength: number, maxLength: number) {
        return (control: AbstractControl): { invalidLength: boolean } | null =>
        {
            if (control?.value.trim().length < minLength || control?.value.trim().length > maxLength) {
                return {invalidLength: true};
            }
            return null;
        }
    }

    constructor() {
    }

    ngOnInit(): void {
    }

    register() {

    }
}
