//using System;
//using System.Reflection;
//using System.Web.Mvc;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;




//namespace BhartiNetwork.Controllers
//{
//    internal class OnActionAttribute : Attribute
//    {
//        public string ButtonName { get; set; }

//        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
//        {
//            var req = controllerContext.RequestContext.HttpContext.Request;
//            return !string.IsNullOrEmpty(req.Form[this.ButtonName]);
//        }
//    }
//}


namespace BhartiNetwork.Filter
{
    public class SimpleAttribute
    {
    }
    public class OnActionAttribute : ActionMethodSelectorAttribute
    {
        public string ButtonName { get; set; }

        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            var req = controllerContext.RequestContext.HttpContext.Request;
            return !string.IsNullOrEmpty(req.Form[this.ButtonName]);
        }
    }
}




