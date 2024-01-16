using System.Web.Mvc;
using CustomerSurveySystem.Services.Class;
using CustomerSurveySystem.Services.Interface;
using Unity;
using Unity.Mvc5;

namespace CustomerSurveySystem
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            container.RegisterType<IService, Service>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}