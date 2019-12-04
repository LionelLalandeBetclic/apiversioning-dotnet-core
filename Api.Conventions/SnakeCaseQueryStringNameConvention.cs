using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Api.Conventions
{
    public sealed class SnakeCaseQueryStringNameConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controllerModel)
        {
            foreach (var action in controllerModel.Actions)
            {
                foreach (var parameter in action.Parameters)
                {
                    if (parameter.BindingInfo?.BindingSource == BindingSource.Query)
                        parameter.ParameterName = parameter.ParameterName.ToSnakeCase();
                }
            }
        }
    }
}
