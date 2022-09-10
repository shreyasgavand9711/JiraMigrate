using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace jiraDamy.Controllers
{
    public class BaseController : Controller
    {
        public string RenderPartialToString(string viewName, object model)
        {
            //ControllerContext ControllerContext = this.ControllerContext;
            //if (string.IsNullOrEmpty(viewName))
            //    viewName = ControllerContext.RouteData.GetRequiredString("action");
            //ViewDataDictionary ViewData = new ViewDataDictionary();
            //TempDataDictionary TempData = new TempDataDictionary();
            //ViewData.Model = model;

            //using (StringWriter sw = new StringWriter())
            //{
            //    ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
            //    ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
            //    viewResult.View.Render(viewContext, sw);

            //    return sw.GetStringBuilder().ToString();
            //}
            return string.Empty;

        }
    }
}