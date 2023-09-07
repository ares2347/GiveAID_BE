using System.Net;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using GAID.Shared;

namespace GAID.Application.Attachment;

public class AttachmentService : IAttachmentService
{
    public async Task<string> SaveFileAsync(Stream file, string fileName, string contentType)
    {
        var container = new BlobContainerClient(AppSettings.Instance.AzureStorage.ConnectionString,
            "attachments");
        try
        {
            var blob = container.GetBlobClient(fileName);
            file.Position = 0;
            file.Seek(0, SeekOrigin.Begin);
            await blob.UploadAsync(file, new BlobHttpHeaders { ContentType = contentType });
            var fileUrl = blob.Uri.AbsoluteUri;
            return fileUrl;
        }
        catch (Exception ex)
        {
            throw new HttpException("Upload file failed.", HttpStatusCode.BadRequest);
        }
    }

    public async Task<Stream> BrowseFile(string fileName)
    {
        try
        {
            var container = new BlobContainerClient(AppSettings.Instance.AzureStorage.ConnectionString,
                "attachments");
            var blob = container.GetBlobClient(fileName);
            var res = await blob.DownloadContentAsync();
            return res.Value.Content.ToStream();
        }
        catch (Exception e)
        {
            throw new HttpException("Get file failed.", HttpStatusCode.BadRequest);
        }
    }

    public async Task<MemoryStream> GetFileMemoryStream(string path)
    {
        HttpException.ThrowIfNull(path);
        var stream = new MemoryStream();
        await using var fs = File.Open(path, FileMode.Open);
        await fs.CopyToAsync(stream);
        return stream;
    }

    public void RemoveFile(string path)
    {
        if (!File.Exists(path))
        {
            throw new HttpException("Remove file failed", HttpStatusCode.BadRequest);
        }
        
        File.Delete(path);
    }
}