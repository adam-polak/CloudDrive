import type { User } from "$lib/user";
import { ValidPassword, validPassword, ValidUser, validUsername } from "$lib/validateText";
import { redirect, type Actions } from "@sveltejs/kit"

// @ts-ignore
export const load = async (event) => {
    if(event.cookies.get("user")) {
        redirect(302, "/signedin")
    }
    
    return {};
}

export const actions: Actions = {
    default: async (event) => {
        const formData = await event.request.formData();

        const username = formData.get("username")?.toString() ?? "";
        let usernameError = null;
        let usernameIsValid = false;
        switch(validUsername(username)) {
            case ValidUser.Length:
                usernameError = "Username must be atleast 3 characters";
                break;
            case ValidUser.OnlyLetters:
                usernameError = "Username should only contain letters";
                break;
            case ValidUser.Space:
                usernameError = "Username should not have any spaces";
                break;
            default:
                usernameIsValid = true;
                break;
        }

        const password = formData.get("password")?.toString() ?? "";
        let passwordError = null;
        let passwordIsValid = false;
        switch(validPassword(password)) {
            case ValidPassword.Length:
                passwordError = "Password must be atleast 6 characters";
                break;
            case ValidPassword.Space:
                passwordError = "Password should not have any spaces";
                break;
            default:
                passwordIsValid = true;
                break;
        }
        
        if(!usernameIsValid && !passwordIsValid) {
            return {
                usernameError: usernameError,
                passwordError: passwordError
            }
        } else if(!passwordIsValid) {
            return {
                passwordError: passwordError
            }
        } else if(!usernameIsValid) {
            return {
                usernameError: usernameError
            }
        }

        // Create user through backend
        try {
            const result = await event.fetch(
                `/userapi/createuser/${username}/${password}`,
                {
                    method: "POST"
                }
            );

            if(result.status != 200) {
                return {
                    message: "Username already exists"
                }
            }

        } catch(e) {
            console.log(e);
            return {
                message: "Server error"
            }
        }

        return redirect(302, "/user/login");
        
    }
}