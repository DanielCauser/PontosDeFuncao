using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ContagemPontosDeFuncao.Startup))]
namespace ContagemPontosDeFuncao
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
