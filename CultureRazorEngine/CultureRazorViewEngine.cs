using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;

namespace CultureRazorEngine
{
    public class CultureRazorViewEngine : RazorViewEngine
    {
        public override ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            if (controllerContext == null)
                throw new ArgumentNullException("controllerContext");
            if (string.IsNullOrEmpty(partialViewName))
                throw new ArgumentException("The partial view is null or empty.", "partialViewName");

            var searchedLocations = new List<string>();
            var result = base.FindPartialView(controllerContext, string.Format("{0}.{1}", partialViewName, CultureInfo.CurrentUICulture.Name), useCache);
            if (result.View != null)
                return result;

            var defaultResult = base.FindPartialView(controllerContext, partialViewName, useCache);
            if (defaultResult != null)
                return defaultResult;

            searchedLocations.AddRange(result.SearchedLocations);
            searchedLocations.AddRange(defaultResult.SearchedLocations);

            return new ViewEngineResult(searchedLocations);
        }

        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            if (controllerContext == null)
                throw new ArgumentNullException("controllerContext");
            if (string.IsNullOrEmpty(viewName))
                throw new ArgumentException("The view is null or empty.", "partialViewName");

            var searchedLocations = new List<string>();
            var result = base.FindView(controllerContext, string.Format("{0}.{1}", viewName, CultureInfo.CurrentUICulture.Name), masterName, useCache);
            if (result.View != null)
                return result;

            var defaultResult = base.FindView(controllerContext, viewName, masterName, useCache);
            if (defaultResult != null)
                return defaultResult;

            searchedLocations.AddRange(result.SearchedLocations);
            searchedLocations.AddRange(defaultResult.SearchedLocations);

            return new ViewEngineResult(searchedLocations);
        }
    }
}