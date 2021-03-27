using AutoMapper;

using FluentValidation;

using MediatR;

using Microsoft.Extensions.DependencyInjection;
using MyDemo.Core;

namespace MyDemo.Business.Common
{
	/// <summary>
	/// Расширение для регистрации зависимостей бизнес-слоя.
	/// </summary>
	public static class BusinessDependencyInjection
	{
		/// <summary>
		/// Регистрирует зависимости бизнес-логики.
		/// </summary>
		/// <param name="services">Коллекция зависимостей приложения.</param>
		/// <returns>Измененная коллекция.</returns>
		public static IServiceCollection AddBusinessServices(this IServiceCollection services)
		{
			services.AddMediatR(typeof(BusinessLayer));
			services.Scan(scan => scan
				.FromAssemblyOf<BusinessLayer>()
				.AddClasses(classes => classes.AssignableTo<Profile>())
				.As<Profile>()
				.WithSingletonLifetime());
			services.AddSingleton(sp =>
			{
				var mapper = new MapperConfiguration(
					cfg =>
					{
						foreach (var profile in sp.GetServices<Profile>())
						{
							cfg.AddProfile(profile);
						}
					});

				mapper.AssertConfigurationIsValid();
				return mapper.CreateMapper();
			});

			services.AddValidatorsFromAssemblyContaining<BusinessLayer>();

			services
				.AddHttpClient(nameof(DownloadHttpClient))
				.AddTypedClient((client, servicesProvider) => new DownloadHttpClient());

			return services;
		}
	}
}
