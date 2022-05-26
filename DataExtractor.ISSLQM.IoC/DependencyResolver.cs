using System;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using DataExtractor.ISSLQM.CsvParser.Implementation;
using DataExtractor.ISSLQM.CsvParser.Interface;

namespace DataExtractor.ISSLQM.IoC
{
    public class DependencyResolver
    {
        #region Declarations
        private readonly IWindsorContainer _container;
        #endregion Declarations

        #region Properties
        private static DependencyResolver resolver;
        public static DependencyResolver Instance
        {
            get
            {
                if (resolver == null) resolver = new DependencyResolver();
                return resolver;
            }
        }
        #endregion Properties

        #region Constructor
        public DependencyResolver()
        {
            _container = new WindsorContainer();
            this.Register(_container);
        }
        #endregion Constructor


        #region Methods
        private void Register(IWindsorContainer container)
        {
            try
            {
                #region Container Registration for Csv
                container.Register(
                    Component.For<ICsvReader>()
                        .ImplementedBy<CsvReaderNuget>()
                        .LifestylePerThread());

                container.Register(
                    Component.For<ICsvWriter>()
                        .ImplementedBy<CsvWriterNuget>()
                        .LifestylePerThread());
                #endregion Container Registration for Csv
            }
            catch (Exception ex)
            {
                throw new ApplicationException("A ComponentActivator error occurred. Please contact administrator.", ex);
            }
        }

        public T GetInstance<T>()
        {
            return _container.Resolve<T>();
        }
        #endregion Methods
    }
}
