using System.Diagnostics.CodeAnalysis;

namespace Infrastructure;

[ExcludeFromCodeCoverage]
public class InfrastructureException(string businessMessage) : Exception(businessMessage);