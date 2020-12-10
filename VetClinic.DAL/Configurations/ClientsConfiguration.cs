using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VetClinic.DAL.Entities;

namespace VetClinic.DAL.Configurations
{
    public class ClientsConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasData(
                new Client[]
                {
                    new Client{ Id=1,UserId=4},
                    new Client{ Id=2,UserId=5},
                    new Client{ Id=3,UserId=6},
                    new Client{ Id=4,UserId=7}
                });
        }
    }
}
