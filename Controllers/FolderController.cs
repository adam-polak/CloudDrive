using CloudDrive.Controllers.Lib;
using CloudDrive.DataAccess.Controllers;
using CloudDrive.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace CloudDrive.Controllers;

[Route("folderapi")]
public class FolderController : ControllerBase
{
    private FolderDataController _folderDataController;
    private FileDataController _fileDataController;
    private UserDataController _userDataController;
    private StorageManager _storageManager;

    public FolderController()
    {
        _fileDataController = new FileDataController();
        _folderDataController = new FolderDataController();
        _userDataController = new UserDataController();
        _storageManager = new StorageManager();
    }

    private string GetLoginKey(HttpRequest request)
    {
        if(!request.Query.TryGetValue("loginkey", out StringValues value)) {
            throw new InvalidOperationException(
                "LoginMiddleware shouldn't allow program to this point if loginkey isn't present in query params"
                );
        }

        return value.ToString();
    }

    private int GetUserId(string loginKey)
    {
        return _userDataController.GetUserId(loginKey);
    }

    private int GetUserId(HttpRequest request)
    {
        return GetUserId(GetLoginKey(request));
    }

    [HttpPost("createfolder")]
    public IActionResult CreateFolder()
    {
        HttpRequest request = HttpContext.Request;

        int userId = GetUserId(request);

        if(!request.Query.TryGetValue("foldername", out StringValues value)) 
        {
            return BadRequest();
        }

        string folderName = value.ToString();

        if(!request.Query.TryGetValue("folderid", out value)) 
        {
            return BadRequest();
        }

        try {
            int parentId = int.Parse(value.ToString());
            if(_folderDataController.ContainsFolder(parentId, folderName)) 
            {
                return Conflict();
            }

            _folderDataController.CreateFolder(userId, parentId, folderName);

            return Ok();
        } catch {
            return BadRequest();
        }
    }

    [HttpGet("getFolderByPath")]
    public IActionResult GetFolderIdFromPath()
    {
        HttpRequest request = HttpContext.Request;

        int userId = GetUserId(request);

        if (!request.Query.TryGetValue("path", out StringValues value))
        {
            return BadRequest("No path provided");
        }

        try {
            Folder folder =  _folderDataController.GetFolderByPath(userId, value.ToString());

            string json = JsonConvert.SerializeObject(folder);

            return Ok(json);
        } catch {
            return BadRequest();
        }
    }

    [HttpGet("getpath")]
    public IActionResult GetPath()
    {
        HttpRequest request = HttpContext.Request;

        int userId = GetUserId(request);

        if(!request.Query.TryGetValue("folderid", out StringValues value))
        {
            return BadRequest("FolderId missing from query params");
        }

        try {
            int folderId = int.Parse(value.ToString());

            Folder folder = _folderDataController.GetFolder(userId, folderId);
            string path = "/" + folder.Folder_Name;
            while(folder.ParentId != 0)
            {
                folderId = folder.ParentId;
                folder = _folderDataController.GetFolder(userId, folderId);
                path = "/" + folder.Folder_Name + path;
            }

            return Ok(path);
        } catch {
            return BadRequest();
        }
    }

    [HttpGet("getfolder")]
    public IActionResult GetFolder()
    {
        HttpRequest request = HttpContext.Request;
        
        int userId = GetUserId(request);

        if(!request.Query.TryGetValue("folderid", out StringValues value))
        {
            return BadRequest("FolderId missing from query params");
        }

        try {
            int folderId = int.Parse(value.ToString());

            Folder folder = _folderDataController.GetFolder(userId, folderId);

            string json = JsonConvert.SerializeObject(folder);

            return Ok(json);
        } catch {
            return BadRequest();
        }
    }

    [HttpPost("deletefolder")]
    public async Task<IActionResult> DeleteFolder()
    {
        HttpRequest request = HttpContext.Request;

        int userId = GetUserId(request);

        if(!request.Query.TryGetValue("folderid", out StringValues value)) 
        {
            return BadRequest("FolderId missing from query params");
        }

        try {
            int folderId = int.Parse(value.ToString());

            await _storageManager.DeleteFolderAndContents(userId, folderId);

            return Ok();
        } catch {
            return BadRequest();
        }
    }

    [HttpGet("getcontents")]
    public IActionResult GetContents()
    {
        HttpRequest request = HttpContext.Request;

        int userId = GetUserId(request);

        if(!request.Query.TryGetValue("folderid", out StringValues value)) 
        {
            return BadRequest("FolderId missing from query params");
        }

        try {
            int folderId = int.Parse(value.ToString());

            List<Folder> folders = _folderDataController.GetNestedFolders(userId, folderId);
            List<FileModel> files = _fileDataController.GetFilesInFolder(folderId);

            object response = new {
                Folders = folders,
                Files = files
            };

            string json = JsonConvert.SerializeObject(response);

            return Ok(json);
        } catch {
            return BadRequest();
        }
    }
}