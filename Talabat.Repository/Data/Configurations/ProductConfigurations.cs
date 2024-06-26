﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repository.Data.Configurations
{
    internal class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
                .HasKey(p => p.Id);
            builder
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnType("varchar");
            builder
                .Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnType("varchar");
            builder
                .Property(p => p.Price)
                .HasColumnType("decimal(18, 2)");
            builder
                .HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.CategoryId);
            builder
                .HasOne(p => p.Brand)
                .WithMany()
                .HasForeignKey (p => p.BrandId);
        }
    }
}
