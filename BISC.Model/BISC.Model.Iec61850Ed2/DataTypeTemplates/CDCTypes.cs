using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using BISC.Model.Iec61850Ed2.Common;
using BISC.Modules.Connection.Infrastructure.Connection;

namespace BISC.Model.Iec61850Ed2.DataTypeTemplates
{   
    public class SGCB : DOData
    {
        public SGCB()
    {
        cdc = tCDCEnumEd2.CURVE;
    }
    public INT32U NumOfSG { get; set; }
    public INT32U ActSG { get; set; }
    public INT32U EditSG { get; set; }
    public BOOLEAN CnfEdit { get; set; }
    public TimeStamp LActTm { get; set; }
    public INT32U ResvTms { get; set; }
}

//#region MyRegion







///// <summary>
///// This class defines the common data attribute types that are defined for his use in the ACD
///// common data class. 
///// </summary>
//public class ACD : DOData
//{
//    /// <summary>
//    /// Directional protection activation information (ACD)
//    /// </summary>
//    /// <param name="name">
//    /// A name identifying the DO
//    /// </param>
//    /// <param name="lnType">
//    /// Containing more detailed functional specification about LN. 
//    /// </param>
//    /// <param name="iedType">
//    /// The manufacturer IED type of the IED to which this LN type belongs.
//    /// </param>
//    public ACD(string name, string lnType, string iedType) : base(name, lnType + name)
//    {
//        cdc = tCDCEnumEd1.ACD;
//        id = type;
//        this.iedType = iedType;
//        this.cdcName = new VisString255("cdcName", tFCEnum.EX);
//        this.cdcNs = new VisString255("cdcNs", tFCEnum.EX);
//        this.d = new VisString255("d", tFCEnum.DC);
//        this.dataNs = new VisString255("dataNs", tFCEnum.EX);
//        this.dirGeneral = new dirGeneral(tFCEnum.ST);
//        this.dirNeut = new dirNeut(tFCEnum.ST);
//        this.dirPhsA = new dirPhsA(tFCEnum.ST);
//        this.dirPhsB = new dirPhsB(tFCEnum.ST);
//        this.dirPhsC = new dirPhsC(tFCEnum.ST);
//        this.dU = new Unicode255("dU", tFCEnum.DC);
//        this.general = new BOOLEAN("general", tFCEnum.ST);
//        this.neut = new BOOLEAN("neut", tFCEnum.ST);
//        this.phsA = new BOOLEAN("phsA", tFCEnum.ST);
//        this.phsB = new BOOLEAN("phsB", tFCEnum.ST);
//        this.phsC = new BOOLEAN("phsC", tFCEnum.ST);
//        this.q = new Quality("q", tFCEnum.ST);
//        this.t = new Timestamp("t", tFCEnum.ST);
//    }


//    public ACD()
//    {
//        cdc = tCDCEnumEd1.ACD;
//    }

//    [Browsable(false)]
//    public VisString255 cdcName { get; set; }

//    [Browsable(false)]
//    public VisString255 cdcNs { get; set; }

//    [Browsable(false)]
//    public VisString255 d { get; set; }

//    [Browsable(false)]
//    public VisString255 dataNs { get; set; }

//    [Required]
//    [Browsable(false)]
//    public dirGeneral dirGeneral { get; set; }

//    [Browsable(false)]
//    public dirNeut dirNeut { get; set; }

//    [Browsable(false)]
//    public dirPhsA dirPhsA { get; set; }

//    [Browsable(false)]
//    public dirPhsB dirPhsB { get; set; }

//    [Browsable(false)]
//    public dirPhsC dirPhsC { get; set; }

//    [Browsable(false)]
//    public Unicode255 dU { get; set; }

//    [Required]
//    [Browsable(false)]
//    public BOOLEAN general { get; set; }

//    [Browsable(false)]
//    public BOOLEAN neut { get; set; }

//    [Browsable(false)]
//    public BOOLEAN phsA { get; set; }

//    [Browsable(false)]
//    public BOOLEAN phsB { get; set; }

//    [Browsable(false)]
//    public BOOLEAN phsC { get; set; }

//    [Required]
//    [Browsable(false)]
//    public Quality q { get; set; }

//    [Required]
//    [Browsable(false)]
//    public Timestamp t { get; set; }

//}

///// <summary>
///// This class defines the common data attribute types that are defined for his use in the ACT
///// common data class. 
///// </summary>
//public class ACT : DOData
//{
//    /// <summary>
//    /// Protection activation information (ACT)
//    /// </summary>
//    /// <param name="name">
//    /// A name identifying the DO.
//    /// </param>
//    /// <param name="lnType">
//    /// Containing more detailed functional specification about LN.
//    /// </param>
//    /// <param name="iedType">
//    /// The manufacturer IED type of the IED to which this LN type belongs.
//    /// </param>
//    public ACT(string name, string lnType, string iedType) : base(name, lnType + name)
//    {
//        cdc = tCDCEnumEd1.ACT;
//        id = type;
//        this.iedType = iedType;
//        this.cdcName = new VisString255("cdcName", tFCEnum.EX);
//        this.cdcNs = new VisString255("cdcNs", tFCEnum.EX);
//        this.d = new VisString255("d", tFCEnum.DC);
//        this.dataNs = new VisString255("dataNs", tFCEnum.EX);
//        this.dU = new Unicode255("dU", tFCEnum.DC);
//        this.general = new BOOLEAN("general", tFCEnum.ST);
//        this.neut = new BOOLEAN("neut", tFCEnum.ST);
//        this.operTm = new Timestamp("operTm", tFCEnum.CF);
//        this.phsA = new BOOLEAN("phsA", tFCEnum.ST);
//        this.phsB = new BOOLEAN("phsB", tFCEnum.ST);
//        this.phsC = new BOOLEAN("phsC", tFCEnum.ST);
//        this.q = new Quality("q", tFCEnum.ST);
//        this.t = new Timestamp("t", tFCEnum.ST);
//    }

//    public ACT()
//    {
//        cdc = tCDCEnumEd1.ACT;
//    }
//    [Browsable(false)]
//    public VisString255 cdcName { get; set; }

//    [Browsable(false)]
//    public VisString255 cdcNs { get; set; }

//    [Browsable(false)]
//    public VisString255 d { get; set; }

//    [Browsable(false)]
//    public VisString255 dataNs { get; set; }

//    [Browsable(false)]
//    public Unicode255 dU { get; set; }

//    [Required]
//    [Browsable(false)]
//    public BOOLEAN general { get; set; }

//    [Browsable(false)]
//    public BOOLEAN neut { get; set; }

//    [Browsable(false)]
//    public Timestamp operTm { get; set; }

//    [Browsable(false)]
//    public BOOLEAN phsA { get; set; }

//    [Browsable(false)]
//    public BOOLEAN phsB { get; set; }

//    [Browsable(false)]
//    public BOOLEAN phsC { get; set; }

//    [Required]
//    [Browsable(false)]
//    public Quality q { get; set; }

//    [Required]
//    [Browsable(false)]
//    public Timestamp t { get; set; }
//}

///// <summary>
///// This class defines the common data attribute types that are defined for his use in the APC
///// common data class.
///// </summary>
//public class APC : DOData
//{
//    /// <summary>
//    /// Controllable analogue set point information (APC)
//    /// </summary>
//    /// <param name="name">
//    /// A name identifying the DO.
//    /// </param>
//    /// <param name="lnType">
//    /// Containing more detailed functional specification about LN.
//    /// </param>
//    /// <param name="iedType">
//    /// The manufacturer IED type of the IED to which this LN type belongs.
//    /// </param>
//    public APC(string name, string lnType, string iedType) : base(name, lnType + name)
//    {
//        cdc = tCDCEnumEd1.APC;
//        id = type;
//        this.iedType = iedType;
//        this.cdcName = new VisString255("cdcName", tFCEnum.EX);
//        this.cdcNs = new VisString255("cdcNs", tFCEnum.EX);
//        this.ctlModel = new ctlModel(iedType, id, tFCEnum.CF);
//        this.d = new VisString255("d", tFCEnum.DC);
//        this.dataNs = new VisString255("dataNs", tFCEnum.EX);
//        this.dU = new Unicode255("dU", tFCEnum.DC);
//        this.maxVal = new maxVal(iedType, id, tFCEnum.CF);
//        this.minVal = new minVal(iedType, id, tFCEnum.CF);
//        this.operTm = new Timestamp("operTm", tFCEnum.SP);
//        this.origin = new origin(iedType, id, tFCEnum.SP);
//        this.q = new Quality("q", tFCEnum.MX);
//        this.setMag2 = new setMag2(iedType, id, tFCEnum.SP);
//        this.stepSize = new stepSize(iedType, id, tFCEnum.CF);
//        this.sVC = new sVC(iedType, id, tFCEnum.CF);
//        this.t = new Timestamp("t", tFCEnum.MX);
//        this.units = new units(iedType, id, tFCEnum.CF);
//        this.SBO = new VisString65("SBO", tFCEnum.SP);
//        this.SBOw = new SBOw(iedType, id, tFCEnum.SP);
//        this.Oper = new Oper(iedType, id, tFCEnum.SP);
//        this.Cancel = new Cancel(iedType, id, tFCEnum.SP);
//    }
//    public APC()
//    {
//        cdc = tCDCEnumEd1.APC;
//    }
//    [Browsable(false)]
//    public VisString255 cdcName { get; set; }

//    [Browsable(false)]
//    public VisString255 cdcNs { get; set; }

//    [Required]
//    [Browsable(false)]
//    public ctlModel ctlModel { get; set; }

//    [Browsable(false)]
//    public VisString255 d { get; set; }

//    [Browsable(false)]
//    public VisString255 dataNs { get; set; }

//    [Browsable(false)]
//    public Unicode255 dU { get; set; }

//    [Browsable(false)]
//    public maxVal maxVal { get; set; }

//    [Browsable(false)]
//    public minVal minVal { get; set; }

//    [Browsable(false)]
//    public Timestamp operTm { get; set; }

//    [Browsable(false)]
//    public origin origin { get; set; }

//    [Required]
//    [Browsable(false)]
//    public Quality q { get; set; }

//    [Required]
//    [Browsable(false)]
//    public setMag2 setMag2 { get; set; }

//    [Browsable(false)]
//    public stepSize stepSize { get; set; }

//    [Browsable(false)]
//    public sVC sVC { get; set; }

//    [Required]
//    [Browsable(false)]
//    public Timestamp t { get; set; }

//    [Browsable(false)]
//    public units units { get; set; }

//    [Browsable(false)]
//    public VisString65 SBO { get; set; }

//    [Browsable(false)]
//    public SBOw SBOw { get; set; }

//    [Browsable(false)]
//    public Oper Oper { get; set; }

//    [Browsable(false)]
//    public Cancel Cancel { get; set; }
//}

///// <summary>
///// This class defines the common data attribute types that are defined for his use in the ASG
///// common data class.
///// </summary>
//public class ASG : DOData
//{
//    /// <summary>
//    /// Analogue setting (ASG)
//    /// </summary>
//    /// <param name="name">
//    /// A name identifying the DO.
//    /// </param>
//    /// <param name="lnType">
//    /// Containing more detailed functional specification about LN. 
//    /// </param>
//    /// <param name="iedType">
//    /// The manufacturer IED type of the IED to which this LN type belongs.
//    /// </param>
//    public ASG(string name, string lnType, string iedType) : base(name, lnType + name)
//    {
//        cdc = tCDCEnumEd1.ASG;
//        id = type;
//        this.iedType = iedType;
//        this.cdcName = new VisString255("cdcName", tFCEnum.EX);
//        this.cdcNs = new VisString255("cdcNs", tFCEnum.EX);
//        this.d = new VisString255("d", tFCEnum.DC);
//        this.dataNs = new VisString255("dataNs", tFCEnum.EX);
//        this.dU = new Unicode255("dU", tFCEnum.DC);
//        this.maxVal = new maxVal(iedType, id, tFCEnum.CF);
//        this.minVal = new minVal(iedType, id, tFCEnum.CF);
//        this.setMag = new setMag(iedType, id, tFCEnum.SP);
//        this.setMag2 = new setMag2(iedType, id, tFCEnum.SG);
//        this.stepSize = new stepSize(iedType, id, tFCEnum.CF);
//        this.sVC = new sVC(iedType, id, tFCEnum.CF);
//        this.units = new units(iedType, id, tFCEnum.CF);
//    }
//    public ASG()
//    {
//        cdc = tCDCEnumEd1.ASG;
//    }

//    [Browsable(false)]
//    public VisString255 cdcName { get; set; }

//    [Browsable(false)]
//    public VisString255 cdcNs { get; set; }

//    [Browsable(false)]
//    public VisString255 d { get; set; }

//    [Browsable(false)]
//    public VisString255 dataNs { get; set; }

//    [Browsable(false)]
//    public Unicode255 dU { get; set; }

//    [Browsable(false)]
//    public maxVal maxVal { get; set; }

//    [Browsable(false)]
//    public minVal minVal { get; set; }

//    [Browsable(false)]
//    public setMag setMag { get; set; }

//    [Browsable(false)]
//    public setMag2 setMag2 { get; set; }

//    [Browsable(false)]
//    public stepSize stepSize { get; set; }

//    [Browsable(false)]
//    public sVC sVC { get; set; }

//    [Browsable(false)]
//    public units units { get; set; }
//}

///// <summary>
///// This class defines the common data attribute types that are defined for his use in the BCR
///// common data class.
///// </summary>
//public class BCR : DOData
//{

//    public BCR()
//    {
//        cdc = tCDCEnumEd1.BCR;
//    }

//    [Required]
//    [Browsable(false)]
//    public INT128 actVal { get; set; }

//    [Browsable(false)]
//    public VisString255 cdcName { get; set; }

//    [Browsable(false)]
//    public VisString255 cdcNs { get; set; }

//    [Browsable(false)]
//    public VisString255 d { get; set; }

//    [Browsable(false)]
//    public VisString255 dataNs { get; set; }

//    [Browsable(false)]
//    public Unicode255 dU { get; set; }

//    [Browsable(false)]
//    public BOOLEAN frEna { get; set; }

//    [Browsable(false)]
//    public INT32 frPd { get; set; }

//    [Browsable(false)]
//    public BOOLEAN frRs { get; set; }

//    [Browsable(false)]
//    public Timestamp frTm { get; set; }

//    [Browsable(false)]
//    public INT128 frVal { get; set; }

//    [Required]
//    [Browsable(false)]
//    public FLOAT32 pulsQty { get; set; }

//    [Required]
//    [Browsable(false)]
//    public Quality q { get; set; }

//    [Browsable(false)]
//    public TimeStamp strTm { get; set; }

//    [Required]
//    [Browsable(false)]
//    public TimeStamp t { get; set; }

//    [Browsable(false)]
//    public Unit units { get; set; }
//}

///// <summary>
///// This class defines the common data attribute types that are defined for his use in the BSC
///// common data class.
///// </summary>
//public class BSC : DOData
//{

//    public BSC()
//    {
//        cdc = tCDCEnumEd1.BSC;
//    }

//    [Browsable(false)]
//    public VisString255 cdcName { get; set; }

//    [Browsable(false)]
//    public VisString255 cdcNs { get; set; }

//    [Required]
//    [Browsable(false)]
//    public ctlModel ctlModel { get; set; }

//    [Browsable(false)]
//    public INT8U ctlNum { get; set; }

//    [Browsable(false)]
//    public ctlVal ctlVal { get; set; }

//    [Browsable(false)]
//    public VisString255 d { get; set; }

//    [Browsable(false)]
//    public VisString255 dataNs { get; set; }

//    [Browsable(false)]
//    public Unicode255 dU { get; set; }

//    [Browsable(false)]
//    public INT8 maxVal { get; set; }

//    [Browsable(false)]
//    public INT8 minVal { get; set; }

//    [Browsable(false)]
//    public TimeStamp operTm { get; set; }

//    [Browsable(false)]
//    public Originator origin { get; set; }

//    [Required]
//    [Browsable(false)]
//    public BOOLEAN persistent { get; set; }

//    [Browsable(false)]
//    public Quality q { get; set; }

//    [Browsable(false)]
//    public sboClass sboClass { get; set; }

//    [Browsable(false)]
//    public INT32U sboTimeout { get; set; }

//    [Browsable(false)]
//    public INT8U stepSize { get; set; }

//    [Browsable(false)]
//    public BOOLEAN stSeldField { get; set; }

//    [Browsable(false)]
//    public BOOLEAN subEna { get; set; }

//    [Browsable(false)]
//    public VisString64 subID { get; set; }

//    [Browsable(false)]
//    public Quality subQ { get; set; }

//    [Browsable(false)]
//    public subVal subVal { get; set; }

//    [Browsable(false)]
//    public TimeStamp t { get; set; }

//    [Browsable(false)]
//    public valWTr valWTr { get; set; }

//    [Browsable(false)]
//    public VisString65 SBO { get; set; }

//    [Browsable(false)]
//    public SBOw SBOw { get; set; }

//    [Browsable(false)]
//    public Oper Oper { get; set; }

//    [Browsable(false)]
//    public Cancel Cancel { get; set; }
//}

///// <summary>
///// This class defines the common data attribute types that are defined for his use in the CMV
///// common data class.
///// </summary>
//public class CMV : DOData
//{

//    public CMV()
//    {
//        cdc = tCDCEnumEd1.CMV;
//        db = new INT32U();
//        db.fc = tFCEnum.CF;
//        zeroDb = new INT32U();
//        zeroDb.fc = tFCEnum.CF;

//    }

//    [Browsable(false)]
//    public angRef angRef { get; set; }

//    [Browsable(false)]
//    public angSVC angSVC { get; set; }

//    [Browsable(false)]
//    public VisString255 cdcName { get; set; }

//    [Browsable(false)]
//    public VisString255 cdcNs { get; set; }

//    [Required]
//    [Browsable(false)]
//    public cVal cVal { get; set; }

//    [Browsable(false)]
//    public VisString255 d { get; set; }

//    [Browsable(false)]
//    public VisString255 dataNs { get; set; }

//    [Browsable(false)]
//    public INT32U db { get; set; }

//    [Browsable(false)]
//    public Unicode255 dU { get; set; }

//    [Browsable(false)]
//    public instCVal instCVal { get; set; }

//    [Browsable(false)]
//    public magSVC magSVC { get; set; }

//    [Required]
//    [Browsable(false)]
//    public Quality q { get; set; }

//    [Browsable(false)]
//    public range range { get; set; }

//    [Browsable(false)]
//    public rangeC rangeC { get; set; }

//    [Browsable(false)]
//    public INT32U smpRate { get; set; }

//    [Browsable(false)]
//    public subCVal subCVal { get; set; }

//    [Browsable(false)]
//    public BOOLEAN subEna { get; set; }

//    [Browsable(false)]
//    public VisString64 subID { get; set; }

//    [Browsable(false)]
//    public Quality subQ { get; set; }

//    [Required]
//    [Browsable(false)]
//    public TimeStamp t { get; set; }

//    [Browsable(false)]
//    public Unit units { get; set; }

//    [Browsable(false)]
//    public INT32U zeroDb { get; set; }
//}

///// <summary>
///// This class defines the common data attribute types that are defined for his use in the CSD
///// common data class.
///// </summary>
//public class CSD : DOData
//{
//    /// <summary>
//    /// Curve shape description (CSD)
//    /// </summary>
//    /// <param name="name">
//    /// A name identifying the DO.
//    /// </param>
//    /// <param name="lnType">
//    /// Containing more detailed functional specification about LN.  
//    /// </param>
//    /// <param name="iedType">
//    /// The manufacturer IED type of the IED to which this LN type belongs.
//    /// </param>


//    public CSD()
//    {
//        cdc = tCDCEnumEd1.CSD;
//    }

//    [Browsable(false)]
//    public VisString255 cdcName { get; set; }

//    [Browsable(false)]
//    public VisString255 cdcNs { get; set; }

//    [Required]
//    [Browsable(false)]
//    public crvPts crvPts { get; set; }

//    [Required]
//    [Browsable(false)]
//    public VisString255 d { get; set; }

//    [Browsable(false)]
//    public VisString255 dataNs { get; set; }

//    [Browsable(false)]
//    public Unicode255 dU { get; set; }

//    [Required]
//    [Browsable(false)]
//    public INT16U numPts { get; set; }

//    [Required]
//    [Browsable(false)]
//    public VisString255 xD { get; set; }

//    [Required]
//    [Browsable(false)]
//    public Unit xUnit { get; set; }

//    [Required]
//    [Browsable(false)]
//    public VisString255 yD { get; set; }

//    [Required]
//    [Browsable(false)]
//    public Unit yUnit { get; set; }
//}

///// <summary>
///// This class defines the common data attribute types that are defined for his use in the CURVE
///// common data class.
///// </summary>
//public class CURVE : DOData
//{
//    /// <summary>
//    /// Setting curve (CURVE)
//    /// </summary>
//    /// <param name="name">
//    /// A name identifying the DO.
//    /// </param>
//    /// <param name="lnType">
//    /// Containing more detailed functional specification about LN. 
//    /// </param>
//    /// <param name="iedType">
//    /// The manufacturer IED type of the IED to which this LN type belongs.
//    /// </param>
//    public CURVE(string name, string lnType, string iedType) : base(name, lnType + name)
//    {
//        cdc = tCDCEnumEd1.CURVE;
//        id = type;
//        this.iedType = iedType;
//        this.cdcName = new VisString255("cdcName", tFCEnum.EX);
//        this.cdcNs = new VisString255("cdcNs", tFCEnum.EX);
//        this.d = new VisString255("d", tFCEnum.DC);
//        this.dataNs = new VisString255("dataNs", tFCEnum.EX);
//        this.dU = new Unicode255("dU", tFCEnum.DC);
//        this.setCharact = new VisString255("setCharact", tFCEnum.SP);
//        this.setCharact2 = new VisString255("setCharact2", tFCEnum.SG);
//        this.setParA = new FLOAT32("setParA", tFCEnum.SP);
//        this.setParA2 = new FLOAT32("setParA2", tFCEnum.SG);
//        this.setParB = new FLOAT32("setParB", tFCEnum.SP);
//        this.setParB2 = new FLOAT32("setParB2", tFCEnum.SG);
//        this.setParC = new FLOAT32("setParC", tFCEnum.SP);
//        this.setParC2 = new FLOAT32("setParC2", tFCEnum.SG);
//        this.setParD = new FLOAT32("setParD", tFCEnum.SP);
//        this.setParD2 = new FLOAT32("setParD2", tFCEnum.SG);
//        this.setParE = new FLOAT32("setParE", tFCEnum.SP);
//        this.setParE2 = new FLOAT32("setParE2", tFCEnum.SG);
//        this.setParF = new FLOAT32("setParF", tFCEnum.SP);
//        this.setParF2 = new FLOAT32("setParF2", tFCEnum.SG);
//    }
//    public CURVE()
//    {
//        cdc = tCDCEnumEd1.CURVE;
//    }


//    [Browsable(false)]
//    public VisString255 cdcName { get; set; }

//    [Browsable(false)]
//    public VisString255 cdcNs { get; set; }

//    [Browsable(false)]
//    public VisString255 d { get; set; }

//    [Browsable(false)]
//    public VisString255 dataNs { get; set; }

//    [Browsable(false)]
//    public Unicode255 dU { get; set; }

//    [Browsable(false)]
//    public VisString255 setCharact { get; set; }

//    [Browsable(false)]
//    public VisString255 setCharact2 { get; set; }

//    [Browsable(false)]
//    public FLOAT32 setParA { get; set; }

//    [Browsable(false)]
//    public FLOAT32 setParA2 { get; set; }

//    [Browsable(false)]
//    public FLOAT32 setParB { get; set; }

//    [Browsable(false)]
//    public FLOAT32 setParB2 { get; set; }

//    [Browsable(false)]
//    public FLOAT32 setParC { get; set; }

//    [Browsable(false)]
//    public FLOAT32 setParC2 { get; set; }

//    [Browsable(false)]
//    public FLOAT32 setParD { get; set; }

//    [Browsable(false)]
//    public FLOAT32 setParD2 { get; set; }

//    [Browsable(false)]
//    public FLOAT32 setParE { get; set; }

//    [Browsable(false)]
//    public FLOAT32 setParE2 { get; set; }

//    [Browsable(false)]
//    public FLOAT32 setParF { get; set; }

//    [Browsable(false)]
//    public FLOAT32 setParF2 { get; set; }
//}

///// <summary>
///// This class defines the common data attribute types that are defined for his use in the DEL
///// common data class.
///// </summary>
//public class DEL : DOData
//{
//    /// <summary>
//    /// Phase to phase related measured values of a three phase system (DEL)
//    /// </summary>
//    /// <param name="name">
//    /// A name identifying the DO.
//    /// </param>
//    /// <param name="lnType">
//    /// Containing more detailed functional specification about LN.
//    /// </param>
//    /// <param name="iedType">
//    /// The manufacturer IED type of the IED to which this LN type belongs.
//    /// </param>
//    public DEL(string name, string lnType, string iedType) : base(name, lnType + name)
//    {
//        cdc = tCDCEnumEd1.DEL;
//        id = type;
//        this.iedType = iedType;
//        this.angRef = new angRef(tFCEnum.CF);
//        this.cdcName = new VisString255("cdcName", tFCEnum.EX);
//        this.cdcNs = new VisString255("cdcNs", tFCEnum.EX);
//        this.d = new VisString255("d", tFCEnum.DC);
//        this.dataNs = new VisString255("dataNs", tFCEnum.EX);
//        this.dU = new Unicode255("dU", tFCEnum.DC);
//        this.phsAB = new CMV("phsAB", id, iedType);
//        this.phsBC = new CMV("phsBC", id, iedType);
//        this.phsCA = new CMV("phsCA", id, iedType);
//    }

//    public DEL()
//    {
//        cdc = tCDCEnumEd1.DEL;
//    }
//    [Browsable(false)]
//    public angRef angRef { get; set; }

//    [Browsable(false)]
//    public VisString255 cdcName { get; set; }

//    [Browsable(false)]
//    public VisString255 cdcNs { get; set; }

//    [Browsable(false)]
//    public VisString255 d { get; set; }

//    [Browsable(false)]
//    public VisString255 dataNs { get; set; }

//    [Browsable(false)]
//    public Unicode255 dU { get; set; }

//    [Browsable(false)]
//    public CMV phsAB { get; set; }

//    [Browsable(false)]
//    public CMV phsBC { get; set; }

//    [Browsable(false)]
//    public CMV phsCA { get; set; }
//}

///// <summary>
///// This class defines the common data attribute types that are defined for his use in the DPC
///// common data class.
///// </summary>
//public class DPC : DOData
//{
//    /// <summary>
//    /// Controllable double point (DPC)
//    /// </summary>
//    /// <param name="name">
//    /// A name identifying the DO.
//    /// </param>
//    /// <param name="lnType">
//    /// Containing more detailed functional specification about LN.
//    /// </param>
//    /// <param name="iedType">
//    /// The manufacturer IED type of the IED to which this LN type belongs.
//    /// </param>
//    public DPC(string name, string lnType, string iedType) : base(name, lnType + name)
//    {
//        cdc = tCDCEnumEd1.DPC;
//        id = type;
//        this.iedType = iedType;
//        this.cdcName = new VisString255("cdcName", tFCEnum.EX);
//        this.cdcNs = new VisString255("cdcNs", tFCEnum.EX);
//        this.ctlModel = new ctlModel(iedType, id, tFCEnum.CF);
//        this.ctlNum = new INT8U("ctlNum", tFCEnum.CO);
//        this.ctlVal = new BOOLEAN("ctlVal", tFCEnum.CO);
//        this.d = new VisString255("d", tFCEnum.DC);
//        this.dataNs = new VisString255("dataNs", tFCEnum.EX);
//        this.dU = new Unicode255("dU", tFCEnum.DC);
//        this.operTm = new Timestamp("operTm", tFCEnum.CO);
//        this.origin = new origin(iedType, id, tFCEnum.CO);
//        this.PulseConfig = new PulseConfig(iedType, id, tFCEnum.CF);
//        this.q = new Quality("q", tFCEnum.ST);
//        this.sboClass = new sboClass(iedType, id, tFCEnum.CF);
//        this.sboTimeout = new INT32U("sboTimeout", tFCEnum.CF);
//        this.stSeld = new BOOLEAN("stSeld", tFCEnum.ST);
//        this.stVal = new stVal(tFCEnum.ST);
//        this.subEna = new BOOLEAN("subEna", tFCEnum.SV);
//        this.subID = new VisString64("subID", tFCEnum.SV);
//        this.subQ = new Quality("subQ", tFCEnum.SV);
//        this.subVal = new subVal(tFCEnum.SV);
//        this.t = new Timestamp("t", tFCEnum.ST);
//        this.SBO = new VisString65("SBO", tFCEnum.CO);
//        this.SBO.Visible = false;
//        this.SBOw = new SBOw(iedType, id, tFCEnum.CO);
//        this.Oper = new Oper(iedType, id, tFCEnum.CO);
//        this.Cancel = new Cancel(iedType, id, tFCEnum.CO);
//    }


//    public DPC()
//    {
//        stVal = new stVal();
//        subVal = new subVal();
//        cdc = tCDCEnumEd1.DPC;

//    }

//    [Browsable(false)]
//    public VisString255 cdcName { get; set; }

//    [Browsable(false)]
//    public VisString255 cdcNs { get; set; }

//    [Required]
//    [Browsable(false)]
//    public ctlModel ctlModel { get; set; }

//    [Browsable(false)]
//    public INT8U ctlNum { get; set; }

//    [Browsable(false)]
//    public BOOLEAN ctlVal { get; set; }

//    [Browsable(false)]
//    public VisString255 d { get; set; }

//    [Browsable(false)]
//    public VisString255 dataNs { get; set; }

//    [Browsable(false)]
//    public Unicode255 dU { get; set; }

//    [Browsable(false)]
//    public Timestamp operTm { get; set; }

//    [Browsable(false)]
//    public origin origin { get; set; }

//    [Browsable(false)]
//    public PulseConfig PulseConfig { get; set; }

//    [Required]
//    [Browsable(false)]
//    public Quality q { get; set; }

//    [Browsable(false)]
//    public sboClass sboClass { get; set; }

//    [Browsable(false)]
//    public INT32U sboTimeout { get; set; }

//    [Browsable(false)]
//    public BOOLEAN stSeld { get; set; }

//    [Required]
//    [Browsable(false)]
//    public stVal stVal { get; set; }

//    [Browsable(false)]
//    public BOOLEAN subEna { get; set; }

//    [Browsable(false)]
//    public VisString64 subID { get; set; }

//    [Browsable(false)]
//    public Quality subQ { get; set; }

//    [Browsable(false)]
//    public subVal subVal { get; set; }

//    [Required]
//    [Browsable(false)]
//    public Timestamp t { get; set; }

//    [Browsable(false)]
//    public VisString65 SBO { get; set; }

//    [Browsable(false)]
//    public SBOw SBOw { get; set; }

//    [Browsable(false)]
//    public Oper Oper { get; set; }

//    [Browsable(false)]
//    public Cancel Cancel { get; set; }
//}

///// <summary>
///// This class defines the common data attribute types that are defined for his use in the DPL
///// common data class.
///// </summary>
//public class DPL : DOData
//{
//    /// <summary>
//    /// Device name plate (DPL)
//    /// </summary>
//    /// <param name="name">
//    /// A name identifying the DO.
//    /// </param>
//    /// <param name="lnType">
//    /// Containing more detailed functional specification about LN.
//    /// </param>
//    /// <param name="iedType">
//    /// The manufacturer IED type of the IED to which this LN type belongs.
//    /// </param>
//    public DPL(string name, string lnType, string iedType) : base(name, lnType + name)
//    {
//        cdc = tCDCEnumEd1.DPL;
//        id = type;
//        this.iedType = iedType;
//        this.cdcName = new VisString255("cdcName", tFCEnum.EX);
//        this.cdcNs = new VisString255("cdcNs", tFCEnum.EX);
//        this.dataNs = new VisString255("dataNs", tFCEnum.EX);
//        this.hwRev = new VisString255("hwRev", tFCEnum.DC);
//        this.location = new VisString255("location", tFCEnum.DC);
//        this.model = new VisString255("model", tFCEnum.DC);
//        this.serNum = new VisString255("serNum", tFCEnum.DC);
//        this.swRev = new VisString255("swRev", tFCEnum.DC);
//        this.vendor = new VisString255("vendor", tFCEnum.DC);
//    }
//    public DPL()
//    {
//        cdc = tCDCEnumEd1.DPL;
//    }

//    [Browsable(false)]
//    public VisString255 cdcName { get; set; }

//    [Browsable(false)]
//    public VisString255 cdcNs { get; set; }

//    [Browsable(false)]
//    public VisString255 dataNs { get; set; }

//    [Browsable(false)]
//    public VisString255 hwRev { get; set; }

//    [Browsable(false)]
//    public VisString255 location { get; set; }

//    [Browsable(false)]
//    public VisString255 model { get; set; }

//    [Browsable(false)]
//    public VisString255 serNum { get; set; }

//    [Browsable(false)]
//    public VisString255 swRev { get; set; }

//    [Required]
//    [Browsable(false)]
//    public VisString255 vendor { get; set; }
//}

///// <summary>
///// This class defines the common data attribute types that are defined for his use in the DPS
///// common data class.
///// </summary>
//public class DPS : DOData
//{
//    /// <summary>
//    /// Double point status (DPS)
//    /// </summary>
//    /// <param name="name">
//    /// A name identifying the DO.
//    /// </param>
//    /// <param name="lnType">
//    /// Containing more detailed functional specification about LN.
//    /// </param>
//    /// <param name="iedType">
//    /// The manufacturer IED type of the IED to which this LN type belongs.
//    /// </param>
//    public DPS(string name, string lnType, string iedType) : base(name, lnType + name)
//    {
//        cdc = tCDCEnumEd1.DPS;
//        id = type;
//        this.iedType = iedType;
//        this.cdcName = new VisString255("cdcName", tFCEnum.EX);
//        this.cdcNs = new VisString255("cdcNs", tFCEnum.EX);
//        this.d = new VisString255("d", tFCEnum.DC);
//        this.dataNs = new VisString255("dataNs", tFCEnum.EX);
//        this.dU = new Unicode255("dU", tFCEnum.DC);
//        this.q = new Quality("q", tFCEnum.ST);
//        this.stVal = new stVal(tFCEnum.ST);
//        this.subEna = new BOOLEAN("subEna", tFCEnum.SV);
//        this.subID = new VisString64("subID", tFCEnum.SV);
//        this.subQ = new Quality("subQ", tFCEnum.SV);
//        this.subVal = new subVal(tFCEnum.SV);
//        this.t = new Timestamp("t", tFCEnum.ST);
//    }
//    public DPS()
//    {
//        cdc = tCDCEnumEd1.DPS;
//    }

//    [Browsable(false)]
//    public VisString255 cdcName { get; set; }

//    [Browsable(false)]
//    public VisString255 cdcNs { get; set; }

//    [Browsable(false)]
//    public VisString255 d { get; set; }

//    [Browsable(false)]
//    public VisString255 dataNs { get; set; }

//    [Browsable(false)]
//    public Unicode255 dU { get; set; }

//    [Required]
//    [Browsable(false)]
//    public Quality q { get; set; }

//    [Required]
//    [Browsable(false)]
//    public stVal stVal { get; set; }

//    [Browsable(false)]
//    public BOOLEAN subEna { get; set; }

//    [Browsable(false)]
//    public VisString64 subID { get; set; }

//    [Browsable(false)]
//    public Quality subQ { get; set; }

//    [Browsable(false)]
//    public subVal subVal { get; set; }

//    [Required]
//    [Browsable(false)]
//    public Timestamp t { get; set; }
//}

///// <summary>
///// This class defines the common data attribute types that are defined for his use in the HDEL
///// common data class.
///// </summary>
//public class HDEL : DOData
//{
//    //		FIXME : These attributes have a data type ARRAY[0..numHar] OF Vector and we have to verify how it works
//    //		      according to the IEC 61850 standard.
//    //		private phsABHar phsABHarField;
//    //		private phsBCHar phsBCHarField;
//    //		private phsCAHar phsCAHarField;

//    /// <summary>
//    /// Harmonic value for DEL (HDEL)
//    /// </summary>
//    /// <param name="name">
//    /// A name identifying the DO.
//    /// </param>
//    /// <param name="lnType">
//    /// Containing more detailed functional specification about LN. 
//    /// </param>
//    /// <param name="iedType">
//    /// The manufacturer IED type of the IED to which this LN type belongs.
//    /// </param>
//    public HDEL(string name, string lnType, string iedType) : base(name, lnType + name)
//    {
//        cdc = tCDCEnumEd1.HDEL;
//        id = type;
//        this.iedType = iedType;
//        this.angRef = new angRef(tFCEnum.CF);
//        this.cdcName = new VisString255("cdcName", tFCEnum.EX);
//        this.cdcNs = new VisString255("cdcNs", tFCEnum.EX);
//        this.d = new VisString255("d", tFCEnum.DC);
//        this.dataNs = new VisString255("dataNs", tFCEnum.EX);
//        this.dU = new Unicode255("dU", tFCEnum.DC);
//        this.evalTm = new INT16U("evalTm", tFCEnum.CF);
//        this.frequency = new FLOAT32("frequency", tFCEnum.CF);
//        this.hvRef = new hvRef(tFCEnum.CF);
//        this.numCyc = new INT16U("numCyc", tFCEnum.CF);
//        this.numHar = new INT16U("numHar", tFCEnum.CF);
//        //			this.phsABHarField = new phsABHar(iedType, this.id, tFCEnum.MX);
//        //			this.phsBCHarField = new phsBCHar(iedType, this.id, tFCEnum.MX);
//        //			this.phsCAHarField = new phsCAHar(iedType, this.id, tFCEnum.MX);
//        this.q = new Quality("q", tFCEnum.MX);
//        this.rmsCyc = new INT16U("rmsCyc", tFCEnum.CF);
//        this.smpRate = new INT32U("smpRate", tFCEnum.CF);
//        this.t = new Timestamp("t", tFCEnum.MX);
//        this.units = new units(iedType, id, tFCEnum.CF);
//    }

//    public HDEL()
//    {
//        cdc = tCDCEnumEd1.HDEL;
//    }

//    [Browsable(false)]
//    public angRef angRef { get; set; }

//    [Browsable(false)]
//    public VisString255 cdcName { get; set; }

//    [Browsable(false)]
//    public VisString255 cdcNs { get; set; }

//    [Browsable(false)]
//    public VisString255 d { get; set; }

//    [Browsable(false)]
//    public VisString255 dataNs { get; set; }

//    [Browsable(false)]
//    public Unicode255 dU { get; set; }

//    [Required]
//    [Browsable(false)]
//    public INT16U evalTm { get; set; }

//    [Required]
//    [Browsable(false)]
//    public FLOAT32 frequency { get; set; }

//    [Browsable(false)]
//    public hvRef hvRef { get; set; }

//    [Required]
//    [Browsable(false)]
//    public INT16U numCyc { get; set; }

//    [Required]
//    [Browsable(false)]
//    public INT16U numHar { get; set; }

//    //		[Required]
//    //		[Browsable(false)]
//    //		public phsABHar phsABHar
//    //		{
//    //			get	
//    //			{
//    //				return this.phsABHarField;
//    //			}
//    //			set
//    //			{
//    //				this.phsABHarField = value;
//    //			}
//    //		}
//    //		
//    //		[Browsable(false)]
//    //		public phsBCHar phsBCHar
//    //		{
//    //			get
//    //			{
//    //				return this.phsBCHarField;
//    //			}
//    //			set
//    //			{
//    //				this.phsBCHarField = value;
//    //			}
//    //		}
//    //		
//    //		[Browsable(false)]
//    //		public phsCAHar phsCAHar
//    //		{
//    //			get
//    //			{
//    //				return this.phsCAHarField;
//    //			}
//    //			set
//    //			{
//    //				this.phsCAHarField = value;
//    //			}
//    //		}

//    [Required]
//    [Browsable(false)]
//    public Quality q { get; set; }

//    [Browsable(false)]
//    public INT16U rmsCyc { get; set; }

//    [Browsable(false)]
//    public INT32U smpRate { get; set; }

//    [Browsable(false)]
//    public Timestamp t { get; set; }

//    [Browsable(false)]
//    public units units { get; set; }
//}

///// <summary>
///// This class defines the common data attribute types that are defined for his use in the HMV
///// common data class.
///// </summary>
//public class HMV : DOData
//{
//    //		FIXME : This attribute has a data type ARRAY[0..numHar] OF Vector and we have to verify how it works
//    //		      according to the IEC 61850 standard.		
//    //		private har harField;

//    /// <summary>
//    /// Harmonic Value (HMV)
//    /// </summary>
//    /// <param name="name">
//    /// A name identifying the DO.
//    /// </param>
//    /// <param name="lnType">
//    /// Containing more detailed functional specification about LN. 
//    /// </param>
//    /// <param name="iedType">
//    /// The manufacturer IED type of the IED to which this LN type belongs.
//    /// </param>
//    public HMV(string name, string lnType, string iedType) : base(name, lnType + name)
//    {
//        cdc = tCDCEnumEd1.HMV;
//        id = type;
//        this.iedType = iedType;
//        this.cdcName = new VisString255("cdcName", tFCEnum.EX);
//        this.cdcNs = new VisString255("cdcNs", tFCEnum.EX);
//        this.d = new VisString255("d", tFCEnum.DC);
//        this.dataNs = new VisString255("dataNs", tFCEnum.EX);
//        this.dU = new Unicode255("dU", tFCEnum.DC);
//        this.evalTm = new INT16U("evalTm", tFCEnum.CF);
//        this.frequency = new FLOAT32("frequency", tFCEnum.CF);
//        //			this.harField = new har(iedType, this.id, tFCEnum.MX);
//        this.hvRef = new hvRef(tFCEnum.CF);
//        this.numCyc = new INT16U("numCyc", tFCEnum.CF);
//        this.numHar = new INT16U("numHar", tFCEnum.CF);
//        this.q = new Quality("q", tFCEnum.MX);
//        this.rmsCyc = new INT16U("rmsCyc", tFCEnum.CF);
//        this.smpRate = new INT32U("smpRate", tFCEnum.CF);
//        this.t = new Timestamp("t", tFCEnum.MX);
//        this.units = new units(iedType, id, tFCEnum.CF);
//    }
//    public HMV()
//    {
//        cdc = tCDCEnumEd1.HMV;
//    }

//    [Browsable(false)]
//    public VisString255 cdcName { get; set; }

//    [Browsable(false)]
//    public VisString255 cdcNs { get; set; }

//    [Browsable(false)]
//    public VisString255 d { get; set; }

//    [Browsable(false)]
//    public VisString255 dataNs { get; set; }

//    [Browsable(false)]
//    public Unicode255 dU { get; set; }

//    [Required]
//    [Browsable(false)]
//    public INT16U evalTm { get; set; }

//    [Required]
//    [Browsable(false)]
//    public FLOAT32 frequency { get; set; }

//    //		[Required]
//    //		[Browsable(false)]
//    //		public har har
//    //		{
//    //			get
//    //			{
//    //				return this.harField;
//    //			}
//    //			set
//    //			{
//    //				this.harField = value;
//    //			}
//    //		}	

//    [Browsable(false)]
//    public hvRef hvRef { get; set; }

//    [Required]
//    [Browsable(false)]
//    public INT16U numCyc { get; set; }

//    [Required]
//    [Browsable(false)]
//    public INT16U numHar { get; set; }

//    [Required]
//    [Browsable(false)]
//    public Quality q { get; set; }

//    [Browsable(false)]
//    public INT16U rmsCyc { get; set; }

//    [Browsable(false)]
//    public INT32U smpRate { get; set; }

//    [Required]
//    [Browsable(false)]
//    public Timestamp t { get; set; }

//    [Browsable(false)]
//    public units units { get; set; }
//}

///// <summary>
///// This class defines the common data attribute types that are defined for his use in the HWYE
///// common data class.
///// </summary>
//public class HWYE : DOData
//{
//    //		FIXME : The attributes commented have a data type ARRAY[0..numHar] OF Vector and we have to verify how it works
//    //		      according to the IEC 61850 standard.		
//    //		private netHar netHarField;
//    //		private neutHar neutHarField;
//    //		private phsAHar phsAHarField;
//    //		private phsBHar phsBHarField;
//    //		private phsCHar phsCHarField;
//    //		private resHar resHarField;

//    /// <summary>
//    /// Harmonic value for WYE (HWYE)
//    /// </summary>
//    /// <param name="name">
//    /// A name identifying the DO.
//    /// </param>
//    /// <param name="lnType">
//    /// Containing more detailed functional specification about LN. 
//    /// </param>
//    /// <param name="iedType">
//    /// The manufacturer IED type of the IED to which this LN type belongs.
//    /// </param>
//    public HWYE(string name, string lnType, string iedType) : base(name, lnType + name)
//    {
//        cdc = tCDCEnumEd1.HWYE;
//        id = type;
//        this.iedType = iedType;
//        this.angRef = new angRef(tFCEnum.CF);
//        this.cdcName = new VisString255("cdcName", tFCEnum.EX);
//        this.cdcNs = new VisString255("cdcNs", tFCEnum.EX);
//        this.d = new VisString255("d", tFCEnum.DC);
//        this.dataNs = new VisString255("dataNs", tFCEnum.EX);
//        this.dU = new Unicode255("dU", tFCEnum.DC);
//        this.evalTm = new INT16U("evalTm", tFCEnum.CF);
//        this.frequency = new FLOAT32("frequency", tFCEnum.CF);
//        this.hvRef = new hvRef(tFCEnum.CF);
//        //			this.netHarField = new netHar(iedType, this.id, tFCEnum.MX);
//        //			this.neutHarField = new neutHar(iedType, this.id, tFCEnum.MX);
//        this.numCyc = new INT16U("numCyc", tFCEnum.CF);
//        this.numHar = new INT16U("numHar", tFCEnum.CF);
//        //			this.phsAHarField = new phsAHar(iedType, this.id, tFCEnum.MX);
//        //			this.phsBHarField = new phsBHar(iedType, this.id, tFCEnum.MX);
//        //			this.phsCHarField = new phsCHar(iedType, this.id, tFCEnum.MX);
//        this.q = new Quality("q", tFCEnum.MX);
//        //			this.resHarField = new resHar(iedType, this.id, tFCEnum.MX);
//        this.rmsCyc = new INT16U("rmsCyc", tFCEnum.CF);
//        this.smpRate = new INT32U("smpRate", tFCEnum.CF);
//        this.t = new Timestamp("t", tFCEnum.MX);
//        this.units = new units(iedType, id, tFCEnum.CF);
//    }
//    public HWYE()
//    {
//        cdc = tCDCEnumEd1.HWYE;
//    }

//    [Browsable(false)]
//    public angRef angRef { get; set; }

//    [Browsable(false)]
//    public VisString255 cdcName { get; set; }

//    [Browsable(false)]
//    public VisString255 cdcNs { get; set; }

//    [Browsable(false)]
//    public VisString255 d { get; set; }

//    [Browsable(false)]
//    public VisString255 dataNs { get; set; }

//    [Browsable(false)]
//    public Unicode255 dU { get; set; }

//    [Required]
//    [Browsable(false)]
//    public INT16U evalTm { get; set; }

//    [Required]
//    [Browsable(false)]
//    public FLOAT32 frequency { get; set; }

//    [Browsable(false)]
//    public hvRef hvRef { get; set; }

//    //		[Browsable(false)]
//    //		public netHar netHar
//    //		{
//    //			get
//    //			{
//    //				return this.netHarField;
//    //			}
//    //			set
//    //			{
//    //				this.netHarField = value;
//    //			}
//    //		}
//    //		
//    //		[Browsable(false)]
//    //		public neutHar neutHar
//    //		{
//    //			get
//    //			{
//    //				return this.neutHarField;
//    //			}
//    //			set
//    //			{
//    //				this.neutHarField = value;
//    //			}
//    //		}

//    [Required]
//    [Browsable(false)]
//    public INT16U numCyc { get; set; }

//    [Required]
//    [Browsable(false)]
//    public INT16U numHar { get; set; }

//    //		[Required]
//    //		[Browsable(false)]
//    //		public phsAHar phsAHar
//    //		{
//    //			get
//    //			{
//    //				return this.phsAHarField;
//    //			}
//    //			set
//    //			{
//    //				this.phsAHarField = value;
//    //			}
//    //		}
//    //		
//    //		[Browsable(false)]
//    //		public phsBHar phsBHar
//    //		{
//    //			get
//    //			{
//    //				return this.phsBHarField;
//    //			}
//    //			set
//    //			{
//    //				this.phsBHarField = value;
//    //			}
//    //		}
//    //		
//    //		[Browsable(false)]
//    //		public phsCHar phsCHar	
//    //		{
//    //			get
//    //			{
//    //				return this.phsCHarField;
//    //			}
//    //			set
//    //			{
//    //				this.phsCHarField = value;
//    //			}
//    //		}

//    [Required]
//    [Browsable(false)]
//    public Quality q { get; set; }

//    //		[Browsable(false)]
//    //		public resHar resHar
//    //		{
//    //			get 
//    //			{
//    //				return this.resHarField;
//    //			}
//    //			set
//    //			{
//    //				this.resHarField = value;
//    //			}
//    //		}

//    [Browsable(false)]
//    public INT16U rmsCyc { get; set; }

//    [Browsable(false)]
//    public INT32U smpRate { get; set; }

//    [Required]
//    [Browsable(false)]
//    public Timestamp t { get; set; }

//    [Browsable(false)]
//    public units units { get; set; }
//}

///// <summary>
///// This class defines the common data attribute types that are defined for his use in the INC
///// common data class.
///// </summary>
///// <remarks>
///// The original type of the fields "ctlVal" and "stVal" was INT32U, it was changed because they are used as an EnumType on the DataTypeTemplates.
///// </remarks>	
//public class INC : DOData
//{
//    /// <summary>
//    /// Controllable integer status (INC)
//    /// </summary>
//    /// <param name="name">
//    /// A name identifying the DO.
//    /// </param>
//    /// <param name="lnType">
//    /// Containing more detailed functional specification about LN. 
//    /// </param>
//    /// <param name="iedType">
//    /// The manufacturer IED type of the IED to which this LN type belongs.
//    /// </param>
//    /// <remarks>
//    /// The original type of the fields "ctlVal" and "stVal" was INT32U, it was changed because they are used as an EnumType on the DataTypeTemplates.
//    /// </remarks>			
//    public INC(string name, string lnType, string iedType) : base(name, lnType + name)
//    {
//        cdc = tCDCEnumEd1.INC;
//        id = type;
//        this.iedType = iedType;
//        this.ctlVal = new ctlVal(tFCEnum.CO);
//        this.operTm = new Timestamp("operTm", tFCEnum.CO);
//        this.origin = new origin(iedType, id, tFCEnum.CO);
//        this.ctlNum = new INT8U("ctlNum", tFCEnum.CO);
//        this.stVal = new stVal(tFCEnum.ST);
//        this.q = new Quality("q", tFCEnum.ST);
//        this.t = new Timestamp("t", tFCEnum.ST);
//        this.stSeld = new BOOLEAN("stSeld", tFCEnum.ST);
//        this.subEna = new BOOLEAN("subEna", tFCEnum.SV);
//        this.subVal = new INT32("subVal", tFCEnum.SV);
//        this.subQ = new Quality("subQ", tFCEnum.SV);
//        this.subID = new VisString64("subID", tFCEnum.SV);
//        this.ctlModel = new ctlModel(iedType, id, tFCEnum.CF);
//        this.sboTimeout = new INT32U("sboTimeout", tFCEnum.CF);
//        this.sboClass = new sboClass(iedType, id, tFCEnum.CF);
//        this.minVal = new INT32("minVal", tFCEnum.CF);
//        this.maxVal = new INT32("maxVal", tFCEnum.CF);
//        this.stepSize = new INT32U("stepSize", tFCEnum.CF);
//        this.d = new VisString255("d", tFCEnum.DC);
//        this.dU = new Unicode255("dU", tFCEnum.DC);
//        this.cdcNs = new VisString255("cdcNs", tFCEnum.EX);
//        this.cdcName = new VisString255("cdcName", tFCEnum.EX);
//        this.dataNs = new VisString255("dataNs", tFCEnum.EX);
//        this.SBO = new VisString65("SBO", tFCEnum.CO);
//        this.SBO.Visible = false;
//        this.SBOw = new SBOw(iedType, id, tFCEnum.CO);
//        this.Oper = new Oper(iedType, id, tFCEnum.CO);
//        this.Cancel = new Cancel(iedType, id, tFCEnum.CO);
//    }

//    public INC()
//    {
//        cdc = tCDCEnumEd1.INC;
//    }

//    public INC(string type)
//    {
//        cdc = tCDCEnumEd1.INC;
//        if (type == "Mod")
//        {
//            this.stVal = new stVal(type);
//            Oper = new Oper(type);
//        }



//    }





//    [Browsable(false)]
//    public VisString255 cdcName { get; set; }

//    [Browsable(false)]
//    public VisString255 cdcNs { get; set; }

//    [Required]
//    [Browsable(false)]
//    public ctlModel ctlModel { get; set; }

//    [Browsable(false)]
//    public INT8U ctlNum { get; set; }

//    [Browsable(false)]
//    public ctlVal ctlVal { get; set; }

//    [Browsable(false)]
//    public VisString255 d { get; set; }

//    [Browsable(false)]
//    public VisString255 dataNs { get; set; }

//    [Browsable(false)]
//    public Unicode255 dU { get; set; }

//    [Browsable(false)]
//    public INT32 maxVal { get; set; }

//    [Browsable(false)]
//    public INT32 minVal { get; set; }

//    [Browsable(false)]
//    public Timestamp operTm { get; set; }

//    [Browsable(false)]
//    public origin origin { get; set; }

//    [Required]
//    [Browsable(false)]
//    public Quality q { get; set; }

//    [Browsable(false)]
//    public sboClass sboClass { get; set; }

//    [Browsable(false)]
//    public INT32U sboTimeout { get; set; }

//    [Browsable(false)]
//    public INT32U stepSize { get; set; }

//    [Browsable(false)]
//    public BOOLEAN stSeld { get; set; }

//    [Required]
//    [Browsable(false)]
//    public stVal stVal { get; set; }

//    [Browsable(false)]
//    public BOOLEAN subEna { get; set; }

//    [Browsable(false)]
//    public VisString64 subID { get; set; }

//    [Browsable(false)]
//    public Quality subQ { get; set; }

//    [Browsable(false)]
//    public INT32 subVal { get; set; }

//    [Required]
//    [Browsable(false)]
//    public Timestamp t { get; set; }

//    [Browsable(false)]
//    public VisString65 SBO { get; set; }

//    [Browsable(false)]
//    public SBOw SBOw { get; set; }

//    [Browsable(false)]
 // public Oper Oper { get; set; }

//    [Browsable(false)]
//    public Cancel Cancel { get; set; }
//}

///// <summary>
///// This class defines the common data attribute types that are defined for his use in the ING
///// common data class.
///// </summary>
//public class ING : DOData
//{
//    /// <summary>
//    /// Integer status setting (ING)
//    /// </summary>
//    /// <param name="name">
//    /// A name identifying the DO.
//    /// </param>
//    /// <param name="lnType">
//    /// Containing more detailed functional specification about LN.
//    /// </param>
//    /// <param name="iedType">
//    /// The manufacturer IED type of the IED to which this LN type belongs.
//    /// </param>
//    public ING(string name, string lnType, string iedType) : base(name, lnType + name)
//    {
//        cdc = tCDCEnumEd1.ING;
//        id = type;
//        this.iedType = iedType;
//        this.setVal = new INT32("setVal", tFCEnum.SP);
//        this.setVal2 = new INT32("setVal2", tFCEnum.SG);
//        this.minVal = new INT32("minVal", tFCEnum.CF);
//        this.maxVal = new INT32("maxVal", tFCEnum.CF);
//        this.stepSize = new INT32U("stepSize", tFCEnum.CF);
//        this.d = new VisString255("d", tFCEnum.DC);
//        this.dU = new Unicode255("dU", tFCEnum.DC);
//        this.cdcNs = new VisString255("cdcNs", tFCEnum.EX);
//        this.cdcName = new VisString255("cdcName", tFCEnum.EX);
//        this.dataNs = new VisString255("dataNs", tFCEnum.EX);
//    }

//    public ING()
//    {
//        cdc = tCDCEnumEd1.ING;
//    }
//    [Browsable(false)]
//    public VisString255 cdcName { get; set; }

//    [Browsable(false)]
//    public VisString255 cdcNs { get; set; }

//    [Browsable(false)]
//    public VisString255 d { get; set; }

//    [Browsable(false)]
//    public VisString255 dataNs { get; set; }

//    [Browsable(false)]
//    public Unicode255 dU { get; set; }

//    [Browsable(false)]
//    public INT32 maxVal { get; set; }

//    [Browsable(false)]
//    public INT32 minVal { get; set; }

//    [Browsable(false)]
//    public INT32 setVal { get; set; }

//    [Browsable(false)]
//    public INT32 setVal2 { get; set; }

//    [Browsable(false)]
//    public INT32U stepSize { get; set; }
//}

///// <summary>
///// This class defines the common data attribute types that are defined for his use in the INS
///// common data class.
///// </summary>
///// <remarks>
///// The original type of the fields "stVal" was INT32U, it was changed because they are used as an EnumType on the DataTypeTemplates.
///// </remarks>		
//public class INS : DOData
//{
//    private string v;

//    /// <summary>
//    /// Integer status (INS)
//    /// </summary>
//    /// <param name="name">
//    /// A name identifying the DO.
//    /// </param>
//    /// <param name="lnType">
//    /// Containing more detailed functional specification about LN.
//    /// </param>
//    /// <param name="iedType">
//    /// The manufacturer IED type of the IED to which this LN type belongs.
//    /// </param>
//    public INS(string name, string lnType, string iedType) : base(name, lnType + name)
//    {
//        cdc = tCDCEnumEd1.INS;
//        id = type;
//        this.iedType = iedType;
//        this.stVal = new stVal(tFCEnum.ST);
//        this.q = new Quality("q", tFCEnum.ST);
//        this.t = new Timestamp("t", tFCEnum.ST);
//        this.subEna = new BOOLEAN("subEna", tFCEnum.SV);
//        this.subVal = new subVal(tFCEnum.SV);
//        this.subQ = new Quality("subQ", tFCEnum.SV);
//        this.subID = new VisString64("subID", tFCEnum.SV);
//        this.d = new VisString255("d", tFCEnum.DC);
//        this.dU = new Unicode255("dU", tFCEnum.DC);
//        this.cdcNs = new VisString255("cdcNs", tFCEnum.EX);
//        this.cdcName = new VisString255("cdcName", tFCEnum.EX);
//        this.dataNs = new VisString255("dataNs", tFCEnum.EX);
//    }
//    public INS()
//    {
//        cdc = tCDCEnumEd1.INS;
//        this.stVal = new stVal();
//    }

//    public INS(string type)
//    {
//        cdc = tCDCEnumEd1.INS;
//        if (type == "Beh")
//        {
//            this.stVal = new stVal("Beh");
//            this.stVal.type = "Beh";
//        }
//        if (type == "Health")
//        {
//            this.stVal = new stVal("Health");
//            this.stVal.type = "Health";
//        }
//        if (type == "CBOpCap")
//        {
//            this.stVal = new stVal("CBOpCap");
//            this.stVal.type = "CBOpCap";
//        }
//        if (type == "IntIn")
//        {
//            this.stVal = new stVal("IntIn");
//            this.stVal.type = "IntIn";
//        }
//        if (type == "AutoRecSt")
//        {
//            this.stVal = new stVal("AutoRecSt");
//            this.stVal.type = "AutoRecSt";
//        }
//    }

//    [Browsable(false)]
//    public VisString255 cdcName { get; set; }

//    [Browsable(false)]
//    public VisString255 cdcNs { get; set; }

//    [Browsable(false)]
//    public VisString255 d { get; set; }

//    [Browsable(false)]
//    public VisString255 dataNs { get; set; }

//    [Browsable(false)]
//    public Unicode255 dU { get; set; }

//    [Required]
//    [Browsable(false)]
//    public Quality q { get; set; }

//    [Required]
//    [Browsable(false)]
//    public stVal stVal { get; set; }

//    [Browsable(false)]
//    public BOOLEAN subEna { get; set; }

//    [Browsable(false)]
//    public VisString64 subID { get; set; }

//    [Browsable(false)]
//    public Quality subQ { get; set; }

//    [Browsable(false)]
//    public subVal subVal { get; set; }

//    [Required]
//    [Browsable(false)]
//    public Timestamp t { get; set; }
//}

///// <summary>
///// This class defines the common data attribute types that are defined for his use in the ISC
///// common data class.
///// </summary>
//public class ISC : DOData
//{
//    /// <summary>
//    /// Integer controlled step position information (ISC)
//    /// </summary>
//    /// <param name="name">
//    /// A name identifying the DO.
//    /// </param>
//    /// <param name="lnType">
//    /// Containing more detailed functional specification about LN. 
//    /// </param>
//    /// <param name="iedType">
//    /// The manufacturer IED type of the IED to which this LN type belongs.
//    /// </param>
//    public ISC(string name, string lnType, string iedType) : base(name, lnType + name)
//    {
//        cdc = tCDCEnumEd1.ISC;
//        id = type;
//        this.iedType = iedType;
//        this.ctlVal = new INT8("ctlVal", tFCEnum.CO);
//        this.operTm = new Timestamp("operTm", tFCEnum.CO);
//        this.origin = new origin(iedType, id, tFCEnum.CO);
//        this.ctlNum = new INT8U("ctlNum", tFCEnum.CO);
//        this.valWTr = new valWTr(iedType, id, tFCEnum.ST);
//        this.q = new Quality("q", tFCEnum.ST);
//        this.t = new Timestamp("t", tFCEnum.ST);
//        this.stSeld = new BOOLEAN("stSeld", tFCEnum.ST);
//        this.subEna = new BOOLEAN("subEna", tFCEnum.SV);
//        this.subVal = new subVal(iedType, id, tFCEnum.SV);
//        this.subVal.SetLinkSDIDADataTypeBDA(new ValWithTrans(iedType, this.subVal.id));
//        this.subQ = new Quality("subQ", tFCEnum.SV);
//        this.subID = new VisString64("subID", tFCEnum.SV);
//        this.ctlModel = new ctlModel(iedType, id, tFCEnum.CF);
//        this.sboTimeout = new INT32U("sboTimeout", tFCEnum.CF);
//        this.sboClass = new sboClass(iedType, id, tFCEnum.CF);
//        this.minVal = new INT8("minVal", tFCEnum.CF);
//        this.maxVal = new INT8("maxVal", tFCEnum.CF);
//        this.stepSize = new INT8U("stepSize", tFCEnum.CF);
//        this.d = new VisString255("d", tFCEnum.DC);
//        this.dU = new Unicode255("dU", tFCEnum.DC);
//        this.cdcNs = new VisString255("cdcNs", tFCEnum.EX);
//        this.cdcName = new VisString255("cdcName", tFCEnum.EX);
//        this.dataNs = new VisString255("dataNs", tFCEnum.EX);
//        this.SBO = new VisString65("SBO", tFCEnum.CO);
//        this.SBO.Visible = false;
//        this.SBOw = new SBOw(iedType, id, tFCEnum.CO);
//        this.Oper = new Oper(iedType, id, tFCEnum.CO);
//        this.Cancel = new Cancel(iedType, id, tFCEnum.CO);
//    }

//    public ISC()
//    {
//        cdc = tCDCEnumEd1.ISC;
//    }
//    [Browsable(false)]
//    public VisString255 cdcName { get; set; }

//    [Browsable(false)]
//    public VisString255 cdcNs { get; set; }

//    [Required]
//    [Browsable(false)]
//    public ctlModel ctlModel { get; set; }

//    [Browsable(false)]
//    public INT8U ctlNum { get; set; }

//    [Browsable(false)]
//    public INT8 ctlVal { get; set; }

//    [Browsable(false)]
//    public VisString255 d { get; set; }

//    [Browsable(false)]
//    public VisString255 dataNs { get; set; }

//    [Browsable(false)]
//    public Unicode255 dU { get; set; }

//    [Browsable(false)]
//    public INT8 maxVal { get; set; }

//    [Browsable(false)]
//    public INT8 minVal { get; set; }

//    [Browsable(false)]
//    public Timestamp operTm { get; set; }

//    [Browsable(false)]
//    public origin origin { get; set; }

//    [Browsable(false)]
//    public Quality q { get; set; }

//    [Browsable(false)]
//    public sboClass sboClass { get; set; }

//    [Browsable(false)]
//    public INT32U sboTimeout { get; set; }

//    [Browsable(false)]
//    public INT8U stepSize { get; set; }

//    [Browsable(false)]
//    public BOOLEAN stSeld { get; set; }

//    [Browsable(false)]
//    public BOOLEAN subEna { get; set; }

//    [Browsable(false)]
//    public VisString64 subID { get; set; }

//    [Browsable(false)]
//    public Quality subQ { get; set; }

//    [Browsable(false)]
//    public subVal subVal { get; set; }

//    [Browsable(false)]
//    public Timestamp t { get; set; }

//    [Browsable(false)]
//    public valWTr valWTr { get; set; }

//    [Browsable(false)]
//    public VisString65 SBO { get; set; }

//    [Browsable(false)]
//    public SBOw SBOw { get; set; }

//    [Browsable(false)]
//    public Oper Oper { get; set; }

//    [Browsable(false)]
//    public Cancel Cancel { get; set; }
//}

///// <summary>
///// This class defines the common data attribute types that are defined for his use in the LPL
///// common data class.
///// </summary>
//public class LPL : DOData
//{
//    /// <summary>
//    /// Logical node name plate (LPL)
//    /// </summary>
//    /// <param name="name">
//    /// A name identifying the DO.
//    /// </param>
//    /// <param name="lnType">
//    /// Containing more detailed functional specification about LN.
//    /// </param>
//    /// <param name="iedType">
//    /// The manufacturer IED type of the IED to which this LN type belongs.
//    /// </param>
//    public LPL(string name, string lnType, string iedType) : base(name, lnType + name)
//    {
//        cdc = tCDCEnumEd1.LPL;
//        id = type;
//        this.iedType = iedType;
//        this.vendor = new VisString255("vendor", tFCEnum.DC);
//        this.swRev = new VisString255("swRev", tFCEnum.DC);
//        this.d = new VisString255("d", tFCEnum.DC);
//        this.dU = new Unicode255("dU", tFCEnum.DC);
//        this.configRev = new VisString255("configRev", tFCEnum.DC);
//        this.ldNs = new VisString255("ldNs", tFCEnum.EX);
//        this.lnNs = new VisString255("lnNs", tFCEnum.EX);
//        this.cdcNs = new VisString255("cdcNs", tFCEnum.EX);
//        this.cdcName = new VisString255("cdcName", tFCEnum.EX);
//        this.dataNs = new VisString255("dataNs", tFCEnum.EX);
//    }

//    public LPL()
//    {
//        cdc = tCDCEnumEd1.LPL;
//    }

//    [Browsable(false)]
//    public VisString255 cdcName { get; set; }

//    [Browsable(false)]
//    public VisString255 cdcNs { get; set; }

//    [Browsable(false)]
//    public VisString255 configRev { get; set; }

//    [Required]
//    [Browsable(false)]
//    public VisString255 d { get; set; }

//    [Browsable(false)]
//    public VisString255 dataNs { get; set; }

//    [Browsable(false)]
//    public Unicode255 dU { get; set; }

//    [Browsable(false)]
//    public VisString255 ldNs { get; set; }

//    [Browsable(false)]
//    public VisString255 lnNs { get; set; }

//    [Required]
//    [Browsable(false)]
//    public VisString255 swRev { get; set; }

//    [Required]
//    [Browsable(false)]
//    public VisString255 vendor { get; set; }
//}

///// <summary>
///// This class defines the common data attribute types that are defined for his use in the MV
///// common data class.
///// </summary>
//public class MV : DOData
//{
//    /// <summary>
//    /// Measured value (MV)
//    /// </summary>
//    /// <param name="name">
//    /// A name identifying the DO.
//    /// </param>
//    /// <param name="lnType">
//    /// Containing more detailed functional specification about LN.
//    /// </param>
//    /// <param name="iedType">
//    /// The manufacturer IED type of the IED to which this LN type belongs.
//    /// </param>
//    public MV(string name, string lnType, string iedType) : base(name, lnType + name)
//    {
//        cdc = tCDCEnumEd1.MV;
//        id = type;
//        this.iedType = iedType;
//        this.instMag = new instMag(iedType, id, tFCEnum.MX);
//        this.mag = new mag(iedType, id, tFCEnum.MX);
//        this.range = new range(iedType, id, tFCEnum.MX);
//        this.q = new Quality("q", tFCEnum.MX);
//        this.t = new Timestamp("t", tFCEnum.MX);
//        this.subEna = new BOOLEAN("subEna", tFCEnum.SV);
//        this.subVal = new subVal(iedType, id, tFCEnum.SV);
//        this.subVal.SetLinkSDIDADataTypeBDA(new AnalogueValue(iedType, this.subVal.id));
//        this.subQ = new Quality("subQ", tFCEnum.SV);
//        this.subID = new VisString64("subID", tFCEnum.SV);
//        this.units = new units(iedType, id, tFCEnum.CF);
//        this.db = new INT32U("db", tFCEnum.CF);
//        this.zeroDb = new INT32U("zeroDb", tFCEnum.CF);
//        this.sVC = new sVC(iedType, id, tFCEnum.CF);
//        this.rangeC = new rangeC(iedType, id, tFCEnum.CF);
//        this.smpRate = new INT32U("smpRate", tFCEnum.CF);
//        this.d = new VisString255("d", tFCEnum.DC);
//        this.dU = new Unicode255("dU", tFCEnum.DC);
//        this.cdcNs = new VisString255("cdcNs", tFCEnum.EX);
//        this.cdcName = new VisString255("cdcName", tFCEnum.EX);
//        this.dataNs = new VisString255("dataNs", tFCEnum.EX);
//    }
//    public MV()
//    {
//        cdc = tCDCEnumEd1.MV;
//    }
//    [Browsable(false)]
//    public VisString255 cdcName { get; set; }

//    [Browsable(false)]
//    public VisString255 cdcNs { get; set; }

//    [Browsable(false)]
//    public VisString255 d { get; set; }

//    [Browsable(false)]
//    public VisString255 dataNs { get; set; }

//    [Browsable(false)]
//    public INT32U db { get; set; }

//    [Browsable(false)]
//    public Unicode255 dU { get; set; }

//    [Browsable(false)]
//    public instMag instMag { get; set; }

//    [Required]
//    [Browsable(false)]
//    public mag mag { get; set; }

//    [Required]
//    [Browsable(false)]
//    public Quality q { get; set; }

//    [Browsable(false)]
//    public range range { get; set; }

//    [Browsable(false)]
//    public rangeC rangeC { get; set; }

//    [Browsable(false)]
//    public INT32U smpRate { get; set; }

//    [Browsable(false)]
//    public BOOLEAN subEna { get; set; }

//    [Browsable(false)]
//    public VisString64 subID { get; set; }

//    [Browsable(false)]
//    public Quality subQ { get; set; }

//    [Browsable(false)]
//    public subVal subVal { get; set; }

//    [Browsable(false)]
//    public sVC sVC { get; set; }

//    [Required]
//    [Browsable(false)]
//    public Timestamp t { get; set; }

//    [Browsable(false)]
//    public units units { get; set; }

//    [Browsable(false)]
//    public INT32U zeroDb { get; set; }

//    [Browsable(false)]
//    public mag subMag { get; set; }

//}

///// <summary>
///// This class defines the common data attribute types that are defined for his use in the SAV
///// common data class.
///// </summary>
//public class SAV : DOData
//{
//    /// <summary>
//    /// Sampled value (SAV)
//    /// </summary>
//    /// <param name="name">
//    /// A name identifying the DO.
//    /// </param>
//    /// <param name="lnType">
//    /// Containing more detailed functional specification about LN. 
//    /// </param>
//    /// <param name="iedType">
//    /// The manufacturer IED type of the IED to which this LN type belongs.
//    /// </param>
//    public SAV(string name, string lnType, string iedType) : base(name, lnType + name)
//    {
//        cdc = tCDCEnumEd1.SAV;
//        id = type;
//        this.iedType = iedType;
//        this.instMag = new instMag(iedType, id, tFCEnum.MX);
//        this.q = new Quality("q", tFCEnum.MX);
//        this.t = new Timestamp("t", tFCEnum.MX);
//        this.units = new units(iedType, id, tFCEnum.CF);
//        this.sVC = new sVC(iedType, id, tFCEnum.CF);
//        this.max = new max(iedType, id, tFCEnum.CF);
//        this.min = new min(iedType, id, tFCEnum.CF);
//        this.d = new VisString255("d", tFCEnum.DC);
//        this.dU = new Unicode255("dU", tFCEnum.DC);
//        this.cdcNs = new VisString255("cdcNs", tFCEnum.EX);
//        this.cdcName = new VisString255("cdcName", tFCEnum.EX);
//        this.dataNs = new VisString255("dataNs", tFCEnum.EX);
//    }
//    public SAV()
//    {
//        cdc = tCDCEnumEd1.SAV;
//    }
//    [Browsable(false)]
//    public VisString255 cdcName { get; set; }

//    [Browsable(false)]
//    public VisString255 cdcNs { get; set; }

//    [Browsable(false)]
//    public VisString255 d { get; set; }

//    [Browsable(false)]
//    public VisString255 dataNs { get; set; }

//    [Browsable(false)]
//    public Unicode255 dU { get; set; }

//    [Required]
//    [Browsable(false)]
//    public instMag instMag { get; set; }

//    [Browsable(false)]
//    public max max { get; set; }

//    [Browsable(false)]
//    public min min { get; set; }

//    [Required]
//    [Browsable(false)]
//    public Quality q { get; set; }

//    [Browsable(false)]
//    public sVC sVC { get; set; }

//    [Browsable(false)]
//    public Timestamp t { get; set; }

//    [Browsable(false)]
//    public units units { get; set; }
//}

///// <summary>
///// This class defines the common data attribute types that are defined for his use in the SEC
///// common data class.
///// </summary>
//public class SEC : DOData
//{
//    /// <summary>
//    /// Security violation counting (SEC)
//    /// </summary>
//    /// <param name="name">
//    /// A name identifying the DO.
//    /// </param>
//    /// <param name="lnType">
//    /// Containing more detailed functional specification about LN.
//    /// </param>
//    /// <param name="iedType">
//    /// The manufacturer IED type of the IED to which this LN type belongs.
//    /// </param>
//    public SEC(string name, string lnType, string iedType) : base(name, lnType + name)
//    {
//        cdc = tCDCEnumEd1.SEC;
//        id = type;
//        this.iedType = iedType;
//        this.cnt = new INT32U("cnt", tFCEnum.ST);
//        this.sev = new sev(tFCEnum.ST);
//        this.t = new Timestamp("t", tFCEnum.ST);
//        this.addr = new Octet64("addr", tFCEnum.ST);
//        this.addInfo = new VisString64("addInfo", tFCEnum.ST);
//        this.d = new VisString255("d", tFCEnum.DC);
//        this.dU = new Unicode255("dU", tFCEnum.DC);
//        this.cdcNs = new VisString255("cdcNs", tFCEnum.EX);
//        this.cdcName = new VisString255("cdcName", tFCEnum.EX);
//        this.dataNs = new VisString255("dataNs", tFCEnum.EX);
//    }
//    public SEC()
//    {
//        cdc = tCDCEnumEd1.SEC;
//    }
//    [Browsable(false)]
//    public VisString64 addInfo { get; set; }

//    [Browsable(false)]
//    public Octet64 addr { get; set; }

//    [Browsable(false)]
//    public VisString255 cdcName { get; set; }

//    [Browsable(false)]
//    public VisString255 cdcNs { get; set; }

//    [Required]
//    [Browsable(false)]
//    public INT32U cnt { get; set; }

//    [Browsable(false)]
//    public VisString255 d { get; set; }

//    [Browsable(false)]
//    public VisString255 dataNs { get; set; }

//    [Browsable(false)]
//    public Unicode255 dU { get; set; }

//    [Required]
//    [Browsable(false)]
//    public sev sev { get; set; }

//    [Required]
//    [Browsable(false)]
//    public Timestamp t { get; set; }
//}

///// <summary>
///// This class defines the common data attribute types that are defined for his use in the SEQ
///// common data class.
///// </summary>
//public class SEQ : DOData
//{
//    /// <summary>
//    /// Sequence (SEQ)
//    /// </summary>
//    /// <param name="name">
//    /// A name identifying the DO.
//    /// </param>
//    /// <param name="lnType">
//    /// Containing more detailed functional specification about LN. 
//    /// </param>
//    /// <param name="iedType">
//    /// The manufacturer IED type of the IED to which this LN type belongs.
//    /// </param>
//    public SEQ(string name, string lnType, string iedType) : base(name, lnType + name)
//    {
//        cdc = tCDCEnumEd1.SEQ;
//        id = type;
//        this.iedType = iedType;
//        this.c1 = new CMV("c1", id, iedType);
//        this.c2 = new CMV("c2", id, iedType);
//        this.c3 = new CMV("c3", id, iedType);
//        SeqT = new seqT(tFCEnum.MX);
//        this.phsRef = new phsRef(tFCEnum.CF);
//        this.d = new VisString255("d", tFCEnum.DC);
//        this.dU = new Unicode255("dU", tFCEnum.DC);
//        this.cdcNs = new VisString255("cdcNs", tFCEnum.EX);
//        this.cdcName = new VisString255("cdcName", tFCEnum.EX);
//        this.dataNs = new VisString255("dataNs", tFCEnum.EX);
//    }
//    public SEQ()
//    {
//        cdc = tCDCEnumEd1.SEQ;
//    }
//    [Required]
//    [Browsable(false)]
//    public CMV c1 { get; set; }

//    [Required]
//    [Browsable(false)]
//    public CMV c2 { get; set; }

//    [Required]
//    [Browsable(false)]
//    public CMV c3 { get; set; }

//    [Browsable(false)]
//    public VisString255 cdcName { get; set; }

//    [Browsable(false)]
//    public VisString255 cdcNs { get; set; }

//    [Browsable(false)]
//    public VisString255 d { get; set; }

//    [Browsable(false)]
//    public VisString255 dataNs { get; set; }

//    [Browsable(false)]
//    public Unicode255 dU { get; set; }

//    [Browsable(false)]
//    public phsRef phsRef { get; set; }

//    [Required]
//    [Browsable(false)]
//    public seqT SeqT { get; set; }
//    [Required]
//    [Browsable(false)]
//    public seqT seqT { get; set; }
//}

///// <summary>
///// This class defines the common data attribute types that are defined for his use in the SPC
///// common data class.
///// </summary>
//public class SPC : DOData
//{
//    /// <summary>
//    /// Controllable single point (SPC)
//    /// </summary>
//    /// <param name="name">
//    /// A name identifying the DO.
//    /// </param>
//    /// <param name="lnType">
//    /// Containing more detailed functional specification about LN. 
//    /// </param>
//    /// <param name="iedType">
//    /// The manufacturer IED type of the IED to which this LN type belongs.
//    /// </param>
//    public SPC(string name, string lnType, string iedType) : base(name, lnType + name)
//    {
//        cdc = tCDCEnumEd1.SPC;
//        id = type;
//        this.iedType = iedType;
//        this.ctlVal = new BOOLEAN("ctlVal", tFCEnum.CO);
//        this.operTm = new Timestamp("operTm", tFCEnum.CO);
//        this.ctlNum = new INT8U("ctlNum", tFCEnum.CO);
//        this.stVal = new stVal(tFCEnum.ST);
//        this.q = new Quality("q", tFCEnum.ST);
//        this.t = new Timestamp("t", tFCEnum.ST);
//        this.stSeld = new BOOLEAN("stSeld", tFCEnum.ST);
//        this.subEna = new BOOLEAN("subEna", tFCEnum.SV);
//        this.subVal = new BOOLEAN("subVal", tFCEnum.SV);
//        this.subQ = new Quality("subQ", tFCEnum.SV);
//        this.subID = new VisString64("subID", tFCEnum.SV);
//        this.sboTimeout = new INT32U("sboTimeout", tFCEnum.CF);
//        this.d = new VisString255("d", tFCEnum.DC);
//        this.dU = new Unicode255("dU", tFCEnum.DC);
//        this.cdcNs = new VisString255("cdcNs", tFCEnum.EX);
//        this.cdcName = new VisString255("cdcName", tFCEnum.EX);
//        this.dataNs = new VisString255("dataNs", tFCEnum.EX);
//        this.origin = new origin(iedType, id, tFCEnum.CO);
//        this.PulseConfig = new PulseConfig(iedType, id, tFCEnum.CF);
//        this.ctlModel = new ctlModel(iedType, id, tFCEnum.CF);
//        this.sboClass = new sboClass(iedType, id, tFCEnum.CF);
//        this.SBO = new VisString65("SBO", tFCEnum.CO);
//        this.SBO.Visible = false;
//        //Nota hay propiedades que se repiten hay q buscarlos y crear sus temporales para no generar basura
//        this.SBOw = new SBOw(iedType, id, tFCEnum.CO);
//        this.Oper = new Oper(iedType, id, tFCEnum.CO);
//        this.Cancel = new Cancel(iedType, id, tFCEnum.CO);
//    }
//    public SPC()
//    {
//        cdc = tCDCEnumEd1.SPC;
//        stVal = new stVal();
//    }
//    public SPC(string type)
//    {
//        cdc = tCDCEnumEd1.SPC;
//        if (type == "Beh")
//        {
//            this.stVal = new stVal("BlkOpn");
//            this.stVal.type = "BlkOpn";
//        }
//    }



//    [Browsable(false)]
//    public VisString255 cdcName { get; set; }

//    [Browsable(false)]
//    public VisString255 cdcNs { get; set; }

//    [Required]
//    [Browsable(false)]
//    public ctlModel ctlModel { get; set; }

//    [Browsable(false)]
//    public INT8U ctlNum { get; set; }

//    [Browsable(false)]
//    public BOOLEAN ctlVal { get; set; }

//    [Browsable(false)]
//    public VisString255 d { get; set; }

//    [Browsable(false)]
//    public VisString255 dataNs { get; set; }

//    [Browsable(false)]
//    public Unicode255 dU { get; set; }

//    [Browsable(false)]
//    public Timestamp operTm { get; set; }

//    [Browsable(false)]
//    public origin origin { get; set; }

//    [Browsable(false)]
//    public PulseConfig PulseConfig { get; set; }

//    [Browsable(false)]
//    public Quality q { get; set; }

//    [Browsable(false)]
//    public sboClass sboClass { get; set; }

//    [Browsable(false)]
//    public INT32U sboTimeout { get; set; }

//    [Browsable(false)]
//    public BOOLEAN stSeld { get; set; }

//    [Browsable(false)]
//    public stVal stVal { get; set; }

//    [Browsable(false)]
//    public BOOLEAN subEna { get; set; }

//    [Browsable(false)]
//    public VisString64 subID { get; set; }

//    [Browsable(false)]
//    public Quality subQ { get; set; }

//    [Browsable(false)]
//    public BOOLEAN subVal { get; set; }

//    [Browsable(false)]
//    public Timestamp t { get; set; }

//    [Browsable(false)]
//    public VisString65 SBO { get; set; }

//    [Browsable(false)]
//    public SBOw SBOw { get; set; }

//    [Browsable(false)]
//    public Oper Oper { get; set; }

//    [Browsable(false)]
//    public Cancel Cancel { get; set; }
//}

///// <summary>
///// This class defines the common data attribute types that are defined for his use in the SPG
///// common data class.
///// </summary>
//public class SPG : DOData
//{
//    /// <summary>
//    /// Single point setting (SPG)
//    /// </summary>
//    /// <param name="name">
//    /// A name identifying the DO.
//    /// </param>
//    /// <param name="lnType">
//    /// Containing more detailed functional specification about LN. 
//    /// </param>
//    /// <param name="iedType">
//    /// The manufacturer IED type of the IED to which this LN type belongs.
//    /// </param>
//    public SPG(string name, string lnType, string iedType) : base(name, lnType + name)
//    {
//        cdc = tCDCEnumEd1.SPG;
//        id = type;
//        this.iedType = iedType;
//        this.setVal = new BOOLEAN("setVal", tFCEnum.SP);
//        this.setVal2 = new BOOLEAN("setVal2", tFCEnum.SG);
//        this.d = new VisString255("d", tFCEnum.DC);
//        this.dU = new Unicode255("dU", tFCEnum.DC);
//        this.cdcNs = new VisString255("cdcNs", tFCEnum.EX);
//        this.cdcName = new VisString255("cdcName", tFCEnum.EX);
//        this.dataNs = new VisString255("dataNs", tFCEnum.EX);
//    }
//    public SPG()
//    {
//        cdc = tCDCEnumEd1.SPG;
//    }

//    [Browsable(false)]
//    public VisString255 cdcName { get; set; }

//    [Browsable(false)]
//    public VisString255 cdcNs { get; set; }

//    [Browsable(false)]
//    public VisString255 d { get; set; }

//    [Browsable(false)]
//    public VisString255 dataNs { get; set; }

//    [Browsable(false)]
//    public Unicode255 dU { get; set; }

//    [Browsable(false)]
//    public BOOLEAN setVal { get; set; }

//    [Browsable(false)]
//    public BOOLEAN setVal2 { get; set; }
//}

///// <summary>
///// This class defines the common data attribute types that are defined for his use in the SPS
///// common data class.
///// </summary>
//public class SPS : DOData
//{
//    /// <summary>
//    /// Single point status (SPS)
//    /// </summary>
//    /// <param name="name">
//    /// A name identifying the DO.
//    /// </param>
//    /// <param name="lnType">
//    /// Containing more detailed functional specification about LN. 
//    /// </param>
//    /// <param name="iedType">
//    /// The manufacturer IED type of the IED to which this LN type belongs.
//    /// </param>
//    public SPS(string name, string lnType, string iedType) : base(name, lnType + name)
//    {
//        cdc = tCDCEnumEd1.SPS;
//        id = type;
//        this.iedType = iedType;
//        this.stVal = new BOOLEAN("stVal", tFCEnum.ST);
//        this.q = new Quality("q", tFCEnum.ST);
//        this.t = new Timestamp("t", tFCEnum.ST);
//        this.subEna = new BOOLEAN("subEna", tFCEnum.SV);
//        this.subVal = new BOOLEAN("subVal", tFCEnum.SV);
//        this.subQ = new Quality("subQ", tFCEnum.SV);
//        this.subID = new VisString64("subID", tFCEnum.SV);
//        this.d = new VisString255("d", tFCEnum.DC);
//        this.dU = new Unicode255("dU", tFCEnum.DC);
//        this.cdcNs = new VisString255("cdcNs", tFCEnum.EX);
//        this.cdcName = new VisString255("cdcName", tFCEnum.EX);
//        this.dataNs = new VisString255("dataNs", tFCEnum.EX);
//    }
//    public SPS()
//    {
//        cdc = tCDCEnumEd1.SPS;
//        this.stVal = new BOOLEAN();
//    }

//    [Browsable(false)]
//    public VisString255 cdcName { get; set; }

//    [Browsable(false)]
//    public VisString255 cdcNs { get; set; }

//    [Browsable(false)]
//    public VisString255 d { get; set; }

//    [Browsable(false)]
//    public VisString255 dataNs { get; set; }

//    [Browsable(false)]
//    public Unicode255 dU { get; set; }

//    [Required]
//    [Browsable(false)]
//    public Quality q { get; set; }

//    [Required]
//    [Browsable(false)]
//    public BOOLEAN stVal { get; set; }

//    [Browsable(false)]
//    public BOOLEAN subEna { get; set; }

//    [Browsable(false)]
//    public VisString64 subID { get; set; }

//    [Browsable(false)]
//    public Quality subQ { get; set; }

//    [Browsable(false)]
//    public BOOLEAN subVal { get; set; }

//    [Required]
//    [Browsable(false)]
//    public Timestamp t { get; set; }
//}

///// <summary>
///// This class defines the common data attribute types that are defined for his use in the WYE
///// common data class.
///// </summary>
//public class WYE : DOData
//{
//    /// <summary>
//    /// Phase to ground related measured values of a three phase system (WYE)
//    /// </summary>
//    /// <param name="name">
//    /// A name identifying the DO.
//    /// </param>
//    /// <param name="lnType">
//    /// Containing more detailed functional specification about LN. 
//    /// </param>
//    /// <param name="iedType">
//    /// The manufacturer IED type of the IED to which this LN type belongs.
//    /// </param>
//    public WYE(string name, string lnType, string iedType) : base(name, lnType + name)
//    {
//        cdc = tCDCEnumEd1.WYE;
//        id = type;
//        this.iedType = iedType;
//        this.phsA = new CMV("phsA", id, iedType);
//        this.phsB = new CMV("phsB", id, iedType);
//        this.phsC = new CMV("phsC", id, iedType);
//        this.neut = new CMV("neut", id, iedType);
//        this.net = new CMV("net", id, iedType);
//        this.res = new CMV("res", id, iedType);
//        this.angRef = new angRef(tFCEnum.CF);
//        this.d = new VisString255("d", tFCEnum.DC);
//        this.dU = new Unicode255("dU", tFCEnum.DC);
//        this.cdcNs = new VisString255("cdcNs", tFCEnum.EX);
//        this.cdcName = new VisString255("cdcName", tFCEnum.EX);
//        this.dataNs = new VisString255("dataNs", tFCEnum.EX);
//    }

//    public WYE()
//    {
//        cdc = tCDCEnumEd1.WYE;


//    }
//    [Browsable(false)]
//    public angRef angRef { get; set; }

//    [Browsable(false)]
//    public VisString255 cdcName { get; set; }

//    [Browsable(false)]
//    public VisString255 cdcNs { get; set; }

//    [Browsable(false)]
//    public VisString255 d { get; set; }

//    [Browsable(false)]
//    public VisString255 dataNs { get; set; }

//    [Browsable(false)]
//    public Unicode255 dU { get; set; }

//    [Browsable(false)]
//    public CMV net { get; set; }

//    [Browsable(false)]
//    public CMV neut { get; set; }
//    [Browsable(false)]
//    public CMV neut1 { get; set; }
//    [Browsable(false)]
//    public CMV neut2 { get; set; }
//    [Browsable(false)]
//    public CMV neut3 { get; set; }
//    [Browsable(false)]
//    public CMV neut4 { get; set; }
//    [Browsable(false)]
//    public CMV neut5 { get; set; }
//    [Browsable(false)]
//    public CMV neut6 { get; set; }
//    [Browsable(false)]
//    public CMV neut7 { get; set; }

//    [Browsable(false)]
//    public CMV phsA { get; set; }

//    [Browsable(false)]
//    public CMV phsB { get; set; }

//    [Browsable(false)]
//    public CMV phsC { get; set; }

//    [Browsable(false)]
//    public CMV res { get; set; }



//}


//#endregion
/// <summary>
/// Angle reference.
/// tBasicTypeEnum.Enum
/// </summary>
public class angRef : DADataType
    {
        private ConversionObject _conversionObject;
        public angRef()
        {
            bType = tBasicTypeEnum.Enum;
        }
        public angRef(tFCEnum fCEnum)
        {
            name = "angRef";
            bType = tBasicTypeEnum.Enum;
            fc = fCEnum;
            this._conversionObject = new ConversionObject();
            if (Value == null)
            {
                Value = this._conversionObject.SetEnumObjectToString(angRefEnum.Va);
            }
            id = type = "angRefEnum";
        }

