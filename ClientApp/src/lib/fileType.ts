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
            return true;
        default:
            return false;
    }
}

export function shouldConvertToPdf(fileSuffix: string) {
    switch(fileSuffix) {
        case "doc":
        case "docx":
            return true;
        default:
            return false;
    }
}