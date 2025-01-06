<script lang="ts">
  import type { FileModel, FolderModel } from "$lib/types.js";

  let { data } = $props();

  let folders = $state(data.contents?.Folders ?? []);
  let files = $state(data.contents?.Files ?? []);
  let switchFolderJson = $state("");

  async function deleteFolder(folder: FolderModel) {
    folders = folders.filter((x) => x.Id != folder.Id);

    fetch("/signedin/deleteFolder", {
      method: "POST",
      body: `${folder.Id}`,
    });
  }

  async function deleteFile(file: FileModel) {
    files = files.filter((x) => x.Id != file.Id);

    fetch("/signedin/deleteFile", {
      method: "POST",
      body: `${file.Id}`
    });
  }
</script>

<div class="bg-gray-100 min-h-screen flex flex-col">
  <!-- Main Content -->
  <main class="flex-grow p-6">
    <div
      class="p-4 bg-gray-100 rounded-lg shadow-md mb-6 flex items-center gap-2"
    >
      <h1 class="text-xl">Path:</h1>
      <div class="bg-blue-400 text-white px-2 py-2 rounded-md">
        {#each data.folderPathsPretty ?? [] as path}
          <span>{ path } &gt;&nbsp;</span>
        {/each}
      </div>
    </div>
    {#if data.currentFolder?.Id != data.RootFolderId}
      <form method="POST" action="?/backFolder">
        <input
          type="hidden"
          name="parentId"
          value={data.currentFolder?.ParentId}
        />
        <button
          class="mb-3 bg-blue-500 text-white px-4 py-2 rounded-md hover:bg-blue-600"
          >Back</button
        >
      </form>
    {/if}
    {#if data.contents}
      <div class="flex flex-row gap-2 ml-3 flex-wrap mx-auto">
        <form method="POST" action="?/switchfolder">
          <input
            type="hidden"
            name="switchFolderJson"
            value={switchFolderJson}
          />
          <div class="flex flex-row gap-2 ml-3 flex-wrap">
            {#each folders as folder}
              <div class="flex-col text-center">
                <button
                  type="button"
                  style="position: absolute;"
                  onclick={() => deleteFolder(folder)}
                  class="bg-red-400 mt-2 ml-4 hover:bg-red-500 text-white w-20 shadow rounded-sm mb-2"
                  >X</button
                >
                <button
                  type="submit"
                  style="z-index: -1;"
                  onclick={() => {
                    switchFolderJson = JSON.stringify(folder);
                  }}
                >
                  <div
                    class="bg-white hover:bg-gray-100 shadow pt-10 h-32 w-28 rounded-lg p-4 text-center"
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
                    <div>
                      <p class="text-sm font-semibold">{folder.Folder_Name}</p>
                    </div>
                  </div>
                </button>
              </div>
            {/each}
          </div>
        </form>

        {#each files as file}
          <div class="flex flex-col">
            <button
              type="button"
              style="position: absolute;"
              onclick={() => deleteFile(file)}
              class="bg-red-400 mt-2 ml-4 hover:bg-red-500 text-white w-20 shadow rounded-sm mb-2"
              >X</button>
            <button class="bg-white hover:bg-gray-100 shadow rounded-lg pt-10 px-2 text-center h-32 w-28">
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
              <div class="h-9 w-[100%] overflow-auto">
                <p class="text-sm font-semibold">{file.File_Name}</p>
              </div>
            </button>
          </div>
        {/each}
      </div>
    {/if}
  </main>
</div>