        [Category("DA"), DisplayName("Value"), Description("Value of attribute.")]
        public angRefEnum tValue
        {
            get
            {
                return (angRefEnum)this._conversionObject.SetStringToEnumObject(Value, typeof(angRefEnum));
            }
            set
            {
                Value = this._conversionObject.SetEnumObjectToString(value);
            }
        }
    }

    /// <summary>
    ///  Scaled value configuration for angles.
    /// </summary>
    public class angSVC : SDIDADataTypeBDA
    {
        public angSVC()
        {
            bType = tBasicTypeEnum.Struct;

        }
        public angSVC(string iedType, string lnType, tFCEnum fCEnum)
        {
            name = "angSVC";
            bType = tBasicTypeEnum.Struct;
            fc = fCEnum;
            id = type = lnType + "angSVC";
            this.iedType = iedType;
            this.ScaledValueConfig = new ScaledValueConfig(iedType, id);
            this.ScaledValueConfig.CheckSelection = true;
        }

        public ScaledValueConfig ScaledValueConfig { get; set; }
    }

    /// <summary>
    /// The array with the points specifying a curve shape.
    /// </summary>	
    public class crvPts : SDIDADataTypeBDA
    {
        public crvPts()
        {
            bType = tBasicTypeEnum.Struct;
        }
        public crvPts(string iedType, string lnType, tFCEnum fCEnum)
        {
            this.iedType = iedType;
            name = "crvPts";
            bType = tBasicTypeEnum.Struct;
            fc = fCEnum;
            id = type = lnType + "crvPts";
            bType = tBasicTypeEnum.Struct;
            this.Point = new Point(iedType, id);
            this.Point.CheckSelection = true;
        }

