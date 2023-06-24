namespace online_shopping_app.Models
{
    public interface IDatabaseSettings
    {
        string CollectionsName { get; set; }
        string CollectionForUsers { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
