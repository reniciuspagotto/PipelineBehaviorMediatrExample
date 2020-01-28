using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PipelineBehaviorMediatrExample.Application.Response
{
    public class ApplicationResponse
    {
        private readonly IList<string> _messages = new List<string>();

        public IEnumerable<string> Errors { get; }
        public object Result { get; }

        public ApplicationResponse() => Errors = new ReadOnlyCollection<string>(_messages);

        public ApplicationResponse(object result) : this() => Result = result;

        public ApplicationResponse AddError(string message)
        {
            _messages.Add(message);
            return this;
        }
    }
}
