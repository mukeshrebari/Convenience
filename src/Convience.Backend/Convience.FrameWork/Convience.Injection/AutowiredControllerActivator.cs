﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Convience.Injection
{
    public class AutowiredControllerActivator : IControllerActivator
    {
        /// <summary>
        /// 递归的方式从controller层面逐级装配
        /// </summary>
        public object Create(ControllerContext context)
        {
            // 获取控制器类型
            var controllerType = context.ActionDescriptor.ControllerTypeInfo.AsType();

            // 通过容器获取控制器实例
            var controllerInstance = context.HttpContext.RequestServices.GetRequiredService(controllerType);

            // 获取所有被标记自动装配的字段
            var autowiredFields = controllerType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(field => field.GetCustomAttribute<AutowiredAttribute>() != null);

            // 循环创建实例
            foreach (var field in autowiredFields)
            {
                var fieldInstance = CreateInstance(context.HttpContext.RequestServices, field.FieldType);
                field.SetValue(controllerInstance, fieldInstance);
            }

            return controllerInstance;
        }

        public void Release(ControllerContext context, object controller)
        {
        }

        /// <summary>
        /// 递归创建字段类型实例内的字段实例
        /// </summary>
        private object CreateInstance(IServiceProvider serviceProvider, Type type)
        {
            var instance = serviceProvider.GetRequiredService(type);

            // 在类型中查找包含自动装配的字段
            var autowiredFields = instance.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(field => field.GetCustomAttribute<AutowiredAttribute>() != null);

            // 循环创建实例
            foreach (var field in autowiredFields)
            {
                var fieldInstance = CreateInstance(serviceProvider, field.FieldType);
                field.SetValue(instance, fieldInstance);
            }
            return instance;
        }
    }
}
