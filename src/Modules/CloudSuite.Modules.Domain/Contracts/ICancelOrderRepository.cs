using CloudSuite.Modules.Common.ValueObjects;
using CloudSuite.Modules.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Domain.Contracts
{
	public interface ICancelOrderRepository
	{
		Task<CancelOrder> GetbyCnpj(Cnpj cnpj);

		Task<CancelOrder> GetByRequestDate(DateTimeOffset requestDate);

		Task<IEnumerable<CancelOrder>> GetList();

		Task Add(CancelOrder cancelOrder);

		void Update(CancelOrder cancelOrder);

		void Remove(CancelOrder cancelOrder);
	}
}
