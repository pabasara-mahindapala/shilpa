namespace EE5207.Project.Web.Models.Account
{
    public class RegisterResultViewModel
    {
        public string TenancyName { get; set; }
        
        public string UserName { get; set; }

        public string EmailAddress { get; set; }
        
        public string NameAndSurname { get; set; }

        public bool IsActive { get; set; }
    }
}