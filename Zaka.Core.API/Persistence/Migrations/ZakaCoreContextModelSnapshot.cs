﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Zaka.Core.API.Persistence;

namespace Zaka.Core.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ZakaCoreContext))]
    partial class ZakaCoreContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Zaka.Core.API.Domain.Entities.Account", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<Guid>("AccountGuid")
                        .HasColumnType("uuid");

                    b.Property<int>("AccountTypeId")
                        .HasColumnType("integer");

                    b.Property<bool>("AccountVerified")
                        .HasColumnType("boolean");

                    b.Property<string>("CellphoneNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("SecretKey")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("AccountId");

                    b.HasIndex("AccountTypeId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Zaka.Core.API.Domain.Entities.AccountType", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Type")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("AccountId");

                    b.ToTable("AccountTypes");

                    b.HasData(
                        new
                        {
                            AccountId = 1,
                            Type = "Individual"
                        },
                        new
                        {
                            AccountId = 2,
                            Type = "Business"
                        });
                });

            modelBuilder.Entity("Zaka.Core.API.Domain.Entities.Transaction", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<string>("FromPhone")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("FromWallet")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("ToPhone")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("ToWallet")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<Guid>("TransactionReference")
                        .HasColumnType("uuid");

                    b.Property<string>("TransactionStatus")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("TransactionTypeCode")
                        .HasColumnType("text");

                    b.Property<string>("VoucherNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.HasKey("TransactionId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Zaka.Core.API.Domain.Entities.Wallet", b =>
                {
                    b.Property<int>("WalletId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AccountId")
                        .HasColumnType("integer");

                    b.Property<decimal>("AvailableMoney")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("TopUpDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<decimal>("TopUpMoney")
                        .HasColumnType("numeric");

                    b.Property<string>("WalletNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.HasKey("WalletId");

                    b.HasIndex("AccountId");

                    b.ToTable("Wallets");
                });

            modelBuilder.Entity("Zaka.Core.API.Domain.Entities.Account", b =>
                {
                    b.HasOne("Zaka.Core.API.Domain.Entities.AccountType", "AccountType")
                        .WithMany()
                        .HasForeignKey("AccountTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AccountType");
                });

            modelBuilder.Entity("Zaka.Core.API.Domain.Entities.Wallet", b =>
                {
                    b.HasOne("Zaka.Core.API.Domain.Entities.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });
#pragma warning restore 612, 618
        }
    }
}