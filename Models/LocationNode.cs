namespace AdPlat.Models
{
    public class LocationNode
    {
        public string Path { get; set; } = string.Empty;
        public HashSet<string> AdPlatforms { get; set; } = new HashSet<string>();
        public Dictionary<string, LocationNode> Children { get; set; } = new Dictionary<string, LocationNode>();
    }
}