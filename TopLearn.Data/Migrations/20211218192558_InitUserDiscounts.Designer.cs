﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TopLearn.Data.Context;

namespace TopLearn.Data.Migrations
{
    [DbContext(typeof(TopLearnContext))]
    [Migration("20211218192558_InitUserDiscounts")]
    partial class InitUserDiscounts
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("TopLearn.Data.Entities.Course.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("CourseDemoFileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CourseDescription")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("CourseImageName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CoursePrice")
                        .HasColumnType("int");

                    b.Property<string>("CourseTitle")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("LevelId")
                        .HasColumnType("int");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<int?>("SubId")
                        .HasColumnType("int");

                    b.Property<string>("Tags")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("CourseId");

                    b.HasIndex("GroupId");

                    b.HasIndex("LevelId");

                    b.HasIndex("StatusId");

                    b.HasIndex("SubId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("TopLearn.Data.Entities.Course.CourseEpisode", b =>
                {
                    b.Property<int>("EpisodeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("EpisodeFileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("EpisodeTime")
                        .HasColumnType("time");

                    b.Property<string>("EpisodeTitle")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsFree")
                        .HasColumnType("bit");

                    b.HasKey("EpisodeId");

                    b.HasIndex("CourseId");

                    b.ToTable("CourseEpisodes");
                });

            modelBuilder.Entity("TopLearn.Data.Entities.Course.CourseGroup", b =>
                {
                    b.Property<int>("GroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("GroupTitle")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.HasKey("GroupId");

                    b.HasIndex("ParentId");

                    b.ToTable("CourseGroups");
                });

            modelBuilder.Entity("TopLearn.Data.Entities.Course.CourseLevel", b =>
                {
                    b.Property<int>("LevelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("LevelTitle")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("LevelId");

                    b.ToTable("CourseLevels");
                });

            modelBuilder.Entity("TopLearn.Data.Entities.Course.CourseStatus", b =>
                {
                    b.Property<int>("StatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("StatusTitle")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("StatusId");

                    b.ToTable("CourseStatuses");
                });

            modelBuilder.Entity("TopLearn.Data.Entities.Order.Discount", b =>
                {
                    b.Property<int>("DiscountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("DiscountCode")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("DiscountPercent")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UsableCount")
                        .HasColumnType("int");

                    b.HasKey("DiscountId");

                    b.ToTable("Discounts");
                });

            modelBuilder.Entity("TopLearn.Data.Entities.Order.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsFinally")
                        .HasColumnType("bit");

                    b.Property<int>("OrderSum")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("TopLearn.Data.Entities.Order.OrderDetail", b =>
                {
                    b.Property<int>("DetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.HasKey("DetailId");

                    b.HasIndex("CourseId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("TopLearn.Data.Entities.Permission.Permission", b =>
                {
                    b.Property<int>("PermissionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.Property<string>("PermissionTitle")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("PermissionId");

                    b.HasIndex("ParentId");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("TopLearn.Data.Entities.Permission.RolePermission", b =>
                {
                    b.Property<int>("RP_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("PermissionId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("RP_Id");

                    b.HasIndex("PermissionId");

                    b.HasIndex("RoleId");

                    b.ToTable("RolePermissions");
                });

            modelBuilder.Entity("TopLearn.Data.Entities.User.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("RoleTilte")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("TopLearn.Data.Entities.User.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ActiveCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AvatarName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("RegisterDate")
                        .HasMaxLength(200)
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TopLearn.Data.Entities.User.UserDiscount", b =>
                {
                    b.Property<int>("UD_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("DiscountId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("UD_Id");

                    b.HasIndex("DiscountId");

                    b.HasIndex("UserId");

                    b.ToTable("UserDiscounts");
                });

            modelBuilder.Entity("TopLearn.Data.Entities.User.UserRole", b =>
                {
                    b.Property<int>("UR_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("UR_Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("TopLearn.Data.Entities.Wallet.Wallet", b =>
                {
                    b.Property<int>("WalletId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Amount")
                        .HasMaxLength(200)
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("IsPayed")
                        .HasColumnType("bit");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("WalletId");

                    b.HasIndex("TypeId");

                    b.HasIndex("UserId");

                    b.ToTable("Wallets");
                });

            modelBuilder.Entity("TopLearn.Data.Entities.Wallet.WalletType", b =>
                {
                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.Property<string>("TypeTitle")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("TypeId");

                    b.ToTable("WalletTypes");
                });

            modelBuilder.Entity("TopLearn.Data.Entities.Course.Course", b =>
                {
                    b.HasOne("TopLearn.Data.Entities.Course.CourseGroup", "CourseGroup")
                        .WithMany("Courses")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TopLearn.Data.Entities.Course.CourseLevel", "CourseLevel")
                        .WithMany("Courses")
                        .HasForeignKey("LevelId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TopLearn.Data.Entities.Course.CourseStatus", "CourseStatus")
                        .WithMany("Courses")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TopLearn.Data.Entities.Course.CourseGroup", "SubGroup")
                        .WithMany("SubCourses")
                        .HasForeignKey("SubId");

                    b.HasOne("TopLearn.Data.Entities.User.User", "User")
                        .WithMany("Courses")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("CourseGroup");

                    b.Navigation("CourseLevel");

                    b.Navigation("CourseStatus");

                    b.Navigation("SubGroup");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TopLearn.Data.Entities.Course.CourseEpisode", b =>
                {
                    b.HasOne("TopLearn.Data.Entities.Course.Course", "Course")
                        .WithMany("CourseEpisodes")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("TopLearn.Data.Entities.Course.CourseGroup", b =>
                {
                    b.HasOne("TopLearn.Data.Entities.Course.CourseGroup", null)
                        .WithMany("CourseGroups")
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("TopLearn.Data.Entities.Order.Order", b =>
                {
                    b.HasOne("TopLearn.Data.Entities.User.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("TopLearn.Data.Entities.Order.OrderDetail", b =>
                {
                    b.HasOne("TopLearn.Data.Entities.Course.Course", "Course")
                        .WithMany("OrderDetails")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TopLearn.Data.Entities.Order.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("TopLearn.Data.Entities.Permission.Permission", b =>
                {
                    b.HasOne("TopLearn.Data.Entities.Permission.Permission", null)
                        .WithMany("Permissions")
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("TopLearn.Data.Entities.Permission.RolePermission", b =>
                {
                    b.HasOne("TopLearn.Data.Entities.Permission.Permission", "Permission")
                        .WithMany("RolePermissions")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TopLearn.Data.Entities.User.Role", "Role")
                        .WithMany("RolePermissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("TopLearn.Data.Entities.User.UserDiscount", b =>
                {
                    b.HasOne("TopLearn.Data.Entities.Order.Discount", "Discount")
                        .WithMany("UserDiscounts")
                        .HasForeignKey("DiscountId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TopLearn.Data.Entities.User.User", "User")
                        .WithMany("UserDiscounts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Discount");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TopLearn.Data.Entities.User.UserRole", b =>
                {
                    b.HasOne("TopLearn.Data.Entities.User.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TopLearn.Data.Entities.User.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TopLearn.Data.Entities.Wallet.Wallet", b =>
                {
                    b.HasOne("TopLearn.Data.Entities.Wallet.WalletType", "WalletType")
                        .WithMany("Wallets")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TopLearn.Data.Entities.User.User", "User")
                        .WithMany("Wallets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("WalletType");
                });

            modelBuilder.Entity("TopLearn.Data.Entities.Course.Course", b =>
                {
                    b.Navigation("CourseEpisodes");

                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("TopLearn.Data.Entities.Course.CourseGroup", b =>
                {
                    b.Navigation("CourseGroups");

                    b.Navigation("Courses");

                    b.Navigation("SubCourses");
                });

            modelBuilder.Entity("TopLearn.Data.Entities.Course.CourseLevel", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("TopLearn.Data.Entities.Course.CourseStatus", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("TopLearn.Data.Entities.Order.Discount", b =>
                {
                    b.Navigation("UserDiscounts");
                });

            modelBuilder.Entity("TopLearn.Data.Entities.Order.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("TopLearn.Data.Entities.Permission.Permission", b =>
                {
                    b.Navigation("Permissions");

                    b.Navigation("RolePermissions");
                });

            modelBuilder.Entity("TopLearn.Data.Entities.User.Role", b =>
                {
                    b.Navigation("RolePermissions");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("TopLearn.Data.Entities.User.User", b =>
                {
                    b.Navigation("Courses");

                    b.Navigation("Orders");

                    b.Navigation("UserDiscounts");

                    b.Navigation("UserRoles");

                    b.Navigation("Wallets");
                });

            modelBuilder.Entity("TopLearn.Data.Entities.Wallet.WalletType", b =>
                {
                    b.Navigation("Wallets");
                });
#pragma warning restore 612, 618
        }
    }
}
