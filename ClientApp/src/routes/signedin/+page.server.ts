import type { FolderModel, GetContentsResponse } from "$lib/types";


export const load = async ({ cookies, fetch }) => {

    const user = JSON.parse(cookies.get("user") ?? "");

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