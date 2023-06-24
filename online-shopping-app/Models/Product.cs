using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace online_shopping_app.Models
{
    [BsonIgnoreExtraElements]
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductId { get; set; } = String.Empty;
        [BsonElement("productName")]
        public string ProductName { get; set; } = String.Empty;
        [BsonElement("description")]
        public string Description { get; set; } = String.Empty;
        [BsonElement("price")]
        public int Price { get; set; }
        [BsonElement("features")]
        public string Features { get; set; } = String.Empty;
        [BsonElement("noOfOrdersPlaced")]
        public int NoOfOrdersPlaced { get; set; }
        [BsonElement("quantityAvailable")]
        public int QuantityAvaiable { get; set; }

        [BsonElement("productStatus")]
        public string ProductStatus { get; set; } = String.Empty;
        
    }
}
