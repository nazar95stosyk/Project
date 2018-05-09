using System.Data.Entity;
using Test.Models;

namespace Test.Context
{

    public class SentencesContext : DbContext
    {
        public DbSet<Result> Results { get; set; }
    }
}
