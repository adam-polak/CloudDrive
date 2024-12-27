import { redirect } from "@sveltejs/kit";

export const load = (event) => {
    event.cookies.delete("user", {
        path: "/"
    });
    return redirect(302, "/user/login")
}