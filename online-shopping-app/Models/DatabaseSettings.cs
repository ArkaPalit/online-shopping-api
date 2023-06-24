namespace online_shopping_app.Models
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string CollectionsName { get; set; } = String.Empty;
        public string  CollectionForUsers { get; set; } = String.Empty;
        public string ConnectionString { get; set; } = String.Empty;
        public string DatabaseName { get; set; } = String.Empty;
    }
}
