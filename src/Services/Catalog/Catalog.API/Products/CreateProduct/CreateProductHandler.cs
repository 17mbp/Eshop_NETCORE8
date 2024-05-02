using MediatR;

public record CreatedProductCommand(string name, List<string> Category, string Description, string ImageFile, decimal Price)
    : IRequest<CreatedProductResult>;
public record CreatedProductResult(Guid Id);

internal class CreatedProductCommandHandler : IRequestHandler<CreatedProductCommand, CreatedProductResult>
{
    public Task<CreatedProductResult> Handle(CreatedProductCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
