// @ts-ignore

import { redirect } from "@sveltejs/kit";
import type { User } from "$lib/user";

export const load = async ({ cookies, fetch }) => {

    const userJson: string | undefined = cookies.get("user");

    if(!userJson) return redirect(302, "/user/login");

    const user: User = JSON.parse(userJson);

    try {
        const result = await fetch(
            `/userapi/checkloginkey?loginkey=${user.LoginKey}`
        );

        if(result.status != 200) {
            cookies.delete("user", {
                path: "/"
            });
            return redirect(302, "/user/login");
        }
    } catch(e) {
        cookies.delete("user", {
            path: "/"
        });
        return redirect(302, "/user/login");
    }

    return {
        user: user
    }
}