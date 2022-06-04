using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(N01516955_Proposal_Project1.Startup))]
namespace N01516955_Proposal_Project1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
