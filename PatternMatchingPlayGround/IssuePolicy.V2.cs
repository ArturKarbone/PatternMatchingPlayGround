using System;
using Xunit;

namespace PatternMatchingPlayGround.V2
{
    public class IssuePolicyTests
    {
        [Fact]
        public void run()
        {
            var result = new IssuePolicy().Handle(new IssuePolicy.Request { Amount = 1, ClientId = 10, Product = "OCTA" });

            var respone = result switch
            {
                (_, ArgumentNullException e, _, _) => -1,
                (_, _, ArgumentNullException e, _) => -2,
                (_, _, _, ArgumentNullException e) => -3,
                (IssuePolicy.Response p, _, _, _) when p.PolicyNumber == 10 => 1,//WHEN?
            };
        }
    }

    class IssuePolicy
    {
        public (Response response, ArgumentNullException clientIdException, ArgumentNullException amountException, ArgumentNullException productException) Handle(Request request)
        {
            ArgumentNullException clientIdException = null;
            ArgumentNullException amountException = null;
            ArgumentNullException productException = null;


            if (!request.ClientId.HasValue)
            {
                clientIdException = new ArgumentNullException("specify client id");
            }

            if (request.Amount < 0)
            {
                amountException = new ArgumentNullException("amount < 0");
            }

            if (string.IsNullOrEmpty(request.Product))
            {
                productException = new ArgumentNullException("specify product");
            }

            //do some actions

            return (new Response { PolicyNumber = 10 }, clientIdException, amountException, productException);
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
        }
    }

}
