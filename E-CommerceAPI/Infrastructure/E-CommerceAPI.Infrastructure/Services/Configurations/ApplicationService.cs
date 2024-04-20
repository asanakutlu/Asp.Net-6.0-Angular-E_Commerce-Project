using E_CommerceAPI.Application.Abstractions.Services.Configurations;
using E_CommerceAPI.Application.CustomAttributes;
using E_CommerceAPI.Application.DTOs.Configuraiton;
using E_CommerceAPI.Application.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Infrastructure.Services.Configurations
{
    public class ApplicationService : IApplicationService
    {
        public List<Menu> GetAuthorizeDefinitionEndpoints(Type type)
        {
            Assembly assembly = Assembly.GetAssembly(type);
            var controllers = assembly.GetTypes().Where(o => o.IsAssignableTo(typeof(ControllerBase)));
            List<Menu> menus = new();
            if (controllers != null)
                foreach (var controller in controllers)
                {
                    var actions = controller.GetMethods().Where(o => o.IsDefined(typeof(AuthorizeDefinitionAttribute)));
                    if (actions != null)
                        foreach (var action in actions)
                        {
                            var attributes = action.GetCustomAttributes(true);
                            if (attributes != null)
                            {
                                Menu menu = null;
                                var authorizeDefinitionAttribute = attributes.FirstOrDefault(o => o.GetType() == typeof(AuthorizeDefinitionAttribute)) as AuthorizeDefinitionAttribute;
                                if (menus.Any(o => o.Name == authorizeDefinitionAttribute.Menu))
                                {
                                    menu = new() { Name = authorizeDefinitionAttribute.Menu };
                                    menus.Add(menu);
                                }
                                else
                                    menu = menus.FirstOrDefault(o => o.Name == authorizeDefinitionAttribute.Menu);

                                Application.DTOs.Configuraiton.Action _action = new()
                                {
                                    ActionType =Enum.GetName(typeof(ActionType),authorizeDefinitionAttribute.ActionType),
                                    Definition=authorizeDefinitionAttribute.Definition,
                                 };
                              var httpAttribute = attributes.FirstOrDefault(o => o.GetType().IsAssignableTo(typeof(HttpMethodAttribute))) as HttpMethodAttribute;
                                if (httpAttribute != null)
                                    _action.HttpType = httpAttribute.HttpMethods.First();
                                else
                                    _action.HttpType = HttpMethods.Get;
                                _action.Code = $"{_action.HttpType}.{_action.ActionType}.{_action.Definition.Replace(" ","")}";
                                menu.Actions.Add(_action);
                            }
                        }
                }
            return menus;
        }
    }
}
