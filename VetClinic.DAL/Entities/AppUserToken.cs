using Microsoft.AspNetCore.Identity;


namespace VetClinic.DAL.Entities
{
    public partial class AppUserToken : IdentityUserToken<int>
    {
        public int Id { get; set; }
    }
}
