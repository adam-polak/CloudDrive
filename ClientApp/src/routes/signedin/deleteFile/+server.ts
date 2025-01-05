import type { FolderModel, User } from "$lib/types";
import { json, redirect } from "@sveltejs/kit";

export async function POST({ request, cookies, fetch }: { request: any, cookies: any, fetch: any }) {
    const userJson = cookies.get("user");
    if(userJson == null) {
        return redirect(302, "/user/login");
    }

    const user: User = JSON.parse(userJson);
    
    const folderJson = cookies.get("currentFolder");
    if(folderJson == null) {
        return redirect(302, "/signedin")
    }

    const folder: FolderModel = JSON.parse(folderJson);

    try {
        const fileId = parseInt(await request.text());

        fetch(
            `/fileapi/deletefile?loginkey=${user.LoginKey}&folderid=${folder.Id}&fileid=${fileId}`, {
                method: "POST"
            }
        );
    } catch {
        return Response.error()
    }

    return json("done");
}