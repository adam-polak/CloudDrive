<script lang="ts">
    import DisplayFile from "$lib/DisplayFile.svelte";

    let { data } = $props();

    const b64 = data.file.Data.split(',')[1];
    const file = new File([atob(b64)], data.file.Name);
    const dataUrl = URL.createObjectURL(file);

    let message = $state();
</script>

<div class="bg-gray-100 h-[87.5vh] flex flex-col">
    <main>
        <div
          class="text-center p-4 mx-auto mt-10 w-[70vw] md:w-[60vw] lg:w-[40vw] h-[75vh] rounded-md shadow bg-white"
        >
            <div class="mb-2 h-14">
                <h1 class="text-2xl mb-2 overflow-y-auto break-words">{data.file.Name}</h1>
            </div>
            {#if message}
                <h4 class="text-center mt-[-1em] text-md text-red-400">
                    *{message}
                </h4>
            {/if}
            <div class="shadow h-[75%] overflow-y-auto mb-4 bg-gray-50">
                <DisplayFile name={data.file.Name} fileStr={data.file.Data} />
            </div>
            <div class="flex justify-between">
                <a
                    href="/signedin"
                    class="bg-red-400 text-white text-l px-5 py-2 rounded-md hover:bg-red-500 inline-block"
                    >Go Back</a>
                <a
                    href={dataUrl}
                    download={data.file.Name}
                    class="bg-blue-400 text-white text-l px-5 py-2 rounded-md hover:bg-blue-500 inline-block" 
                    >Download</a>
            </div>
        </div>
    </main>
</div>