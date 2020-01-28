using MediatR;
using PipelineBehaviorMediatrExample.Application.Response;

namespace PipelineBehaviorMediatrExample.Application.Commands
{
    public class CreateUserCommand : IRequest<ApplicationResponse>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
