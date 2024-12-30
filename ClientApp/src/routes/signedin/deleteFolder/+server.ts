import { json, redirect } from "@sveltejs/kit";

export async function POST({ request, cookies, fetch }) {

    const userJson = cookies.get("user");
    if(userJson == null) {
        return redirect(302, "/user/login")
    }

    const user = JSON.parse(userJson);

    try {
        const folderId = parseInt(await request.text());

        fetch(
            `/folderapi/deletefolder?loginkey=${user.LoginKey}&folderid=${folderId}`, {
                method: "POST"
            }
        );
    } catch {
        return Response.error()
    }

    return json("done");
}