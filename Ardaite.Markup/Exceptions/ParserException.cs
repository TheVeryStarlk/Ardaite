using Ardaite.Markup.Lexing;

namespace Ardaite.Markup.Exceptions;

public sealed class ParserException(TokenType expected, TokenType actual, int line, int column)
    : Exception($"Expected '{expected}' but got '{actual}' instead. @ {line}:{column}\n");