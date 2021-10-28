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

        //methods to prompt for user input
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

                Console.WriteLine($"Welcome! There are currently {bandCount} bands in the database.");

                Console.WriteLine("");
                Console.WriteLine("Please choose a menu option below:");
                Console.WriteLine("********************************");
                Console.WriteLine("[A]dd, [V]iew, [C]ontracts, or [Q]uit");
                Console.WriteLine("********************************");

                var choice = Console.ReadLine().ToLower();
                Console.WriteLine("");
                // quit
                if (choice == "q")
                {
                    keepGoing = false;
                }
                // add
                else if (choice == "a")
                {
                    Console.WriteLine("Add Band, Album, or Song?");
                    var addChoice = Console.ReadLine();

                    //add band
                    if (addChoice == "band")
                    {
                        Console.WriteLine("");
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
                    // add album
                    if (addChoice == "album")
                    {
                        Console.Write("What is the album title?: ");
                        var albumTitle = Console.ReadLine();

                        Console.Write("Is it explicit? Yes/No: ");
                        var explicitYesNo = Console.ReadLine();

                        Console.Write("What is the release date? YYYY/MM/DD ");
                        var releaseDate = DateTime.Parse(Console.ReadLine());

                        Console.Write("What is the band ID associated with this album? ");
                        var bandID = int.Parse(Console.ReadLine());


                        Console.WriteLine("");
                        Console.WriteLine($"You've added {albumTitle} to the album database!");

                        var newAlbum = new Album
                        {
                            Title = albumTitle,
                            IsExplicit = explicitYesNo,
                            ReleaseDate = releaseDate,
                            BandID = bandID
                        };
                        context.Albums.Add(newAlbum);
                        context.SaveChanges();

                    }
                    // add song
                    if (addChoice == "song")
                    {
                        Console.Write("What is the track number? ");
                        var trackNumber = int.Parse(Console.ReadLine());

                        Console.Write("What is the song's title? ");
                        var trackTitle = Console.ReadLine();

                        Console.Write("What is the duration? ");
                        var trackDuration = int.Parse(Console.ReadLine());

                        Console.Write("What is the album ID associated with this song? ");
                        var albumID = int.Parse(Console.ReadLine());

                        var newSong = new Song
                        {
                            TrackNumber = trackNumber,
                            Title = trackTitle,
                            Duration = trackDuration,
                            AlbumID = albumID
                        };
                        context.Songs.Add(newSong);
                        context.SaveChanges();

                        Console.WriteLine("");
                        Console.WriteLine($"You've added {trackTitle} to the database.");
                    }
                }
                else if (choice == "v")
                {
                    Console.WriteLine("Type [all] to view all bands. Type [album] to see all albums. Type [find] to find all albums by a band. ");
                    var viewChoice = Console.ReadLine();

                    //view all bands
                    if (viewChoice == "all")
                    {
                        var sorted = context.Bands.Select(band => band.Name);
                        Console.WriteLine("");
                        Console.WriteLine("Bands in the database:");
                        foreach (var band in context.Bands)
                        {
                            Console.WriteLine(band.Name);
                        }
                    }
                    // view all albums ordered by release date
                    if (viewChoice == "album")
                    {
                        Console.WriteLine("");

                        var sorted = context.Albums.OrderBy(album => album.ReleaseDate);

                        foreach (var album in sorted)
                        {
                            Console.WriteLine($"{album.Title} was released on {album.ReleaseDate}.");
                        }
                    }
                    if (viewChoice == "find")
                    {
                        //find album 
                        Console.WriteLine("Choose a band to see their albums: ");
                        var bandChosen = Console.ReadLine();
                        Console.WriteLine("");

                        var bandsAndAlbums = context.Bands.Include(band => band.Albums).FirstOrDefault(band => band.Name == bandChosen);

                        foreach (var album in bandsAndAlbums.Albums)
                        {
                            Console.WriteLine($"{bandChosen} - Album: {album.Title}");
                        }
                    }
                }
                else if (choice == "c")
                {
                    Console.WriteLine("Type [A] to alter contracts. Type [V] to view contracts.");
                    var contractChoice = Console.ReadLine();

                    if (contractChoice == "a")
                    {
                        Console.WriteLine("Type [S] to sign a band. Type [L] to let a band go. ");
                        var secondContractChoice = Console.ReadLine();

                        //sign band
                        if (secondContractChoice == "s")
                        {
                            Console.Write("What is the name of the band you want to sign? ");
                            var bandToSign = Console.ReadLine();

                            //firstordefault returns single element of the list for the first item the expression returns true
                            var existingBandUnsigned = context.Bands.FirstOrDefault(band => band.Name == bandToSign);

                            existingBandUnsigned.IsSigned = "Yes";

                            Console.WriteLine("");
                            Console.WriteLine($"You have signed the band {bandToSign}.");

                            context.SaveChanges();
                        }
                        //unsign band
                        if (secondContractChoice == "l")
                        {
                            Console.Write("What is the name of the band you want to let go? ");
                            var bandToLetGo = Console.ReadLine();

                            var existingBand = context.Bands.FirstOrDefault(band => band.Name == bandToLetGo);

                            existingBand.IsSigned = "No";

                            Console.WriteLine("");
                            Console.WriteLine($"You have un-signed the band {bandToLetGo}.");

                            context.SaveChanges();
                        }
                    }

                    //view all contracts
                    if (contractChoice == "v")
                    {
                        var sorted = context.Bands.Where(band => band.IsSigned == "Yes");

                        Console.WriteLine("");
                        Console.WriteLine("Bands that are signed:");
                        foreach (var band in sorted)
                        {
                            Console.WriteLine(band.Name);
                        }

                        Console.WriteLine("");
                        var sortedNotSigned = context.Bands.Where(band => band.IsSigned == "No");

                        Console.WriteLine("Bands that are not signed:");
                        foreach (var band in sortedNotSigned)
                        {
                            Console.WriteLine(band.Name);
                        }

                    }

                }


            }
        }
    }
}
