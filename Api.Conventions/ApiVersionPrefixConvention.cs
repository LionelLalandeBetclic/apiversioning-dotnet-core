using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;

namespace Api.Conventions
{
    internal sealed class ApiVersionPrefixConvention : IApplicationModelConvention
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
                            Template = "v{version:apiVersion}/[controller]"
                        };
                    }
                    else if (!selector.AttributeRouteModel.Template.Contains("v{version:apiVersion}", StringComparison.OrdinalIgnoreCase))
                    {
                        var versionedConstraintRouteModel = new AttributeRouteModel
                        {
                            Template = "v{version:apiVersion}"
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
