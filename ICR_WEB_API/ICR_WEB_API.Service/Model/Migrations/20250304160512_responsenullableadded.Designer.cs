﻿// <auto-generated />
using System;
using ICR_WEB_API.Service.BLL.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ICR_WEB_API.Service.Model.Migrations
{
    [DbContext(typeof(ICRSurveyDBContext))]
    [Migration("20250304160512_responsenullableadded")]
    partial class responsenullableadded
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ICR_WEB_API.Service.Entity.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.Property<int?>("RatingItemId")
                        .HasColumnType("int");

                    b.Property<int?>("RatingValue")
                        .HasColumnType("int");

                    b.Property<int>("ResponseId")
                        .HasColumnType("int");

                    b.Property<int?>("SelectedOptionId")
                        .HasColumnType("int");

                    b.Property<string>("TextResponse")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.HasIndex("RatingItemId");

                    b.HasIndex("ResponseId");

                    b.HasIndex("SelectedOptionId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("ICR_WEB_API.Service.Entity.Option", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("OptionText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Options");
                });

            modelBuilder.Entity("ICR_WEB_API.Service.Entity.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsShowable")
                        .HasColumnType("bit");

                    b.Property<float>("SortOrder")
                        .HasColumnType("real");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("ICR_WEB_API.Service.Entity.RatingScaleItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ItemText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("RatingScaleItems");
                });

            modelBuilder.Entity("ICR_WEB_API.Service.Entity.Response", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AIESECActivity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DistrictName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsSubmited")
                        .HasColumnType("bit");

                    b.Property<string>("LicenseIssueDateLabel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Municipality")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerIDNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShopName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SubmissionDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UnifiedLicenseNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("imageBase64")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Responses");
                });

            modelBuilder.Entity("ICR_WEB_API.Service.Entity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ICR_WEB_API.Service.Entity.Answer", b =>
                {
                    b.HasOne("ICR_WEB_API.Service.Entity.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ICR_WEB_API.Service.Entity.RatingScaleItem", "RatingItem")
                        .WithMany()
                        .HasForeignKey("RatingItemId");

                    b.HasOne("ICR_WEB_API.Service.Entity.Response", "Response")
                        .WithMany("Answers")
                        .HasForeignKey("ResponseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ICR_WEB_API.Service.Entity.Option", "SelectedOption")
                        .WithMany()
                        .HasForeignKey("SelectedOptionId");

                    b.Navigation("Question");

                    b.Navigation("RatingItem");

                    b.Navigation("Response");

                    b.Navigation("SelectedOption");
                });

            modelBuilder.Entity("ICR_WEB_API.Service.Entity.Option", b =>
                {
                    b.HasOne("ICR_WEB_API.Service.Entity.Question", "Question")
                        .WithMany("Options")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("ICR_WEB_API.Service.Entity.RatingScaleItem", b =>
                {
                    b.HasOne("ICR_WEB_API.Service.Entity.Question", "Question")
                        .WithMany("RatingScaleItems")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("ICR_WEB_API.Service.Entity.Response", b =>
                {
                    b.HasOne("ICR_WEB_API.Service.Entity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ICR_WEB_API.Service.Entity.Question", b =>
                {
                    b.Navigation("Options");

                    b.Navigation("RatingScaleItems");
                });

            modelBuilder.Entity("ICR_WEB_API.Service.Entity.Response", b =>
                {
                    b.Navigation("Answers");
                });
#pragma warning restore 612, 618
        }
    }
}
