namespace MyCoolWebServer.Application.Views.BrowseCakes
{
    using Server.Contracts;
    using System.IO;
    using System.Reflection;

    public class BrowseCakesView : IView
    {
        public string View()
        {
            string path = Path.GetFullPath(
                   Path.Combine(
                       Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"..\..\..\Application\Views\Resources\search.html"));

            return File.ReadAllText(path);
        }
    }
}