        public Point Point { get; set; }
    }

    /// <summary>
    /// Specifies the control model that corresponds to the behaviour of the data. 
    /// </summary>
    public class ctlModel : SDIDADataTypeBDA
    {
        private ConversionObject conversionObject;
        public ctlModel()
        {
            bType = tBasicTypeEnum.Enum;
            EnumVal = new List<tEnumVal>();
            FullEnumList(typeof(ctlModelsEnum));
        }


        public ctlModel(string iedType, string lnType, tFCEnum fCEnum)
        {
            name = "ctlModel";
            bType = tBasicTypeEnum.Struct;
            fc = fCEnum;
            id = type = lnType + "ctlModel";
            this.iedType = iedType;
            this.ctlModels = new ctlModels();
            this.ctlModels.CheckSelection = true;
        }
        public ctlModel(tFCEnum fCEnum)
        {
            this.conversionObject = new ConversionObject();
            if (Value == null)
            {
                Value = this.conversionObject.SetEnumObjectToString(ctlModelsEnum.status_only);
            }
            name = "ctlModel";
            fc = fCEnum;
            bType = tBasicTypeEnum.Enum;
            id = type = "ctlModelsEnum";

        }
        [XmlIgnore]
        [Category("DA"), DisplayName("Value"), Description("Value of attribute.")]
        public ctlModelsEnum tValue
        {
            get
            {
                return (ctlModelsEnum)this.conversionObject.SetStringToEnumObject(Value, typeof(ctlModelsEnum));
            }
            set
            {
                Value = this.conversionObject.SetEnumObjectToString(value);
            }
        }
        [XmlIgnore]
        public ctlModels ctlModels { get; set; }
    }

