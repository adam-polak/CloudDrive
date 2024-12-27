import { redirect } from "@sveltejs/kit";
import { userStore } from "$lib/stores";
import type { User } from "$lib/user";

export const load = async () => {
    userStore.subscribe((user: User | null) => {

        if(user == null) {
            return redirect(302, "/user/login");
        }

        fetch(`http://localhost:5141/checkloginkey?loginkey=${user.LoginKey}`)
        .then(result => {
            if(result.status == 401) {
                return redirect(302, "/user/login");
            }

            return {
                user: user
            }
        }).catch(() => {
            return redirect(302, "/user/login");
        });

    });
}