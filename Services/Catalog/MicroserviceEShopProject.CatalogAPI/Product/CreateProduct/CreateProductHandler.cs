namespace MicroserviceEShopProject.CatalogAPI.Product.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
        : ICommand<CreateProductResult>;

    public record CreateProductResult(Guid Id);

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is required");
        }
    }

    internal sealed class CreateProductHandler(IDocumentSession session, ILogger<CreateProductHandler> logger) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handler called by {@Command}", command);

            var product = new Models.Product
            {
                Description = command.Description,
                Name = command.Name,
                Price = command.Price,
                Category = command.Category,
                ImageFile = command.ImageFile,
            };

            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);

            return new CreateProductResult(product.Id);
        }
    }
}
