using System.Web.Mvc;

namespace FifaBattle
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
			//filters.Add(new AuthorizeAttribute());
		}
	}
}
