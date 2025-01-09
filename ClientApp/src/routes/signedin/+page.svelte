<script lang="ts">
  import type { ContentModel, FileModel, FolderModel } from "$lib/types.js";

  let { data } = $props();

  let contents = $state(data.contents ?? []);
  let switchFolderJson = $state("");
  let viewFileJson = $state("");
  let contentAction = $state("");
  let filterWord = $state("");
  let showDelete = $state(false);
  let contentToDelete: ContentModel | undefined = $state();

  function setContentToDelete(x: ContentModel) {
    contentToDelete = x;
    showDelete = true;
  }

  async function confirmDeleteContent(x: ContentModel) {
    if(!showDelete) return;

    if(x.Folder) {
      deleteFolder(x.Folder);
    } else if(x.File) {
      deleteFile(x.File);
    }

    showDelete = false;
  }

  function cancelDelete() {
    contentToDelete = undefined;
    showDelete = false;
  }

  function filterContents(event: any, word: string) {
    if(event.key === "Backspace") word = word.substring(0, word.length - 1);
    else if(event.key.length == 1) word += event.key;
    else return;

    if(word == "") {
      contents = data.contents ?? [];
      return;
    }

    contents = contents.filter(x => {
      const n = (x.File?.File_Name ?? x.Folder?.Folder_Name ?? "").toLocaleLowerCase();

      if(n == "") return false;
      return n.includes(word.toLowerCase());
    });

  }

  async function deleteFolder(folder: FolderModel) {
    contents = contents.filter((x) => x.Folder?.Id != folder.Id);

    fetch("/signedin/deleteFolder", {
      method: "POST",
      body: `${folder.Id}`,
    });
  }

  async function deleteFile(file: FileModel) {
    contents = contents.filter(x => x.File?.Id != file.Id);

    fetch("/signedin/deleteFile", {
      method: "POST",
      body: `${file.Id}`
    });
  }
</script>

<div class="bg-gray-100 h-[87.5vh] flex-col">
  <!-- Main Content -->
  <main class="flex-grow p-6">
    <div
      class="flex flex-wrap p-4 bg-white rounded-lg shadow-md mb-6 items-center gap-2"
    >
      <h1 class="text-xl">Path:</h1>
      <div class="bg-blue-400 text-white px-2 py-2 rounded-md overflow-x-auto">
        {#each data.folderPathsPretty ?? [] as path}
          <span>{ path } &gt;&nbsp;</span>
        {/each}
      </div>
      <div class="mt-4 w-[100%]">
        <input onkeydown={(e) => filterContents(e, filterWord)} type="text" class="border-blue-300 border-2 p-1 rounded-md" placeholder="Search folder..." bind:value={filterWord} />
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
      {#if showDelete && contentToDelete}
        <div style="position: absolute; z-index: 1;" class="p-5 justify-between flex flex-col ml-[25vw] lg:ml-[35vw] mb-[-1em] bg-white rounded-md w-[50vw] lg:w-[30vw] h-[30vh] lg:h-[25vh] shadow">
          <div class="text-lg text-center font-medium overflow-y-auto break-words">
            {#if contentToDelete.Folder}
              <p>Are you sure you want to delete folder: "<em>{contentToDelete.Folder.Folder_Name}</em>"?</p>
            {:else if contentToDelete.File}
              <p>Are you sure you want to delete file: "<em>{contentToDelete.File.File_Name}</em>"?</p>
            {/if}
          </div>
          <div class="flex flex-row justify-between">
            <button
             class="px-4 py-2 bg-gray-200 hover:bg-gray-300 rounded-md"
             onclick={() => { cancelDelete() }}
             >Cancel</button>
            <button
             class="px-4 py-2 bg-red-400 hover:bg-red-500 rounded-md"
             onclick={() => { 
                if(contentToDelete) {
                  confirmDeleteContent(contentToDelete);
                }
              }}
             >Yes, delete it</button>
          </div>
        </div>
      {/if}
      <div class="flex flex-row ml-3 flex-wrap mx-auto">
        <form method="POST" action={contentAction}>
          <input type="hidden" name="switchFolderJson" value={switchFolderJson} />
          <input type="hidden" name="fileJson" value={viewFileJson} />
          <div class="flex flex-row ml-3 gap-2 flex-wrap mx-auto">
            {#each contents as content}
              <div class="flex flex-col">
                {#if content.Folder}
                  <button
                    type="button"
                    style="position: absolute;"
                    onclick={() => {
                      if(content.Folder){
                        setContentToDelete(content);
                      }
                    }}
                    class="bg-red-400 mt-2 ml-4 hover:bg-red-500 text-white w-20 shadow rounded-sm mb-2"
                  >X</button>
                  <button
                    type="submit"
                    onclick={() => {
                      if(content.Folder) {
                        switchFolderJson = JSON.stringify(content.Folder);
                      }
                      contentAction = "?/switchfolder";
                    }}
                  >
                    <div
                      class="bg-white hover:bg-gray-100 shadow pt-10 h-36 w-28 rounded-lg p-4 text-center"
                    >
                      <div class="text-blue-500 mb-2 mt-2">
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
                      <div class="h-9 w-[100%] overflow-auto break-words">
                        <p class="text-sm font-semibold">{content.Folder.Folder_Name}</p>
                      </div>
                    </div>
                  </button>
                {:else if content.File}
                  <div class="flex flex-col">
                      <button
                      type="button"
                      style="position: absolute;"
                      onclick={() => {
                        if(content.File){
                          setContentToDelete(content);
                        }
                      }}
                      class="bg-red-400 mt-2 ml-4 hover:bg-red-500 text-white w-20 shadow rounded-sm mb-2"
                    >X</button>
                    <button
                      type="submit"
                      onclick={() => {
                        if(content.File) {
                          viewFileJson = JSON.stringify(content.File);
                        }
                        contentAction = "?/view";
                      }}
                    >
                      <div
                        class="bg-white hover:bg-gray-100 shadow pt-10 h-36 w-28 rounded-lg p-4 text-center"
                      >
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
                        <div class="h-9 w-[100%] overflow-auto break-words">
                          <p class="text-sm font-semibold">{content.File.File_Name}</p>
                        </div>
                      </div>
                    </button>
                  </div>
                {/if}
              </div>
            {/each}
          </div>
        </form>
      </div>
    {/if}
  </main>
</div>
