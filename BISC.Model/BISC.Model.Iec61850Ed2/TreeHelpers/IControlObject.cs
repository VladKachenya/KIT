using System;
using System.Collections.Generic;
using BISC.Model.Infrastructure.Common;

namespace BISC.Model.Iec61850Ed2.TreeHelpers
{
    public interface IControlObject : IObjectReferenced, IParentable
    {
        ctlModelsEnum? CtlModelValue { get; }
        Action<ctlModelsEnum> CtlModelValueChanged { get; set; }
        void Subscribe();
        string OrCat { get; set; }
        string OrIdent { get; set; }

        List<string> OrCatCollection { get; set; }
        string CtlVal { get; set; }
        List<string> CtlValCollection { get; set; }
        string StVal { get; }
        string CtlNum { get; set; }
        bool SynchroCheck { get; set; }
        bool InterlockCheck { get; set; }
        bool TestMode { get; set; }
    }
}