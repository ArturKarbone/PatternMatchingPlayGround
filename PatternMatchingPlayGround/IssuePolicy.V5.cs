using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PatternMatchingPlayGround.V5
{
    public class IssuePolicyTests
    {
        [Fact]
        public void run()
        {
            var result = new IssuePolicy().Handle(new IssuePolicy.Request { Amount = 1, ClientId = 10, Product = "OCTA" });
            var respone = result.Match<string>(ok: ok => "1", errors => (-1).ToString());
        }
    }


    class Either<TOk, TError>
    {
        private readonly TOk _ok;
        private readonly TError _error;
        private readonly bool _isError;
        public Either(TOk ok) => (_ok, _isError) = (ok, false);
        public Either(TError error) => (_error, _isError) = (error, true);
        public T Match<T>(Func<TOk, T> ok, Func<TError, T> error)
            => _isError ? error(_error) : ok(_ok);
    }

    class ValidationErrors
    {
        public void Add(string error) => Errors.Add(error);
        public List<string> Errors { get; private set; } = new List<string>();
        public bool IsValid => !Errors.Any();
    }

    class IssuePolicy
    {
        public Either<Response, ValidationErrors> Handle(Request request)
        {
            var response = new Response();
            var errors = new ValidationErrors();

            if (!request.ClientId.HasValue)
            {
                errors.Add("specify client id");
            }

            if (request.Amount < 0)
            {
                errors.Add("amount < 0");
            }

            if (string.IsNullOrEmpty(request.Product))
            {
                errors.Add("specify product");
            }

            if (!errors.IsValid)
            {
                return new Either<Response, ValidationErrors>(errors);
            }
            //do some actions

            response.PolicyNumber = 10;

            return new Either<Response, ValidationErrors>(response);
        }

        public class Request
        {
            public int? ClientId { get; set; }
            public decimal Amount { get; set; }
            public string Product { get; set; }
        }

        public class Response
        {
            public int PolicyNumber { get; set; }
            public List<string> Errors { get; set; } = new List<string>();
            public bool IsValid => !Errors.Any();
        }
    }
}
