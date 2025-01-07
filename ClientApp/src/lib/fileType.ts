export function isImage(fileSuffix: string) {
    switch(fileSuffix) {
        case "apng":
        case "png":
        case "avif":
        case "gif":
        case "jpg":
        case "jpeg":
        case "jfif":
        case "pjpeg":
        case "pjp":
        case "svg":
        case "webp":
        case "pdf":
            return true;
        default:
            return false;
    }
}

export function shouldDisplayWithMicrosoft(fileSuffix: string) {
    switch(fileSuffix) {
        case "doc":
        case "docx":
        case "xlsx":
            return true;
        default:
            return false;
    }
}