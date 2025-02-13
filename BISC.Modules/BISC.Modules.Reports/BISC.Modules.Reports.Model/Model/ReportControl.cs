﻿using BISC.Model.Global.Model;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.Reports.Infrastructure.Keys;
using BISC.Modules.Reports.Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.Reports.Infrastructure.Services;
using BISC.Modules.Reports.Model.Services;

namespace BISC.Modules.Reports.Model.Model
{
    public class ReportControl : ModelElement, IReportControl
    {
        private IReportControlNameService _reportControlNameService = new ReportControlNameService();
        public ReportControl()
        {
            ElementName = ReportsKeys.ReportsModelKeys.ReportControlModelKey;
        }
        public string Name { get; set; }
        public string RptID { get; set; }
        public bool Buffered { get; set; }
        public long BufTime { get; set; }
        public string DataSet { get; set; }
        public long IntgPd { get; set; }
        public long ConfRev { get; set; }

        public bool IsDynamic => _reportControlNameService.GetIsDynamic(Name);


        public ChildModelProperty<ITrgOps> TrgOps => new ChildModelProperty<ITrgOps>(this, ReportsKeys.ReportsModelKeys.TrgOpsModelKey);
        public ChildModelProperty<IOptFields> OptFields => new ChildModelProperty<IOptFields>(this, ReportsKeys.ReportsModelKeys.OptFieldsModelKey);
        public ChildModelProperty<IRptEnabled> RptEnabled => new ChildModelProperty<IRptEnabled>(this, ReportsKeys.ReportsModelKeys.RptEnabledModelKey);
        public bool RptEnabledBool { get; set; }
        public bool GiBool { get; set; }


        public override bool ModelElementCompareTo(IModelElement obj)
        {
            if (!base.ModelElementCompareTo(obj)) return false;
            if (!(obj is IReportControl)) return false;
            var element = obj as IReportControl;
            if (element.Name != Name) return false;
            //if (element.RptID != RptID) return false;
            if (element.Buffered != Buffered) return false;
            if (element.BufTime != BufTime) return false;
            if (element.DataSet != DataSet) return false;
            //if (element.IntgPd != IntgPd) return false;
            if (element.ConfRev != ConfRev) return false;
            //if (element.IsDynamic != IsDynamic) return false;
            return true;
        }

        #region Implementation of ICloneable<out IReportControl>

        //public IReportControl Clone()
        //{
        //    IReportControl reportControl=new ReportControl();
        //    reportControl.Name = Name;
        //    reportControl.RptID = RptID;
        //    reportControl.BufTime = BufTime;
        //    reportControl.Buffered = Buffered;
        //    reportControl.DataSet = DataSet;
        //    reportControl.ConfRev = ConfRev;
        //    reportControl.GiBool = GiBool;
        //    reportControl.IntgPd = IntgPd;
        //    reportControl.IsDynamic = IsDynamic;
        //    return reportControl;
        //}

        #endregion
    }
}
