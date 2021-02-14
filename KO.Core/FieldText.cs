using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace KO.Core
{

    public enum FieldType
    {

        NONE,
        SECURITY,
        NETWORK,
        XAMARIN,
        REACT,
        ANGULAR,
        ENGLISH,
        NIGERIA,
        TECHNICAL_JARGONS,
        UNITED_KINGDOM,
        ASP_NET_CORE,
        UNITED_STATES,
        UI_UX_DESIGN
        //add new field type here
}
    public class FieldText
    {
        public int Id { get; set; }

        [Required, StringLength(80)]
        [Display(Name = "Terminlogy/Term")]
        public string Text { get; set; }

        // [Required, StringLength(255)]
        [Display(Name = "Related Materials and Reference Links")]
        public string RelatedMaterialsLinks { get; set; }

        public string Keywords { get; set; }

        //[Required, StringLength(255)]
        public string Usecases { get; set; }

        public FieldType FieldType { get; set; }

        public string FieldName { get; set; } 

        public KoUser User { get; set; }

    }
    //https://stackoverflow.com/questions/60736091/whats-the-difference-between-microsoft-extensions-identity-stores-and-microsoft
    public class KoUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
