import type { User } from "$lib/types";
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
    default: async ({ cookies, request, fetch }) => {
        const formData = await request.formData();

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

        // Verify login with backend
        try {
            const result = await fetch(
                `/userapi/login/${username}/${password}`,
                {
                    method: "POST"
                }
            );

            if(result.status != 200) {
                return {
                    message: "Invalid username or password"
                }
            }

            const body = JSON.parse(await result.text());

            const loginKey = body.LoginKey;

            const rootFolderId = body.RootFolderId;

            const user: User = {
                // need to encode loginkey otherwise if there is a `+` it will be received as a ` ` on backend
                LoginKey: encodeURIComponent(loginKey),
                Username: username,
                RootFolderId: rootFolderId
            }

            if(cookies.get("user") != null) {
                cookies.delete("user", {
                    path: "/"
                });
            }

            cookies.set("user", JSON.stringify(user), {
                path: "/"
            });

            return redirect(302, "/signedin");

        } catch {
            return {
                message: "Server error"
            }
        }
        
    }
}