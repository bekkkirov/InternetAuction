import {AbstractControl} from "@angular/forms";

export class AppValidators {
    static validateLength(minLength: number, maxLength: number) {
        return (control: AbstractControl): { invalidLength: boolean } | null => {
            if (control?.value.trim().length < minLength || control?.value.trim().length > maxLength) {
                return {invalidLength: true};
            }
            return null;
        }
    }
}
