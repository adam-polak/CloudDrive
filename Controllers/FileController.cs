using Microsoft.AspNetCore.Mvc;
using CloudDrive.DataAccess.Controllers;
using CloudDrive.BlobAccess.Controllers;
using Microsoft.Extensions.Primitives;
using CloudDrive.Controllers.Lib;
using CloudDrive.Models;
using Newtonsoft.Json;
using CloudDrive.DataAccess.Models;

namespace CloudDrive.Controllers;

[Route("fileapi")]
public class FileController : ControllerBase
{

    private FileDataController _fileDataController;
    private BlobFileController _blobFileController;
    private StorageManager _storageManager;

    public FileController()
    {
        _fileDataController = new FileDataController();
        _blobFileController = new BlobFileController();
        _storageManager = new StorageManager();
    }

    [HttpGet("getfiles")]
    public IActionResult GetFiles()
    {
        // TODO
        return Ok();
    }

    [HttpPost("uploadfile")]
    public async Task<IActionResult> UploadFile()
    {
        // folderid, file name
        HttpRequest request = HttpContext.Request;

        if(!request.Query.TryGetValue("folderid", out StringValues value))
        {
            return BadRequest("FolderId missing from query params");
        }

        if(!int.TryParse(value.ToString(), out int folderId))
        {
            return BadRequest("FolderId must be an integer");
        }

        string body = await BodyReader.GetStringFromStream(request.Body);
        UploadFileModel? file = JsonConvert.DeserializeObject<UploadFileModel>(body);

        try {

            if(file == null)
            {
                return BadRequest("Invalid request body format");
            }

            if(_fileDataController.ContainsFileName(folderId, file.Name))
            {
                return Conflict("File name already exists in folder");
            }

            _fileDataController.CreateFile(folderId, file.Name);
            FileModel? fileModel = _fileDataController.GetFile(folderId, file.Name);

            if(fileModel != null)
            {
                await _blobFileController.UploadFileAsync(fileModel, body);
            }

            return Ok();

        } catch {
            if(file != null)
            {
                FileModel? fileModel = _fileDataController.GetFile(folderId, file.Name);
                if(fileModel != null)
                {
                    _fileDataController.DeleteFile(folderId, fileModel.Id);
                } 
            }
            return BadRequest();
        }
    }

    [HttpPost("deletefile")]
    public async Task<IActionResult> DeleteFile()
    {
        HttpRequest request = HttpContext.Request;

        if(!request.Query.TryGetValue("folderid", out StringValues folderIdValue))
        {
            return BadRequest("FolderId missing from query params");
        }

        if(!request.Query.TryGetValue("fileid", out StringValues fileIdValue))
        {
            return BadRequest("FileId missing from query params");
        }

        try {
            int folderId = int.Parse(folderIdValue.ToString());
            int fileId = int.Parse(fileIdValue.ToString());

            await _storageManager.DeleteFile(folderId, fileId);

            return Ok();
        } catch {
            return BadRequest();
        }
    }

}
