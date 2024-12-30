import type { FolderModel, GetContentsResponse } from "$lib/types";
import { redirect, type Actions } from "@sveltejs/kit";


export const load = async ({ cookies, fetch }) => {

    const userJson = cookies.get("user");
    if(userJson == null) {
        return redirect(302, "/user/login")
    }

    const user = JSON.parse(userJson);

    const folderJson = cookies.get("currentFolder");

    let folder: FolderModel;
    if(folderJson == null) {
        folder = {
            Id: 0,
            OwnerId: 0,
            Folder_Name: "root",
            ParentId: 0
        }
    } else {
        folder = JSON.parse(folderJson);
    }

    try {
        const result = await fetch(
            `/folderapi/getcontents?loginkey=${user.LoginKey}&folderid=${folder.Id}`
        );

        const body = await result.text();

        const contents: GetContentsResponse = JSON.parse(body);

        return {
            currentFolder: folder,
            contents: contents
        }
    } catch {
        return {
            message: "Couldn't retrieve contents..."
        }
    }
}

export const actions: Actions = {
    switchfolder: async ({ cookies, request }) => {
        const formData = await request.formData();

        const switchFolderJson = formData.get("switchFolderJson");

        if(switchFolderJson != null) {
            cookies.set("currentFolder", switchFolderJson.toString(), {
                path: "/"
            });
        }

        return {}
    },
    backFolder: async ({ cookies, request, fetch }) => {

        const userJson = cookies.get("user");
        if(userJson == null) {
            return redirect(302, "/user/login")
        }
    
        const user = JSON.parse(userJson);

        const formData = await request.formData();

        const parentIdFormData = formData.get("parentId");

        try {
            const parentId = parseInt(parentIdFormData?.toString() ?? "");

            if(parentId == 0) {
                const folder: FolderModel = {
                    Id: 0,
                    OwnerId: 0,
                    Folder_Name: "root",
                    ParentId: 0
                }
                cookies.set("currentFolder", JSON.stringify(folder), {
                    path: "/"
                });

                return {}
            }

            const result = await fetch(
                `/folderapi/getfolder/?loginkey=${user.LoginKey}&folderid=${parentId}`
            );

            const body = await result.text();

            cookies.set("currentFolder", body, {
                path: "/"
            });

            return {}
        } catch {
            return {
                message: "Operation failed try again"
            }
        }
    }
}