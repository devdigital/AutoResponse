{
    using System;

    using Microsoft.AspNetCore.Mvc.ModelBinding;

    /// <summary>
    /// File upload model binder provider.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ModelBinding.IModelBinderProvider" />
    public class FileUploadModelBinderProvider : IModelBinderProvider
    {
        /// <summary>
        /// Creates a <see cref="T:Microsoft.AspNetCore.Mvc.ModelBinding.IModelBinder" /> based on <see cref="T:Microsoft.AspNetCore.Mvc.ModelBinding.ModelBinderProviderContext" />.
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.ModelBinding.ModelBinderProviderContext" />.</param>
        /// <returns>
        /// An <see cref="T:Microsoft.AspNetCore.Mvc.ModelBinding.IModelBinder" />.
        /// </returns>
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var isFileUpload =
                context.Metadata.ModelType.IsGenericType &&
                context.Metadata.ModelType.GetGenericTypeDefinition() == typeof(FileUpload<>);

            return isFileUpload ? new FileUploadModelBinder() : null;
        }
    }
}