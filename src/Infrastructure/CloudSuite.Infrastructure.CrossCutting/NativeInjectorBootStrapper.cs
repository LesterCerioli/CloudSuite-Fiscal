using CloudSuite.Infrastructure.Context;
using CloudSuite.Modules.Domain.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Infrastructure.CrossCutting
{
	public class NativeInjectorBootStrapper
	{
		public static void RegisterServices(IServiceCollection services)
		{
			// Infrastructure
			services.AddScoped<IAddressRepository>();
			services.AddScoped<ICancelOrderRepository>();
			services.AddScoped<ICityRepository>();
			services.AddScoped<ICountryRepository>();
			services.AddScoped<IDarfRepository>();
			services.AddScoped<IDASRepository>();
			services.AddScoped<IDeclaracaoIRRepository>();
			services.AddScoped<IDistrictRepository>();
			services.AddScoped<IFederalTaxRepository>();
			services.AddScoped<IIdeCancelamentoRepository>();
			services.AddScoped<INoteRepository>();
			services.AddScoped<IPrestadorRepository>();
			services.AddScoped<IStateRepository>();
			services.AddScoped<ITomadorServicoRepository>();
			services.AddScoped<FiscalDbContext>();

			// Application
			//services.AddScoped<IAddressAppService, AddressAppService>();

		}
	}
}
