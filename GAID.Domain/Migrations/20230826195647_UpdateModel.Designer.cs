﻿// <auto-generated />
using System;
using GAID.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GAID.Domain.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230826195647_UpdateModel")]
    partial class UpdateModel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GAID.Domain.Models.Attachment.Attachment", b =>
                {
                    b.Property<Guid>("AttachmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid?>("CreatedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("ModifiedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid?>("ModifiedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Size")
                        .HasColumnType("bigint");

                    b.HasKey("AttachmentId");

                    b.HasIndex("CreatedById");

                    b.HasIndex("ModifiedById");

                    b.ToTable("Attachments");
                });

            modelBuilder.Entity("GAID.Domain.Models.Donation.Donation", b =>
                {
                    b.Property<Guid>("DonationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid?>("CreatedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EnrollmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<int>("Method")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("ModifiedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid?>("ModifiedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Remark")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("DonationId");

                    b.HasIndex("CreatedById");

                    b.HasIndex("EnrollmentId");

                    b.HasIndex("ModifiedById");

                    b.ToTable("Donation");
                });

            modelBuilder.Entity("GAID.Domain.Models.Email.EmailTemplate", b =>
                {
                    b.Property<Guid>("EmailTemplateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EmailTemplateType")
                        .HasColumnType("int");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmailTemplateId");

                    b.ToTable("EmailTemplates");
                });

            modelBuilder.Entity("GAID.Domain.Models.Enrollment.Enrollment", b =>
                {
                    b.Property<Guid>("EnrollmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid?>("CreatedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("ModifiedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid?>("ModifiedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProgramId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("EnrollmentId");

                    b.HasIndex("CreatedById");

                    b.HasIndex("ModifiedById");

                    b.HasIndex("ProgramId");

                    b.ToTable("Enrollment");
                });

            modelBuilder.Entity("GAID.Domain.Models.Page.Page", b =>
                {
                    b.Property<Guid>("PageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid?>("CreatedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("ModifiedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid?>("ModifiedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("PageType")
                        .HasColumnType("int");

                    b.Property<Guid?>("PartnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ProgramId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PageId");

                    b.HasIndex("CreatedById");

                    b.HasIndex("ModifiedById");

                    b.HasIndex("PartnerId")
                        .IsUnique()
                        .HasFilter("[PartnerId] IS NOT NULL");

                    b.HasIndex("ProgramId")
                        .IsUnique()
                        .HasFilter("[ProgramId] IS NOT NULL");

                    b.ToTable("Page");
                });

            modelBuilder.Entity("GAID.Domain.Models.Partner.Partner", b =>
                {
                    b.Property<Guid>("PartnerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid?>("CreatedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("ModifiedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid?>("ModifiedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PartnerThumbnailId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PartnerId");

                    b.HasIndex("CreatedById");

                    b.HasIndex("ModifiedById");

                    b.HasIndex("PartnerThumbnailId")
                        .IsUnique();

                    b.ToTable("Partners");
                });

            modelBuilder.Entity("GAID.Domain.Models.Program.Program", b =>
                {
                    b.Property<Guid>("ProgramId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid?>("CreatedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DonationInfo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DonationReason")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("date");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("ModifiedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid?>("ModifiedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PartnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProgramThumbnailId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Target")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ProgramId");

                    b.HasIndex("CreatedById");

                    b.HasIndex("ModifiedById");

                    b.HasIndex("PartnerId");

                    b.HasIndex("ProgramThumbnailId")
                        .IsUnique();

                    b.ToTable("Programs");
                });

            modelBuilder.Entity("GAID.Domain.Models.Subscription.Subscription", b =>
                {
                    b.Property<Guid>("SubscriptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid?>("CreatedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("ModifiedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid?>("ModifiedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PartnerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("SubscriptionId");

                    b.HasIndex("CreatedById");

                    b.HasIndex("ModifiedById");

                    b.HasIndex("PartnerId");

                    b.ToTable("Subscription");
                });

            modelBuilder.Entity("GAID.Domain.Models.User.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("GAID.Domain.Models.User.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentInformation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ProfilePictureId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("GAID.Domain.Models.Attachment.Attachment", b =>
                {
                    b.HasOne("GAID.Domain.Models.User.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("GAID.Domain.Models.User.User", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedById");

                    b.Navigation("CreatedBy");

                    b.Navigation("ModifiedBy");
                });

            modelBuilder.Entity("GAID.Domain.Models.Donation.Donation", b =>
                {
                    b.HasOne("GAID.Domain.Models.User.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("GAID.Domain.Models.Enrollment.Enrollment", "Enrollment")
                        .WithMany("Donations")
                        .HasForeignKey("EnrollmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GAID.Domain.Models.User.User", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedById");

                    b.Navigation("CreatedBy");

                    b.Navigation("Enrollment");

                    b.Navigation("ModifiedBy");
                });

            modelBuilder.Entity("GAID.Domain.Models.Enrollment.Enrollment", b =>
                {
                    b.HasOne("GAID.Domain.Models.User.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("GAID.Domain.Models.User.User", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedById");

                    b.HasOne("GAID.Domain.Models.Program.Program", "Program")
                        .WithMany("Enrollments")
                        .HasForeignKey("ProgramId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedBy");

                    b.Navigation("ModifiedBy");

                    b.Navigation("Program");
                });

            modelBuilder.Entity("GAID.Domain.Models.Page.Page", b =>
                {
                    b.HasOne("GAID.Domain.Models.User.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("GAID.Domain.Models.User.User", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedById");

                    b.HasOne("GAID.Domain.Models.Partner.Partner", null)
                        .WithOne("Page")
                        .HasForeignKey("GAID.Domain.Models.Page.Page", "PartnerId");

                    b.HasOne("GAID.Domain.Models.Program.Program", null)
                        .WithOne("Page")
                        .HasForeignKey("GAID.Domain.Models.Page.Page", "ProgramId");

                    b.Navigation("CreatedBy");

                    b.Navigation("ModifiedBy");
                });

            modelBuilder.Entity("GAID.Domain.Models.Partner.Partner", b =>
                {
                    b.HasOne("GAID.Domain.Models.User.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("GAID.Domain.Models.User.User", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedById");

                    b.HasOne("GAID.Domain.Models.Attachment.Attachment", "PartnerThumbnail")
                        .WithOne()
                        .HasForeignKey("GAID.Domain.Models.Partner.Partner", "PartnerThumbnailId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("CreatedBy");

                    b.Navigation("ModifiedBy");

                    b.Navigation("PartnerThumbnail");
                });

            modelBuilder.Entity("GAID.Domain.Models.Program.Program", b =>
                {
                    b.HasOne("GAID.Domain.Models.User.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("GAID.Domain.Models.User.User", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedById");

                    b.HasOne("GAID.Domain.Models.Partner.Partner", "Partner")
                        .WithMany("Programs")
                        .HasForeignKey("PartnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GAID.Domain.Models.Attachment.Attachment", "ProgramThumbnail")
                        .WithOne()
                        .HasForeignKey("GAID.Domain.Models.Program.Program", "ProgramThumbnailId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("CreatedBy");

                    b.Navigation("ModifiedBy");

                    b.Navigation("Partner");

                    b.Navigation("ProgramThumbnail");
                });

            modelBuilder.Entity("GAID.Domain.Models.Subscription.Subscription", b =>
                {
                    b.HasOne("GAID.Domain.Models.User.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("GAID.Domain.Models.User.User", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedById");

                    b.HasOne("GAID.Domain.Models.Partner.Partner", "Partner")
                        .WithMany("Subscriptions")
                        .HasForeignKey("PartnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedBy");

                    b.Navigation("ModifiedBy");

                    b.Navigation("Partner");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("GAID.Domain.Models.User.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("GAID.Domain.Models.User.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("GAID.Domain.Models.User.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("GAID.Domain.Models.User.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GAID.Domain.Models.User.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("GAID.Domain.Models.User.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GAID.Domain.Models.Enrollment.Enrollment", b =>
                {
                    b.Navigation("Donations");
                });

            modelBuilder.Entity("GAID.Domain.Models.Partner.Partner", b =>
                {
                    b.Navigation("Page")
                        .IsRequired();

                    b.Navigation("Programs");

                    b.Navigation("Subscriptions");
                });

            modelBuilder.Entity("GAID.Domain.Models.Program.Program", b =>
                {
                    b.Navigation("Enrollments");

                    b.Navigation("Page")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
