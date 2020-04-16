namespace MyCoolWebServer.CalculatorApplication.Controllers
{
    using Infrastructure;
    using Server.Http.Contracts;
    using System;
    using System.Collections.Generic;

    public class CalculatorController : Controller
    {
        public IHttpResponse Index() => this.FileViewResponse("\\index", new Dictionary<string, string>()
        {
            ["display"] = "none"
        });

        public IHttpResponse Index(string operandOne, string @operator, string operandTwo)
        {
            double result = 0;

            switch (@operator)
            {
                case "+": result = double.Parse(operandOne) + double.Parse(operandTwo); break;
                case "-": result = double.Parse(operandOne) - double.Parse(operandTwo); break;
                case "*": result = double.Parse(operandOne) * double.Parse(operandTwo); break;
                case "/": result = double.Parse(operandOne) / double.Parse(operandTwo); break;
                case "%": result = double.Parse(operandOne) % double.Parse(operandTwo); break;
                default: throw new InvalidOperationException("The operator must be an arithmetic one.");
            }
            return this.FileViewResponse("\\index", new Dictionary<string, string>
            {
                ["result"] = result.ToString(),
                ["display"] = "block"
            });
        }
    }
}
