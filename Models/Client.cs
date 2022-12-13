using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication10.Models
{
    public class Client
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string name { get; set; }
        public string message { get; set; }
        public DateTime LogTime { get; set; }      

    }
    public class ClientDBContext : DbContext
    {
        public DbSet<Client> clients { get; set; }
    }
}