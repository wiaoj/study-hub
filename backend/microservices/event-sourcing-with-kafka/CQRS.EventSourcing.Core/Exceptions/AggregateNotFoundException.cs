namespace CQRS.EventSourcing.Core.Exceptions;
public class AggregateNotFoundException(String message) : Exception(message);