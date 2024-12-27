import type { User } from "$lib/user";
import { ValidPassword, validPassword, ValidUser, validUsername, validUsername } from "$lib/validateText";
import { redirect, type Actions } from "@sveltejs/kit"

// @ts-ignore
export const load = async (event) => {
    if(event.locals.user) {
        redirect(302, "/signedin");
    }
    
    return {};
}

export const actions: Actions = {
    default: async (event) => {
        const formData = await event.request.formData();

        const username = formData.get("username")?.toString() ?? "";
        const usernameIsValid = validUsername(username) == ValidUser.OK;

        const password = formData.get("password")?.toString() ?? "";
        const passwordIsValid = validPassword(password) == ValidPassword.OK;
        
        if(!usernameIsValid && !passwordIsValid) {
            return {
                usernameError: "Invalid username",
                passwordError: "Invalid password"
            }
        } else if(!passwordIsValid) {
            return {
                passwordError: "Invalid password"
            }
        } else if(!usernameIsValid) {
            return {
                usernameError: "Invalid username"
            }
        }

        // make api request

        return {};

        // const user: User = { 
        //     Username: "test",
        //     LoginKey: "agoodkey"
        // }

        // event.cookies.set("loginkey", user.LoginKey, {
        //     path: "/"
        // });

        // event.cookies.set("username", user.Username, {
        //     path: "/"
        // });

        // return redirect(302, "/signedin");
    }
}