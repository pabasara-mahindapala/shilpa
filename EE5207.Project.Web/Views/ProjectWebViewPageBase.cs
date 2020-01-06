using Abp.Web.Mvc.Views;

namespace EE5207.Project.Web.Views
{
    public abstract class ProjectWebViewPageBase : ProjectWebViewPageBase<dynamic>
    {

    }

    public abstract class ProjectWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected ProjectWebViewPageBase()
        {
            LocalizationSourceName = ProjectConsts.LocalizationSourceName;
        }
    }
}