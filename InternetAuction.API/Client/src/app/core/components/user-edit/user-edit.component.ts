import {Component, OnInit} from '@angular/core';
import {AccountService} from "../../services/account.service";
import {LoggedInUser} from "../../models/logged-in-user.model";
import {take} from "rxjs";
import {UserService} from "../../services/user.service";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {AppValidators} from "../../validators/app-validators";
import {ToastrService} from "ngx-toastr";

@Component({
    selector: 'app-user-edit',
    templateUrl: './user-edit.component.html',
    styleUrls: ['./user-edit.component.css']
})
export class UserEditComponent implements OnInit {
    user: LoggedInUser;
    form = new FormGroup({
        "profileImage": new FormControl(null),
        "firstName": new FormControl("", [AppValidators.validateLength(2, 30)]),
        "lastName": new FormControl("", [AppValidators.validateLength(2, 30)]),
        "balance": new FormControl("", [Validators.min(1)]),
    })

    constructor(private accountService: AccountService, private userService: UserService,
                private toastr: ToastrService) {
    }

    ngOnInit(): void {
        this.accountService.currentUser$.pipe(take(1)).subscribe(currentUser => this.user = currentUser);

        this.userService.getByUserName(this.user.userName).pipe(take(1)).subscribe(user => {
            this.form.patchValue({
                "firstName": user.firstName,
                "lastName": user.lastName,
                "balance": user.balance
            })
        });
    }

    update() {
        this.userService.updateProfile(this.form.value).subscribe({
            complete: () => this.toastr.success("Profile has been updated!")
        });
    }

    onFileInput(event) {
        let image = event.target.files[0];

        if(image) {
            this.userService.addProfileImage(image).subscribe(response => {
                this.user.profileImage = response.url;
                this.accountService.setCurrentUser(this.user);
                this.toastr.success("Profile image has been updated!")
            });
        }
    }
}
