using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Saitynai_REST_L01.Data;

namespace Saitynai_REST_L01.Migrations
{
    [DbContext(typeof(PlayerContext))]
    partial class PlayerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Saitynai_REST_L01.Models.Player", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("first_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasMaxLength(250);

                    b.Property<string>("last_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("position")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int>("command_id")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("Player");
                });
#pragma warning restore 612, 618
        }
    }
}