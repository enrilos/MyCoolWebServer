namespace MyCoolWebServer.ByTheCakeApplication.Controllers
{
    using Infrastructure;
    using Server.Http;
    using Server.Http.Contracts;
    using Server.Http.Response;
    using System.Collections.Generic;

    public class AccountController : Controller
    {
        public IHttpResponse Login()
        {
            this.ViewData["showError"] = "none";

            return this.FileViewResponse("account\\login");
        }

        public IHttpResponse Login(IHttpRequest request)
        {
            const string formNameKey = "name";
            const string formPasswordKey = "password";

            if (!request.FormData.ContainsKey(formNameKey)
                || !request.FormData.ContainsKey(formPasswordKey))
            {
                return new BadRequestResponse();
            }

            var name = request.FormData[formNameKey];
            var password = request.FormData[formPasswordKey];

            if (string.IsNullOrWhiteSpace(name)
                || string.IsNullOrWhiteSpace(password))
            {
                this.ViewData["error"] = "All credentials are mandatory";
                this.ViewData["showError"] = "block";

                this.FileViewResponse("account\\login");
            }

            if (true) // Check (the session) if the unique username is already logged in.
            {
                request.Session.Add(SessionStore.CurrentUserKey, name);
            }

            return new RedirectResponse("/");
        }
    }
}