    /// <summary> 
    ///  Determines the control activity.
    /// </summary>
    /// <remarks>	
    /// The set of values for ctlValEnum and valModEnum was modified and a constructor method was created 
    /// to fullfill the standard IEC61850 Ed.1.0.
    /// </remarks>
    public class ctlVal : DADataType
    {
        private ConversionObject _conversionObject;

        public ctlVal()
        {
            this._conversionObject = new ConversionObject();
            if (Value == null)
            {
                Value = this._conversionObject.SetEnumObjectToString(valModEnum.on);
            }
            name = "ctlVal";
            bType = tBasicTypeEnum.BOOLEAN;
        }

        public ctlVal(string enumtype)
        {
            name = "ctlVal";
            bType = tBasicTypeEnum.BOOLEAN;
            if (enumtype == "Mod")
            {
                bType = tBasicTypeEnum.Enum;
                FullEnumList(typeof(modEnum));
            }

        }

        public ctlVal(tFCEnum fCEnum)
        {
            this._conversionObject = new ConversionObject();
            if (Value == null)
            {
                Value = this._conversionObject.SetEnumObjectToString(valModEnum.on);
            }
            name = "ctlVal";
            bType = tBasicTypeEnum.Enum;
            fc = fCEnum;
            id = type = "ctlValEnum";
        }

