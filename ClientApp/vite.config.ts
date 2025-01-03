import { sveltekit } from '@sveltejs/kit/vite';
import { defineConfig } from 'vite';

export default defineConfig({
	plugins: [sveltekit()],
	server: {
		proxy: {
			'/userapi': 'http://localhost:5141',
			'/folderapi': 'http://localhost:5141',
      '/fileapi': 'http://localhost:5141'
		}
	}
});
