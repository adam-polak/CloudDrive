<script lang="ts">
    import { isImage, shouldConvertToPdf } from "./fileType";

    let { name, fileStr } = $props();

    let data = $state();


    const arr = name.split('.');
    const type = arr[arr.length - 1];

    if(shouldConvertToPdf(type)) {
        // TODO
    } else if(!isImage(type)) {
        const b64 = fileStr.split(',')[1];
        data = atob(b64);
    }
</script>

<div class="bg-gray-50">
    {#if shouldConvertToPdf(type)}
        <p>Convert to PDF</p>
    {:else if isImage(type)}
        <img src={fileStr} alt="Display file" />
    {:else}
        <p class="overflow-y-auto break-words">{data}</p>
    {/if}
</div>