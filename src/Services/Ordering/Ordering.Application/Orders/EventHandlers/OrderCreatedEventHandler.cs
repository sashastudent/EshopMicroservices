﻿namespace Ordering.Application.Orders.EventHandlers
{
    internal class OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger)
        : INotificationHandler<OrderCreatedEvent>
    {
        public  Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);
            return Task.CompletedTask;

            //if (await featureManager.IsEnabledAsync("OrderFullfilment"))
            //{
            //    var orderCreatedIntegrationEvent = domainEvent.order.ToOrderDto();
            //    await publishEndpoint.Publish(orderCreatedIntegrationEvent, cancellationToken);
            //}
        }
    }
}
