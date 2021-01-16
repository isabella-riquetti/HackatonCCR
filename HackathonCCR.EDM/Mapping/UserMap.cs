using System.Data.Entity.ModelConfiguration;

namespace HackathonCCR.EDM.Models.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            ToTable("User");

            HasKey(t => t.UserId);
            Property(t => t.UserId).HasColumnName("UserId");
            Property(t => t.Name).HasColumnName("Name").HasMaxLength(255).IsRequired();
            Property(t => t.Email).HasColumnName("Email").HasMaxLength(255).IsRequired();
            Property(t => t.Password).HasColumnName("Password").HasMaxLength(255).IsRequired();
        }
    }
}
