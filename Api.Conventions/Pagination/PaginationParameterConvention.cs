using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Api.Conventions
{
    public sealed class PaginationParameterConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controllerModel)
        {
            foreach (var action in controllerModel.Actions)
            {
                foreach (var parameter in action.Parameters)
                {
                    if (parameter.BindingInfo?.BindingSource == BindingSource.Body &&
                        parameter.ParameterInfo.ParameterType == typeof(Pagination))
                        parameter.BindingInfo.BindingSource = BindingSource.Special;
                }
            }
        }
    }
}
