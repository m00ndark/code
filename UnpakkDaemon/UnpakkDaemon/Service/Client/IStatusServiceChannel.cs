using System.ServiceModel;
using UnpakkDaemon.Service.Common;

namespace UnpakkDaemon.Service.Client
{
	public interface IStatusServiceChannel : IStatusService, IClientChannel { }
}
