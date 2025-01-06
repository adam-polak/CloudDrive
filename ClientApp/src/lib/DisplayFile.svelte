<script lang="ts">
    import { isImage, shouldConvertToPdf } from "./fileType";


    let { name, fileStr } = $props();

    const type = name.split('.')[1];

    let mode = $state();

    if(shouldConvertToPdf(type)) {
        // TODO
    }

</script>

<div>
    {#if shouldConvertToPdf(type)}
        <p>Convert to PDF</p>
    {:else if isImage(type)}
        <img src={fileStr} alt="Display file" />
    {:else if type == "txt"}
        <p class="overflow-y-auto break-words">{fileStr}</p>
    {:else}
        <p class="text-red-400 text-lg">*Display for file type not supported</p>
    {/if}
</div>