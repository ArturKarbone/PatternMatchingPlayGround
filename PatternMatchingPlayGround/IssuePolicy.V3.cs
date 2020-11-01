using System;
using Xunit;

namespace PatternMatchingPlayGround.V3
{
    public class IssuePolicyTests
    {
        [Fact]
        public void run()
        {
            var result = new IssuePolicy().Handle(new IssuePolicy.Request { Amount = 1, ClientId = 10, Product = "OCTA" });

            var respone = result switch
            {
                (_, ClientIdException e, _, _) => -1,
                (_, _, AmountException e, _) => -2,
                (_, _, _, ProductException e) => -3,
                (IssuePolicy.Response p, _, _, _) when p.PolicyNumber == 10 => 1,//WHEN?
                _ => throw new NotImplementedException(),
            };
        }
    }

    class ClientIdException : ArgumentNullException
    {
        public ClientIdException(string paramName) : base(paramName)
        {

        }
    }

    class AmountException : ArgumentNullException
    {
        public AmountException(string paramName) : base(paramName)
        {

        }
    }

    class ProductException : ArgumentNullException
    {
        public ProductException(string paramName) : base(paramName)
        {

        }
    }

    class IssuePolicy
    {

        public (Response response, ClientIdException clientIdException, AmountException amountException, ProductException productException) Handle(Request request)
        {
            ClientIdException clientIdException = null;
            AmountException amountException = null;
            ProductException productException = null;


            if (!request.ClientId.HasValue)
            {
                clientIdException = new ClientIdException("specify client id");
            }

            if (request.Amount < 0)
            {
                amountException = new AmountException("amount < 0");
            }

            if (string.IsNullOrEmpty(request.Product))
            {
                productException = new ProductException("specify product");
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