        [Category("DA"), DisplayName("Value"), Description("Value of attribute.")]
        public valModEnum tValue
        {
            get
            {
                return (valModEnum)this._conversionObject.SetStringToEnumObject(Value, typeof(valModEnum));
            }
            set
            {
                Value = this._conversionObject.SetEnumObjectToString(value);
            }
        }
    }

    /// <summary>
    /// Deadbanded complex value.
    /// tBasicTypeEnum.struct
    /// </summary>
    public class cVal : SDIDADataTypeBDA
    {
        public cVal()
        {
            bType = tBasicTypeEnum.Struct;
            Vector = new Vector();
        }
        public cVal(string iedType, string lnType, tFCEnum fCEnum)
        {
            name = "cVal";
            bType = tBasicTypeEnum.Struct;
            fc = fCEnum;
            id = type = lnType + "cVal";
            this.iedType = iedType;
            this.Vector = new Vector(iedType, id);
            this.Vector.CheckSelection = true;
        }

        public Vector Vector { get; set; }

        public mag mag { get { return Vector.mag; } set { Vector.mag = value; } }

    }

    /// <summary>
    /// General direction of the fault.
    /// tBasicTypeEnum.Enum
    /// </summary>
    public class dirGeneral : DADataType
    {
        private ConversionObject _conversionObject;
        public dirGeneral()
        {
            bType = tBasicTypeEnum.Enum;
            type = "dir";
            FullEnumList(typeof(dirEnum));
        }
   
