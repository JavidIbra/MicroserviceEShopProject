namespace MicroserviceEShopProject.CatalogAPI.Helpers
{
    public class PaginationHeader(int currentPage, int itemsPerPage, int totalItems)
    {
        public int CurrentPage { get; set; } = currentPage;
        public int ItemsPerPage { get; set; } = itemsPerPage;
        public int TotalItems { get; set; } = totalItems;
        public int TotalPages { get; } = itemsPerPage > 0 ? (int)Math.Ceiling(totalItems / (double)itemsPerPage) : currentPage;
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
    }
}
