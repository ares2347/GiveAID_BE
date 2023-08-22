using System.Net;
using GAID.Shared;

namespace GAID.Application.Attachment;

public class AttachmentService : IAttachmentService
{
    public async Task<string> SaveFileAsync(Stream file, string fileName)
    {
        throw new NotImplementedException();
        // var container = new BlobContainerClient(AppSettings.Instance.AzureStorage.ConnectionString,
        //     "attachments");
        // try
        // {
        //     var blob = container.GetBlobClient(fileName);
        //     file.Position = 0;
        //     await blob.UploadAsync(file);
        //     var fileUrl = blob.Uri.AbsoluteUri;
        //     return fileUrl;
        // }
        // catch (Exception ex)
        // {
        //     throw new HttpException("Upload file failed.", HttpStatusCode.BadRequest);
        // }
    }

    public async Task<Stream> BrowseFile(string fileName)
    {
        throw new NotImplementedException();
        // try
        // {
        //     var container = new BlobContainerClient(AppSettings.Instance.AzureStorage.ConnectionString,
        //         "attachments");
        //     var blob = container.GetBlobClient(fileName);
        //     var res = await blob.DownloadContentAsync();
        //     return res.Value.Content.ToStream();
        // }
        // catch (Exception e)
        // {
        //     throw new HttpException("Get file failed.", HttpStatusCode.BadRequest);
        // }
    }

    public async Task<MemoryStream> GetFileMemoryStream(string path)
    {
        throw new NotImplementedException();
        // HttpException.ThrowIfNull(path);
        // var stream = new MemoryStream();
        // await using var fs = File.Open(path, FileMode.Open);
        // await fs.CopyToAsync(stream);
        // return stream;
    }

    public void RemoveFile(string path)
    {
        throw new NotImplementedException();
        // if (!File.Exists(path))
        // {
        //     throw new HttpException("Remove file failed", HttpStatusCode.BadRequest);
        // }
        //
        // File.Delete(path);
    }
}