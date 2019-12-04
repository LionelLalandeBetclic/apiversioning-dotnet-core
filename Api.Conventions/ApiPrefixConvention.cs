using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Api.Conventions
{
    internal sealed class ApiPrefixConvention : IApplicationModelConvention

    {
        public void Apply(ApplicationModel application)
        {
            foreach (var controller in application.Controllers)
            {
                foreach (var selector in controller.Selectors)
                {
                    if (selector.AttributeRouteModel == null)
                    {
                        selector.AttributeRouteModel = new AttributeRouteModel
                        {
                            Template = "api/[controller]"
                        };
                    }
                    else
                    {
                        var versionedConstraintRouteModel = new AttributeRouteModel
                        {
                            Template = "api"
                        };

                        selector.AttributeRouteModel =
                            AttributeRouteModel.CombineAttributeRouteModel(versionedConstraintRouteModel,
                                selector.AttributeRouteModel);
                    }
                }
            }
        }
    }
}
