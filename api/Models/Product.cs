namespace Stock.Models;

using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Product
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Guid { get; set; }

    [Required(ErrorMessage = "O campo nome é necessário")]
    [StringLength(20, MinimumLength = 5, ErrorMessage = "O produto deve ter entre 5 e 20 caracteres")]
    public string? Name { get; set; }
    
    [Required]
    public int Stock { get; set; }
}