using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PatternMatchingPlayGround.V4
{
    public class IssuePolicyTests
    {
        [Fact]
        public void run()
        {
            var result = new IssuePolicy().Handle(new IssuePolicy.Request { Amount = 1, ClientId = 10, Product = "OCTA" });

            var respone = result switch
            {
                IssuePolicy.Response r when r.IsValid => 1,
                IssuePolicy.Response r when !r.IsValid => -1,
                _ => throw new NotImplementedException(),
            };
        }
    }


    class IssuePolicy
    {

        public Response Handle(Request request)
        {
            var response = new Response();


            if (!request.ClientId.HasValue)
            {
                response.Errors.Add("specify client id");
            }

            if (request.Amount < 0)
            {
                response.Errors.Add("amount < 0");
            }

            if (string.IsNullOrEmpty(request.Product))
            {
                response.Errors.Add("specify product");
            }

            //do some actions

            response.PolicyNumber = 10;

            return response;
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