        public dirGeneralEnum tValue
        {
            get
            {
                return (dirGeneralEnum)this._conversionObject.SetStringToEnumObject(Value, typeof(dirGeneralEnum));
            }
            set
            {
                Value = this._conversionObject.SetEnumObjectToString(value);
            }
        }
    }

 

    /// <summary>
    /// Direction of the fault for phase A.
    /// </summary>
    public class dirPhs : DADataType
    {
        public dirPhs()
        {
            bType = tBasicTypeEnum.Enum;
            FullEnumList(typeof(dirPhsEnum));
        }
    }
    

    /// <summary>
    /// Specifies the reference type which the data attribute mag of the data attribute type Vector 
    /// contain.
    /// </summary>
    public class hvRef : DADataType
    {
        private ConversionObject _conversionObject;
        public hvRef()
        {
            bType = tBasicTypeEnum.Enum;
        }
        public hvRef(tFCEnum fCEnum)
        {
            this._conversionObject = new ConversionObject();
            if (Value == null)
            {
                Value = this._conversionObject.SetEnumObjectToString(hvRefEnum.fundamental);
            }
            name = "hvRef";
            bType = tBasicTypeEnum.Enum;
            fc = fCEnum;
            id = type = "hvRefEnum";
        }

        [Category("DA"), DisplayName("Value"), Description("It´s the value of attribute.")]
        public hvRefEnum tValue
        {
            get
            {
                return (hvRefEnum)this._conversionObject.SetStringToEnumObject(Value, typeof(hvRefEnum));
            }
            set
            {
                Value = this._conversionObject.SetEnumObjectToString(value);
            }
        }
    }

