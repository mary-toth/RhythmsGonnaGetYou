using System;
using RhythmsGonnaGetYou.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace RhythmsGonnaGetYou
{
    class BandDatabase1Context : DbContext
    {
        public DbSet<Band> Bands { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Song> Songs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("server=localhost;database=BandDatabase1");
        }
    }

    class Band
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string CountryOfOrigin { get; set; }
        public int NumberOfMembers { get; set; }
        public string Website { get; set; }
        public string Style { get; set; }
        public string IsSigned { get; set; }
        public string ContactName { get; set; }
        public string ContactPhoneNumber { get; set; }
        public List<Album> Albums { get; set; }
    }

    class Album
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string IsExplicit { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int BandID { get; set; }
        public List<Song> Songs { get; set; }
    }

    class Song
    {
        public int ID { get; set; }
        public int TrackNumber { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }
        public int AlbumID { get; set; }
        public Album Album { get; set; }
    }

    class Program
    {

        static string PromptForString(string prompt)
        {
            Console.Write(prompt);
            var userStringInput = Console.ReadLine();
            return userStringInput;
        }
        static int PromptForInteger(string prompt)
        {
            Console.Write(prompt);
            int userIntInput = int.Parse(Console.ReadLine());

            return userIntInput;
        }

        static void Main(string[] args)
        {

            var keepGoing = true;

            while (keepGoing)
            {
                var context = new BandDatabase1Context();

                var bandCount = context.Bands.Count();
                Console.WriteLine("");
                Console.WriteLine($"There are currently {bandCount} bands in the database.");

                // show albums that correspond with bands
                // var bandsAndAlbums = context.Bands.Include(band => band.Albums);

                // foreach (var band in bandsAndAlbums)
                // {
                //     Console.WriteLine($"Band: {band.Name} - Album: {band.Albums[0].Title}");
                // }


                Console.WriteLine("");
                Console.WriteLine("Choose a menu option:");
                Console.WriteLine("***");
                Console.WriteLine("Add new band - type Add Band");
                Console.WriteLine("View all bands - type View");
                Console.WriteLine("Add an album to the database - type Add Album");
                Console.WriteLine("Add a song to an album - type Add Song");
                Console.WriteLine("Let a band go - type Let Go");
                Console.WriteLine("Resign a band - type Resign");
                Console.WriteLine("View all albums of a band - type View Album");
                Console.WriteLine("View all albums ordered by release date- type Release");
                Console.WriteLine("View all bands that are signed - type Signed");
                Console.WriteLine("View all bands that are not signed - type Not Signed");
                Console.WriteLine("Quit the program - type Quit");
                Console.WriteLine("***");

                var choice = Console.ReadLine().ToLower();
                Console.WriteLine("");

                if (choice == "quit")
                {
                    keepGoing = false;
                }
                else if (choice == "add band")
                {
                    //add band
                    //broken, album ID issue

                    Console.Write("Enter the name of the band: ");
                    var name = Console.ReadLine();

                    Console.Write("Enter the band's country: ");
                    var country = Console.ReadLine();

                    Console.Write("How many members? ");
                    var numberOfMembers = int.Parse(Console.ReadLine());

                    Console.Write("What is their website? ");
                    var website = Console.ReadLine();

                    Console.Write("What is their genre? ");
                    var style = Console.ReadLine();

                    Console.Write("Are they signed? Yes/No: ");
                    var signedOrNot = Console.ReadLine();

                    Console.Write("Add a contact name: ");
                    var contactName = Console.ReadLine();

                    Console.Write("Add a contact number: ");
                    var contactNumber = Console.ReadLine();

                    var newBand = new Band
                    {
                        Name = name,
                        CountryOfOrigin = country,
                        NumberOfMembers = numberOfMembers,
                        Website = website,
                        Style = style,
                        IsSigned = signedOrNot,
                        ContactName = contactName,
                        ContactPhoneNumber = contactNumber,
                    };
                    context.Bands.Add(newBand);
                    context.SaveChanges();

                    Console.WriteLine("");
                    Console.WriteLine($"You have added the band '{newBand.Name}' to the database!");
                }
                //view
                else if (choice == "view")
                {
                    var sorted = context.Bands.Select(band => band.Name);
                    foreach (var band in context.Bands)
                    {
                        Console.WriteLine($"There is a band named {band.Name}.");
                    }
                }
                else if (choice == "add album")
                {
                    Console.Write("What is the album title?: ");
                    var albumTitle = Console.ReadLine();

                    Console.Write("Is it explicit? Yes/No: ");
                    var explicitYesNo = Console.ReadLine();

                    Console.Write("What is the release date? ");
                    var releaseDate = Console.ReadLine();

                    Console.WriteLine("");
                    Console.WriteLine("You've added an album!");

                    var newAlbum = new Album
                    {
                        Title = albumTitle,
                        IsExplicit = explicitYesNo,
                        // ReleaseDate = DateTime.releaseDate
                    };
                    context.Albums.Add(newAlbum);
                    context.SaveChanges();

                }
                else if (choice == "add song")
                {
                    var newSongTrackNumber = PromptForInteger("Enter the song's track number: ");
                    var newSongTitle = PromptForString("Enter the song's title: ");
                    var newSongDuration = PromptForInteger("Enter the song's duration: ");

                    Console.WriteLine("");
                    Console.WriteLine("You've added a song!");
                }
                else if (choice == "let go")
                {
                    Console.WriteLine("Let a band go! Update IsSigned to False!");
                }
                else if (choice == "resign")
                {
                    Console.WriteLine("Resign a band! Update IsSigned to True");
                }
                else if (choice == "view album")
                {
                    Console.WriteLine("View all albums of a band!");
                }
                else if (choice == "release")
                {
                    Console.WriteLine("View albums ordered by ReleaseDate");
                }
                else if (choice == "signed")
                {
                    Console.WriteLine("View all bands that are signed!");
                }
                else
                {
                    Console.WriteLine("View all bands that are NOT signed!");
                }

            }
        }
    }
}
