using MicroserviceEShopProject.BuildingBlocks.Exceptions;

namespace MicroserviceEShopProject.CatalogAPI.Exceptions
{
    public class ProductNotFoundException(Guid Id) : NotFoundException("Product" , Id)
    {
    }
}
