using FluentValidation;
using FluentValidation.Results;
using MediatR;
using PipelineBehaviorMediatrExample.Application.Response;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PipelineBehaviorMediatrExample.Application.Validators
{
    public class FailFastBehaviorValidator<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse> where TResponse : ApplicationResponse
    {
        private readonly IEnumerable<IValidator> _validators;

        public FailFastBehaviorValidator(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var context = new ValidationContext(request);
            var failures = _validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            return failures.Any()
                ? Errors(failures)
                : next();
        }

        private static Task<TResponse> Errors(IEnumerable<ValidationFailure> failures)
        {
            var response = new ApplicationResponse();

            foreach (var failure in failures)
            {
                response.AddError(failure.ErrorMessage);
            }

            return Task.FromResult(response as TResponse);
        }
    }
}
