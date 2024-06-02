namespace Ardaite.Markup.Exceptions;

public sealed class LexerException(string message, int line, int column) : Exception($"{message}. @ {line}:{column}\n");