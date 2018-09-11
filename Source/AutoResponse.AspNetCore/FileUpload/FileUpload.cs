{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// File upload.
    /// </summary>
    /// <typeparam name="TMeta">The type of the meta.</typeparam>
    public class FileUpload<TMeta>
    {
        /// <summary>
        /// Gets or sets the meta.
        /// </summary>
        /// <value>
        /// The meta.
        /// </value>
        public TMeta Meta { get; set; }

        /// <summary>
        /// Gets or sets the files.
        /// </summary>
        /// <value>
        /// The files.
        /// </value>
        public IList<IFormFile> Files { get; set; }
    }
}