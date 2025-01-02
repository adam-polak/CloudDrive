<script lang="ts">
  import type { FolderModel } from "$lib/types.js";

  let { data } = $props();

  let folders = $state(data.contents?.Folders ?? []);
  let files = $state(data.contents?.Files ?? []);
  let switchFolderJson = $state("");
  let showFolderDeleteId = $state(0);
  let hoverCount = $state(0);

  async function deleteFolder(folder: FolderModel) {
    if (folders == null) return;

    folders = folders.filter((x) => x.Id != folder.Id);

    fetch("/signedin/deleteFolder", {
      method: "POST",
      body: `${folder.Id}`,
    });
  }
</script>

<div class="bg-gray-100 min-h-screen flex flex-col">
  <!-- Header -->
  <header
    class="bg-white shadow-md p-4 flex gap-3 justify-between items-center"
  >
    <div>
      <h1 class="font-bold text-2xl text-blue-600">CloudDrive</h1>
      <a
        href="/signedin/signout"
        class="hover:underline text-sm ml-2 text-black">Sign out</a
      >
    </div>
    <div class="flex gap-2">
      <input
        type="text"
        placeholder="Search folder..."
        class="border rounded-md px-4 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-blue-400"
      />
      <a
        href="signedin/createFolder"
        class="bg-blue-500 text-white px-4 py-2 rounded-md hover:bg-blue-600"
        >Create Folder</a
      >
      <button
        class="bg-blue-500 text-white px-4 py-2 rounded-md hover:bg-blue-600"
        >Upload File</button
      >
    </div>
  </header>

  <!-- Main Content -->
  <main class="flex-grow p-6">
    <div
      class="p-4 bg-gray-100 rounded-lg shadow-md mb-6 flex items-center gap-2"
    >
      <h1 class="text-xl">Path:</h1>
      <div class="bg-blue-400 text-white px-2 py-2 rounded-md">
        <span><h4>{data.folderPath}</h4></span>
      </div>
    </div>
    {#if data.currentFolder != null && data.currentFolder.Id != 0}
      <form method="POST" action="?/backFolder">
        <input
          type="hidden"
          name="parentId"
          value={data.currentFolder.ParentId}
        />
        <button
          class="mb-3 bg-blue-500 text-white px-4 py-2 rounded-md hover:bg-blue-600"
          >Back</button
        >
      </form>
    {/if}
    {#if data.contents}
      <div class="flex flex-col mx-auto">
        <form method="POST" action="?/switchfolder">
          <input
            type="hidden"
            name="switchFolderJson"
            value={switchFolderJson}
          />
          <div class="flex flex-row gap-2 ml-3 flex-wrap w-[100%]">
            {#each folders as folder}
              <div class="flex-col text-center h-5 w-24">
                <button
                  type="button"
                  style="position: absolute;"
                  onclick={() => deleteFolder(folder)}
                  class="bg-red-400 mt-1 ml-2 hover:bg-red-500 text-white w-16 shadow rounded-sm mb-2"
                  >X</button
                >
                <button
                  type="submit"
                  style="z-index: -1;"
                  class="mt-[-.25em]"
                  onclick={() => {
                    switchFolderJson = JSON.stringify(folder);
                  }}
                >
                  <div
                    class="bg-white hover:bg-gray-100 shadow pt-10 h-32 w-20 rounded-lg p-4 text-center"
                  >
                    <div class="text-blue-500 mb-2">
                      <svg
                        xmlns="http://www.w3.org/2000/svg"
                        fill="none"
                        viewBox="0 0 24 24"
                        stroke-width="2"
                        stroke="currentColor"
                        class="w-10 h-10 mx-auto"
                      >
                        <path
                          stroke-linecap="round"
                          stroke-linejoin="round"
                          d="M3 7a2 2 0 012-2h3.5a2 2 0 011.44.56l1.12 1.12c.37.37.88.56 1.44.56H19a2 2 0 012 2v8a2 2 0 01-2 2H5a2 2 0 01-2-2V7z"
                        />
                      </svg>
                    </div>
                    <p class="text-sm font-semibold">{folder.Folder_Name}</p>
                  </div>
                </button>
              </div>
            {/each}
          </div>
        </form>

        {#each files as file}
          <div class="bg-white shadow rounded-lg p-4 text-center">
            <div class="text-gray-500 mb-2">
              <svg
                xmlns="http://www.w3.org/2000/svg"
                fill="none"
                viewBox="0 0 24 24"
                stroke-width="2"
                stroke="currentColor"
                class="w-10 h-10 mx-auto"
              >
                <path
                  stroke-linecap="round"
                  stroke-linejoin="round"
                  d="M14 2H6a2 2 0 00-2 2v16a2 2 0 002 2h12a2 2 0 002-2V8l-6-6z"
                />
                <path
                  stroke-linecap="round"
                  stroke-linejoin="round"
                  d="M14 2v6h6"
                />
              </svg>
            </div>
            <p class="text-sm font-semibold">{file.File_Name}</p>
          </div>
        {/each}
      </div>
    {/if}
  </main>
</div>
