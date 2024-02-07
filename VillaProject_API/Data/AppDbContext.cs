using Microsoft.EntityFrameworkCore;
using VillaProject_API.Models;

namespace VillaProject_API.Data
{
	public class AppDbContext : DbContext
	{

		public DbSet<Villa> Villas { get; set; }
		public DbSet<VillaNumber> VillaNumbers { get; set; }
		public DbSet<LocalUser> LocalUsers {  get; set; } 
		public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<VillaNumber>().HasData(
				new VillaNumber
				{
					VillaNo = 101,
					SpecialDetails = "Some details for first room in the first villa"
				},
				new VillaNumber
				{
					VillaNo = 102,
					SpecialDetails = "Some details for the second room in the first villa"
				});

			modelBuilder.Entity<Villa>().HasData(
				new Villa
				{
					Id = 1,
					Name = "FirstVilla",
					Details = "Great villa",
					ImageUrl = "https://u.cubeupload.com/anton_demyanchuk/villa1.jpg ",
					Occupancy = 4,
					Rate = 300,
					Sqft = 500,
					Amenity = "",
					CreateDate = DateTime.Now
				},
				new Villa
				{
					Id = 2,
					Name = "SecondVilla",
					Details = "Great villa",
					ImageUrl = "https://u.cubeupload.com/anton_demyanchuk/villa2.jpg",
					Occupancy = 3,
					Rate = 200,
					Sqft = 300,
					Amenity = "",
					CreateDate = DateTime.Now
				},
				new Villa
				{
					Id = 3,
					Name = "ThirdVilla",
					Details = "Great villa",
					ImageUrl = "https://u.cubeupload.com/anton_demyanchuk/villa5.jpg",
					Occupancy = 5,
					Rate = 500,
					Sqft = 700,
					Amenity = "",
					CreateDate = DateTime.Now
				},
				new Villa
				{
					Id = 4,
					Name = "FourthVilla",
					Details = "Great villa",
					ImageUrl = "https://u.cubeupload.com/anton_demyanchuk/villa4.jpg",
					Occupancy = 4,
					Rate = 330,
					Sqft = 550,
					Amenity = "",
					CreateDate = DateTime.Now
				});
			modelBuilder.Entity<LocalUser>().HasData(
				new LocalUser
				{
					Id = 1,
					UserName = "testUser",
					Name = "JustUser",
					Password = "12345",
					Role = "admin"
				});
		}

	}
}
