namespace SplitHub.Api.ErrorHandling;

/// <summary>
/// Stable ProblemDetails "type" identifiers that clients branch on instead of parsing
/// title or detail text. They are opaque identifiers, not resolvable URLs.
/// </summary>
public static class ProblemTypes
{
    public const string InternalServerError = "urn:splithub:problem:internal-server-error";
}
