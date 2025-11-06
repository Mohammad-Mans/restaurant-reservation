using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Configurations;

public class ReservationDetailsViewConfiguration : IEntityTypeConfiguration<ReservationDetailsView>
{
    public void Configure(EntityTypeBuilder<ReservationDetailsView> builder)
    {
        builder.HasNoKey();
        builder.ToView("vw_ReservationDetails");
    }
}