using Marten.Linq.QueryHandlers;

namespace MicroserviceEShopProject.CatalogAPI.Product.GetProductByCategory
{
    public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
    public record GetProductByCategoryResult(IEnumerable<Models.Product> Products);

    internal sealed class GetProductByCategoryQueryHandler(IDocumentSession documentSession, ILogger<GetProductByCategoryQueryHandler> logger)
        : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("handler called by {@Query} ", query);

            var products = await documentSession.Query<Models.Product>()
                                .Where(p => p.Category.Contains(query.Category))
                                   .ToListAsync(cancellationToken);

            return new GetProductByCategoryResult(products);
        }
    }
}
