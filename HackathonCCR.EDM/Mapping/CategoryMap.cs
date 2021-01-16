using System.Data.Entity.ModelConfiguration;

namespace HackathonCCR.EDM.Models.Mapping
{
    public class CategoryMap : EntityTypeConfiguration<Category>
    {
        public CategoryMap()
        {
            ToTable("Category");

            HasKey(t => t.CategoryId);
            Property(t => t.CategoryId).HasColumnName("CategoryId");
            Property(t => t.Description).HasColumnName("Description").IsRequired();
            Property(t => t.Color).HasColumnName("Color");
        }
    }
}
