using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB2_Projekat.Models;

namespace Data.Models.Configurations
{
    public class ProdavacKonfiguration : IEntityTypeConfiguration<Prodavac>
    {
        public void Configure(EntityTypeBuilder<Prodavac> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