    /// <summary>
    /// Instant value of a vector type value.
    /// </summary>
    public class instCVal : SDIDADataTypeBDA
    {
        public instCVal()
        {
            bType = tBasicTypeEnum.Struct;
            AnalogueValue=new AnalogueValue();
           Vector=new Vector();
        }
        public instCVal(string iedType, string lnType, tFCEnum fCEnum)
        {
            name = "instCVal";
            bType = tBasicTypeEnum.Struct;
            fc = fCEnum;
            id = type = lnType + "instCVal";
            this.iedType = iedType;
            this.Vector = new Vector(iedType, id);
            this.Vector.CheckSelection = true;
        }

        public AnalogueValue AnalogueValue { get; set; }
        public FLOAT32 f
        {
            get { return AnalogueValue.f; }
            set { AnalogueValue.f = value; }
        }
        public Vector Vector { get; set; }

        public mag mag
        {
            get => Vector.mag;
            set => Vector.mag = value;
        }
    }

    /// <summary>
    /// Magnitude of a the instantaneous value of a measured value.
    /// </summary>
    public class instMag : SDIDADataTypeBDA
    {
        public instMag()
        {
            bType = tBasicTypeEnum.Struct;
            AnalogueValue=new AnalogueValue();
        }
        public instMag(string iedType, string lnType, tFCEnum fCEnum)
        {
            name = "instMag";
            bType = tBasicTypeEnum.Struct;
            fc = fCEnum;
            id = type = lnType + "instMag";
            this.iedType = iedType;
            this.AnalogueValue = new AnalogueValue(iedType, id);
            this.AnalogueValue.CheckSelection = true;
        }

        public AnalogueValue AnalogueValue { get; set; }
        public FLOAT32 f
        {
            get { return AnalogueValue.f; }
            set { AnalogueValue.f = value; }
        }
    }

    /// <summary>
    /// Deadbanded value.
    /// </summary>
    public class mag : SDIDADataTypeBDA
    {
        public mag()
        {
            bType = tBasicTypeEnum.Struct;
            AnalogueValue = new AnalogueValue();
        }
        public mag(string iedType, string lnType, tFCEnum fCEnum)
        {
            name = "mag";
            bType = tBasicTypeEnum.Struct;
            fc = fCEnum;
            id = type = lnType + "mag";
            this.iedType = iedType;
            this.AnalogueValue = new AnalogueValue(iedType, id);
            this.AnalogueValue.CheckSelection = true;
        }

        public mag(string iedType, string lnType)
        {
            name = "mag";
            bType = tBasicTypeEnum.Struct;
            id = type = lnType + "mag";
            this.iedType = iedType;
            this.AnalogueValue = new AnalogueValue(iedType, id);
            this.AnalogueValue.CheckSelection = true;
        }

        public AnalogueValue AnalogueValue { get; set; }

        public FLOAT32 f
        {
            get { return AnalogueValue.f; }
            set { AnalogueValue.f = value; }
        }
    }

    /// <summary>
    /// Scaled value configuration for magnitude.
    /// </summary>
    public class magSVC : SDIDADataTypeBDA
    {
        public magSVC()
        {
            bType = tBasicTypeEnum.Struct;
        }
        public magSVC(string iedType, string lnType, tFCEnum fCEnum)
        {
            name = "magSVC";
            bType = tBasicTypeEnum.Struct;
            fc = fCEnum;
            id = type = lnType + "magSVC";
            this.iedType = iedType;
            this.ScaledValueConfig = new ScaledValueConfig(iedType, id);
            this.ScaledValueConfig.CheckSelection = true;
        }

        public ScaledValueConfig ScaledValueConfig { get; set; }
    }

    /// <summary>
    /// Maximum process measurement for which values of i or f are considered within process limits.
    /// </summary>
    public class max : SDIDADataTypeBDA
    {
        public max()
        {
            bType = tBasicTypeEnum.Struct;
            this.AnalogueValue = new AnalogueValue(iedType, id);
        }
        public max(string iedType, string lnType, tFCEnum fCEnum)
        {
            name = "max";
            bType = tBasicTypeEnum.Struct;
            fc = fCEnum;
            id = type = lnType + "max";
            this.iedType = iedType;
            this.AnalogueValue = new AnalogueValue(iedType, id);
            this.AnalogueValue.CheckSelection = true;
        }

        public max(string iedType, string lnType)
        {
            name = "max";
            bType = tBasicTypeEnum.Struct;
            id = type = lnType + "max";
            this.iedType = iedType;
            this.AnalogueValue = new AnalogueValue(iedType, id);
            this.AnalogueValue.CheckSelection = true;
        }

        public AnalogueValue AnalogueValue { get; set; }
        public FLOAT32 f
        {
            get { return AnalogueValue.f; }
            set { AnalogueValue.f = value; }
        }
    }

    /// <summary>
    /// Defines together with minVal the setting range.
    /// INC -> ING -> tBasicTypeEnum.INT32 
    /// BSC -> ISC -> tBasicTypeEnum.INT8
    /// APC -> ASG -> estructura compleja AnalogueValue tBasicTypeEnum.Struct
    /// </summary>
    public class maxVal : SDIDADataTypeBDA
    {
        public maxVal()
        {
            bType = tBasicTypeEnum.Struct;
        }
        public maxVal(string iedType, string lnType, tFCEnum fCEnum)
        {
            name = "maxVal";
            bType = tBasicTypeEnum.Struct;
            fc = fCEnum;
            id = type = lnType + "maxVal";
            this.iedType = iedType;
            this.AnalogueValue = new AnalogueValue(iedType, id);
            this.AnalogueValue.CheckSelection = true;
        }
        public AnalogueValue AnalogueValue { get; set; }
        public FLOAT32 f
        {
            get { return AnalogueValue.f; }
            set { AnalogueValue.f = value; }
        }
    }

    /// <summary>
    /// Minimum process measurement for which values of i or f are considered within process limits.
    /// </summary>
    public class min : SDIDADataTypeBDA
    {
        public min()
        {
            bType = tBasicTypeEnum.Struct;
            this.AnalogueValue = new AnalogueValue(iedType, id);
        }


        public min(string iedType, string lnType, tFCEnum fCEnum)
        {
            name = "min";
            bType = tBasicTypeEnum.Struct;
            fc = fCEnum;
            id = type = lnType + "min";
            this.iedType = iedType;
            this.AnalogueValue = new AnalogueValue(iedType, id);
            this.AnalogueValue.CheckSelection = true;
        }

        public min(string iedType, string lnType)
        {
            name = "min";
            bType = tBasicTypeEnum.Struct;
            id = type = lnType + "min";
            this.iedType = iedType;
            this.AnalogueValue = new AnalogueValue(iedType, id);
            this.AnalogueValue.CheckSelection = true;
        }

        public AnalogueValue AnalogueValue { get; set; }
        public FLOAT32 f
        {
            get { return AnalogueValue.f; }
            set { AnalogueValue.f = value; }
        }
    }

    /// <summary>
    /// Defines together with maxVal the setting range.
    /// INC -> ING -> tBasicTypeEnum.INT32
    /// BSC -> ISC -> tBasicTypeEnum.INT8
    /// APC -> ASG -> estructura compleja AnalogueValue tBasicTypeEnum.Struct
    /// </summary>
    public class minVal : SDIDADataTypeBDA
    {
        public minVal()
        {
            bType = tBasicTypeEnum.Struct;
        }
        public minVal(string iedType, string lnType, tFCEnum fCEnum)
        {
            name = "minVal";
            bType = tBasicTypeEnum.Struct;
            fc = fCEnum;
            id = type = lnType + "minVal";
            this.iedType = iedType;
            this.AnalogueValue = new AnalogueValue(iedType, id);
            this.AnalogueValue.CheckSelection = true;
        }
        public AnalogueValue AnalogueValue { get; set; }
        public FLOAT32 f
        {
            get { return AnalogueValue.f; }
            set { AnalogueValue.f = value; }
        }
    }

