using Microsoft.AspNetCore.Identity;

namespace InfoPress.Models
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; } = "";
        public DateTime RegisteredDate { get; set; } = DateTime.Now;
        public bool IsSubscribedToNewsletter { get; set; } = false;
        
        // Subscription System
        public bool IsPremiumSubscriber { get; set; } = false;
        public DateTime? SubscriptionExpiryDate { get; set; }
    }
}
