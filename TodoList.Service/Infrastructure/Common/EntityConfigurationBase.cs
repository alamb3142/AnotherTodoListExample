using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Common;

public abstract class EntityConfigurationBase<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : Entity
{
	public void Configure(EntityTypeBuilder<TEntity> builder)
	{
		builder.Ignore(x => x.DomainEvents);
		builder.HasKey(t => t.Id);
		builder.Property(t => t.Id).IsRequired();
		ConfigureEntity(builder);
	}

	public abstract void ConfigureEntity(EntityTypeBuilder<TEntity> builder);
}