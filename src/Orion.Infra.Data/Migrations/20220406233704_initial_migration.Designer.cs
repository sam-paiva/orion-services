﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Orion.Infra.Data;

#nullable disable

namespace Orion.Infra.Data.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20220406233704_initial_migration")]
    partial class initial_migration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Orion.Core.Entities.Properties.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)")
                        .HasColumnName("city");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("creation_date");

                    b.Property<string>("District")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)")
                        .HasColumnName("district");

                    b.Property<Guid>("ImmobileId")
                        .HasColumnType("uuid")
                        .HasColumnName("immobile_id");

                    b.Property<DateTime?>("LastModification")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_modification");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)")
                        .HasColumnName("state");

                    b.HasKey("Id")
                        .HasName("pk_addresses");

                    b.HasIndex("ImmobileId")
                        .IsUnique()
                        .HasDatabaseName("ix_addresses_immobile_id");

                    b.ToTable("addresses", (string)null);
                });

            modelBuilder.Entity("Orion.Core.Entities.Properties.Immobile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<int>("AcquisitionType")
                        .HasColumnType("integer")
                        .HasColumnName("acquisition_type");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("creation_date");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(700)
                        .HasColumnType("character varying(700)")
                        .HasColumnName("description");

                    b.Property<int>("ImmobileType")
                        .HasColumnType("integer")
                        .HasColumnName("immobile_type");

                    b.Property<DateTime?>("LastModification")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_modification");

                    b.Property<string[]>("PhotosUrl")
                        .IsRequired()
                        .HasColumnType("text[]")
                        .HasColumnName("photos_url");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric")
                        .HasColumnName("price");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)")
                        .HasColumnName("title");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_properties");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_properties_user_id");

                    b.ToTable("properties", (string)null);
                });

            modelBuilder.Entity("Orion.Core.Entities.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("creation_date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)")
                        .HasColumnName("email");

                    b.Property<bool>("EmailIsVerified")
                        .HasColumnType("boolean")
                        .HasColumnName("email_is_verified");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("first_name");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("is_active");

                    b.Property<DateTime?>("LastModification")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_modification");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("last_name");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password_hash");

                    b.Property<string>("PhoneContact")
                        .HasColumnType("text")
                        .HasColumnName("phone_contact");

                    b.Property<string>("ProfileImageUrl")
                        .HasColumnType("text")
                        .HasColumnName("profile_image_url");

                    b.Property<Guid>("TokenHash")
                        .HasColumnType("uuid")
                        .HasColumnName("token_hash");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("Orion.Core.Entities.Properties.Address", b =>
                {
                    b.HasOne("Orion.Core.Entities.Properties.Immobile", "Immobile")
                        .WithOne("Address")
                        .HasForeignKey("Orion.Core.Entities.Properties.Address", "ImmobileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_addresses_properties_immobile_id");

                    b.Navigation("Immobile");
                });

            modelBuilder.Entity("Orion.Core.Entities.Properties.Immobile", b =>
                {
                    b.HasOne("Orion.Core.Entities.Users.User", "UserOwner")
                        .WithMany("Properties")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_properties_users_user_id1");

                    b.Navigation("UserOwner");
                });

            modelBuilder.Entity("Orion.Core.Entities.Properties.Immobile", b =>
                {
                    b.Navigation("Address");
                });

            modelBuilder.Entity("Orion.Core.Entities.Users.User", b =>
                {
                    b.Navigation("Properties");
                });
#pragma warning restore 612, 618
        }
    }
}