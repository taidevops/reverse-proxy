﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using k8s;
using k8s.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Kubernetes;
using Microsoft.Kubernetes.Controller.Hosting;
using Microsoft.Kubernetes.Controller.Informers;
using System.Linq;

namespace Microsoft.Kubernetes.Controller;

public static class KubernetesControllerExtensions
{
    public static IServiceCollection AddKubernetesControllerRuntime(this IServiceCollection services)
    {
        services = services.AddKubernetesCore();

        if (!services.Any(serviceDescriptor => serviceDescriptor.ServiceType == typeof(IResourceInformer<>)))
        {
            services = services.AddSingleton(typeof(IResourceInformer<>), typeof(ResourceInformer<>));
        }

        return services;
    }

    /// <summary>
    /// Registers the resource informer.
    /// </summary>
    /// <typeparam name="TResource">The type of the t related resource.</typeparam>
    /// <param name="services">The services.</param>
    /// <returns>IServiceCollection.</returns>
    public static IServiceCollection RegisterResourceInformer<TResource>(this IServiceCollection services)
        where TResource : class, IKubernetesObject<V1ObjectMeta>, new()
    {
        return services
            .RegisterHostedService<IResourceInformer<TResource>>();
    }
}
