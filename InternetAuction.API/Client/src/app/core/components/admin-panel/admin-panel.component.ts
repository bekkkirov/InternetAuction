import {Component, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {AppValidators} from "../../validators/app-validators";
import {AdminService} from "../../services/admin.service";
import {ToastrService} from "ngx-toastr";

@Component({
    selector: 'app-admin-panel',
    templateUrl: './admin-panel.component.html',
    styleUrls: ['./admin-panel.component.css']
})
export class AdminPanelComponent implements OnInit {
    categoryForm: FormGroup = new FormGroup({
        "name": new FormControl(null, [Validators.required, AppValidators.validateLength(3, 30)])
    });

    rolesForm: FormGroup = new FormGroup({
        "userName": new FormControl(null, [Validators.required]),
        "roleName": new FormControl(null, [Validators.required])
    });

    constructor(private adminService: AdminService, private toastr: ToastrService) {
    }

    ngOnInit(): void {
    }

    createCategory() {
        this.adminService.createCategory(this.categoryForm.value).subscribe(() =>
            this.toastr.success("Category has been successfully created"));
    }

    assignRole() {
        let userName = this.rolesForm.get('userName')?.value;
        let roleName = this.rolesForm.get('roleName')?.value;

        this.adminService.assignRole(userName, roleName).subscribe(() =>
            this.toastr.success("Role has been successfully assigned"));
    }
}
