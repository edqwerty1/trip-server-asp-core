﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using TripServer.Models;

namespace TripServer.Migrations
{
    [DbContext(typeof(TripContext))]
    partial class TripContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc2-20901")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TripServer.Models.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address1");

                    b.Property<string>("Address2");

                    b.Property<string>("Address3");

                    b.Property<string>("Address4");

                    b.Property<string>("Address5");

                    b.Property<string>("PostCode");

                    b.HasKey("Id");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("TripServer.Models.Location", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("AddressId");

                    b.Property<string>("ImageUrl");

                    b.Property<string>("Name");

                    b.Property<int>("Nights");

                    b.Property<double>("Price");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("TripServer.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DisplayName");

                    b.Property<Guid?>("DownVoteLocationId");

                    b.Property<string>("Password");

                    b.Property<Guid?>("UpVoteLocationId");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.HasIndex("DownVoteLocationId");

                    b.HasIndex("UpVoteLocationId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TripServer.Models.Location", b =>
                {
                    b.HasOne("TripServer.Models.Address")
                        .WithMany()
                        .HasForeignKey("AddressId");
                });

            modelBuilder.Entity("TripServer.Models.User", b =>
                {
                    b.HasOne("TripServer.Models.Location")
                        .WithMany()
                        .HasForeignKey("DownVoteLocationId");

                    b.HasOne("TripServer.Models.Location")
                        .WithMany()
                        .HasForeignKey("UpVoteLocationId");
                });
        }
    }
}
