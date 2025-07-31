namespace MicroserviceEShopProject.CatalogAPI.Product.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price)
        : ICommand<UpdateProductResult>;

    public record UpdateProductResult(bool IsSuccess);

    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Product id is required");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Product id is required").Length(2,150).WithMessage("Name must be between 2 and 150 character");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }

    internal sealed class UpdateProductCommandHandler(IDocumentSession documentSession, ILogger<UpdateProductCommandHandler> logger)
        : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handler called by {@Command}", command);

            var product = await documentSession.LoadAsync<Models.Product>(command.Id);

            if (product is null)
                throw new ProductNotFoundException();

            product.Name = command.Name;
            product.Category = command.Category;
            product.Description = command.Description;
            product.ImageFile = command.ImageFile;
            product.Price = command.Price;

            documentSession.Update(product);
            await documentSession.SaveChangesAsync(cancellationToken);

            return new UpdateProductResult(true);
        }
    }
}
