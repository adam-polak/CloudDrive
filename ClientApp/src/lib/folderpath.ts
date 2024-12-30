export function removeFromPath(path: string) {

    let i = path.length - 1;
    while(i > 0) {
        if(path[i] == '/') break;
        i--;
    }

    return path.substring(0, i);
}

export function addToPath(path: string, addFolderName: string) {
    return `${path}/${addFolderName}`;
}