namespace GAID.Application.Attachment;

public interface IAttachmentService
{
    Task<string> SaveFileAsync(Stream file, string fileName, string contentType);
    Task<Stream> BrowseFile(string fileName);
    Task<MemoryStream> GetFileMemoryStream(string path);
    void RemoveFile(string path);
}