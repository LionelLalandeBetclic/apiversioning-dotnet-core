using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;
using System.Text.RegularExpressions;

namespace Api.Conventions
{
    public sealed class KebabCaseControllerNameConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controllerModel)
        {
            var controllerName = controllerModel.ControllerName;

            // In case versioning is in the name of the controller like this: <Name>Controller<Version>
            if (ExtractVersion(ref controllerName, out var apiVersion))
            {
                var builder = new ControllerApiVersionConventionBuilder<ControllerModel>();
                builder.HasApiVersion(apiVersion);
                builder.ApplyTo(controllerModel);
            }

            // In case Asp.Net Core doesn't remove 'Controller' suffix because of incompatible conventions.
            var index = controllerName.IndexOf("Controller");
            if (index >= 0)
                controllerName = controllerName.Substring(0, index);

            // In case versioning is in the name of the controller like this: <Name><Version>Controller
            if (ExtractVersion(ref controllerName, out apiVersion))
            {
                var builder = new ControllerApiVersionConventionBuilder<ControllerModel>();
                builder.HasApiVersion(apiVersion);
                builder.ApplyTo(controllerModel);
            }

            controllerModel.ControllerName = controllerName.ToKebabCase();
        }

        private static bool ExtractVersion(ref string controllerName, out ApiVersion apiVersion)
        {
            var match = Regex.Match(controllerName, @"V(\d+(.\d+)*)$");
            if (match.Success)
            {
                controllerName = Regex.Replace(controllerName, @"V\d+(.\d+)*$", string.Empty);
                var version = match.Groups[1].Value;
                if (ApiVersion.TryParse(version, out apiVersion))
                {
                    return true;
                }
            }

            apiVersion = default;
            return false;
        }
    }
}
