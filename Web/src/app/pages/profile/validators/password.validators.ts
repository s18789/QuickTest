import {AbstractControl, ValidationErrors, ValidatorFn} from '@angular/forms';

export function atLeastOneUpperCaseLetterValidator(): ValidatorFn {
    return (control: AbstractControl) : ValidationErrors | null => {

        const value = control.value;

        const hasUpperCase = /[A-Z]+/.test(value);

        return !hasUpperCase || !value ? {upperLetter:true}: null;
    }
}

export function atLeastOnelowerCaseLetterValidator(): ValidatorFn {
    return (control: AbstractControl) : ValidationErrors | null => {

        const value = control.value;

        const hasLowerCase = /[a-z]+/.test(value);

        return !hasLowerCase || !value ? {lowerLetter:true}: null;
    }
}

export function atLeastOneNumberValidator(): ValidatorFn {
    return (control: AbstractControl) : ValidationErrors | null => {

        const value = control.value;

        const hasDigit = /[0-9]+/.test(value);

        return !hasDigit || !value ? {digits:true}: null;
    }
}

export function atLeastOneSpecialCharacterValidator(): ValidatorFn {
    return (control: AbstractControl) : ValidationErrors | null => {

        const value = control.value;

        const hasSpecialCharacter = /[ `!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?~]/.test(value);

        return !hasSpecialCharacter || !value ? {specialChar:true}: null;
    }
}

