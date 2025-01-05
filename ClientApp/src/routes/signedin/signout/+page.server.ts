import { redirect } from "@sveltejs/kit";

export const load = (event: any) => {
    event.cookies.delete("user", {
        path: "/"
    });
    return redirect(302, "/user/login")
}