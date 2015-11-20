using BamApps.Excido.Data.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace BamApps.Excido.Data.Map {
    public class SharedContentUnitMap : EntityTypeConfiguration<SharedContentUnit> {
        /// <summary>
        /// Initializes a new instance of the <see cref="SharedUnitMap"/> class.
        /// </summary>
        public SharedContentUnitMap() {
            ToTable("SharedContentUnit");

            HasKey(x => x.Id);

            Property(x => x.Name)
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute() { IsUnique = true }));

            Property(x => x.Slug)
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute() { IsUnique = true }))
                .HasColumnAnnotation("DefaultValue", "NEWID()");

            Property(x => x.Created)
                .IsRequired()
                .HasColumnAnnotation("DefaultValue", "GETUTCDATE()");
        }
    }
}
