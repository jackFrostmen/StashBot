﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StashBot.Module.Database.Account.Sqlite;

namespace StashBot.Migrations
{
    [DbContext(typeof(UsersContext))]
    [Migration("20191107214740_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0");

            modelBuilder.Entity("StashBot.Module.Database.Account.Sqlite.UserModel", b =>
                {
                    b.Property<long>("UserModelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("ChatId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("HashPassword")
                        .HasColumnType("TEXT");

                    b.HasKey("UserModelId");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
