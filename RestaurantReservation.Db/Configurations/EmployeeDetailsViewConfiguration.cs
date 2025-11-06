using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Configurations;

public class EmployeeDetailsViewConfiguration : IEntityTypeConfiguration<EmployeeDetailsView>
{
    public void Configure(EntityTypeBuilder<EmployeeDetailsView> builder)
    {
        builder.HasNoKey();
        builder.ToView("vw_EmployeeDetails");
    }
}