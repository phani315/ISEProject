﻿// <auto-generated />

using Kanini.InvestmentSearchEngine.SWOTAnalysis.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;


#nullable disable

namespace Kanini.InvestmentSearchEngine.SWOTAnalysis.Migrations
{
    [DbContext(typeof(SWOTContext))]
    partial class SWOTContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Kanini.InvestmentSearchEngine.SWOTAnalysis.Models.Oppurtunity", b =>
                {
                    b.Property<int>("OppurtunityId")
                        .HasColumnType("int");

                    b.Property<string>("OppurtunityDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OppurtunityValue")
                        .HasColumnType("int");

                    b.HasKey("OppurtunityId");

                    b.ToTable("Oppurtunities");
                });

            modelBuilder.Entity("Kanini.InvestmentSearchEngine.SWOTAnalysis.Models.Strength", b =>
                {
                    b.Property<int>("StrengthId")
                        .HasColumnType("int");

                    b.Property<string>("StrengthDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StrengthValue")
                        .HasColumnType("int");

                    b.HasKey("StrengthId");

                    b.ToTable("Strengths");
                });

            modelBuilder.Entity("Kanini.InvestmentSearchEngine.SWOTAnalysis.Models.SWOT", b =>
                {
                    b.Property<int>("SwotId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SwotId"), 1L, 1);

                    b.Property<int>("CompanyID")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.HasKey("SwotId");

                    b.ToTable("SWOT");
                });

            modelBuilder.Entity("Kanini.InvestmentSearchEngine.SWOTAnalysis.Models.Threat", b =>
                {
                    b.Property<int>("ThreatId")
                        .HasColumnType("int");

                    b.Property<string>("ThreatDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ThreatValue")
                        .HasColumnType("int");

                    b.HasKey("ThreatId");

                    b.ToTable("Threats");
                });

            modelBuilder.Entity("Kanini.InvestmentSearchEngine.SWOTAnalysis.Models.Weakness", b =>
                {
                    b.Property<int>("WeaknessId")
                        .HasColumnType("int");

                    b.Property<string>("WeaknessDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WeaknessValue")
                        .HasColumnType("int");

                    b.HasKey("WeaknessId");

                    b.ToTable("Weaknesses");
                });

            modelBuilder.Entity("Kanini.InvestmentSearchEngine.SWOTAnalysis.Models.Oppurtunity", b =>
                {
                    b.HasOne("Kanini.InvestmentSearchEngine.SWOTAnalysis.Models.SWOT", "SWOT")
                        .WithOne("oppurtunity")
                        .HasForeignKey("Kanini.InvestmentSearchEngine.SWOTAnalysis.Models.Oppurtunity", "OppurtunityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SWOT");
                });

            modelBuilder.Entity("Kanini.InvestmentSearchEngine.SWOTAnalysis.Models.Strength", b =>
                {
                    b.HasOne("Kanini.InvestmentSearchEngine.SWOTAnalysis.Models.SWOT", "SWOT")
                        .WithOne("strength")
                        .HasForeignKey("Kanini.InvestmentSearchEngine.SWOTAnalysis.Models.Strength", "StrengthId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SWOT");
                });

            modelBuilder.Entity("Kanini.InvestmentSearchEngine.SWOTAnalysis.Models.Threat", b =>
                {
                    b.HasOne("Kanini.InvestmentSearchEngine.SWOTAnalysis.Models.SWOT", "SWOT")
                        .WithOne("threat")
                        .HasForeignKey("Kanini.InvestmentSearchEngine.SWOTAnalysis.Models.Threat", "ThreatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SWOT");
                });

            modelBuilder.Entity("Kanini.InvestmentSearchEngine.SWOTAnalysis.Models.Weakness", b =>
                {
                    b.HasOne("Kanini.InvestmentSearchEngine.SWOTAnalysis.Models.SWOT", "SWOT")
                        .WithOne("weakness")
                        .HasForeignKey("Kanini.InvestmentSearchEngine.SWOTAnalysis.Models.Weakness", "WeaknessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SWOT");
                });

            modelBuilder.Entity("Kanini.InvestmentSearchEngine.SWOTAnalysis.Models.SWOT", b =>
                {
                    b.Navigation("oppurtunity");

                    b.Navigation("strength");

                    b.Navigation("threat");

                    b.Navigation("weakness");
                });
#pragma warning restore 612, 618
        }
    }
}