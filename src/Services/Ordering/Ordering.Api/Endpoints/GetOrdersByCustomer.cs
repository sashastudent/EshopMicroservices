using Ordering.Application.Orders.Queries.GetOrderByCustomer;

namespace Ordering.Api.Endpoints
{
    //- Accepts a customer ID.
    //- Uses a GetOrdersByCustomerQuery to fetch orders.
    //- Returns the list of orders for that customer.

    //public record GetOrdersByCustomerRequest(Guid id);

    public record GetOrdersByCustomerResponse(IEnumerable<OrderDto> Orders);

    public class GetOrdersByCustomer : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders/customer/{customerId}", async (Guid customerId, ISender sender) =>
            {
                var request = await sender.Send(new GetOrdersByCustomerQuery(customerId));

                var response = request.Adapt<GetOrdersByCustomerResponse>();

                return Results.Ok(response);
            })
             .WithName("GetOrdersByCustomer")
            .Produces<GetOrdersByCustomerResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Orders By Customer")
            .WithDescription("Get Orders By Customer");
        }
    }
}
