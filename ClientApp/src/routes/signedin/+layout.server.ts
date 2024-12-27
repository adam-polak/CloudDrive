// @ts-ignore

import { redirect } from "@sveltejs/kit";
import type { User } from "$lib/user";
import type { Cookies } from "@sveltejs/kit";

export const load = async (event) => {

    console.log(event.cookies.get("loginkey"));
    console.log(event.cookies.get("username"));


    // fetch(`http://localhost:5141/checkloginkey?loginkey=${loginKey}`)
    // .then(result => {
    //     if(result.status == 401) {
    //         return redirect(302, "/user/login");
    //     }

    //     const user: User = { Username: "temp", LoginKey: loginKey };

    //     return {
    //         user: user
    //     }
    // }).catch(() => {
    //     return redirect(302, "/user/login");
    // });
}