namespace CloudDrive.DataAccess.Models;

public class Folder
{
    public int Id { get; set; }
    public int OwnerId { get; set; }
    public required string Folder_Name { get; set; }
    public int ParentId { get; set; }
}