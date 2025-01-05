using CloudDrive.BlobAccess.Controllers;
using CloudDrive.DataAccess.Controllers;
using CloudDrive.DataAccess.Models;

namespace CloudDrive.Controllers.Lib;

public class StorageManager
{
    private FolderDataController _folderData;
    private FileDataController _fileData;
    private BlobFileController _blobController;

    public StorageManager()
    {
        _folderData = new FolderDataController();
        _fileData = new FileDataController();
        _blobController = new BlobFileController();
    }

    public async Task DeleteFolderAndContents(int userId, int folderId)
    {
        List<Folder> folders = _folderData.GetNestedFolders(userId, folderId);
        List<FileModel> files = _fileData.GetFilesInFolder(folderId);

        // Delete files from storage
        foreach(FileModel file in files)
        {
            await _blobController.DeleteFile(file);
        }

        // Delete files from db
        _fileData.DeleteFilesInFolder(folderId);

        // Delete nested folders
        foreach(Folder folder in folders)
        {
            await DeleteFolderAndContents(userId, folder.Id);
        }

        // Delete folder
        _folderData.DeleteFolder(userId, folderId);
    }
}