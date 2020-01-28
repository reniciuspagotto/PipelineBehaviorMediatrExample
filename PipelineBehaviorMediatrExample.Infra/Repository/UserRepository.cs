using PipelineBehaviorMediatrExample.Domain.Contracts;
using PipelineBehaviorMediatrExample.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace PipelineBehaviorMediatrExample.Infra.Repository
{
    public class UserRepository : IUserRepository
    {
        public async Task Save(User user)
        {
            Console.WriteLine("Salvo!");
            await Task.FromResult(true);
        }
    }
}
