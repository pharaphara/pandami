﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pandami.Data;

namespace Pandami.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20210120090251_AjoutboolFeat")]
    partial class AjoutboolFeat
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("Pandami.Models.Adresse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("CodePostale")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomDeVoie")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumeroDeVoie")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ville")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("latitude")
                        .HasColumnType("real");

                    b.Property<float>("longitude")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Adresses");
                });

            modelBuilder.Entity("Pandami.Models.CategorieAide", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Categorie")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CategorieAides");
                });

            modelBuilder.Entity("Pandami.Models.Disponibilite", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("DebutHeure")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FinHeure")
                        .HasColumnType("datetime2");

                    b.Property<short?>("JourId")
                        .HasColumnType("smallint");

                    b.Property<int?>("MembreId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ValiditeDebutDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ValiditeFinDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("JourId");

                    b.HasIndex("MembreId");

                    b.ToTable("Disponibilites");
                });

            modelBuilder.Entity("Pandami.Models.Feat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime?>("AcceptationDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("AdresseId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("AnnulationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ClotureDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CreateurId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("EchangeMonetaire")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("EnCoursRealisation")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FinFeatHelper")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("HeureDebut")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("HeureFin")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RealisationDate")
                        .HasColumnType("datetime2");

                    b.Property<float?>("SommeAvancee")
                        .HasColumnType("real");

                    b.Property<DateTime?>("SommePrevoir")
                        .HasColumnType("datetime2");

                    b.Property<float?>("SommeRembourseeDate")
                        .HasColumnType("real");

                    b.Property<DateTime?>("SurPlace")
                        .HasColumnType("datetime2");

                    b.Property<int?>("TypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AdresseId");

                    b.HasIndex("CreateurId");

                    b.HasIndex("TypeId");

                    b.ToTable("Feats");
                });

            modelBuilder.Entity("Pandami.Models.JourDeLaSemaine", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .UseIdentityColumn();

                    b.Property<string>("NomDuJour")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("JourDeLaSemaines");
                });

            modelBuilder.Entity("Pandami.Models.Litige", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime?>("ClotureDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Commentaire")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("FeatId")
                        .HasColumnType("int");

                    b.Property<int?>("MembreId")
                        .HasColumnType("int");

                    b.Property<int?>("TypeLitigeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FeatId");

                    b.HasIndex("MembreId");

                    b.HasIndex("TypeLitigeId");

                    b.ToTable("Litiges");
                });

            modelBuilder.Entity("Pandami.Models.Materiel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("NomMateriel")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Materiels");
                });

            modelBuilder.Entity("Pandami.Models.Membre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("AdresseId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Inscription")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date");

                    b.Property<string>("Mdp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Naissance")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nom")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Prenom")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("SexeId")
                        .HasColumnType("int");

                    b.Property<string>("Telephone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AdresseId");

                    b.HasIndex("SexeId");

                    b.ToTable("Membres");
                });

            modelBuilder.Entity("Pandami.Models.PreferenceAide", b =>
                {
                    b.Property<int>("IdMembre")
                        .HasColumnType("int");

                    b.Property<int>("IdTypeAide")
                        .HasColumnType("int");

                    b.Property<DateTime>("ValiditeDebut")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ValiditeFin")
                        .HasColumnType("datetime2");

                    b.HasKey("IdMembre", "IdTypeAide");

                    b.HasIndex("IdTypeAide");

                    b.ToTable("PreferenceAides");
                });

            modelBuilder.Entity("Pandami.Models.Repondre", b =>
                {
                    b.Property<int>("IdMembre")
                        .HasColumnType("int");

                    b.Property<int>("IdFeat")
                        .HasColumnType("int");

                    b.Property<DateTime>("AcceptationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DesistementDate")
                        .HasColumnType("datetime2");

                    b.HasKey("IdMembre", "IdFeat");

                    b.HasIndex("IdFeat");

                    b.ToTable("Repondres");
                });

            modelBuilder.Entity("Pandami.Models.Sexe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("NomSexe")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Sexes");
                });

            modelBuilder.Entity("Pandami.Models.Suspension", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("DebutDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FinDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("MembreId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MembreId");

                    b.ToTable("Suspensions");
                });

            modelBuilder.Entity("Pandami.Models.TypeAide", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("CategorieAideId")
                        .HasColumnType("int");

                    b.Property<int?>("MaterielId")
                        .HasColumnType("int");

                    b.Property<string>("NomAide")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategorieAideId");

                    b.HasIndex("MaterielId");

                    b.ToTable("TypeAides");
                });

            modelBuilder.Entity("Pandami.Models.TypeLitige", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Libelle")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TypeLitiges");
                });

            modelBuilder.Entity("Pandami.Models.Disponibilite", b =>
                {
                    b.HasOne("Pandami.Models.JourDeLaSemaine", "Jour")
                        .WithMany()
                        .HasForeignKey("JourId");

                    b.HasOne("Pandami.Models.Membre", null)
                        .WithMany("Disponibilites")
                        .HasForeignKey("MembreId");

                    b.Navigation("Jour");
                });

            modelBuilder.Entity("Pandami.Models.Feat", b =>
                {
                    b.HasOne("Pandami.Models.Adresse", "Adresse")
                        .WithMany()
                        .HasForeignKey("AdresseId");

                    b.HasOne("Pandami.Models.Membre", "Createur")
                        .WithMany("Feats")
                        .HasForeignKey("CreateurId");

                    b.HasOne("Pandami.Models.TypeAide", "Type")
                        .WithMany("Feats")
                        .HasForeignKey("TypeId");

                    b.Navigation("Adresse");

                    b.Navigation("Createur");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("Pandami.Models.Litige", b =>
                {
                    b.HasOne("Pandami.Models.Feat", null)
                        .WithMany("Litiges")
                        .HasForeignKey("FeatId");

                    b.HasOne("Pandami.Models.Membre", null)
                        .WithMany("Litiges")
                        .HasForeignKey("MembreId");

                    b.HasOne("Pandami.Models.TypeLitige", "TypeLitige")
                        .WithMany()
                        .HasForeignKey("TypeLitigeId");

                    b.Navigation("TypeLitige");
                });

            modelBuilder.Entity("Pandami.Models.Membre", b =>
                {
                    b.HasOne("Pandami.Models.Adresse", "Adresse")
                        .WithMany()
                        .HasForeignKey("AdresseId");

                    b.HasOne("Pandami.Models.Sexe", "Sexe")
                        .WithMany()
                        .HasForeignKey("SexeId");

                    b.Navigation("Adresse");

                    b.Navigation("Sexe");
                });

            modelBuilder.Entity("Pandami.Models.PreferenceAide", b =>
                {
                    b.HasOne("Pandami.Models.Membre", "Membre")
                        .WithMany("PreferenceAides")
                        .HasForeignKey("IdMembre")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pandami.Models.TypeAide", "TypeAide")
                        .WithMany("PreferenceAides")
                        .HasForeignKey("IdTypeAide")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Membre");

                    b.Navigation("TypeAide");
                });

            modelBuilder.Entity("Pandami.Models.Repondre", b =>
                {
                    b.HasOne("Pandami.Models.Feat", "Feat")
                        .WithMany("Reponses")
                        .HasForeignKey("IdFeat")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pandami.Models.Membre", "Membre")
                        .WithMany("Inscriptions")
                        .HasForeignKey("IdMembre")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Feat");

                    b.Navigation("Membre");
                });

            modelBuilder.Entity("Pandami.Models.Suspension", b =>
                {
                    b.HasOne("Pandami.Models.Membre", null)
                        .WithMany("Suspensions")
                        .HasForeignKey("MembreId");
                });

            modelBuilder.Entity("Pandami.Models.TypeAide", b =>
                {
                    b.HasOne("Pandami.Models.CategorieAide", "CategorieAide")
                        .WithMany()
                        .HasForeignKey("CategorieAideId");

                    b.HasOne("Pandami.Models.Materiel", "Materiel")
                        .WithMany()
                        .HasForeignKey("MaterielId");

                    b.Navigation("CategorieAide");

                    b.Navigation("Materiel");
                });

            modelBuilder.Entity("Pandami.Models.Feat", b =>
                {
                    b.Navigation("Litiges");

                    b.Navigation("Reponses");
                });

            modelBuilder.Entity("Pandami.Models.Membre", b =>
                {
                    b.Navigation("Disponibilites");

                    b.Navigation("Feats");

                    b.Navigation("Inscriptions");

                    b.Navigation("Litiges");

                    b.Navigation("PreferenceAides");

                    b.Navigation("Suspensions");
                });

            modelBuilder.Entity("Pandami.Models.TypeAide", b =>
                {
                    b.Navigation("Feats");

                    b.Navigation("PreferenceAides");
                });
#pragma warning restore 612, 618
        }
    }
}
