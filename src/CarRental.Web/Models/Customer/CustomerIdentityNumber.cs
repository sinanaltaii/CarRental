namespace CarRental.Web.Models.Customer
{
    public record CustomerIdentityNumber
    {
        public CustomerIdentityNumber(string personalIdentityNumber)
        {
            PersonalIdentityNumber = personalIdentityNumber;
        }
        public string PersonalIdentityNumber { get; set; }
    }
}