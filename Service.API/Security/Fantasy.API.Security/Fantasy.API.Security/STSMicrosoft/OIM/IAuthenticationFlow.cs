using System.Text;
using System.Threading.Tasks;

namespace Fantasy.API.Security.STSMicrosoft.OIM
{
    public interface IAuthenticationFlow
    {

        Task ExecuteWorkflowAsync();
    }
}
