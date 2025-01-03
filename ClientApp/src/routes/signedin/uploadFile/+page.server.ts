import { User, FolderModel } from "$lib/types";

export const load = async ({ cookies, fetch }: { cookies: any, fetch: any }) => {
}

export const actions: Actions = {
  default: async ({ cookies, request }) => {
    const userJson = cookies.get("user");
    if(userJson == null) {
      return redirect(302, "/user/login");
    }

    const user: User = JSON.parse(userJson);

    const folderJson = cookies.get("currentFolder");
    if(folderJson == null) {
      return redirect(302, "/signedin");
    }

    const folder: FolderModel = JSON.parse(folderJson);

    const formData = await request.formData();

    try {

      const file = formData.get("file");

      if(file == null) {
        return {
          message: "Must choose a file"
        }
      }

      const result = await fetch(
        `/fileapi/uploadfile?loginkey=${user.LoginKey}&folderid=${folder.Id}`,
        {
          method: "POST",
          body: "<insert body>",
          headers: {
            "Content-Type": "application/json"
          }
        }
      );

      if(result.status != 200) {
        return {
          message: "Try again"
        }
      }



    } catch {
      return {
        message: "Server error"
      }
    }
      

  }
}
