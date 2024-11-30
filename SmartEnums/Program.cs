// See https://aka.ms/new-console-template for more information

using Microsoft.EntityFrameworkCore;
using SmartEnums;
using SmartEnums.Models;
using SmartEnums.Persistence;

var myContext = new MyDbContext();

// var role = myContext.Roles.First(r => r.Id == Role.Admin);

var user = new User("camilo", Role.Admin);

myContext.Users.Add(user);

myContext.SaveChanges();

Console.WriteLine($"User {user.Name} is {user.Role}");