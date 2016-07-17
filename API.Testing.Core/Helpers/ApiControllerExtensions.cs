namespace API.Testing.Core.Helpers
{
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Hosting;
    using System.Web.Http.Routing;

    public static class ApiControllerExtensions
    {
        public static void MockRequest(this ApiController self, HttpMethod httpMethod, HttpRouteValueDictionary routesValues)
        {
            var config = new HttpConfiguration();
            const string requestUri = "http://localhost:11111/api";
            var request = new HttpRequestMessage(httpMethod, requestUri);
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, routesValues);

            self.ControllerContext = new HttpControllerContext(config, routeData, request);
            self.Request = request;
            self.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }
    }
}
