﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PracticaWeb.Data;

#nullable disable

namespace PracticaWeb.Migrations
{
    [DbContext(typeof(PracticaWebContext))]
    [Migration("20240719034013_Base")]
    partial class Base
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PracticaWeb.Models.Productos", b =>
                {
                    b.Property<int>("ID_Producto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID_Producto"));

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Clave")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaveProveedor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PrecioProveedor")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("TipoProducto")
                        .HasColumnType("int");

                    b.HasKey("ID_Producto");

                    b.ToTable("Productos");
                });
#pragma warning restore 612, 618
        }
    }
}
