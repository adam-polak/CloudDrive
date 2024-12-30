<script lang="ts">

    let { data } = $props();

    let switchFolderJson = $state("");

</script>

<div class="bg-gray-100 min-h-screen flex flex-col">
    <!-- Header -->
    <header class="bg-white shadow-md p-4 flex gap-3 justify-between items-center">
        <div>
            <h1 class="font-bold text-2xl text-blue-600">CloudDrive</h1>
            <a href="/signedin/signout" class="hover:underline text-sm ml-2 text-black">Sign out</a>
        </div>
        <div class="flex gap-2">
            <input 
                type="text" 
                placeholder="Search folder..." 
                class="border rounded-md px-4 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-blue-400"
            />
            <button class="bg-blue-500 text-white px-4 py-2 rounded-md hover:bg-blue-600">Create Folder</button>
            <button class="bg-blue-500 text-white px-4 py-2 rounded-md hover:bg-blue-600">Upload File</button>
        </div>
    </header>

    <!-- Main Content -->
    <main class="flex-grow p-6">
        {#if data.currentFolder != null && data.currentFolder.Id != 0}
            <form method="POST" action="?/backFolder">
                <input type="hidden" name="parentId" value={data.currentFolder.ParentId} />
                <button class="mb-3 bg-blue-500 text-white px-4 py-2 rounded-md hover:bg-blue-600">Back</button>
            </form>
        {/if}
        {#if data.contents}
            <div class="grid grid-cols-2 md:grid-cols-4 lg:grid-cols-6 gap-4">
                <form method="POST" action="?/switchfolder">
                    <input type="hidden" name="switchFolderJson" value={switchFolderJson} />
                    {#each data.contents.Folders as folder}
                        <button type="submit" onclick={() => { switchFolderJson = JSON.stringify(folder); }}>
                            <div class="bg-white shadow rounded-lg p-4 text-center">
                                <div class="text-blue-500 mb-2">
                                    <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" class="w-10 h-10 mx-auto">
                                        <path stroke-linecap="round" stroke-linejoin="round" d="M3 7a2 2 0 012-2h3.5a2 2 0 011.44.56l1.12 1.12c.37.37.88.56 1.44.56H19a2 2 0 012 2v8a2 2 0 01-2 2H5a2 2 0 01-2-2V7z" />
                                    </svg>
                                </div>
                                <p class="text-sm font-semibold">{folder.Folder_Name}</p>
                            </div>
                        </button>
                    {/each}
                </form>
                
                {#each data.contents.Files as file}
                    <div class="bg-white shadow rounded-lg p-4 text-center">
                        <div class="text-gray-500 mb-2">
                            <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" class="w-10 h-10 mx-auto">
                                <path stroke-linecap="round" stroke-linejoin="round" d="M14 2H6a2 2 0 00-2 2v16a2 2 0 002 2h12a2 2 0 002-2V8l-6-6z" />
                                <path stroke-linecap="round" stroke-linejoin="round" d="M14 2v6h6" />
                            </svg>
                        </div>
                        <p class="text-sm font-semibold">{file.File_Name}</p>
                    </div>
                {/each}
            </div>
        {/if}
    </div>