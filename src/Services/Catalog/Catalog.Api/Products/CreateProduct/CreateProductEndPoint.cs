 using Carter; // For endpoint definition
 using Mapster; // for mapping requests to commands 
 using MediatR; // for apply design pattern CQRS
 using Marten;
 using BuildingBlocks.CQRS;
 using Catalog.Api.Models;


namespace Catalog.Api.Products.CreateProduct
{
    public record CreateProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price);
    public record CreateProductResponse(Guid Id);

    public class CreateProductEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products", async (CreateProductRequest request, ISender sendr) =>
            {
                var command = request.Adapt<CreateProductCommand>(); 

                var result = await sendr.Send(command);

                var response = result.Adapt<CreateProductResponse>();

                return Results.Created($"/products/{response.Id}", response);
            })
              .WithName("CreateProduct")
              .Produces<CreateProductResponse>(StatusCodes.Status201Created)
              .ProducesProblem(StatusCodes.Status400BadRequest)
              .WithSummary("Create a new product")
              .WithDescription("Create a new product");
        }
    }
}
