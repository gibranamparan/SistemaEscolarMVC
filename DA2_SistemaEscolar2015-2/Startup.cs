using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DA2_SistemaEscolar2015_2.Startup))]
namespace DA2_SistemaEscolar2015_2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
