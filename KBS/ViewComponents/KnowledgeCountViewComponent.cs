
using KO.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KBS.ViewComponents
{
    public class KnowledgeCountViewComponent
         : ViewComponent
    {
        private readonly IKnowledgeData knowledgeData;

        public KnowledgeCountViewComponent(IKnowledgeData knowledgeData)
        {
            this.knowledgeData = knowledgeData;
        }

        public IViewComponentResult Invoke()
        {
            var count = knowledgeData.GetCountOfFieldTexts();
            return View(count);
        }

    }
}
