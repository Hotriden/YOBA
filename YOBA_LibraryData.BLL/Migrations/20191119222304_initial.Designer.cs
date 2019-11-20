﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using YOBA_LibraryData.BLL;

namespace YOBA_LibraryData.BLL.Migrations
{
    [DbContext(typeof(YOBAContext))]
    [Migration("20191119222304_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("YOBA_LibraryData.BLL.Entities.Finance.Expence", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Expences");
                });

            modelBuilder.Entity("YOBA_LibraryData.BLL.Entities.Finance.Income", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Incomes");
                });

            modelBuilder.Entity("YOBA_LibraryData.BLL.Entities.Finance.Tax", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<double>("Percent");

                    b.HasKey("Id");

                    b.ToTable("Taxes");
                });

            modelBuilder.Entity("YOBA_LibraryData.BLL.Entities.Products.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ProductName")
                        .IsRequired();

                    b.Property<int?>("WareHouseId");

                    b.HasKey("ProductId");

                    b.HasIndex("WareHouseId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("YOBA_LibraryData.BLL.Entities.Sell.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("CustomerEmail")
                        .IsRequired();

                    b.Property<string>("CustomerLastName")
                        .IsRequired();

                    b.Property<string>("CustomerName")
                        .IsRequired();

                    b.Property<string>("TelephoneNumber");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("YOBA_LibraryData.BLL.Entities.Sell.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CustomerId");

                    b.Property<int>("ManagerEmployeeId");

                    b.Property<decimal>("OrderSum")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("OrderTime");

                    b.Property<bool>("Paid");

                    b.Property<int>("ProductId");

                    b.Property<bool>("Shipped");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ManagerEmployeeId");

                    b.HasIndex("ProductId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("YOBA_LibraryData.BLL.Entities.Sell.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CusmoterCustomerId");

                    b.Property<int>("IdentialPayNumber");

                    b.Property<int?>("OrderId");

                    b.Property<DateTime>("PayTime");

                    b.Property<int?>("SupplierId");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CusmoterCustomerId");

                    b.HasIndex("OrderId");

                    b.HasIndex("SupplierId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("YOBA_LibraryData.BLL.Entities.Staff.Branch", b =>
                {
                    b.Property<int>("BranchId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BranchName")
                        .IsRequired();

                    b.HasKey("BranchId");

                    b.ToTable("Branches");
                });

            modelBuilder.Entity("YOBA_LibraryData.BLL.Entities.Staff.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BranchId");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("PositionId");

                    b.Property<decimal>("Sallery")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("TelephoneNumber")
                        .IsRequired();

                    b.HasKey("EmployeeId");

                    b.HasIndex("BranchId");

                    b.HasIndex("PositionId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("YOBA_LibraryData.BLL.Entities.Staff.Position", b =>
                {
                    b.Property<int>("PositionId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PositionName")
                        .IsRequired();

                    b.HasKey("PositionId");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("YOBA_LibraryData.BLL.Entities.Supply.Entrance", b =>
                {
                    b.Property<int>("EntranceId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("OrderSum")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("OrderTime");

                    b.Property<bool>("Paid");

                    b.Property<int>("ProductId");

                    b.Property<bool>("Shipped");

                    b.Property<int?>("SupplierId");

                    b.Property<int>("WareHouseId");

                    b.HasKey("EntranceId");

                    b.HasIndex("ProductId");

                    b.HasIndex("SupplierId");

                    b.HasIndex("WareHouseId");

                    b.ToTable("Entrance");
                });

            modelBuilder.Entity("YOBA_LibraryData.BLL.Entities.Supply.Supplier", b =>
                {
                    b.Property<int>("SupplierId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired();

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("SupplierName")
                        .IsRequired();

                    b.Property<string>("TelephoneNumber");

                    b.HasKey("SupplierId");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("YOBA_LibraryData.BLL.Entities.Supply.WareHouse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired();

                    b.Property<int?>("StockManEmployeeId");

                    b.Property<string>("WareHouseName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("StockManEmployeeId");

                    b.ToTable("WareHouse");
                });

            modelBuilder.Entity("YOBA_LibraryData.BLL.Entities.User.Client", b =>
                {
                    b.Property<int>("ClientId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CompanyName");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Login")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.HasKey("ClientId");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("YOBA_LibraryData.BLL.Entities.Products.Product", b =>
                {
                    b.HasOne("YOBA_LibraryData.BLL.Entities.Supply.WareHouse")
                        .WithMany("Products")
                        .HasForeignKey("WareHouseId");
                });

            modelBuilder.Entity("YOBA_LibraryData.BLL.Entities.Sell.Order", b =>
                {
                    b.HasOne("YOBA_LibraryData.BLL.Entities.Sell.Customer", "Customer")
                        .WithMany("CustomerOrders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("YOBA_LibraryData.BLL.Entities.Staff.Employee", "Manager")
                        .WithMany()
                        .HasForeignKey("ManagerEmployeeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("YOBA_LibraryData.BLL.Entities.Products.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("YOBA_LibraryData.BLL.Entities.Sell.Payment", b =>
                {
                    b.HasOne("YOBA_LibraryData.BLL.Entities.Sell.Customer", "Cusmoter")
                        .WithMany("Payments")
                        .HasForeignKey("CusmoterCustomerId");

                    b.HasOne("YOBA_LibraryData.BLL.Entities.Sell.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId");

                    b.HasOne("YOBA_LibraryData.BLL.Entities.Supply.Supplier")
                        .WithMany("Payments")
                        .HasForeignKey("SupplierId");
                });

            modelBuilder.Entity("YOBA_LibraryData.BLL.Entities.Staff.Employee", b =>
                {
                    b.HasOne("YOBA_LibraryData.BLL.Entities.Staff.Branch", "Branch")
                        .WithMany("Employees")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("YOBA_LibraryData.BLL.Entities.Staff.Position", "Position")
                        .WithMany()
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("YOBA_LibraryData.BLL.Entities.Supply.Entrance", b =>
                {
                    b.HasOne("YOBA_LibraryData.BLL.Entities.Products.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("YOBA_LibraryData.BLL.Entities.Supply.Supplier")
                        .WithMany("Entrances")
                        .HasForeignKey("SupplierId");

                    b.HasOne("YOBA_LibraryData.BLL.Entities.Supply.WareHouse", "WareHouse")
                        .WithMany()
                        .HasForeignKey("WareHouseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("YOBA_LibraryData.BLL.Entities.Supply.WareHouse", b =>
                {
                    b.HasOne("YOBA_LibraryData.BLL.Entities.Staff.Employee", "StockMan")
                        .WithMany()
                        .HasForeignKey("StockManEmployeeId");
                });
#pragma warning restore 612, 618
        }
    }
}
