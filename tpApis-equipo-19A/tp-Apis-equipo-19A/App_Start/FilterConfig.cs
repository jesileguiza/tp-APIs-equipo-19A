using System.Web;
using System.Web.Mvc;

namespace tp_Apis_equipo_19A
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
