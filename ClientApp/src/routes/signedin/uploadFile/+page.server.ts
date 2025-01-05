import type { FolderModel, User } from "$lib/types";
import { redirect, type Actions } from "@sveltejs/kit";

export const actions: Actions = {
  default: async ({ cookies, request, fetch }) => {

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

      const fileName = formData.get("name");
      const data = formData.get("data");

      if(fileName == null) {
        return {
          message: "Must choose a file"
        }
      }

      if(data == null) {
        return {
          message: "Try again"
        }
      }

      const bodyObj = { Name: fileName, Data: data };

      const result = await fetch(
        `/fileapi/uploadfile?loginkey=${user.LoginKey}&folderid=${folder.Id}`,
        {
          method: "POST",
          body: JSON.stringify(bodyObj),
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

    return redirect(302, "/signedin");
  }
}
