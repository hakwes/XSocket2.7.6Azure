using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace HtmlHelpers
{

    public static class ExtensionMethods
    {
        public static MvcHtmlString DisplayForWithId<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, string wrapperTag = "div")
        {
            var id = helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(ExpressionHelper.GetExpressionText(expression));
            return MvcHtmlString.Create(string.Format("<{0} id=\"{1}\">{2}</{0}>", wrapperTag, id, helper.DisplayFor(expression)));
        }

        public static MvcHtmlString EditorForWithId<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, string wrapperTag = "div")
        {
            var id = helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(ExpressionHelper.GetExpressionText(expression));
            return MvcHtmlString.Create(string.Format("<{0} id=\"{1}\">{2}</{0}>", wrapperTag, id, helper.EditorFor(expression)));
        }

        public static bool IsDebug(this HtmlHelper htmlHelper)
        {
            if (HttpContext.Current.IsDebuggingEnabled)
            {
                return true;
            }
            else return false;
        }
    }
}
