using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
namespace PosApp.Configurations
{
    public class RoutePrefixConvention : IApplicationModelConvention
    {
        private readonly AttributeRouteModel _routePrefix;

        public RoutePrefixConvention(string routePrefix)
        {
            _routePrefix = new AttributeRouteModel(new RouteAttribute(routePrefix));
        }

        public void Apply(ApplicationModel application)
        {
            foreach (var controller in application.Controllers)
            {
                foreach (var selector in controller.Selectors)
                {
                    if (selector.AttributeRouteModel == null)
                    {
                        selector.AttributeRouteModel = _routePrefix;
                    }
                    else
                    {
                        selector.AttributeRouteModel = AttributeRouteModel.CombineAttributeRouteModel(_routePrefix, selector.AttributeRouteModel);
                    }
                }
            }
        }
    }

}
