import {AbstractControl} from "@angular/forms";
import * as moment from "moment";

export class AppValidators {
    static validateLength(minLength: number, maxLength: number) {
        return (control: AbstractControl): { invalidLength: boolean } | null => {
            if (control?.value?.trim().length < minLength || control?.value?.trim().length > maxLength) {
                return {invalidLength: true};
            }
            return null;
        }
    }

    static validateDate(control: AbstractControl): { invalidLength: boolean } | null {
        if (moment(control?.value).format("YYYY-MM-DD HH:mm:ss") <= moment().format("YYYY-MM-DD HH:mm:ss")) {
            return {invalidLength: true};
        }
        return null;
    }
}
