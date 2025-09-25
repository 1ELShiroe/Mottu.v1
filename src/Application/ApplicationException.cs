using System.Diagnostics.CodeAnalysis;

namespace Application;

[ExcludeFromCodeCoverage]
public class ApplicationException(string businessMessage) : Exception(businessMessage);