using System;
using Xunit;

namespace PatternMatchingPlayGround.V1
{
    public class UnitTest1
    {
        [Fact]
        public void run()
        {
            var result = new IssuePolicy().Handle(new IssuePolicy.Request { Amount = 1, ClientId = 10, Product = "OCTA" });
        }
    }

    class IssuePolicy
    {

        public Response Handle(Request request)
        {
            if (!request.ClientId.HasValue)
            {
                throw new ArgumentNullException("specify client id");
            }

            if (request.Amount < 0)
            {
                throw new ArgumentNullException("amount < 0");
            }

            if (string.IsNullOrEmpty(request.Product))
            {
                throw new ArgumentNullException("specify product");
            }

            //do some actions

            return new Response
            {
                Success = true
            };
        }

        public class Request
        {
            public int? ClientId { get; set; }
            public decimal Amount { get; set; }
            public string Product { get; set; }
        }

        public class Response
        {
            public bool Success { get; set; }
        }
    }
}
