namespace MyCoolWebServer.Application.Views.AddCake
{
    using Server.Contracts;
    using System.IO;
    using System.Reflection;

    public class AddCakeView : IView
    {
        public string View()
        {
            string path = Path.GetFullPath(
                   Path.Combine(
                       Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"..\..\..\Application\Views\Resources\add.html"));

            return File.ReadAllText(path);
        }
    }
}
