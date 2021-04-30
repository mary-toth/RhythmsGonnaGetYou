using System;
using RhythmsGonnaGetYou.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RhythmsGonnaGetYou
{
    class BandDatabase1Context : DbContext
    {
        public DbSet<Band> Bands { get; set; }
        // public DbSet<Album> Albums { get; set; }
        // public DbSet<Song> Songs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("server=localhost;database=BandDatabase1");
        }
    }

    class Band
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string CountryOfOrigin { get; set; }
        public int NumberOfMembers { get; set; }
        public string Website { get; set; }
        public string Style { get; set; }
        public bool IsSigned { get; set; }
        public string ContactName { get; set; }
        public string ContactPhoneNumber { get; set; }
    }

    class Album
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public bool IsExplicit { get; set; }
        public DateTime ReleaseDate { get; set; }
    }

    class Song
    {
        public int ID { get; set; }
        public int TrackNumber { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }

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

            var context = new BandDatabase1Context();

            var keepGoing = true;

            while (keepGoing)
            {

                var bandCount = context.Bands.Count();

                Console.WriteLine($"There are {bandCount} bands in the database");

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
                    var newBandName = PromptForString("Enter the name of the band you want to add: ");
                    var newBandCountry = PromptForString("Enter the band's country of origin: ");
                    var numberOfBandMembers = PromptForInteger("Enter the number of band members: ");
                    var newBandWebsite = PromptForString("Enter the band's website: ");
                    var newBandStyle = PromptForString("Enter the band's style/genre: ");
                    var newBandSignedOrNot = PromptForString("Are they signed? ");
                    var newContactName = PromptForString("Enter the band's contact name: ");
                    var newPhoneNumber = PromptForString("Enter the band's phone number: ");

                    Console.WriteLine("");
                    Console.WriteLine("You have added a band to the database!");
                }
                else if (choice == "view")
                {
                    Console.WriteLine("View all the bands!");
                }
                else if (choice == "add album")
                {
                    var newAlbumName = PromptForString("Enter the album's title: ");
                    var newAlbumExplicitOrNot = PromptForString("Is it explicit? ");
                    var newAlbumReleaseDate = PromptForString("Enter its release date: ");

                    Console.WriteLine("");
                    Console.WriteLine("You've added an album!");
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
