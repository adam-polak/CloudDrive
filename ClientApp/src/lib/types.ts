export type User = {
    LoginKey: string,
    Username: string
}

export type FolderModel = {
    Id: number,
    OwnerId: number,
    Folder_Name: string,
    ParentId: number
}

export type FileModel = {
    Id: number,
    FolderId: number,
    File_Name: string
}

export type GetContentsResponse = {
    Folders: FolderModel[],
    Files: FileModel[]
}