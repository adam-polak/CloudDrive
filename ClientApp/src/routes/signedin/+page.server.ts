import { addToPath, removeFromPath } from "$lib/folderpath";
import type { ContentModel, FileModel, FolderModel, GetContentsResponse, User } from "$lib/types";
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

    if(cookies.get("file") != null) {
        cookies.delete("file", {
            path: "/"
        });
    }

    const folderJson = cookies.get("currentFolder");

    let folder: FolderModel;
    if(folderJson == null) {
        try {
            const result = await fetch(
                `/folderapi/getfolder?loginkey=${user.LoginKey}&folderid=${user.RootFolderId}`
            );

            const body = await result.text();

            folder = JSON.parse(body);
        } catch {
            return {
                message: "Error retrieving folder"
            }
        }
    } else {
        folder = JSON.parse(folderJson);
    }

    cookies.set("currentFolder", JSON.stringify(folder), {
        path: "/"
    });

    try {
        const result = await fetch(
            `/folderapi/getcontents?loginkey=${user.LoginKey}&folderid=${folder.Id}`
        );

        const body = await result.text();

        const contentsResponse: GetContentsResponse = JSON.parse(body);

        let folderPath: string;
        if(folder.Id == user.RootFolderId) {
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

        // Break up the folderPath into an array for the client
        let folderPathsPretty = folderPath.split("/");
        if (folderPath === "/")
            folderPathsPretty = [ "Root" ];
        else
            folderPathsPretty[0] = "Root";

        let contents: ContentModel[] = [];

        contentsResponse.Files.map(x => contents.push({ File: x, Folder: undefined }));
        contentsResponse.Folders.map(x => contents.push({ File: undefined, Folder: x }));

        contents = contents.sort((a, b) => {
            const n1 = (a.File?.File_Name ?? a.Folder?.Folder_Name ?? "").toLowerCase();
            const n2 = (b.File?.File_Name ?? b.Folder?.Folder_Name ?? "").toLowerCase();

            if(n1 < n2) return 1;
            else if(n1 > n2) return -1;
            else return 0;
        });

        return {
            currentFolder: folder,
            contents: contents,
            folderPathsPretty: folderPathsPretty
        }
    } catch {
        return {
            message: "Couldn't retrieve contents..."
        }
    }
}

export const actions: Actions = {
    switchfolder: async ({ cookies, request }) => {
        const userJson = cookies.get("user");
        if(userJson == null) {
            return redirect(302, "/user/login");
        }

        const user: User = JSON.parse(userJson);

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
                    if(folder.ParentId != user.RootFolderId) {
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
    view: async ({ cookies, request }) => {
        const formData = await request.formData();

        try {
            const file: FileModel = JSON.parse(formData.get("fileJson")?.toString() ?? "");

            cookies.set("file", JSON.stringify(file), {
                path: "/"
            });
        } catch {
            return {
                message: "Couldn't view file"
            }
        }

        return redirect(302, "/signedin/viewFile");
    }
}