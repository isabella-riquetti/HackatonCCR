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
            Property(t => t.Type).HasColumnName("Type").IsRequired();
            Property(t => t.Picture).HasColumnName("Picture");
            Property(t => t.PhoneNumber).HasColumnName("PhoneNumber");
            Property(t => t.GraduationId).HasColumnName("GraduationId");
            Property(t => t.RemainingMissingHours).HasColumnName("RemainingMissingHours");
            Property(t => t.WorkingField).HasColumnName("WorkingField");

            HasRequired(t => t.Graduation)
                            .WithMany(t => t.Mentors)
                            .HasForeignKey(d => d.GraduationId)
                            .WillCascadeOnDelete(false);
        }
    }
}
