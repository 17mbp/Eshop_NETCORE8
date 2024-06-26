﻿namespace Catalog.API.Products.CreateProduct;
public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);
public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is requeried");
        RuleFor(x => x.ImageFile).NotEmpty().WithMessage("Imagefile is requeried");
        RuleFor(x => x.Category).NotEmpty().WithMessage("Category is requeried");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater is requeried"); 
    }
}
internal class CreateProductCommandHandler(IDocumentSession session, ILogger<CreateProductCommandHandler> logger)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateProductCommandHandler.Handle called with {@Command}", command);

        //create produt
        // save dbase
        var product = new Product
        {
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Name = command.Name,
            Price = command.Price
        };
        //
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);

        return new CreateProductResult(product.Id);         
    }
}