
using Ordering.Application.Orders.Commands.UpdateOrder;
using System.Runtime.Intrinsics.X86;

namespace Ordering.Api.Endpoints
{
    public record UpdateOrderRequst(OrderDto order);
    
    public record UpdateOrderResponse(bool Success);

    public class UpdateOrder() : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/orders", async (UpdateOrderRequst request, ISender sender) =>
            {
                var command = request.Adapt<UpdateOrderCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<UpdateOrderResponse>();

                return Results.Ok(response);
            })
            .WithName("UpdateOrder")
            .Produces<UpdateOrderResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Order")
            .WithDescription("Update Order");

        }
    }
}
