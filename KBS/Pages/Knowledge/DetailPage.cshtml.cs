using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KO.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KBS.Pages.Knowledge
{
    public class DetailPageModel : PageModel
    {
        public FieldText Language { get; set; }
        public void OnGet(int Id)
        {
            Language = new FieldText();
            Language.Id = Id;
        }
    }
}