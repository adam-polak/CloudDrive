<script lang="ts">
  let { form } = $props();
</script>

<div class="bg-gray-100 h-[87.5vh] flex flex-col">
  <main>
    <div
      class="text-center p-4 mx-auto mt-10 w-96 h-[30vh] rounded-md
      shadow bg-white"
    >
      <form method="POST" action="">
        <div>
          <div class="mb-8">
            <h1 class="text-2xl">Create Folder</h1>
          </div>
          {#if form?.message }
             <h4 class="text-center mt-[-1.5em] mb-1 text-md text-red-400">*{form?.message}</h4>
          {/if}
        </div>
        <div class="mb-10">
          <label class="inline-block text-xl py-1 px-3 shadow bg-blue-400 text-white rounded-md" for="foldername">Folder Name:</label>
          <input
            class="inline-block shadow rounded-md pl-2 py-1"
            type="text"
            id="foldername"
            name="foldername"
            placeholder="Enter folder name..."
          />
        </div>
        <div class="flex justify-between w-[100%]">
          <div>
          <a
            href="/signedin"
            class="bg-red-400 text-white text-l px-5 py-2 rounded-md hover:bg-red-500 inline-block"
            >Cancel</a>
          </div>
          <div>
            <button type="submit" class="bg-blue-500 text-white text-l px-5 py-2 rounded-md hover:bg-blue-600">Create</button>
          </div>
        </div>
      </form>
    </div>
  </main>
</div>
