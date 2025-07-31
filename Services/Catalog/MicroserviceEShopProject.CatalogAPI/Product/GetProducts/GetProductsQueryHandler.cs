namespace MicroserviceEShopProject.CatalogAPI.Product.GetProducts
{
    public record GetProductsQuery(): IQuery<GetProductsResult>;
    public record GetProductsResult(IEnumerable<Models.Product> Products);
    internal sealed class GetProductsQueryHandler(IDocumentSession documentSession,ILogger<GetProductsQueryHandler> logger)
        : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("handler called {@Query}" , query);

            var products =  await documentSession.Query<Models.Product>().ToListAsync(cancellationToken);

            return new GetProductsResult(products);
        }
    }
}
