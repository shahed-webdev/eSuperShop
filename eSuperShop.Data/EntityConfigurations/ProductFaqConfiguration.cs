using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eSuperShop.Data
{
    public class ProductFaqConfiguration : IEntityTypeConfiguration<ProductFaq>
    {
        public void Configure(EntityTypeBuilder<ProductFaq> builder)
        {
            builder.Property(e => e.Question)
                .IsRequired()
                .HasMaxLength(1024);

            builder.Property(e => e.Answer)
                .HasMaxLength(2048);

            builder.Property(e => e.QuestionOnUtc)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getutcdate())");

            builder.Property(e => e.AnswerOnUtc)
                .HasColumnType("datetime");

            builder.HasOne(d => d.Customer)
                .WithMany(p => p.ProductFaq)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_ProductFaq_Customer");

            builder.HasOne(d => d.Product)
                .WithMany(p => p.ProductFaq)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_ProductFaq_Product");
        }
    }
}