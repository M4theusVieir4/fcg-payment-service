namespace FCG.Payments.Domain._Common.Exceptions;
public abstract class FcgException(string? message = default)
    : Exception(message) { }

