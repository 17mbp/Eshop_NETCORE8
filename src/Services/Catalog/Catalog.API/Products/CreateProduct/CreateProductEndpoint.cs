namespace Catalog.API.Products.CreateProduct;
public record CreateProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : ICommand<CreateProductResult>;

public record CreatedProductResponse(Guid Id);

public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateProductCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<CreatedProductResponse>();
            return Results.Created($"/products/{response.Id}", response);
        })
            .WithName("CreateProduct")
            .Produces<CreatedProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("cREATE oRIDYCT")
            .WithDescription("create product");
    }
}