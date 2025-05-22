using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using Ninject;
using GreenBite.Domain.Abstract;
using GreenBite.Domain.Entities;
using Moq;
using GreenBite.Domain.Concrete;
using System.Configuration;
using GreenBite.WebUI.Infrastructure.Abstract;
using GreenBite.WebUI.Infrastructure.Concrete;

namespace GreenBite.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel mykernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            mykernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type myserviceType)
        {
            return mykernel.TryGet(myserviceType);
        }

        public IEnumerable<object> GetServices(Type myserviceType)
        {
            return mykernel.GetAll(myserviceType);
        }

        private void AddBindings()
        {

            mykernel.Bind<ISaladRepository>().To<EFSaladRepository>();
            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse
                (ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            };

            mykernel.Bind<IOrderProcessor>()
                            .To<EmailOrderProcessor>()
                            .WithConstructorArgument("settings", emailSettings);

            // Authentication
            mykernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
        }        
    }
}