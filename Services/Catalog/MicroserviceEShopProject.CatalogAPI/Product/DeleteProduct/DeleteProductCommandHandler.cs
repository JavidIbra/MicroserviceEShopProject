namespace MicroserviceEShopProject.CatalogAPI.Product.DeleteProduct
{
    public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool IsSuccess);

    internal sealed class DeleteProductCommandHandler(IDocumentSession documentSession, ILogger<DeleteProductCommandHandler> logger)
        : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("handler called by {@Command} ", command);

            documentSession.Delete<Models.Product>(command.Id);

            await documentSession.SaveChangesAsync(cancellationToken);

            return new DeleteProductResult(true);
        }
    }
}
