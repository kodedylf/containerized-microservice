using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public class Repository : DbContext {
    public Repository(DbContextOptions<Repository> options) : base(options) { }
    public DbSet<Person> Personer { get; set; }
}

public class Person {
    [Key]
    public int PersonId { get; set; }
    public string Navn { get; set; }
}