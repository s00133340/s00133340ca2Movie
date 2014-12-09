using System.Web;
using System.Web.Mvc;

namespace MovieCa2s00133340
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}