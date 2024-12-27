// @ts-ignore

import { redirect } from "@sveltejs/kit";
import type { User } from "$lib/user";

export const load = async (event) => {

    const userJson: string | undefined = event.cookies.get("user");

    if(!userJson) return redirect(302, "/user/login");

    const user: User = JSON.parse(userJson);

    try {
        const result = await event.fetch(
            `/userapi/loginkey?loginkey=${user.LoginKey}`
        );

        if(result.status != 200) {
            event.cookies.delete("user", {
                path: "/"
            });
            return redirect(302, "/user/login");
        }
    } catch(e) {
        event.cookies.delete("user", {
            path: "/"
        });
        return redirect(302, "/user/login");
    }

    return {
        user: user
    }
}