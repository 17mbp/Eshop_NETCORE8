namespace Catalog.API.Products.UpdateProduct;
public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImagenFile, decimal Price)
    : ICommand<UpdateProductResult>;
public record UpdateProductResult(bool IsSuccess);
public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(command => command.Id).NotEmpty().WithMessage("Product  ID is required");
        RuleFor(command => command.Name).NotEmpty().WithMessage("Name is reuired")
            .Length(2, 150).WithMessage("Name must be between 2 and 150 characters");
        RuleFor(command => command.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}
internal class UpdateProductCommandHandler(IDocumentSession session, ILogger<UpdateProductCommandHandler> logger)
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateProductHandler.Handle called with {@Command}", command);
        var products = await session.LoadAsync<Product>(command.Id, cancellationToken);
        if (products is null)
        {
            throw new ProductNotFoundException(command.Id);
        }
        products.Name = command.Name;
        products.Category = command.Category;
        products.Price = command.Price;
        products.Description = command.Description;
        products.ImageFile = command.ImagenFile;
        session.Update(products);
        await session.SaveChangesAsync(cancellationToken);
        return new UpdateProductResult(true);
    }
}