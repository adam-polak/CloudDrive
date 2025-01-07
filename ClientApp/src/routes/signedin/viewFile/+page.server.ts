import type { FileModel, User } from "$lib/types";
import { redirect } from "@sveltejs/kit";

export const load = async ({ cookies, fetch }: { cookies: any, fetch: any }) => {
    const userJson = cookies.get("user");
    if(userJson == null) {
        return redirect(302, "/user/login");
    }

    const user: User = JSON.parse(userJson);

    const fileJson = cookies.get("file");
    if(fileJson == null) {
        return redirect(302, "/signedin");
    }

    const file: FileModel = JSON.parse(fileJson);

    try {
        const result = await fetch(
            `/fileapi/getcontent?loginkey=${user.LoginKey}&folderid=${file.FolderId}&fileid=${file.Id}`
        );

        const body = await result.text();

        const displayFile = {
            Name: file.File_Name,
            Data: body
        }

        return {
            file: displayFile
        }
    } catch {
        return redirect(302, "/signedin");
    }
}