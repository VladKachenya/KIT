using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Modularity;
using BISC.Model.Iec61850Ed2.DataTypeTemplates;
using BISC.Model.Infrastructure.Services;

namespace BISC.Model.Iec61850Ed2.Module
{
   public class Iec61850Ed2Module:IAppModule
    {
        private readonly IInjectionContainer _injectionContainer;
        private IModelTypesResolvingService _modelTypesResolvingService;

        public Iec61850Ed2Module(IInjectionContainer injectionContainer)
        {
            _injectionContainer = injectionContainer;
        }


        #region Implementation of IAppModule

        public void Initialize()
        {
            _modelTypesResolvingService = _injectionContainer.ResolveType<IModelTypesResolvingService>();
            _modelTypesResolvingService.RegisterType(typeof(CommonLogicalNode), typeof(LNTypesEd2.LLN0), "LLN0",2);
            _modelTypesResolvingService.RegisterType(typeof(CommonLogicalNode), typeof(LNTypesEd2.RDRE), "RDRE", 2);
            _modelTypesResolvingService.RegisterType(typeof(CommonLogicalNode), typeof(LNTypesEd2.LPHD), "LPHD", 2);
            _modelTypesResolvingService.RegisterType(typeof(CommonLogicalNode), typeof(LNTypesEd2.PTOC), "PTOC", 2);
            _modelTypesResolvingService.RegisterType(typeof(CommonLogicalNode), typeof(LNTypesEd2.PTOV), "PTOV", 2);
            _modelTypesResolvingService.RegisterType(typeof(CommonLogicalNode), typeof(LNTypesEd2.GGIO), "GGIO", 2);
            _modelTypesResolvingService.RegisterType(typeof(CommonLogicalNode), typeof(LNTypesEd2.PTOF), "PTOF", 2);
            _modelTypesResolvingService.RegisterType(typeof(CommonLogicalNode), typeof(LNTypesEd2.PTUF), "PTUF", 2);
            _modelTypesResolvingService.RegisterType(typeof(CommonLogicalNode), typeof(LNTypesEd2.PDUP), "PDUP", 2);
            _modelTypesResolvingService.RegisterType(typeof(CommonLogicalNode), typeof(LNTypesEd2.PTTR), "PTTR", 2);
            _modelTypesResolvingService.RegisterType(typeof(CommonLogicalNode), typeof(LNTypesEd2.RREC), "RREC", 2);
            _modelTypesResolvingService.RegisterType(typeof(CommonLogicalNode), typeof(LNTypesEd2.RSYN), "RSYN", 2);
            _modelTypesResolvingService.RegisterType(typeof(CommonLogicalNode), typeof(LNTypesEd2.RBRF), "RBRF", 2);
            _modelTypesResolvingService.RegisterType(typeof(CommonLogicalNode), typeof(LNTypesEd2.RPSB), "RPSB", 2);
            _modelTypesResolvingService.RegisterType(typeof(CommonLogicalNode), typeof(LNTypesEd2.CSWI), "CSWI", 2);
            _modelTypesResolvingService.RegisterType(typeof(CommonLogicalNode), typeof(LNTypesEd2.PDIS), "PDIS", 2);
            _modelTypesResolvingService.RegisterType(typeof(CommonLogicalNode), typeof(LNTypesEd2.XCBR), "XCBR", 2);
            _modelTypesResolvingService.RegisterType(typeof(CommonLogicalNode), typeof(LNTypesEd2.PTRC), "PTRC", 2);
            _modelTypesResolvingService.RegisterType(typeof(CommonLogicalNode), typeof(LNTypesEd2.MMXU), "MMXU", 2);
            _modelTypesResolvingService.RegisterType(typeof(CommonLogicalNode), typeof(LNTypesEd2.MSQI), "MSQI", 2);
            _modelTypesResolvingService.RegisterType(typeof(CommonLogicalNode), typeof(LNTypesEd2.RFLO), "RFLO", 2);
            _modelTypesResolvingService.RegisterType(typeof(CommonLogicalNode), typeof(LNTypesEd2.PTUC), "PTUC", 2);
            _modelTypesResolvingService.RegisterType(typeof(CommonLogicalNode), typeof(LNTypesEd2.PTUV), "PTUV", 2);
            _modelTypesResolvingService.RegisterType(typeof(CommonLogicalNode), typeof(LNTypesEd2.PUPF), "PUPF", 2);
            _modelTypesResolvingService.RegisterType(typeof(CommonLogicalNode), typeof(LNTypesEd2.PDIF), "PDIF", 2);
            _modelTypesResolvingService.RegisterType(typeof(CommonLogicalNode), typeof(LNTypesEd2.MMXN), "MMXN", 2);
            _modelTypesResolvingService.RegisterType(typeof(CommonLogicalNode), typeof(LNTypesEd2.ATCC), "ATCC", 2);
            _modelTypesResolvingService.SetLnStub(() => typeof(LogicalNodeTypeStub));
        }

        #endregion
    }
}
