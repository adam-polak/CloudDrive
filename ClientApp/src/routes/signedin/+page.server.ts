import { addToPath, removeFromPath } from "$lib/folderpath";
import type { FolderModel, GetContentsResponse } from "$lib/types";
import { redirect, type Actions } from "@sveltejs/kit";

async function getFolderPath(fetch: any, loginKey: string, folderId: number) {
    try {
        const result = await fetch(
            `/folderapi/getpath?loginkey=${loginKey}&folderid=${folderId}`
        );

        return await result.text();
    } catch {
        return null;
    }
}


export const load = async ({ cookies, fetch }: { cookies: any, fetch: any }) => {

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
        cookies.set("currentFolder", JSON.stringify(folder), {
            path: "/"
        });
    } else {
        folder = JSON.parse(folderJson);
    }

    try {
        const result = await fetch(
            `/folderapi/getcontents?loginkey=${user.LoginKey}&folderid=${folder.Id}`
        );

        const body = await result.text();

        const contents: GetContentsResponse = JSON.parse(body);

        let folderPath;
        if(folder.Id == 0) {
            folderPath = "/";
            cookies.set("folderPath", folderPath, {
                path: "/"
            });
        } else {
            folderPath = cookies.get("folderPath");

            if(folderPath == null) {
                const path = await getFolderPath(fetch, user.LoginKey, folder.Id);
                if(path != null) {
                    cookies.set("folderPath", path, {
                        path: "/"
                    });

                    folderPath = path;
                }
            }

        }

        return {
            currentFolder: folder,
            contents: contents,
            folderPath: folderPath
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
            try {
                cookies.set("currentFolder", switchFolderJson.toString(), {
                    path: "/"
                });
        
                const folder: FolderModel = JSON.parse(switchFolderJson.toString())
        
                const folderPath = cookies.get("folderPath");
        
                if(folderPath != null) {
                    let newPath;
                    if(folder.ParentId != 0) {
                        newPath = addToPath(folderPath, folder.Folder_Name);
                    } else {
                        newPath = folderPath + folder.Folder_Name;
                    }
                    cookies.set("folderPath", newPath, {
                        path: "/"
                    });
                }

            } catch {
                return {}
            }
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

            const currentFolderPath = cookies.get("folderPath");
            if(currentFolderPath != null) {
                cookies.set("folderPath", removeFromPath(currentFolderPath), {
                    path: "/"
                });
            }

            return {}
        } catch {
            return {
                message: "Operation failed try again"
            }
        }
    },
    deleteFolder: async ({ request, fetch }) => {
        console.log("deleting");
        return {}
    }
}