using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KO.Core;
using KO.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KBS.Pages.Knowledge
{
    public class EditModel : PageModel
    {
        private readonly IKnowledgeData languageData;
        private readonly IHtmlHelper htmlHelper;
        private readonly UserManager<KoUser> _userManager;

        [BindProperty] // 2 way binding
        public FieldText FieldText { get; set; }
        

        public IEnumerable<SelectListItem> Languages { get; set; }
        public EditModel(IKnowledgeData languageData,IHtmlHelper htmlHelper, UserManager<KoUser> userManager)
        {
            this.languageData = languageData;
            this.htmlHelper = htmlHelper;
            _userManager = userManager;
        }
        public IActionResult OnGet(int? id)
        {
            if (id.HasValue)
            {
                FieldText = languageData.GetById(id.Value);
                ViewData["Title"] = "Edit";
            }
            else {
                ViewData["Title"] = "Add a word/phrase";
                FieldText = new FieldText();
                FieldText.FieldType = FieldType.XAMARIN;
            }
            
            Languages = htmlHelper.GetEnumSelectList<FieldType>();
            if (FieldText == null)
            {

                return RedirectToPage("./NotFound");
            }
            return Page();
        }

        public  async Task<IActionResult> OnPost()
        {
            //POST/REDIRECT/GET  PRINCIPLE

            if (FieldText.FieldType == FieldType.NONE)
            {
                Languages = htmlHelper.GetEnumSelectList<FieldType>();
                ModelState.AddModelError("","Select a Field");
                return Page();
            }

            if (!ModelState.IsValid)
            {
                Languages = htmlHelper.GetEnumSelectList<FieldType>();

                return Page();

            }

            FieldText.FieldName = FieldText.FieldType.ToString();
            if (FieldText.Id > 0)
            {                
                languageData.Update(FieldText);
            }
            else {
                var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
                FieldText.User = currentUser;
                languageData.Add(FieldText);
            }
            
            languageData.Commit();
            TempData["Message"] =   "Record Saved";
            return RedirectToPage("./ClientLanguages");
            //return RedirectToPage("./List");
        }
    }
}