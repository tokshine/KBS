using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KO.Core;
using KO.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace KBS.Pages.Knowledge
{
    public class DeleteModel : PageModel
    {
        private readonly IKnowledgeData languageData;

        public FieldText FieldText { get; set; }

        public DeleteModel(IKnowledgeData languageData)
        {
            this.languageData = languageData;
        }

        public IActionResult OnGet(int id)
        {
            FieldText = languageData.GetById(id);
            if(FieldText == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            var FieldText = languageData.Delete(id);
            languageData.Commit();

            if(FieldText == null)
            {
                return RedirectToPage("./NotFound");
            }

            TempData["Message"] = $"{FieldText.Text} deleted";
            return RedirectToPage("./ClientLanguages");
        }
    }
}