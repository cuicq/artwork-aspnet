using Artworks.Configuration;

namespace Artworks.Routing.CommonModel.Internal
{
    /// <summary>
    /// 配置键。
    /// </summary>
    internal class RoutingKeyMap
    {
        public static string Routing = ArtworksConfiguration.Instance.GetValue<string>("routing");
    }
}
