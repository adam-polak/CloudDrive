<script lang="ts">
  let { form } = $props();

  let files: FileList | undefined = $state();
  let message = $state(form?.message);

  const maxSize = 10000000;

  let data: any = $state();

  function displayFileName(name: any) {
    const arr = name.split("\\");
    return arr[arr.length - 1];
  }

  $effect(() => {
    if (!(files && files[0])) {
      data = undefined;
      return;
    }

    if(files[0].size > maxSize) {
      message = "File too large (max 10 mb)";
      files = undefined;
      return;
    }

    const reader = new FileReader();
    reader.onload = () => {
      data = reader.result;
    };

    reader.readAsDataURL(files[0]);
  });
</script>

<div class="bg-gray-100 h-[87.5vh] flex flex-col">
  <main>
    <div
      class="text-center p-6 mx-auto mt-10 w-[70vw] md:w-[60vw] lg:w-[40vw] h-[32vh] rounded-md shadow bg-white"
    >
      <h1 class="text-2xl mb-8">Upload File</h1>
      {#if message}
        <h4 class="text-center mt-[-2em] mb-2 text-md text-red-400">
          *{message}
        </h4>
      {/if}
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
                {#if data && files}
                  <p class="w-[100%] h-6 overflow-hidden">
                    {displayFileName(files[0].name)}
                  </p>
                  <input type="hidden" name="data" value={data} />
                  <input type="hidden" name="name" value={files[0].name} />
                {:else if files}
                  <p>loading...</p>
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
            bind:files
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
