<script lang="ts">
    import { isImage, shouldDisplayWithMicrosoft } from "./fileType";

    let { name, fileStr } = $props();
    
    let data: string[] = $state([]);

    const arr = name.split('.');
    const type = arr[arr.length - 1];
    
    if(!isImage(type)) {
        const b64 = fileStr.split(',')[1];
        const dataStr = atob(b64);
        data = dataStr.split('\n');
    }

</script>

<div class="bg-gray-50 text-start">
    {#if isImage(type)}
        <img src={fileStr} alt="Display file" />
    {:else}
        {#each data as s}
            <p class="ml-2 mb-1 break-words">{s}</p>
        {/each}
    {/if}
</div>