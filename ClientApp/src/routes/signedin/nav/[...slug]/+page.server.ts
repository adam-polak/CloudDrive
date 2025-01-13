import { error } from '@sveltejs/kit';
import type { PageServerLoad } from './$types';
import { redirect } from '@sveltejs/kit';
import type { FolderModel, User } from '$lib/types';

export const load: PageServerLoad = async ({ params, cookies, fetch }) => {

	const userJson = cookies.get("user");
	if (userJson == null) return redirect(302, "/user/login");

	const user = JSON.parse(userJson) as User;
	
	const response = await fetch("/folderapi/getFolderByPath?loginkey=" + user.LoginKey + "&path=" + params.slug);

	const folderData = await response.json() as FolderModel;

	return {
		stuff: folderData.Id.toString()
	};
};