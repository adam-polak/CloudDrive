export type User = {
    LoginKey: string,
    Username: string,
    RootFolderId: number
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

export type ContentModel = {
    File: FileModel | undefined,
    Folder: FolderModel | undefined
}