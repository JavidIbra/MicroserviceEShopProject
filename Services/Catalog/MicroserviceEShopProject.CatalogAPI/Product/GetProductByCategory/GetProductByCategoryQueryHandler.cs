using Marten.Linq.QueryHandlers;

namespace MicroserviceEShopProject.CatalogAPI.Product.GetProductByCategory
{
    public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
    public record GetProductByCategoryResult(IEnumerable<Models.Product> Products);

    internal sealed class GetProductByCategoryQueryHandler(IDocumentSession documentSession)
        : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            var products = await documentSession.Query<Models.Product>()
                                .Where(p => p.Category.Contains(query.Category))
                                   .ToListAsync(cancellationToken);

            return new GetProductByCategoryResult(products);
        }
    }
}
