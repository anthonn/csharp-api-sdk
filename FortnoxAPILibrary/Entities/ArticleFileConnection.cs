using System;
using System.ComponentModel;

// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming

namespace FortnoxAPILibrary.Entities
{
    /// <remarks/>
    public class ArticleFileConnection
	{
        /// <remarks/>
		public string FileId { get; set; }

        /// <remarks/>
        public string ArticleNumber { get; set; }

        /// <summary>This field is Read-Only in Fortnox</summary>
		[ReadOnly(true)]
		public string url { get; set; }
    }
}
