import { redirect, type Actions } from "@sveltejs/kit"

export const load = async ({ cookies } : { cookies: any }) => {
    const userJson = cookies.get("user");
    if(userJson == null) {
        return redirect(302, "/user/login")
    }

    const user = JSON.parse(userJson);

    const folderJson = cookies.get("currentFolder");
    if(folderJson == null) {
        return redirect(302, "/signedin")
    }

    const folder = JSON.parse(folderJson);

    return {
        user: user,
        folder: folder
    }
}

export const actions: Actions = {
    default: async ({ cookies, request, fetch }) => {
        const userJson = cookies.get("user");
        if(userJson == null) {
            return redirect(302, "/user/login")
        }

        const user = JSON.parse(userJson);

        const folderJson = cookies.get("currentFolder");
        if(folderJson == null) {
            return {
                message: "Couldn't create folder leave page and try again"
            }
        }

        const folder = JSON.parse(folderJson);

        const formData = await request.formData();

        const newFolderName = formData.get("foldername");
        if(newFolderName == null) {
            return {
                message: "Please enter folder name"
            }
        }

        try {
            // First check if contains folder already

            // Create folder
            const response = await fetch(
                `/folderapi/createfolder?loginkey=${user.LoginKey}&folderid=${folder.Id}&foldername=${newFolderName}`,
                {
                    method: "POST"
                }
            );

            if(response.status != 200) {
                return {
                    message: "Try again"
                }
            }

        } catch {
            return {
                message: "Try again"
            }
        }

        return redirect(302, "/signedin");
    }
}