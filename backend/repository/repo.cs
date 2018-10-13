using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;

public class Repository : DbContext {
    public Repository(DbContextOptions<Repository> options) : base(options) { }
    public DbSet<Person> Personer { get; set; }
    public DbSet<User> Users { get; set; }
}

public class Person {
    [Key]
    public int PersonId { get; set; }
    public string Navn { get; set; }
}

public class User {
    [Key]
    public int UserId { get; set; }
    public string GoogleUserId { get; set; }
    public string Email { get; set; }
    public string[] Roles { get; set; }
    public string GivenName { get; internal set; }
    public string FamilyName { get; internal set; }
}