using Infrastructure.Dtos;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities;

public class SubscriberEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    [Required]
    public string Email { get; set; } = null!;
    public bool DailyNewsletter { get; set; }
    public bool AdvertisingUpdates { get; set; }
    public bool WeekinReview { get; set; }
    public bool EventUpdates { get; set; }
    public bool StartupsWeekly { get; set; }
    public bool Podcasts { get; set; }

    public static implicit operator SubscriberEntity(Subscriber dto)
    {
        return new SubscriberEntity
        {
            Email = dto.Email,
            DailyNewsletter = dto.DailyNewsletter,
            AdvertisingUpdates = dto.AdvertisingUpdates,
            WeekinReview = dto.WeekinReview,
            EventUpdates = dto.EventUpdates,
            StartupsWeekly = dto.StartupsWeekly,
            Podcasts = dto.Podcasts
        };
    }
}