    /// <summary>
    /// Contains information related to the originator of the last change of the controllable value 
    /// of the data.
    /// </summary>
    public class Originator : SDIDADataTypeBDA
    {
        public Originator()
        {
            bType = tBasicTypeEnum.Struct;
            orCat = new orCat();
            orIdent = new Octet64();
        }
        public Originator(string iedType, string lnType, tFCEnum fCEnum)
        {
            name = "origin";
            bType = tBasicTypeEnum.Struct;
            fc = fCEnum;
            id = type = lnType + "Originator";
            this.iedType = iedType;
            this.orCat = new orCat();
            this.orIdent = new Octet64("orIdent");
        }

        public Originator(string iedType, string lnType)
        {
            name = "origin";
            bType = tBasicTypeEnum.Struct;
            id = type = lnType + "Originator";
            this.iedType = iedType;
            this.orCat = new orCat();
            this.orIdent = new Octet64("orIdent");
        }

        [Required]
        public orCat orCat { get; set; }

        [Required]
        public Octet64 orIdent { get; set; }
    }

    /// <summary>
    /// Indicates which phase has been used as reference for the transformation of phase values to 
    /// sequence values.
    /// tBasicTypeEnum.Enum
    /// </summary>
    public class phsRef : DADataType
    {
        private ConversionObject conversionObject;
        public phsRef()
        {
            bType = tBasicTypeEnum.Enum;
        }
        public phsRef(tFCEnum fCEnum)
        {
            this.conversionObject = new ConversionObject();
            if (Value == null)
            {
                Value = this.conversionObject.SetEnumObjectToString(phsRefEnum.A);
            }
            name = "phsRef";
            bType = tBasicTypeEnum.Enum;
            fc = fCEnum;
            id = type = "phsRefEnum";
        }

        [Category("DA"), DisplayName("Value"), Description("Value of attribute.")]
        public phsRefEnum tValue
        {
            get
            {
                return (phsRefEnum)this.conversionObject.SetStringToEnumObject(Value, typeof(phsRefEnum));
            }
            set
            {
                Value = this.conversionObject.SetEnumObjectToString(value);
            }
        }
    }

    /// <summary>
    /// Magnitude of the counted value per count.
    /// tBasicTypeEnum.FLOAT32
    /// </summary>
    public class pulsQty : SDIDADataTypeBDA
    {
        public pulsQty()
        {
            bType = tBasicTypeEnum.Struct;
        }

        public pulsQty(string iedType, string lnType, tFCEnum fCEnum)
        {
            name = "pulsQty";
            bType = tBasicTypeEnum.Struct;
            fc = fCEnum;
            id = type = lnType + "pulsQty";
            this.iedType = iedType;
            this.PulseConfig = new PulseConfig(iedType, id);
            this.PulseConfig.CheckSelection = true;
        }

        public PulseConfig PulseConfig { get; set; }
    }

    /// <summary>
    /// Range in which the current value of instMag or instCVal.mag is.
    /// </summary>
    public class range : SDIDADataTypeBDA
    {
        private ConversionObject _conversionObject;

        public range()
        {
            bType = tBasicTypeEnum.Enum;
            FullEnumList(typeof(rangeEnum));
            AnalogueValue=new AnalogueValue();
        }
        public range(tFCEnum fCEnum)
        {
            name = "range";
            bType = tBasicTypeEnum.Enum;
            fc = fCEnum;
            id = type = "rangeEnum";
            this._conversionObject = new ConversionObject();
            if (Value == null)
            {
                Value = this._conversionObject.SetEnumObjectToString(rangeEnum.normal);
            }
        }

        public range(string iedType, string lnType, tFCEnum fCEnum)
        {
            name = "range";
            bType = tBasicTypeEnum.Struct;
            fc = fCEnum;
            id = type = lnType + "range";
            this.iedType = iedType;
            this.AnalogueValue = new AnalogueValue(iedType, id);
            this.AnalogueValue.CheckSelection = true;
        }

        [Category("DA"), DisplayName("Value"), Description("It´s the value of attribute.")]
        public rangeEnum tValue
        {
            get
            {
                return (rangeEnum)this._conversionObject.SetStringToEnumObject(Value, typeof(rangeEnum));
            }
            set
            {
                Value = this._conversionObject.SetEnumObjectToString(value);
            }
        }

        public AnalogueValue AnalogueValue { get; set; }
        public FLOAT32 f
        {
            get { return AnalogueValue.f; }
            set { AnalogueValue.f = value; }
        }
    }

    /// <summary>
    /// Configuration parameters as used in the context with the range attribute.
    /// </summary>
    public class rangeC : SDIDADataTypeBDA
    {
        public rangeC()
        {
            bType = tBasicTypeEnum.Enum;
            RangeConfig = new RangeConfig();
        }
        public rangeC(string iedType, string lnType, tFCEnum fCEnum)
        {
            name = "rangeC";
            bType = tBasicTypeEnum.Struct;
            fc = fCEnum;
            id = type = lnType + "rangeC";
            this.iedType = iedType;

            this.RangeConfig = new RangeConfig(iedType, id);
            this.RangeConfig.CheckSelection = true;
        }

        public RangeConfig RangeConfig { get; set; }

    }

    /// <summary>
    /// Specifies the SBO-class according to the control model that corresponds to the behaviour 
    /// of the data. 
    /// </summary>
    public class sboClass : SDIDADataTypeBDA
    {
        public sboClass()
        {
            bType = tBasicTypeEnum.Struct;
        }
        public sboClass(string iedType, string lnType, tFCEnum fCEnum)
        {
            name = "sboClass";
            bType = tBasicTypeEnum.Struct;
            fc = fCEnum;
            id = type = lnType + "sboClass";
            this.iedType = iedType;
            this.SboClasses = new SboClasses();
            this.SboClasses.CheckSelection = true;
        }
        [XmlIgnore]
        public SboClasses SboClasses { get; set; }
    }

    /// <summary>
    /// This attribute shall specify the type of the sequence.
    /// tBasicTypeEnum.Enum
    /// </summary>
    public class seqT : DADataType
    {
        private ConversionObject _conversionObject;
        public seqT()
        {
            bType = tBasicTypeEnum.Enum;
            FullEnumList(typeof(seqTEnum));
        }
        public seqT(tFCEnum fCEnum)
        {
            this._conversionObject = new ConversionObject();
            if (Value == null)
            {
                Value = this._conversionObject.SetEnumObjectToString(seqTEnum.pos_neg_zero);
            }
            name = "seqT";
            bType = tBasicTypeEnum.Enum;
            fc = fCEnum;
            id = type = "seqTEnum";
        }
        [XmlIgnore]
        [Category("DA"), DisplayName("Value"), Description("Value of attribute.")]
        public seqTEnum tValue
        {
            get
            {
                return (seqTEnum)this._conversionObject.SetStringToEnumObject(Value, typeof(seqTEnum));
            }
            set
            {
                Value = this._conversionObject.SetEnumObjectToString(value);
            }
        }
    }

   
    /// <summary>
    /// Severity of the last violation detected. 
    /// tBasicTypeEnum.Enum
    /// </summary>
    public class sev : DADataType
    {
        private ConversionObject _conversionObject;
        public sev()
        {
            bType = tBasicTypeEnum.Enum;
            FullEnumList(typeof(sevEnum));
        }
        public sev(tFCEnum fCEnum)
        {
            this._conversionObject = new ConversionObject();
            if (Value == null)
            {
                Value = this._conversionObject.SetEnumObjectToString(sevEnum.unknown);
            }
            name = "sev";
            bType = tBasicTypeEnum.Enum;
            fc = fCEnum;
            id = type = "sevEnum";
        }
        [XmlIgnore]
        [Category("DA"), DisplayName("Value"), Description("It´s the value of attribute.")]
        public sevEnum tValue
        {
            get
            {
                return (sevEnum)this._conversionObject.SetStringToEnumObject(Value, typeof(sevEnum));
            }
            set
            {
                Value = this._conversionObject.SetEnumObjectToString(value);
            }
        }
    }



    public class ENUMERATED : DADataType
    {
        public ENUMERATED()
        {
            Value = "";
            bType = tBasicTypeEnum.Enum;
        }
    }

    public class ObjectReference : DADataType
    {
        public ObjectReference()
        {
            Value = "";
            bType = tBasicTypeEnum.VisString255;
        }
    }




    public class CodedEnum : DADataType
    {
        public CodedEnum()
        {
            Value = "";
            bType = tBasicTypeEnum.Enum;
             FullEnumList(typeof(valEnum));
        }
    }

    /// <summary>
    /// Status value of the data.
    /// INS -> INC -> tBasicTypeEnum.INT32 
    /// SPS -> SPC -> tBasicTypeEnum.BOOLEAN
    /// DPC -> 	tBasicTypeEnum.Enum
    /// INC -> tBasicTypeEnum.INT32 
    /// </summary>
    public class stVal : DADataType
    {
        private ConversionObject _conversionObject;
        /// <summary>
        /// This constructor was created empty and the data will be set the values when 
        /// the type changes from enum to int32
        /// </summary>
        public stVal()
        {
            Value = "";
            bType = tBasicTypeEnum.BOOLEAN;
        }
        public stVal(string type)
        {
            Value = "";
            bType = tBasicTypeEnum.Enum;
            if (type == "Mod")
            {
                FullEnumList(typeof(modEnum));
                return;
            }
            if (type == "Beh")
            {
               
                return;
            }
            if (type == "CBOpCap")
            {
                FullEnumList(typeof(CBOpCapEnum));
                return;
            }
            if (type == "Health")
            {
                FullEnumList(typeof(healthEnum));
                return;
            }
            if (type == "AutoRecSt")
            {
                FullEnumList(typeof(autoRecStEnum));
                return;
            }
            if ((type == "SupRs") || (type == "BlkCmd") || (type == "BlkMeas") || (type == "BlkUpd"))
            {
                bType = tBasicTypeEnum.BOOLEAN;
                return;
            }
            if ((type == "SrcOpPrm") || (type == "LocSwPos") || (type == "StrCndZ")||(type== "IntIn"))
            {
                bType = tBasicTypeEnum.INT32;
                return;
            }
            
        }

        public stVal(tFCEnum fCEnum)
        {
            this._conversionObject = new ConversionObject();
            if (Value == null)
            {
                Value = this._conversionObject.SetEnumObjectToString(valModEnum.on);
            }
            name = "stVal";
            bType = tBasicTypeEnum.Enum;
            fc = fCEnum;
            id = type = " stValEnum";
            Array valuesEnumArray = this._conversionObject.GetValuesEnumToArray(typeof(valModEnum));
        }
        [XmlIgnore]
        [Category("DA"), DisplayName("Value"), Description("Value of attribute.")]
        public valModEnum tValue
        {
            get
            {
                return (valModEnum)this._conversionObject.SetStringToEnumObject(Value, typeof(valModEnum));
            }
            set
            {
                Value = this._conversionObject.SetEnumObjectToString(value);
            }
        }
    }

    /// <summary>
    /// Value used to substitute the data attribute instCVal.
    /// </summary>
    public class subCVal : SDIDADataTypeBDA
    {
        private ConversionObject _conversionObject;
        public subCVal()
        {
            bType = tBasicTypeEnum.Struct;
            Vector = new Vector();
        }
        public subCVal(string iedType, string lnType, tFCEnum fCEnum)
        {
            this._conversionObject = new ConversionObject();
            if (Value == null)
            {
                Value = this._conversionObject.SetEnumObjectToString(valEnum.intermediate_state);
            }
            name = "subCVal";
            bType = tBasicTypeEnum.Struct;
            fc = fCEnum;
            id = type = lnType + "subCVal";
            this.iedType = iedType;
            this.Vector = new Vector(iedType, id);

        }
        [XmlIgnore]
        [Category("DA"), DisplayName("Value"), Description("It´s the value of attribute.")]
        public valEnum tValue
        {
            get
            {
                return (valEnum)this._conversionObject.SetStringToEnumObject(Value, typeof(valEnum));
            }
            set
            {
                Value = this._conversionObject.SetEnumObjectToString(value);
            }
        }
        [XmlIgnore]
        public Vector Vector { get; set; }
        [XmlIgnore]
        public mag mag { get { return Vector.mag; } set { Vector.mag = value; } }
    }

    /// <summary>
    /// Value used to substitute the attribute representing the value of the data instance.
    /// INS -> DPS -> DPC -> tBasicTypeEnum.Enum
    /// MV -> tBasicTypeEnum.Struct -> AnalogueValue
    /// SPS -> SPC -> tBasicTypeEnum.BOOLEAN
    /// INC -> tBasicTypeEnum.INT32
    /// BSC -> ISC -> ValWithTrans
    /// </summary>
    public class subVal : SDIDADataTypeBDA
    {
        public subVal(tFCEnum fCEnum)
        {
            name = "subVal";
            bType = tBasicTypeEnum.Enum;
            fc = fCEnum;
            id = type = " subValEnum";
            EnumVal = new List<tEnumVal>();
            tEnumVal enumVal0 = new tEnumVal();
            enumVal0.ord = "0";
            enumVal0.Value = "intermediate-state";
            EnumVal.Add(enumVal0);
            tEnumVal enumVal1 = new tEnumVal();
            enumVal1.ord = "1";
            enumVal1.Value = "off";
            EnumVal.Add(enumVal1);
            tEnumVal enumVal2 = new tEnumVal();
            enumVal2.ord = "2";
            enumVal2.Value = "on";
            EnumVal.Add(enumVal2);
            tEnumVal enumVal3 = new tEnumVal();
            enumVal3.ord = "3";
            enumVal3.Value = "bad-state";
            EnumVal.Add(enumVal3);
        }

        public subVal(string iedType, string lnType, tFCEnum fCEnum)
        {
            name = "subVal";
            bType = tBasicTypeEnum.Struct;
            fc = fCEnum;
            id = type = lnType + "subVal";
            this.iedType = iedType;
        }
        public subVal()
        {
            bType = tBasicTypeEnum.Struct;
        }
        public void SetLinkSDIDADataTypeBDA(SDIDADataTypeBDA subValStructure)
        {
            this.SDIDADataTypeBDA = subValStructure;
        }

        public SDIDADataTypeBDA SDIDADataTypeBDA { get; set; }
    }

  
    

    /// <summary>
    /// Value with transient indication.
    /// </summary>
    public class valWTr : SDIDADataTypeBDA
    {
        public valWTr()
        {
            bType = tBasicTypeEnum.Struct;
        }
        public valWTr(string iedType, string lnType, tFCEnum fCEnum)
        {
            name = "valWTr";
            bType = tBasicTypeEnum.Struct;
            fc = fCEnum;
            id = type = lnType + "valWTr";
            this.iedType = iedType;
            this.ValWithTrans = new ValWithTrans(iedType, id);
            this.ValWithTrans.CheckSelection = true;
        }

        public ValWithTrans ValWithTrans { get; set; }
    }

  

    public class valMod : DADataType
    {
        private ConversionObject _conversionObject;

        public valMod()
        {
            bType = tBasicTypeEnum.Enum;
        }
        public valMod(tFCEnum fCEnum)
        {
            this._conversionObject = new ConversionObject();
            if (Value == null)
            {
                Value = this._conversionObject.SetEnumObjectToString(valModEnum.on);
            }
            name = "stVal";
            bType = tBasicTypeEnum.Enum;
            fc = fCEnum;
            id = type = " stValEnum";
        }

        [Category("DA"), DisplayName("Value"), Description("Value of attribute.")]
        public valModEnum tValue
        {
            get
            {
                return (valModEnum)this._conversionObject.SetStringToEnumObject(Value, typeof(valModEnum));
            }
            set
            {
                Value = this._conversionObject.SetEnumObjectToString(value);
            }
        }
    }
}
