using System.Web;
using System.Web.Mvc;

namespace Quab_Ly_ne_nep_thi_dua
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
