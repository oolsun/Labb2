using Labb2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Labb2.Data
{
    public class Labb2DbContext : DbContext
    {
        public Labb2DbContext(DbContextOptions<Labb2DbContext> options) : base(options)
        {

        }
        public DbSet<Teacher> Teachers{ get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Class> Class { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
    }

}