﻿namespace Fortnox.SDK.Requests
{
    public class FileUploadRequest : BaseRequest
    {
        public byte[] FileData { get; set; }
        public string FileName { get; set; }
    }
}