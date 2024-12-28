namespace CloudDrive.DataAccess.Models;

public class FileModel
{
    public int Id { get; set; }
    public int FolderId { get; set; }
    public required string File_Name { get; set; }
}