﻿using System.Net.Http;

namespace Fortnox.SDK.Requests
{
    public class FileDownloadRequest : BaseRequest
    {
        public FileDownloadRequest()
        {
            Method = HttpMethod.Get;
        }
    }
}