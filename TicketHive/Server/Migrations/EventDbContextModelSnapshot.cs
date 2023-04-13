﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TicketHive.Server.Data;

#nullable disable

namespace TicketHive.Server.Migrations
{
    [DbContext(typeof(EventDbContext))]
    partial class EventDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TicketHive.Shared.Models.BookingModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EventModelId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int?>("UserModelId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EventModelId");

                    b.HasIndex("UserModelId");

                    b.ToTable("Bookings", (string)null);
                });

            modelBuilder.Entity("TicketHive.Shared.Models.EventModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AvailableTickets")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("EventDetails")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EventName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EventPlace")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EventType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PricePerTicket")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("TotalTickets")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Events", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AvailableTickets = 100,
                            Date = new DateTime(2024, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EventDetails = "The chess pieces gathered 'round for a thrilling showdown! With knights charging and bishops plotting, who will reign supreme as the ultimate chess champion? Let the games begin!",
                            EventName = "Chess Tournament",
                            EventPlace = "The Basement",
                            EventType = "Sport",
                            Image = "image 25.png",
                            PricePerTicket = 200m,
                            TotalTickets = 150
                        },
                        new
                        {
                            Id = 2,
                            AvailableTickets = 15,
                            Date = new DateTime(2024, 8, 10, 12, 30, 0, 0, DateTimeKind.Unspecified),
                            EventDetails = "The legendary Bengt-Åke is back with a new thrilling Lama Race for the whole family to enjoy. Who will be the next Lama Race Master?",
                            EventName = "Bengt-Åkes Lama Race",
                            EventPlace = "Bengans Trav- & korv-service Arena",
                            EventType = "Sport",
                            Image = "image 13.png",
                            PricePerTicket = 350m,
                            TotalTickets = 100
                        },
                        new
                        {
                            Id = 3,
                            AvailableTickets = 50,
                            Date = new DateTime(2025, 5, 7, 9, 0, 0, 0, DateTimeKind.Unspecified),
                            EventDetails = "Get your earplugs out because your baby niece that you usually only see during Christmas is performing with her friends in this once in a lifetime concert. ",
                            EventName = "Klass 3B`s spring concert",
                            EventPlace = "Folkets Hus, Linköping",
                            EventType = "Concert",
                            Image = "image 19.png",
                            PricePerTicket = 50m,
                            TotalTickets = 60
                        },
                        new
                        {
                            Id = 4,
                            AvailableTickets = 44,
                            Date = new DateTime(2025, 11, 1, 19, 0, 0, 0, DateTimeKind.Unspecified),
                            EventDetails = "Join Benjamin, the blogging guru, for a fun and informative session on crafting killer blog posts! With practical tips and insider secrets, you'll be a pro in no time. Bring your creativity and get ready to write!",
                            EventName = "Benjamins Bloggskola",
                            EventPlace = "Byaskolan",
                            EventType = "Learning",
                            Image = "image 2.png",
                            PricePerTicket = 199m,
                            TotalTickets = 45
                        },
                        new
                        {
                            Id = 5,
                            AvailableTickets = 0,
                            Date = new DateTime(2025, 12, 18, 18, 0, 0, 0, DateTimeKind.Unspecified),
                            EventDetails = "Coming up, coming up this holiday season with E-type, the legendary Swedish pop star, as he takes the stage for a festive concert! Sing and dance along to your favorite hits, and enjoy the magic of Christmas with this one-of-a-kind performance.",
                            EventName = "E-types Christmas Tour",
                            EventPlace = "House Arena",
                            EventType = "Concert",
                            Image = "image 15.png",
                            PricePerTicket = 650m,
                            TotalTickets = 14000
                        },
                        new
                        {
                            Id = 6,
                            AvailableTickets = 5000,
                            Date = new DateTime(2025, 7, 13, 20, 0, 0, 0, DateTimeKind.Unspecified),
                            EventDetails = "Rock out with Lisa Ajax as she puts her own spin on classic Ozzy Osbourne hits in a night of electrifying entertainment! You wn't to miss this epic cover concert.",
                            EventName = "Lisa Ajax vs Ozzy Osbourne",
                            EventPlace = "Cirkus, Stockholm",
                            EventType = "Concert",
                            Image = "image 10.png",
                            PricePerTicket = 700m,
                            TotalTickets = 10000
                        });
                });

            modelBuilder.Entity("TicketHive.Shared.Models.UserModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("UserCountry")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("TicketHive.Shared.Models.BookingModel", b =>
                {
                    b.HasOne("TicketHive.Shared.Models.EventModel", "EventModel")
                        .WithMany()
                        .HasForeignKey("EventModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TicketHive.Shared.Models.UserModel", null)
                        .WithMany("Bookings")
                        .HasForeignKey("UserModelId");

                    b.Navigation("EventModel");
                });

            modelBuilder.Entity("TicketHive.Shared.Models.UserModel", b =>
                {
                    b.Navigation("Bookings");
                });
#pragma warning restore 612, 618
        }
    }
}
