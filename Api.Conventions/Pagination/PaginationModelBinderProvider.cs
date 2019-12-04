using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Api.Conventions
{
    internal sealed class PaginationModelBinderProvider : IModelBinderProvider
    {
        public PaginationModelBinderProvider(Action<PaginationOptions> configureDelegate)
        {
            _options = new PaginationOptions();
            configureDelegate?.Invoke(_options);
        }

        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            if (context.Metadata.ModelType == typeof(Pagination))
                return new PaginationModelBinder(_options);
            
            return null;
        }

        private readonly PaginationOptions _options;
    }
}
