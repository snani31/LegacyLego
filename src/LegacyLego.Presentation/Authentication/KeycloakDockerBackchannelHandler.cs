namespace LegacyLego.Presentation.Authentication;

internal sealed class KeycloakDockerBackchannelHandler : DelegatingHandler
{
    private readonly string _publicBaseUrl;
    private readonly string _internalBaseUrl;
    private readonly bool _shouldRedirect;

    public KeycloakDockerBackchannelHandler(string publicBaseUrl, string internalBaseUrl)
        : base(new HttpClientHandler())
    {
        _publicBaseUrl = publicBaseUrl.TrimEnd('/');
        _internalBaseUrl = internalBaseUrl.TrimEnd('/');

        // Если урлы совпадают (например, в Dev), флаг будет false
        _shouldRedirect = !string.Equals(_publicBaseUrl, _internalBaseUrl, StringComparison.OrdinalIgnoreCase);
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (!_shouldRedirect || request.RequestUri == null)
            return base.SendAsync(request, cancellationToken);

        var uriString = request.RequestUri.ToString();

        if (uriString.StartsWith(_publicBaseUrl, StringComparison.OrdinalIgnoreCase))
        {
            var redirectedUrl = uriString.Replace(_publicBaseUrl, _internalBaseUrl, StringComparison.OrdinalIgnoreCase);
            request.RequestUri = new Uri(redirectedUrl);
        }

        return base.SendAsync(request, cancellationToken);
    }
}