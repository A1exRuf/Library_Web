namespace UseCases;

public sealed record LinkDTO(
    string Rel,
    string Href,
    string Method);
