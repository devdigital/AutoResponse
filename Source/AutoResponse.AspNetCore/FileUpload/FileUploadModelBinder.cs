{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    using Newtonsoft.Json;

    /// <summary>
    /// File upload model binder.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ModelBinding.IModelBinder" />
    public class FileUploadModelBinder : IModelBinder
    {
        /// <summary>
        /// Attempts to bind a model.
        /// </summary>
        /// <param name="bindingContext">The <see cref="T:Microsoft.AspNetCore.Mvc.ModelBinding.ModelBindingContext" />.</param>
        /// <returns>
        /// <para>
        /// A <see cref="T:System.Threading.Tasks.Task" /> which will complete when the model binding process completes.
        /// </para>
        /// <para>
        /// If model binding was successful, the <see cref="P:Microsoft.AspNetCore.Mvc.ModelBinding.ModelBindingContext.Result" /> should have
        /// <see cref="P:Microsoft.AspNetCore.Mvc.ModelBinding.ModelBindingResult.IsModelSet" /> set to <c>true</c>.
        /// </para>
        /// <para>
        /// A model binder that completes successfully should set <see cref="P:Microsoft.AspNetCore.Mvc.ModelBinding.ModelBindingContext.Result" /> to
        /// a value returned from <see cref="M:Microsoft.AspNetCore.Mvc.ModelBinding.ModelBindingResult.Success(System.Object)" />.
        /// </para></returns>
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var meta = bindingContext.ValueProvider.GetValue("meta");
            if (meta.Length == 0)
            {
                AddError(bindingContext, "No meta formdata field provided in the request.");
                return Task.CompletedTask;
            }

            if (meta.Length > 1)
            {
                AddError(bindingContext, "Multiple meta formdata fields provided in the request, expected one.");
                return Task.CompletedTask;
            }

            // All other values should be files in the formdata
            var metaType = bindingContext.ModelType.GenericTypeArguments.First();
            try
            {
                var metaData = JsonConvert.DeserializeObject(meta.FirstValue, metaType);
                if (metaData == null)
                {
                    AddError(bindingContext, "Meta formdata field is invalid JSON.");
                }

                var fileUploadType = typeof(FileUpload<>).MakeGenericType(metaType);
                var fileUpload = Activator.CreateInstance(fileUploadType);
                var metaProperty = fileUpload.GetType().GetProperty("Meta");
                metaProperty.SetValue(fileUpload, metaData);

                // TODO: set model value
                // bindingContext.ModelState.SetModelValue(bindingContext.ModelName,
                //   new ValueProviderResult(new StringValues(fileUpload)));

                // TODO: check there are no other values other than files
                var files = bindingContext.HttpContext?.Request?.Form?.Files?.ToList();
                var filesProperty = fileUpload.GetType().GetProperty("Files");
                filesProperty.SetValue(fileUpload, files ?? Enumerable.Empty<IFormFile>());

                bindingContext.Result = ModelBindingResult.Success(fileUpload);
            }
            catch
            {
                AddError(bindingContext, "There was an error deserializing the meta formdata JSON. Please check it's valid JSON.");
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Adds an error to the model state.
        /// </summary>
        /// <param name="bindingContext">The binding context.</param>
        /// <param name="message">The message.</param>
        private static void AddError(ModelBindingContext bindingContext, string message)
        {
            bindingContext.ModelState.TryAddModelError(
                bindingContext.ModelName, message);
        }
    }
}