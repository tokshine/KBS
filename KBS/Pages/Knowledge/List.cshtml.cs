using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KO.Core;
using KO.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace KBS.Pages.Knowledge
{
    public class ListModel : PageModel
    {
        private readonly IConfiguration configuration;
        private readonly IKnowledgeData languageData;

        [BindProperty(SupportsGet = true)]//binding enable for a get operation
        public string SearchTerm { get; set; }

        public ListModel(IConfiguration config, IKnowledgeData languageData)
        {
            configuration = config;
            this.languageData = languageData;
        }
        public string Message { get; set; }

        [TempData]
        public string FeedMessage { get; set; }
        public IEnumerable<FieldText> FieldTexts { get; set; }
        public void OnGet(string searchTerm)
        {
            Message = configuration["Message"];
            FieldTexts = languageData.GetAll(SearchTerm);

        }
    }

    
}