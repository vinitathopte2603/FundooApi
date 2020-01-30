﻿// <auto-generated />
using System;
using FundooRepositoryLayer.ModelContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FundooRepositoryLayer.Migrations
{
    [DbContext(typeof(UserContext))]
    partial class UserContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FundooCommonLayer.Model.LabelModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("IsCreated");

                    b.Property<DateTime>("IsModified");

                    b.Property<string>("Label");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Labels");
                });

            modelBuilder.Entity("FundooCommonLayer.Model.LabelsNotes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LabelId");

                    b.Property<int>("NoteId");

                    b.HasKey("Id");

                    b.ToTable("labelsNotes");
                });

            modelBuilder.Entity("FundooCommonLayer.Model.NotesModel", b =>
                {
                    b.Property<int>("NotesID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Color");

                    b.Property<string>("Description");

                    b.Property<int>("ID");

                    b.Property<string>("Image");

                    b.Property<bool>("IsArchive");

                    b.Property<DateTime>("IsCreated");

                    b.Property<DateTime>("IsModified");

                    b.Property<bool>("IsPin");

                    b.Property<bool>("IsTrash");

                    b.Property<DateTime?>("Reminder");

                    b.Property<string>("Title");

                    b.HasKey("NotesID");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("FundooCommonLayer.Model.UserDB", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<bool>("IsActive");

                    b.Property<DateTime>("IsCreated");

                    b.Property<DateTime>("IsModified");

                    b.Property<string>("LastName");

                    b.Property<string>("Passwrod");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
