namespace App.Domain.Events;

public record ProductAddedEvent(long Id, string Name, decimal Price) : IEventOrMessage;