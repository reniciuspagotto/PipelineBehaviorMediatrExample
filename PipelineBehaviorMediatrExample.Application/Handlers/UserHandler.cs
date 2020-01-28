using MediatR;
using PipelineBehaviorMediatrExample.Application.Commands;
using PipelineBehaviorMediatrExample.Application.Response;
using PipelineBehaviorMediatrExample.Domain.Contracts;
using PipelineBehaviorMediatrExample.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace PipelineBehaviorMediatrExample.Application.Handlers
{
    public class UserHandler : IRequestHandler<CreateUserCommand, ApplicationResponse>
    {
        private readonly IUserRepository _userRepository;

        public UserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ApplicationResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User(request.Name, request.Email, request.Password);

            await _userRepository.Save(user);

            return new ApplicationResponse("Usuário salvo com sucesso");
        }
    }
}
