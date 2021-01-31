using HCMSSMI.Reader;
using HCMSSMI.Writer;
using Ninject;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HCMSSMI.DI
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        #region Private Members

        /// <summary>
        ///  A single instance from <see cref="Ninject"/>
        /// </summary>
        private IKernel kernel;

        #endregion

        #region Constructor

        public NinjectDependencyResolver(IKernel kernel)
        {
            this.kernel = kernel;
            AddBinding();
        }

        #endregion

        #region Ninject Methods

        public object GetService(Type serviceType) => kernel.TryGet(serviceType);

        public IEnumerable<object> GetServices(Type serviceType) => kernel.GetAll(serviceType);

        #endregion

        #region Private Helpers

        private void AddBinding()
        {
            kernel.Bind<IReaderService>().To<ReaderService>();
            kernel.Bind<IWriterService>().To<WriterService>();
            kernel.Bind<IOAuth>().To<OAuth>();

        }

        #endregion


    }
}