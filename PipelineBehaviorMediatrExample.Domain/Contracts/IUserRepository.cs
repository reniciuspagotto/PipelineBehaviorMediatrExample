using PipelineBehaviorMediatrExample.Domain.Entities;
using System.Threading.Tasks;

namespace PipelineBehaviorMediatrExample.Domain.Contracts
{
    public interface IUserRepository
    {
        Task Save(User user);
    }
}
