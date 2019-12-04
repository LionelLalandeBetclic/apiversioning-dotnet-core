using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Api.Conventions
{
    internal sealed class PaginationModelBinder : IModelBinder
    {
        public PaginationModelBinder(PaginationOptions options) =>
            _options = options;

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null) throw new ArgumentNullException(nameof(bindingContext));

            int offset;
            if (!bindingContext.ActionContext.HttpContext.Request.Query.TryGetValue("offset", out var offsetStr) ||
                !int.TryParse(offsetStr, out offset))
                offset = 0; // default value

            int limit;
            if (!bindingContext.ActionContext.HttpContext.Request.Query.TryGetValue("limit", out var limitStr) ||
                !int.TryParse(limitStr, out limit))
                limit = _options.DefaultLimit; // default value

            var result = new Pagination(offset, limit);
            bindingContext.BindingSource = BindingSource.Custom;
            bindingContext.Result = ModelBindingResult.Success(result);

            return Task.CompletedTask;
        }

        private readonly PaginationOptions _options;
    }
}
