using System.Web;
using System.Web.Mvc;

namespace JJCKManager
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new AuthorizeAttribute());//全部页面要求登录验证
            
        }
    }
}
