using Microsoft.EntityFrameworkCore;
using PhoneBook.Domain.Core.People;
using PhoneBook.Domain.Core.Phones;
using PhoneBook.Domain.Core.Tags;
using PhoneBook.Infrastructures.DataLayer.People;
using PhoneBook.Infrastructures.DataLayer.Phones;
using PhoneBook.Infrastructures.DataLayer.Tags;

namespace PhoneBook.Infrastructures.DataLayer.Common
{
    public class PhoneBookContext:DbContext
    {
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<PersonTag> PersonTags { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public PhoneBookContext(DbContextOptions<PhoneBookContext> option) :base(option)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonConfig());
            modelBuilder.ApplyConfiguration(new PersonTagConfig());
            modelBuilder.ApplyConfiguration(new PhoneConfig());
            modelBuilder.ApplyConfiguration(new TagConfig());
            base.OnModelCreating(modelBuilder);
        }
    }
}
