namespace MyCoolWebServer.Application.Views.Home
{
    using Server.Contracts;
    using System.IO;
    using System.Reflection;

    public class IndexView : IView
    {
        public string View()
        {
            string path = Path.GetFullPath(
                Path.Combine(
                    Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"..\..\..\Application\Views\Resources\index.html"));

            return File.ReadAllText(path);
        }
    }
}
