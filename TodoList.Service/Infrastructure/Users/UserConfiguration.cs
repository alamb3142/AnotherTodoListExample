using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Users;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.Ignore(t => t.DomainEvents);

		builder.HasKey(t => t.Id);
		builder.Property(t => t.Id).IsRequired();
		builder.Property(t => t.HashedPassword).IsRequired();
		builder.Property(t => t.Salt).IsRequired();
	}
}
