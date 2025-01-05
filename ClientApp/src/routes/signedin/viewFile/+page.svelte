<script lang="ts">
    let { data } = $props();

    let message = $state();
</script>

<div class="bg-gray-100 min-h-screen flex flex-col">
    <header class="bg-white shadow-md p-4 flex gap-3 justify-between items-center">
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
          class="text-center p-4 mx-auto mt-10 w-[70vw] md:w-[60vw] lg:w-[40vw] h-[75vh] rounded-md shadow bg-white"
        >
            <h1 class="text-2xl mb-2">{data.file.Name}</h1>
            {#if message}
                <h4 class="text-center mt-[-1em] text-md text-red-400">
                    *{message}
                </h4>
            {/if}
            <div class="shadow h-[80%] mb-4">
                <p class="h-[100%] w-[100%] overflow-y-auto break-words">{data.file.DataObj}</p>
            </div>
            <div>
                <a
                    href="/signedin"
                    class="bg-red-400 text-white text-l px-5 py-2 rounded-md hover:bg-red-500 inline-block"
                    >Back</a>
            </div>
        </div>
    </main>
</div>