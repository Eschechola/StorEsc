﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StorEsc.Infrastructure.Context;

#nullable disable

namespace StorEsc.Infrastructure.Migrations
{
    [DbContext(typeof(StorEscContext))]
    [Migration("20220908233841_AddPaymentHashToRechargeTable")]
    partial class AddPaymentHashToRechargeTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("StorEsc.Domain.Entities.Customer", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("VARCHAR(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("DATETIME");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("VARCHAR(200)")
                        .HasColumnName("Email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("FirstName");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("LastName");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("VARCHAR(120)")
                        .HasColumnName("Password");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("DATETIME");

                    b.Property<string>("WalletId")
                        .IsRequired()
                        .HasColumnType("VARCHAR(36)")
                        .HasColumnName("WalletId");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("WalletId");

                    b.ToTable("Customer", "ste");
                });

            modelBuilder.Entity("StorEsc.Domain.Entities.Order", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("VARCHAR(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("DATETIME");

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasColumnType("VARCHAR(36)")
                        .HasColumnName("CustomerId");

                    b.Property<bool>("IsPaid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("BIT")
                        .HasDefaultValue(false)
                        .HasColumnName("IsPaid");

                    b.Property<decimal>("TotalValue")
                        .HasColumnType("DECIMAL(19,4)")
                        .HasColumnName("TotalValue");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("DATETIME");

                    b.Property<string>("VoucherId")
                        .IsRequired()
                        .HasColumnType("VARCHAR(36)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("VoucherId");

                    b.ToTable("Order", "ste");
                });

            modelBuilder.Entity("StorEsc.Domain.Entities.OrderItem", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("VARCHAR(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("DATETIME");

                    b.Property<int>("ItemCount")
                        .HasColumnType("INT")
                        .HasColumnName("Count");

                    b.Property<string>("OrderId")
                        .IsRequired()
                        .HasColumnType("VARCHAR(36)")
                        .HasColumnName("OrderId");

                    b.Property<string>("ProductId")
                        .IsRequired()
                        .HasColumnType("VARCHAR(36)")
                        .HasColumnName("ProductId");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("DATETIME");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderItem", "ste");
                });

            modelBuilder.Entity("StorEsc.Domain.Entities.Product", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("VARCHAR(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("DATETIME");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("VARCHAR(2000)")
                        .HasColumnName("Description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(200)")
                        .HasColumnName("Name");

                    b.Property<decimal>("Price")
                        .HasColumnType("DECIMAL(14,9)")
                        .HasColumnName("Price");

                    b.Property<string>("SellerId")
                        .IsRequired()
                        .HasColumnType("VARCHAR(36)")
                        .HasColumnName("SellerId");

                    b.Property<int>("Stock")
                        .HasColumnType("INT")
                        .HasColumnName("Stock");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("DATETIME");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("SellerId");

                    b.ToTable("Product", "ste");
                });

            modelBuilder.Entity("StorEsc.Domain.Entities.Recharge", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("VARCHAR(36)");

                    b.Property<decimal>("Amount")
                        .HasColumnType("DECIMAL(14,9)")
                        .HasColumnName("Amount");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("DATETIME");

                    b.Property<string>("PaymentHash")
                        .IsRequired()
                        .HasColumnType("VARCHAR(36)")
                        .HasColumnName("PaymentHash");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("DATETIME");

                    b.Property<string>("WalletId")
                        .IsRequired()
                        .HasColumnType("VARCHAR(36)")
                        .HasColumnName("WalletId");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("WalletId");

                    b.ToTable("Recharge", "ste");
                });

            modelBuilder.Entity("StorEsc.Domain.Entities.Seller", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("VARCHAR(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("DATETIME");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("VARCHAR(200)")
                        .HasColumnName("Email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("FirstName");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("LastName");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("VARCHAR(120)")
                        .HasColumnName("Password");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("DATETIME");

                    b.Property<string>("WalletId")
                        .IsRequired()
                        .HasColumnType("VARCHAR(36)")
                        .HasColumnName("WalletId");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("WalletId");

                    b.ToTable("Seller", "ste");
                });

            modelBuilder.Entity("StorEsc.Domain.Entities.Voucher", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("VARCHAR(36)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("VARCHAR(80)")
                        .HasColumnName("Code");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("DATETIME");

                    b.Property<bool>("IsPercentageDiscount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("BIT")
                        .HasDefaultValue(false)
                        .HasColumnName("IsPercentageDiscount");

                    b.Property<decimal?>("PercentageDiscount")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("DECIMAL(14,9)")
                        .HasColumnName("ValueDiscount");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("DATETIME");

                    b.Property<decimal?>("ValueDiscount")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("DECIMAL(14,9)")
                        .HasColumnName("ValueDiscount");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Voucher", "ste");
                });

            modelBuilder.Entity("StorEsc.Domain.Entities.Wallet", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("VARCHAR(36)");

                    b.Property<decimal>("Amount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DECIMAL(14,9)")
                        .HasDefaultValue(0m)
                        .HasColumnName("Amount");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("DATETIME");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("DATETIME");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Wallet", "ste");
                });

            modelBuilder.Entity("StorEsc.Domain.Entities.Customer", b =>
                {
                    b.HasOne("StorEsc.Domain.Entities.Wallet", "Wallet")
                        .WithMany("Customers")
                        .HasForeignKey("WalletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Wallet");
                });

            modelBuilder.Entity("StorEsc.Domain.Entities.Order", b =>
                {
                    b.HasOne("StorEsc.Domain.Entities.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StorEsc.Domain.Entities.Voucher", "Voucher")
                        .WithMany("Orders")
                        .HasForeignKey("VoucherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Voucher");
                });

            modelBuilder.Entity("StorEsc.Domain.Entities.OrderItem", b =>
                {
                    b.HasOne("StorEsc.Domain.Entities.Order", "Order")
                        .WithMany("OrderItens")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StorEsc.Domain.Entities.Product", "Product")
                        .WithMany("OrderItens")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("StorEsc.Domain.Entities.Product", b =>
                {
                    b.HasOne("StorEsc.Domain.Entities.Seller", "Seller")
                        .WithMany("Products")
                        .HasForeignKey("SellerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("StorEsc.Domain.Entities.Recharge", b =>
                {
                    b.HasOne("StorEsc.Domain.Entities.Wallet", "Wallet")
                        .WithMany("Recharges")
                        .HasForeignKey("WalletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Wallet");
                });

            modelBuilder.Entity("StorEsc.Domain.Entities.Seller", b =>
                {
                    b.HasOne("StorEsc.Domain.Entities.Wallet", "Wallet")
                        .WithMany("Sellers")
                        .HasForeignKey("WalletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Wallet");
                });

            modelBuilder.Entity("StorEsc.Domain.Entities.Customer", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("StorEsc.Domain.Entities.Order", b =>
                {
                    b.Navigation("OrderItens");
                });

            modelBuilder.Entity("StorEsc.Domain.Entities.Product", b =>
                {
                    b.Navigation("OrderItens");
                });

            modelBuilder.Entity("StorEsc.Domain.Entities.Seller", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("StorEsc.Domain.Entities.Voucher", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("StorEsc.Domain.Entities.Wallet", b =>
                {
                    b.Navigation("Customers");

                    b.Navigation("Recharges");

                    b.Navigation("Sellers");
                });
#pragma warning restore 612, 618
        }
    }
}
