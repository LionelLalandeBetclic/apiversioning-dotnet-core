using Microsoft.AspNetCore.Mvc;

namespace Api.Conventions
{
    // TODO can do same for ?sort, ?fields, ?embed, ?cursor query strings
    [ModelBinder(typeof(PaginationModelBinder))]
    public sealed class Pagination
    {
        public int Limit { get; } = 10;

        public int Offset { get; } = 0;

        public Pagination(int offset, int limit) =>
            (Offset, Limit) = (offset, limit);
    }
}
