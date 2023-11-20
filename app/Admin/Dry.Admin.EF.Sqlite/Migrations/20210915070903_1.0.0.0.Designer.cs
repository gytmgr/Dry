﻿// <auto-generated />
using Dry.EF.Contexts;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dry.Admin.EF.Sqlite.Migrations
{
    [DbContext(typeof(DryDbContext<IAdminContext>))]
    [Migration("20210915070903_1.0.0.0")]
    partial class _1000
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("Dry.Admin.Domain.Entities.Application", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasComment("系统id");

                    b.Property<DateTime>("AddTime")
                        .HasColumnType("TEXT")
                        .HasComment("添加时间");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT")
                        .HasComment("说明");

                    b.Property<bool>("Enable")
                        .HasColumnType("INTEGER")
                        .HasComment("是否可用");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasComment("名称");

                    b.Property<string>("Secret")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT")
                        .HasComment("Secret");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER")
                        .HasComment("类型");

                    b.Property<string>("Url")
                        .HasColumnType("TEXT")
                        .HasComment("地址");

                    b.HasKey("Id");

                    b.HasIndex("AddTime");

                    b.ToTable("Application");

                    b
                        .HasComment("应用");
                });

            modelBuilder.Entity("Dry.Admin.Domain.Entities.Resource", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasComment("系统id");

                    b.Property<DateTime>("AddTime")
                        .HasColumnType("TEXT")
                        .HasComment("添加时间");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("TEXT")
                        .HasComment("上级资源id");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Resource");

                    b
                        .HasComment("系统资源");
                });

            modelBuilder.Entity("Dry.Admin.Domain.Entities.ResourceItem", b =>
                {
                    b.Property<Guid>("ResourceId")
                        .HasColumnType("TEXT")
                        .HasComment("资源id");

                    b.Property<byte>("Type")
                        .HasColumnType("INTEGER")
                        .HasComment("资源项类型");

                    b.Property<DateTime>("AddTime")
                        .HasColumnType("TEXT")
                        .HasComment("添加时间");

                    b.HasKey("ResourceId", "Type");

                    b.HasIndex("AddTime");

                    b.ToTable("ResourceItem");

                    b
                        .HasComment("资源项");
                });

            modelBuilder.Entity("Dry.Domain.Entities.ValueObjects.TreeAncestorRelation<Dry.Admin.Domain.Entities.Resource, System.Guid>", b =>
                {
                    b.Property<Guid>("RelationId")
                        .HasColumnType("TEXT")
                        .HasComment("系统资源id");

                    b.Property<Guid>("AncestorId")
                        .HasColumnType("TEXT")
                        .HasComment("祖先id");

                    b.HasKey("RelationId", "AncestorId");

                    b.HasIndex("AncestorId");

                    b.ToTable("ResourceAncestor");

                    b
                        .HasComment("系统资源祖先关系");
                });

            modelBuilder.Entity("Dry.Admin.Domain.Entities.Resource", b =>
                {
                    b.HasOne("Dry.Admin.Domain.Entities.Resource", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("Dry.Admin.Domain.Entities.ResourceItem", b =>
                {
                    b.HasOne("Dry.Admin.Domain.Entities.Resource", "Resource")
                        .WithMany("Items")
                        .HasForeignKey("ResourceId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Resource");
                });

            modelBuilder.Entity("Dry.Domain.Entities.ValueObjects.TreeAncestorRelation<Dry.Admin.Domain.Entities.Resource, System.Guid>", b =>
                {
                    b.HasOne("Dry.Admin.Domain.Entities.Resource", "Ancestor")
                        .WithMany("AncestorRelations")
                        .HasForeignKey("AncestorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Dry.Admin.Domain.Entities.Resource", "Relation")
                        .WithMany("DescendantRelations")
                        .HasForeignKey("RelationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ancestor");

                    b.Navigation("Relation");
                });

            modelBuilder.Entity("Dry.Admin.Domain.Entities.Resource", b =>
                {
                    b.Navigation("AncestorRelations");

                    b.Navigation("Children");

                    b.Navigation("DescendantRelations");

                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
