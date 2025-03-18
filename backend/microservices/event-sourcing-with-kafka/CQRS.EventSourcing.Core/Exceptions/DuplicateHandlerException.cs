namespace CQRS.EventSourcing.Core.Exceptions;
public class DuplicateHandlerException() : Exception("You cannot register the same handler twice!");