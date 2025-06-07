namespace Domain.Abstractions
{
    public class ApiPaginatedResponse<T> : ApiResponse<ApiPaginationData<T>>
    {
        private ApiPaginatedResponse(bool isSuccess, ApiPaginationData<T>? data, ApiError? error)
            : base(isSuccess, data, error)
        {
        }

        public static ApiPaginatedResponse<T> Success(IEnumerable<T> items, int totalItems, int pageSize, int currentPage)
        {
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var data = new ApiPaginationData<T>
            {
                Items = items,
                TotalItems = totalItems,
                PageSize = pageSize,
                CurrentPage = currentPage,
                TotalPages = totalPages,
            };

            return new ApiPaginatedResponse<T>(true, data, null);
        }
    }
}
