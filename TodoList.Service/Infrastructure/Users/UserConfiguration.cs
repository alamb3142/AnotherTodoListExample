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

		builder.OwnsOne<Email>(
			user => user.Email,
			email =>
			{
				email.Property(e => e.Value).HasColumnName("EmailValue").IsRequired();
				email.Property(e => e.Verified).HasColumnName("EmailVerified").IsRequired();
			}
		);
	}
}
