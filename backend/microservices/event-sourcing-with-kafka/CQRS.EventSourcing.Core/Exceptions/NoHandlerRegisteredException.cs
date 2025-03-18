namespace CQRS.EventSourcing.Core.Exceptions;
public class NoHandlerRegisteredException(String handlerName) : Exception($"No handler was registered! ({handlerName})");