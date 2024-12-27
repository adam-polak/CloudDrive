export function hasSpace(value: string) {
    return value.includes(' ');
}

export function allLetters(value: string) {
    for(let i = 0; i < value.length; i++) {
        const c = value.charCodeAt(i);

        // check if not in range of 'a' - 'z' or 'A' - 'Z'
        if(!((c >= 97 && c <= 122) || (c >= 65 && c <= 90))) {
            return false;
        }
    }

    return true;
}

export enum ValidUser {
    Space,
    OnlyLetters,
    Length,
    OK
}

export function validUsername(value: string) {
    if(hasSpace(value)) return ValidUser.Space;
    
    if(!allLetters(value)) return ValidUser.OnlyLetters;

    if(value.length < 3) return ValidUser.Length;

    return ValidUser.OK;
}

export enum ValidPassword {
    Space,
    Length,
    OK
}

export function validPassword(value: string) {
    if(hasSpace(value)) return ValidPassword.Space;
    
    if(value.length < 8) return ValidPassword.Length;

    return ValidPassword.OK;
}