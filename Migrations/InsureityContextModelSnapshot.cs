﻿// <auto-generated />
using System;
using InsureityAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InsureityAPI.Migrations
{
    [DbContext(typeof(InsureityContext))]
    partial class InsureityContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("InsureityAPI.Models.Business", b =>
                {
                    b.Property<int>("BusinessId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BusinessId"));

                    b.Property<int?>("BMId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("BusinessLocation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BusinessName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("BusinessScore")
                        .HasColumnType("int");

                    b.Property<double>("BusinessTurnover")
                        .HasColumnType("float");

                    b.Property<string>("BusinessType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("CapitalInvested")
                        .HasColumnType("float");

                    b.Property<int?>("ConsumerId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<double?>("ROI")
                        .HasColumnType("float");

                    b.Property<int>("TotalEmployees")
                        .HasColumnType("int");

                    b.HasKey("BusinessId");

                    b.HasIndex("BMId");

                    b.HasIndex("BusinessName")
                        .IsUnique();

                    b.HasIndex("ConsumerId");

                    b.ToTable("Businesses");
                });

            modelBuilder.Entity("InsureityAPI.Models.BusinessMaster", b =>
                {
                    b.Property<int>("BMId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BMId"));

                    b.Property<string>("BusinessType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BMId");

                    b.ToTable("BusinessMaster");
                });

            modelBuilder.Entity("InsureityAPI.Models.Consumer", b =>
                {
                    b.Property<int>("ConsumerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ConsumerId"));

                    b.Property<string>("ConsumerEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConsumerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DOB")
                        .HasColumnType("datetime2");

                    b.Property<string>("PAN")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ConsumerId");

                    b.HasIndex("ConsumerEmail")
                        .IsUnique();

                    b.HasIndex("PAN")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("Consumers");
                });

            modelBuilder.Entity("InsureityAPI.Models.LoginDetails", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("UserSalt")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("UserId");

                    b.HasIndex("UserEmail")
                        .IsUnique();

                    b.ToTable("LoginDetailsList");
                });

            modelBuilder.Entity("InsureityAPI.Models.Policy", b =>
                {
                    b.Property<int>("PolicyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PolicyId"));

                    b.Property<int?>("AgentId")
                        .HasColumnType("int");

                    b.Property<double>("AssuredSum")
                        .HasColumnType("float");

                    b.Property<DateTime?>("CreatedAt")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("IssuedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("PlId")
                        .HasColumnType("int");

                    b.Property<string>("PolicyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PolicyStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PremiumAmount")
                        .HasColumnType("float");

                    b.Property<double>("PremiumRate")
                        .HasColumnType("float");

                    b.Property<int>("PropertyId")
                        .HasColumnType("int");

                    b.Property<int?>("QuoteId")
                        .HasColumnType("int");

                    b.HasKey("PolicyId");

                    b.HasIndex("AgentId");

                    b.HasIndex("PlId");

                    b.HasIndex("PropertyId");

                    b.HasIndex("QuoteId");

                    b.ToTable("Policies");
                });

            modelBuilder.Entity("InsureityAPI.Models.PolicyMaster", b =>
                {
                    b.Property<int>("PlId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PlId"));

                    b.Property<double>("AssuredSum")
                        .HasColumnType("float");

                    b.Property<double>("BasePremium")
                        .HasColumnType("float");

                    b.Property<double>("BusinessPenalty")
                        .HasColumnType("float");

                    b.Property<string>("PolicyType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PropertyPenalty")
                        .HasColumnType("float");

                    b.Property<string>("PropertyType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PlId");

                    b.ToTable("PoliciesMaster");
                });

            modelBuilder.Entity("InsureityAPI.Models.Property", b =>
                {
                    b.Property<int>("PropertyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PropertyId"));

                    b.Property<double>("AssetCost")
                        .HasColumnType("float");

                    b.Property<int>("BusinessID")
                        .HasColumnType("int");

                    b.Property<double?>("DepreciationExpense")
                        .HasColumnType("float");

                    b.Property<bool>("IsInsured")
                        .HasColumnType("bit");

                    b.Property<string>("OwnershipType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PMId")
                        .HasColumnType("int");

                    b.Property<int>("PropertyAge")
                        .HasColumnType("int");

                    b.Property<string>("PropertyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PropertyScore")
                        .HasColumnType("int");

                    b.Property<double>("SalvageValue")
                        .HasColumnType("float");

                    b.Property<int>("UsefulLife")
                        .HasColumnType("int");

                    b.HasKey("PropertyId");

                    b.HasIndex("BusinessID");

                    b.HasIndex("PMId");

                    b.ToTable("Properties");
                });

            modelBuilder.Entity("InsureityAPI.Models.PropertyMaster", b =>
                {
                    b.Property<int>("PMId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PMId"));

                    b.Property<string>("PropertyType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PMId");

                    b.ToTable("PropertyMaster");
                });

            modelBuilder.Entity("InsureityAPI.Models.Quote", b =>
                {
                    b.Property<int>("QuoteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("QuoteId"));

                    b.Property<int>("BusinessID")
                        .HasColumnType("int");

                    b.Property<int?>("ConsumerId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<double>("QuoteAmount")
                        .HasColumnType("float");

                    b.Property<int>("Tenure")
                        .HasColumnType("int");

                    b.HasKey("QuoteId");

                    b.HasIndex("BusinessID");

                    b.ToTable("Quotes");
                });

            modelBuilder.Entity("InsureityAPI.Models.Business", b =>
                {
                    b.HasOne("InsureityAPI.Models.BusinessMaster", "BusinessMaster")
                        .WithMany()
                        .HasForeignKey("BMId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InsureityAPI.Models.Consumer", "Consumer")
                        .WithMany()
                        .HasForeignKey("ConsumerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BusinessMaster");

                    b.Navigation("Consumer");
                });

            modelBuilder.Entity("InsureityAPI.Models.Consumer", b =>
                {
                    b.HasOne("InsureityAPI.Models.LoginDetails", "Agent")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Agent");
                });

            modelBuilder.Entity("InsureityAPI.Models.Policy", b =>
                {
                    b.HasOne("InsureityAPI.Models.LoginDetails", null)
                        .WithMany("Policies")
                        .HasForeignKey("AgentId");

                    b.HasOne("InsureityAPI.Models.PolicyMaster", "PolicyMaster")
                        .WithMany()
                        .HasForeignKey("PlId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InsureityAPI.Models.Property", "Property")
                        .WithMany()
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InsureityAPI.Models.Quote", null)
                        .WithMany("Policies")
                        .HasForeignKey("QuoteId");

                    b.Navigation("PolicyMaster");

                    b.Navigation("Property");
                });

            modelBuilder.Entity("InsureityAPI.Models.Property", b =>
                {
                    b.HasOne("InsureityAPI.Models.Business", "Business")
                        .WithMany("Properties")
                        .HasForeignKey("BusinessID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InsureityAPI.Models.PropertyMaster", "PropertyMaster")
                        .WithMany()
                        .HasForeignKey("PMId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Business");

                    b.Navigation("PropertyMaster");
                });

            modelBuilder.Entity("InsureityAPI.Models.Quote", b =>
                {
                    b.HasOne("InsureityAPI.Models.Business", "Business")
                        .WithMany()
                        .HasForeignKey("BusinessID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Business");
                });

            modelBuilder.Entity("InsureityAPI.Models.Business", b =>
                {
                    b.Navigation("Properties");
                });

            modelBuilder.Entity("InsureityAPI.Models.LoginDetails", b =>
                {
                    b.Navigation("Policies");
                });

            modelBuilder.Entity("InsureityAPI.Models.Quote", b =>
                {
                    b.Navigation("Policies");
                });
#pragma warning restore 612, 618
        }
    }
}
