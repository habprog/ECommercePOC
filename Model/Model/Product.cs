using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Model
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string PropductName { get; set; } = null!;

        public decimal Price { get; set; }

        public string Category { get; set; } = null!;
        public int Quantity { get; set; }
        public string Thumbnail { get; set; }
    }
}
