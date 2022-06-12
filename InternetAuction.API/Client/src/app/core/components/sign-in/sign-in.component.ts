import {Component, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";

@Component({
    selector: 'app-sign-in',
    templateUrl: './sign-in.component.html',
    styleUrls: ['./sign-in.component.css']
})
export class SignInComponent implements OnInit {
    form: FormGroup = new FormGroup({
        "userName": new FormControl("", [Validators.required]),
        "password": new FormControl("", [Validators.required]),
    })

    constructor() {
    }

    ngOnInit(): void {
    }

    signIn() {

    }
}
