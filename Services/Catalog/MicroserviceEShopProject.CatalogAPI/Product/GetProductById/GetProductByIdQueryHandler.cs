namespace MicroserviceEShopProject.CatalogAPI.Product.GetProductById
{
    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdQueryResult>;

    public record GetProductByIdQueryResult(Models.Product Product);

    internal sealed class GetProductByIdQueryHandler(IDocumentSession documentSession, ILogger<GetProductByIdQueryHandler> logger) : IQueryHandler<GetProductByIdQuery, GetProductByIdQueryResult>
    {
        public async Task<GetProductByIdQueryResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("handler called with {@Query}", query);

            var product = await documentSession.LoadAsync<Models.Product>(query.Id,cancellationToken);

            return product is null ? throw new ProductNotFoundException() : new GetProductByIdQueryResult(product);
        }
    }
}
