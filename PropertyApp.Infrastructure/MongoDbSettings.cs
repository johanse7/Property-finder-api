namespace PropertyApp.Infrastructure;

public class MongoDbSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public string DatabaseName { get; set; } = string.Empty;
    public string PropertiesCollectionName { get; set; } = "properties";

    public string PropertyImageCollection { get; set; } = "propertyImages";
}
