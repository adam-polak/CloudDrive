<script lang="ts">
  let fileName = $state(null);

  function displayFileName(name: string) {
    const arr = name.split("\\");
    return arr[arr.length - 1];
  }
</script>

<div>
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
  </header>
  <main>
    <div
      class="text-center p-4 mx-auto mt-10 w-[70vw] md:w-[60vw] lg:w-[40vw] h-[32vh] rounded-md shadow bg-white"
    >
      <h1 class="text-2xl mb-8">Upload File</h1>
      <form method="POST" action="">
        <div class="mb-10">
          <label for="file" class="file-upload">
            <div class="flex gap-3">
              <div
                style="cursor: pointer;"
                class="md:w-32 shadow py-1 px-3 bg-blue-400 text-white rounded-md"
              >
                Choose File
              </div>
              <div class="mt-1">
                {#if fileName}
                  <p class="w-[100%] h-6 overflow-hidden">
                    {displayFileName(fileName)}
                  </p>
                {:else}
                  <p>no file selected...</p>
                {/if}
              </div>
            </div>
          </label>
          <input
            class="inline-block py-1 px-3 shadow bg-blue-400 text-white rounded-md mb-10"
            type="file"
            id="file"
            name="file"
            bind:value={fileName}
          />
        </div>
        <div class="mt-3 flex justify-between w-[100%]">
          <div>
            <a
              href="/signedin"
              class="bg-red-400 text-white text-l px-5 py-2 rounded-md hover:bg-red-500 inline-block"
              >Cancel</a
            >
          </div>
          <div>
            <button
              type="submit"
              class="bg-blue-500 text-white text-l px-5 py-2 rounded-md hover:bg-blue-600"
              >Upload</button
            >
          </div>
        </div>
      </form>
    </div>
  </main>
</div>

<style>
  input[type="file"] {
    display: none;
  }

  .file-upload {
    display: inline-block;
  }
</style>
