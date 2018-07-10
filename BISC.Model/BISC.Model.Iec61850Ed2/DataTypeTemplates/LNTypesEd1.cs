namespace BISC.Model.Iec61850Ed2.DataTypeTemplates
{
    //#region MyRegion


    ///// <summary>
    ///// This class defines the logical node attributes that are defined for his use in the LN 
    ///// class "ANCR" (Neutral current regulator).
    ///// </summary>
    ///// <remarks>
    ///// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    ///// has to be changed from Enum to String.
    ///// </remarks>	
    //public class ANCR : CommonLogicalNode
    //    {
    //        public ANCR()
    //        {
    //            this.lnClass = tLNClassEnum.ANCR.ToString();
    //        }

    //        public ANCR(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.lnClass = tLNClassEnum.ANCR.ToString();
    //            //this.TapChg = new BSC("TapChg", lnType, iedType);
    //            //this.OpCntRs = new INC("OpCntRs", lnType, iedType);
    //            //this.Auto = new SPS("Auto", lnType, iedType);
    //            //this.Loc = new SPS("Loc", lnType, iedType);
    //            //this.LCol = new SPC("LCol", lnType, iedType);
    //            //this.RCol = new SPC("RCol", lnType, iedType);
    //            //this.HaRstField = new ING("HaRst", lnType, iedType);
    //        }

    //        [Required]
    //        public BSC TapChg { get; set; }

    //        public INC OpCntRs { get; set; }

    //        public SPC LCol { get; set; }

    //        public SPC RCol { get; set; }

    //        public SPS Auto { get; set; }

    //        [Required]
    //        public SPS Loc { get; set; }

    //        public ING HaRstField { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "ARCO" (Reactive power control).
    //    /// </summary>
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>	
    //    public class ARCO : CommonLogicalNode
    //    {
    //        public ARCO()
    //        {
    //            this.lnClass = tLNClassEnum.ARCO.ToString();
    //        }
    //        public ARCO(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.ARCO.ToString();
    //            this.iedType = iedType;
    //            this.TapChg = new BSC("TapChg", lnType, iedType);
    //            this.OpCntRs = new INC("OpCntRs", lnType, iedType);
    //            this.Auto = new SPS("Auto", lnType, iedType);
    //            this.DschBlk = new SPS("DschBlk", lnType, iedType);
    //            this.Loc = new SPS("Loc", lnType, iedType);
    //            this.NeutAlm = new SPS("NeutAlm", lnType, iedType);
    //            this.VOvSt = new SPS("VOvSt", lnType, iedType);
    //        }

    //        [Required]
    //        public BSC TapChg { get; set; }

    //        public INC OpCntRs { get; set; }

    //        public SPS Auto { get; set; }

    //        public SPS DschBlk { get; set; }

    //        [Required]
    //        public SPS Loc { get; set; }

    //        public SPS NeutAlm { get; set; }

    //        public SPS VOvSt { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "ATCC" (Automatic tap changer controller).
    //    /// </summary>	
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class ATCC : CommonLogicalNode
    //    {
    //        private MV HiDmdAField;
    //        public ATCC()
    //        {
    //            this.lnClass = tLNClassEnum.ATCC.ToString();
    //        }
    //        public ATCC(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.lnClass = tLNClassEnum.ATCC.ToString();
    //        }

    //        public ASG BlkLV { get; set; }

    //        public ASG BlkRV { get; set; }

    //        public ASG BndCtr { get; set; }

    //        public ASG BndWid { get; set; }

    //        public ASG LDCR { get; set; }

    //        public ASG LDCX { get; set; }

    //        public ASG LDCZ { get; set; }

    //        public ASG LimLodA { get; set; }

    //        public ASG RnbkRV { get; set; }

    //        public ASG VRedVal { get; set; }

    //        public BSC TapChg { get; set; }

    //        [Required]
    //        public DPC ParOp { get; set; }

    //        public INC OpCntRs { get; set; }

    //        public ING CtlDlTmms { get; set; }

    //        public ING TapBlkL { get; set; }

    //        public ING TapBlkR { get; set; }

    //        public INS HiTapPos { get; set; }

    //        public INS LoTapPos { get; set; }

    //        public ISC TapPos { get; set; }

    //        public MV CircA { get; set; }

    //        [Required]
    //        public MV CtlV { get; set; }

    //        public MV HiCtlV { get; set; }

    //        public MV HiDmdA { get; set; }

    //        public MV LoCtlV { get; set; }

    //        public MV LodA { get; set; }

    //        public MV PhAng { get; set; }

    //        public SPC LTCBlk { get; set; }

    //        public SPC LTCDragRs { get; set; }

    //        public SPC VRed1 { get; set; }

    //        public SPC VRed2 { get; set; }

    //        public SPG LDC { get; set; }

    //        public SPG TmDlChr { get; set; }

    //        public SPS Auto { get; set; }

    //        [Required]
    //        public SPS Loc { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "AVCO" (Voltage control).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class AVCO : CommonLogicalNode
    //    {
    //        public AVCO()
    //        {
    //            this.lnClass = tLNClassEnum.AVCO.ToString();
    //        }
    //        public AVCO(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.lnClass = tLNClassEnum.AVCO.ToString();
    //        }

    //        public ASG LimAOv { get; set; }

    //        public ASG LimVOv { get; set; }

    //        [Required]
    //        public BSC TapChg { get; set; }

    //        public INC OpCntRs { get; set; }

    //        public SPS Auto { get; set; }

    //        public SPS BlkAOv { get; set; }

    //        public SPS BlkEF { get; set; }

    //        [Required]
    //        public SPS Loc { get; set; }

    //        public SPS BlkVOvField { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "CALH" (Alarm handling).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class CALH : CommonLogicalNode
    //    {
    //        public CALH()
    //        {
    //            this.lnClass = tLNClassEnum.CALH.ToString();
    //        }
    //        public CALH(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.lnClass = tLNClassEnum.CALH.ToString();
    //        }

    //        public SPS AlmLstOv { get; set; }

    //        [Required]
    //        public SPS GrAlm { get; set; }

    //        public SPS GrWrn { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "CCGR" (Cooling group control).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class CCGR : CommonLogicalNode
    //    {
    //        public CCGR()
    //        {
    //            this.lnClass = tLNClassEnum.CCGR.ToString();
    //        }
    //        public CCGR(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.lnClass = tLNClassEnum.CCGR.ToString();
    //        }

    //        public ASG OilTmpSet { get; set; }

    //        public DPL EEName { get; set; }

    //        public INC FanCtl { get; set; }

    //        public INC FanCtlGen { get; set; }

    //        public INC PmpCtl { get; set; }

    //        public INC PmpCtlGen { get; set; }

    //        public INS EEHealth { get; set; }

    //        public INS OpTmh { get; set; }

    //        public MV EnvTmp { get; set; }

    //        public MV FanA { get; set; }

    //        public MV FanFlw { get; set; }

    //        public MV OilMotA { get; set; }

    //        public MV OilTmpln { get; set; }

    //        public MV OilTmpOut { get; set; }

    //        public SPC CECtl { get; set; }

    //        public SPS Auto { get; set; }

    //        public SPS FanOvCur { get; set; }

    //        public SPS PmpAlm { get; set; }

    //        public SPS PmpOvCur { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "CILO" (Interlocking).
    //    /// </summary>
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class CILO : CommonLogicalNode
    //    {
    //        public CILO()
    //        {
    //            this.lnClass = tLNClassEnum.CILO.ToString();
    //        }
    //        public CILO(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.lnClass = tLNClassEnum.CILO.ToString();
    //        }

    //        [Required]
    //        public SPS EnaCls { get; set; }

    //        [Required]
    //        public SPS EnaOpn { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "CPOW" (Point-on-wave switching).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class CPOW : CommonLogicalNode
    //    {
    //        public CPOW()
    //        {
    //            this.lnClass = tLNClassEnum.CPOW.ToString();
    //        }
    //        public CPOW(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.lnClass = tLNClassEnum.CPOW.ToString();
    //        }

    //        public ACT OpCls { get; set; }

    //        public ACT OpOpn { get; set; }

    //        public ING MaxDlTmms { get; set; }

    //        public SPS StrPOW { get; set; }

    //        [Required]
    //        public SPS TmExc { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "CSWI" (Switch controller).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class CSWI : CommonLogicalNode
    //    {
    //        public CSWI()
    //        {
    //            this.lnClass = tLNClassEnum.CSWI.ToString();
    //            this.Pos = new DPC();
    //            Pos.stVal.bType = tBasicTypeEnum.Dbpos;
    //            Pos.subVal.bType = tBasicTypeEnum.Dbpos;
    //        }

    //        public CSWI(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.lnClass = tLNClassEnum.CSWI.ToString();
    //        }
    //        [XmlIgnore]
    //        public ACT OpCls { get; set; }
    //        [XmlIgnore]
    //        public ACT OpOpn { get; set; }

    //        [Required, XmlIgnore]
    //        public DPC Pos { get; set; }
    //        [XmlIgnore]
    //        public DPC PosA { get; set; }
    //        [XmlIgnore]
    //        public DPC PosB { get; set; }
    //        [XmlIgnore]
    //        public DPC PosC { get; set; }
    //        [XmlIgnore]
    //        private INC OpCntRs { get; set; }
    //        [XmlIgnore]
    //        public SPS Loc { get; set; }
    //        [XmlIgnore]
    //        public SPC BlkCmd { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "GAPC" (Generic automatic process control).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class GAPC : CommonLogicalNode
    //    {
    //        public GAPC()
    //        {
    //            this.lnClass = tLNClassEnum.GAPC.ToString();
    //        }
    //        public GAPC(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.lnClass = tLNClassEnum.GAPC.ToString();
    //        }

    //        [Required]
    //        public ACD Str { get; set; }

    //        [Required]
    //        public ACT Op { get; set; }

    //        public ASG StrVal { get; set; }

    //        public DPC DPCSO { get; set; }

    //        public INC ISCSO { get; set; }

    //        public INC OpCntRs { get; set; }

    //        public SPC SPCSO { get; set; }

    //        public SPS Auto { get; set; }

    //        public SPS Loc { get; set; }
    //    }




    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "GGIO" (Generic process I/O).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class GGIO : CommonLogicalNode
    //    {
    //        public GGIO()
    //        {
    //            this.lnClass = tLNClassEnum.GGIO.ToString();
    //            this.DPCSO = new DPC();

    //            DPCSO.stVal.bType = tBasicTypeEnum.Dbpos;

    //            IntIn = new INS("IntIn");
    //        }

    //        public GGIO(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.lnClass = tLNClassEnum.GGIO.ToString();
    //        }
    //        [XmlIgnore]
    //        public DPC DPCSO { get; set; }
    //        [XmlIgnore]
    //        public DPL EEName { get; set; }
    //        [XmlIgnore]
    //        public INC ISCSO { get; set; }
    //        [XmlIgnore]
    //        public INC OpCntRs { get; set; }
    //        [XmlIgnore]
    //        public INS EEHealth { get; set; }
    //        [XmlIgnore]
    //        public INS IntIn { get; set; }
    //        [XmlIgnore]
    //        public MV AnIn { get; set; }
    //        [XmlIgnore]
    //        public SPC SPCSO { get; set; }
    //        [XmlIgnore]
    //        public SPS Alm { get; set; }
    //        [XmlIgnore]
    //        public SPS Ind { get; set; }
    //        [XmlIgnore]
    //        public SPS Loc { get; set; }

    //        [XmlIgnore]
    //        public SPS Ind1 { get; set; }

    //        [XmlIgnore]
    //        public SPS Ind2 { get; set; }
    //        [XmlIgnore]
    //        public SPS Ind3 { get; set; }

    //        [XmlIgnore]
    //        public SPS Ind4 { get; set; }

    //        [XmlIgnore]
    //        public SPS Ind5 { get; set; }
    //        [XmlIgnore]
    //        public SPS Ind6 { get; set; }

    //        [XmlIgnore]
    //        public SPS Ind7 { get; set; }

    //        [XmlIgnore]
    //        public SPS Ind8 { get; set; }

    //        public SPS Ind9 { get; set; }

    //        [XmlIgnore]
    //        public SPS Ind10 { get; set; }
    //        [XmlIgnore]
    //        public SPS Ind11 { get; set; }

    //        [XmlIgnore]
    //        public SPS Ind12 { get; set; }

    //        [XmlIgnore]
    //        public SPS Ind13 { get; set; }
    //        [XmlIgnore]
    //        public SPS Ind14 { get; set; }

    //        [XmlIgnore]
    //        public SPS Ind15 { get; set; }

    //        [XmlIgnore]
    //        public SPS Ind16 { get; set; }

    //        public SPS Ind17 { get; set; }

    //        [XmlIgnore]
    //        public SPS Ind18 { get; set; }
    //        [XmlIgnore]
    //        public SPS Ind19 { get; set; }

    //        [XmlIgnore]
    //        public SPS Ind20 { get; set; }

    //        [XmlIgnore]
    //        public SPS Ind21 { get; set; }
    //        [XmlIgnore]
    //        public SPS Ind22 { get; set; }

    //        [XmlIgnore]
    //        public SPS Ind23 { get; set; }

    //        [XmlIgnore]
    //        public SPS Ind24 { get; set; }
    //        [XmlIgnore]
    //        public SPS GrInd { get; set; }
    //        [XmlIgnore]
    //        public SPC SPCSO1 { get; set; }
    //        [XmlIgnore]
    //        public SPC SPCSO2 { get; set; }
    //        [XmlIgnore]
    //        public SPC SPCSO3 { get; set; }
    //        [XmlIgnore]
    //        public SPC SPCSO4 { get; set; }
    //        [XmlIgnore]
    //        public SPC SPCSO5 { get; set; }
    //        [XmlIgnore]
    //        public SPC SPCSO6 { get; set; }
    //        [XmlIgnore]
    //        public SPC SPCSO7 { get; set; }
    //        [XmlIgnore]
    //        public SPC SPCSO8 { get; set; }
    //        [XmlIgnore]
    //        public SPC SPCSO9 { get; set; }
    //        [XmlIgnore]
    //        public SPC SPCS10 { get; set; }
    //        [XmlIgnore]
    //        public SPC SPCS11 { get; set; }
    //        [XmlIgnore]
    //        public SPC SPCS12 { get; set; }

    //        [XmlIgnore]
    //        public SPC Out1 { get; set; }

    //        [XmlIgnore]
    //        public SPC Out2 { get; set; }

    //        [XmlIgnore]
    //        public SPC Out3 { get; set; }

    //        [XmlIgnore]
    //        public SPC Out4 { get; set; }

    //        [XmlIgnore]
    //        public SPC Out5 { get; set; }

    //        [XmlIgnore]
    //        public SPC Out6 { get; set; }


    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "GSAL" (Generic security application).
    //    /// </summary>
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class GSAL : CommonLogicalNode
    //    {
    //        public GSAL()
    //        {
    //            this.lnClass = tLNClassEnum.GSAL.ToString();
    //        }
    //        public GSAL(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.GSAL.ToString();
    //            this.iedType = iedType;
    //        }

    //        [Required, XmlIgnore]
    //        public INC NumCntRs { get; set; }

    //        [Required, XmlIgnore]
    //        public INC OpCntRs { get; set; }

    //        [Required, XmlIgnore]
    //        public SEC AcsCtlFail { get; set; }

    //        [Required, XmlIgnore]
    //        public SEC AuthFail { get; set; }

    //        [Required, XmlIgnore]
    //        public SEC Ina { get; set; }

    //        [Required, XmlIgnore]
    //        public SEC SvcViol { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "IARC" (Archiving).
    //    /// </summary>	
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class IARC : CommonLogicalNode
    //    {
    //        public IARC()
    //        {
    //            this.lnClass = tLNClassEnum.IARC.ToString();
    //        }
    //        public IARC(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.IARC.ToString();
    //            this.iedType = iedType;
    //        }

    //        [Required]
    //        public INC NumCntRs { get; set; }

    //        [Required]
    //        public INC OpCntRs { get; set; }

    //        public ING MaxNumRcd { get; set; }

    //        public ING MemFull { get; set; }

    //        public ING OpMod { get; set; }

    //        public INS MemUsed { get; set; }

    //        public INS NumRcd { get; set; }

    //        [Required]
    //        public SPS MemOv { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "IHMI" (Human machine interface).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class IHMI : CommonLogicalNode
    //    {
    //        public IHMI()
    //        {
    //            this.lnClass = tLNClassEnum.IHMI.ToString();
    //        }
    //        public IHMI(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.IHMI.ToString();
    //            this.iedType = iedType;
    //        }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "ITCI" (Telecontrol interface).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class ITCI : CommonLogicalNode
    //    {
    //        public ITCI()
    //        {
    //            this.lnClass = tLNClassEnum.ITCI.ToString();
    //        }
    //        public ITCI(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.ITCI.ToString();
    //            this.iedType = iedType;
    //        }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "ITMI" (Telemonitoring interface).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class ITMI : CommonLogicalNode
    //    {
    //        public ITMI()
    //        {
    //            this.lnClass = tLNClassEnum.ITMI.ToString();
    //        }
    //        public ITMI(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.ITMI.ToString();
    //            this.iedType = iedType;
    //        }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "LLN0" (Logical node zero).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class LLN0 : CommonLogicalNode
    //    {
    //        public LLN0(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.LLN0.ToString();
    //            this.iedType = iedType;
    //        }

    //        public LLN0()
    //        {
    //            this.lnClass = tLNClassEnum.LLN0.ToString();
    //        }
    //        public LLN0(string lntype) : base()
    //        {
    //            this.id = lntype;
    //            this.lnClass = tLNClassEnum.LLN0.ToString();
    //        }
    //        [XmlIgnore]
    //        public SPS Loc { get; set; }

    //        [XmlIgnore]
    //        public DPL EEHealth { get; set; }

    //        [XmlIgnore]
    //        public DPL EEName { get; set; }

    //        [XmlIgnore]
    //        public INS OpTmh { get; set; }

    //        [XmlIgnore]
    //        public INS OpCntRs { get; set; }

    //        [XmlIgnore]
    //        public SPC OpCnt { get; set; }

    //        [XmlIgnore]
    //        public SPS IvdCrv { get; set; }
    //        [XmlIgnore]
    //        public SGCB SGCB { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "LPHD" (Physical device information).
    //    /// </summary>
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class LPHD : CommonLogicalNode
    //    {
    //        public LPHD()
    //        {
    //            this.lnClass = tLNClassEnum.LPHD.ToString();
    //            this.Proxy = new SPS();
    //            this.Proxy.stVal.bType = tBasicTypeEnum.BOOLEAN;
    //            PhyHealth = new INS("Health");
    //        }

    //        public LPHD(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.lnClass = tLNClassEnum.LPHD.ToString();
    //            //this.PhyNamField = new DPL("PhyNam", lnType, iedType);
    //            //this.PhyHealthField = new INS("PhyHealth", lnType, iedType);
    //            //this.NumPwrUpField = new INS("NumPwrUp", lnType, iedType);
    //            //this.WrmStrField = new INS("WrmStr", lnType, iedType);
    //            //this.WacTrgField = new INS("WacTrg", lnType, iedType);
    //            //this.RsStatField = new SPC("RsStat", lnType, iedType);
    //            //this.OutOvField = new SPS("OutOv", lnType, iedType);
    //            //this.ProxyField = new SPS("Proxy", lnType, iedType);
    //            //this.InOvField = new SPS("InOv", lnType, iedType);
    //            //this.PwrUpField = new SPS("PwrUp", lnType, iedType);
    //            //this.PwrDnField = new SPS("PwrDn", lnType, iedType);
    //            //this.PwrSupAlmField = new SPS("PwrSupAlm", lnType, iedType);
    //        }

    //        [Required, XmlIgnore]
    //        public DPL PhyNam { get; set; }

    //        [Required, XmlIgnore]
    //        public INS PhyHealth { get; set; }

    //        [XmlIgnore]
    //        public INS NumPwrUp { get; set; }

    //        [XmlIgnore]
    //        public INS WrmStr { get; set; }

    //        [XmlIgnore]
    //        public INS WacTrg { get; set; }

    //        [XmlIgnore]
    //        public SPC RsStat { get; set; }

    //        [XmlIgnore]
    //        public SPS OutOv { get; set; }

    //        [Required, XmlIgnore]
    //        public SPS Proxy { get; set; }

    //        [XmlIgnore]
    //        public SPS InOv { get; set; }

    //        [XmlIgnore]
    //        public SPS PwrUp { get; set; }

    //        [XmlIgnore]
    //        public SPS PwrDn { get; set; }

    //        [XmlIgnore]
    //        public SPS PwrSupAlm { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "MDIF" (Differential measurements).
    //    /// </summary>	
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class MDIF : CommonLogicalNode
    //    {
    //        public MDIF()
    //        {
    //            this.lnClass = tLNClassEnum.MDIF.ToString();
    //        }

    //        public MDIF(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.lnClass = tLNClassEnum.MDIF.ToString();
    //            //this.Amp1 = new SAV("Amp1", lnType, iedType);
    //            //this.Amp2 = new SAV("Amp2", lnType, iedType);
    //            //this.Amp3 = new SAV("Amp3", lnType, iedType);
    //            //this.OpARem = new WYE("OpARem", lnType, iedType);
    //        }

    //        public SAV Amp1 { get; set; }

    //        public SAV Amp2 { get; set; }

    //        public SAV Amp3 { get; set; }

    //        public WYE OpARem { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "MHAI" (Harmonics or interharmonics).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class MHAI : CommonLogicalNode
    //    {
    //        private ASG ThdVValField;
    //        private HWYE HVAField;
    //        public MHAI()
    //        {
    //            this.lnClass = tLNClassEnum.MHAI.ToString();
    //        }
    //        public MHAI(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.MHAI.ToString();
    //            this.iedType = iedType;
    //            this.HzSet = new ASG("HzSet", lnType, iedType);
    //            this.EvTmms = new ASG("EvTmms", lnType, iedType);
    //            this.ThdAVal = new ASG("ThdAVal", lnType, iedType);
    //            this.ThdVValField = new ASG("ThdVVal", lnType, iedType);
    //            this.NomA = new ASG("NomA", lnType, iedType);
    //            this.HRmsPPV = new DEL("HRmsPPV", lnType, iedType);
    //            this.ThdPPV = new DEL("ThdPPV", lnType, iedType);
    //            this.ThdOddPPV = new DEL("ThdOddPPV", lnType, iedType);
    //            this.ThdEvnPPV = new DEL("ThdEvnPPV", lnType, iedType);
    //            this.HCfPPV = new DEL("HCfPPV", lnType, iedType);
    //            this.EEName = new DPL("EEName", lnType, iedType);
    //            this.HPPV = new HDEL("HPPV", lnType, iedType);
    //            this.HA = new HWYE("HA", lnType, iedType);
    //            this.HPhV = new HWYE("HPhV", lnType, iedType);
    //            this.HW = new HWYE("HW", lnType, iedType);
    //            this.HVAr = new HWYE("HVAr", lnType, iedType);
    //            this.HVAField = new HWYE("HVA", lnType, iedType);
    //            this.NumCyc = new ING("NumCyc", lnType, iedType);
    //            this.ThdATmms = new ING("ThdATmms", lnType, iedType);
    //            this.ThdVTmms = new ING("ThdVTmms", lnType, iedType);
    //            this.EEHealth = new INS("EEHealth", lnType, iedType);
    //            this.Hz = new MV("Hz", lnType, iedType);
    //            this.HRmsA = new WYE("HRmsA", lnType, iedType);
    //            this.HRmsPhV = new WYE("HRmsPhV", lnType, iedType);
    //            this.HTuW = new WYE("HTuW", lnType, iedType);
    //            this.HTsW = new WYE("HTsW", lnType, iedType);
    //            this.HATm = new WYE("HATm", lnType, iedType);
    //            this.HKf = new WYE("HKf", lnType, iedType);
    //            this.HTdf = new WYE("HTdf", lnType, iedType);
    //            this.ThdA = new WYE("ThdA", lnType, iedType);
    //            this.ThdOddA = new WYE("ThdOddA", lnType, iedType);
    //            this.ThdEvnA = new WYE("ThdEvnA", lnType, iedType);
    //            this.TddA = new WYE("TddA", lnType, iedType);
    //            this.TddOddA = new WYE("TddOddA", lnType, iedType);
    //            this.TddEvnA = new WYE("TddEvnA", lnType, iedType);
    //            this.ThdPhV = new WYE("ThdPhV", lnType, iedType);
    //            this.ThdOddPhV = new WYE("ThdOddPhV", lnType, iedType);
    //            this.ThdEvnPhV = new WYE("ThdEvnPhV", lnType, iedType);
    //            this.HCfPhV = new WYE("HCfPhV", lnType, iedType);
    //            this.HCfA = new WYE("HCfA", lnType, iedType);
    //            this.HTif = new WYE("HTif", lnType, iedType);
    //        }

    //        public ASG HzSet { get; set; }

    //        public ASG EvTmms { get; set; }

    //        public ASG ThdAVal { get; set; }

    //        public ASG NomA { get; set; }

    //        public DEL HRmsPPV { get; set; }

    //        public DEL ThdPPV { get; set; }

    //        public DEL ThdOddPPV { get; set; }

    //        public DEL ThdEvnPPV { get; set; }

    //        public DEL HCfPPV { get; set; }

    //        public DPL EEName { get; set; }

    //        public HDEL HPPV { get; set; }

    //        public HWYE HA { get; set; }

    //        public HWYE HPhV { get; set; }

    //        public HWYE HW { get; set; }

    //        public HWYE HVAr { get; set; }

    //        public ING NumCyc { get; set; }

    //        public ING ThdATmms { get; set; }

    //        public ING ThdVTmms { get; set; }

    //        public INS EEHealth { get; set; }

    //        public MV Hz { get; set; }

    //        public WYE HRmsA { get; set; }

    //        public WYE HRmsPhV { get; set; }

    //        public WYE HTuW { get; set; }

    //        public WYE HTsW { get; set; }

    //        public WYE HATm { get; set; }

    //        public WYE HKf { get; set; }

    //        public WYE HTdf { get; set; }

    //        public WYE ThdA { get; set; }

    //        public WYE ThdOddA { get; set; }

    //        public WYE ThdEvnA { get; set; }

    //        public WYE TddA { get; set; }

    //        public WYE TddOddA { get; set; }

    //        public WYE TddEvnA { get; set; }

    //        public WYE ThdPhV { get; set; }

    //        public WYE ThdOddPhV { get; set; }

    //        public WYE ThdEvnPhV { get; set; }

    //        public WYE HCfPhV { get; set; }

    //        public WYE HCfA { get; set; }

    //        public WYE HTif { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "MHAN" (Non phase related harmonics or interharmonics).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class MHAN : CommonLogicalNode
    //    {
    //        private ASG ThdVValField;
    //        public MHAN()
    //        {
    //            this.lnClass = tLNClassEnum.MHAN.ToString();
    //        }
    //        public MHAN(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.MHAN.ToString();
    //            this.iedType = iedType;
    //            this.HzSet = new ASG("HzSet", lnType, iedType);
    //            this.EvTmms = new ASG("EvTmms", lnType, iedType);
    //            this.ThdAVal = new ASG("ThdAVal", lnType, iedType);
    //            this.ThdVValField = new ASG("ThdVVal", lnType, iedType);
    //            this.NomA = new ASG("NomA", lnType, iedType);
    //            this.EEName = new DPL("EEName", lnType, iedType);
    //            this.HaAmp = new HMV("HaAmp", lnType, iedType);
    //            this.HaVol = new HMV("HaVol", lnType, iedType);
    //            this.HaWatt = new HMV("HaWatt", lnType, iedType);
    //            this.HaVolAmpr = new HMV("HaVolAmpr", lnType, iedType);
    //            this.HaVolAmp = new HMV("HaVolAmp", lnType, iedType);
    //            this.NumCyc = new ING("NumCyc", lnType, iedType);
    //            this.ThdATmms = new ING("ThdATmms", lnType, iedType);
    //            this.ThdVTmms = new ING("ThdVTmms", lnType, iedType);
    //            this.EEHealth = new INS("EEHealth", lnType, iedType);
    //            this.Hz = new MV("Hz", lnType, iedType);
    //            this.HaRmsAmp = new MV("HaRmsAmp", lnType, iedType);
    //            this.HaRmsVol = new MV("HaRmsVol", lnType, iedType);
    //            this.HaTuWatt = new MV("HaTuWatt", lnType, iedType);
    //            this.HaTsWatt = new MV("HaTsWatt", lnType, iedType);
    //            this.HaAmpTm = new MV("HaAmpTm", lnType, iedType);
    //            this.HaKFact = new MV("HaKFact", lnType, iedType);
    //            this.HaTdFact = new MV("HaTdFact", lnType, iedType);
    //            this.ThdAmp = new MV("ThdAmp", lnType, iedType);
    //            this.ThdOddAmp = new MV("ThdOddAmp", lnType, iedType);
    //            this.ThdEvnAmp = new MV("ThdEvnAmp", lnType, iedType);
    //            this.TddAmp = new MV("TddAmp", lnType, iedType);
    //            this.TddOddAmp = new MV("TddOddAmp", lnType, iedType);
    //            this.TddEvnAmp = new MV("TddEvnAmp", lnType, iedType);
    //            this.ThdVol = new MV("ThdVol", lnType, iedType);
    //            this.ThdOddVol = new MV("ThdOddVol", lnType, iedType);
    //            this.ThdEvnVol = new MV("ThdEvnVol", lnType, iedType);
    //            this.HaCfAmp = new MV("HaCfAmp", lnType, iedType);
    //            this.HaCfVol = new MV("HaCfVol", lnType, iedType);
    //            this.HaTiFact = new MV("HaTiFact", lnType, iedType);
    //        }

    //        public ASG HzSet { get; set; }

    //        public ASG EvTmms { get; set; }

    //        public ASG ThdAVal { get; set; }

    //        public ASG NomA { get; set; }

    //        public DPL EEName { get; set; }

    //        public HMV HaAmp { get; set; }

    //        public HMV HaVol { get; set; }

    //        public HMV HaWatt { get; set; }

    //        public HMV HaVolAmpr { get; set; }

    //        public HMV HaVolAmp { get; set; }

    //        public ING NumCyc { get; set; }

    //        public ING ThdATmms { get; set; }

    //        public ING ThdVTmms { get; set; }

    //        public INS EEHealth { get; set; }

    //        public MV Hz { get; set; }

    //        public MV HaRmsAmp { get; set; }

    //        public MV HaRmsVol { get; set; }

    //        public MV HaTuWatt { get; set; }

    //        public MV HaTsWatt { get; set; }

    //        public MV HaAmpTm { get; set; }

    //        public MV HaKFact { get; set; }

    //        public MV HaTdFact { get; set; }

    //        public MV ThdAmp { get; set; }

    //        public MV ThdOddAmp { get; set; }

    //        public MV ThdEvnAmp { get; set; }

    //        public MV TddAmp { get; set; }

    //        public MV TddOddAmp { get; set; }

    //        public MV TddEvnAmp { get; set; }

    //        public MV ThdVol { get; set; }

    //        public MV ThdOddVol { get; set; }

    //        public MV ThdEvnVol { get; set; }

    //        public MV HaCfAmp { get; set; }

    //        public MV HaCfVol { get; set; }

    //        public MV HaTiFact { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "MMTR" (Non phase related Measurement).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class MMTR : CommonLogicalNode
    //    {
    //        public MMTR()
    //        {
    //            this.lnClass = tLNClassEnum.MMTR.ToString();
    //            SupRs = new SPC();
    //            SupRs.Oper = new Oper("SupRs");
    //            SupRs.stVal = new stVal("SupRs");
    //        }

    //        public MMTR(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.lnClass = tLNClassEnum.MMTR.ToString();
    //            //this.TotVAh = new BCR("TotVAh", lnType, iedType);
    //            //this.TotWh = new BCR("TotWh", lnType, iedType);
    //            //this.TotVArh = new BCR("TotVArh", lnType, iedType);
    //            //this.SupWh = new BCR("SupWh", lnType, iedType);
    //            //this.SupVArh = new BCR("SupVArh", lnType, iedType);
    //            //this.DmdWh = new BCR("DmdWh", lnType, iedType);
    //            //this.DmdVArh = new BCR("DmdVArh", lnType, iedType);
    //            //this.EEName = new DPL("EEName", lnType, iedType);
    //            //this.EEHealth = new INS("EEHealth", lnType, iedType);
    //        }
    //        [XmlIgnore]
    //        public BCR TotVAh { get; set; }
    //        [XmlIgnore]
    //        public BCR TotWh { get; set; }
    //        [XmlIgnore]
    //        public BCR TotVArh { get; set; }
    //        [XmlIgnore]
    //        public BCR SupWh { get; set; }
    //        [XmlIgnore]
    //        public BCR SupVArh { get; set; }
    //        [XmlIgnore]
    //        public BCR DmdWh { get; set; }
    //        [XmlIgnore]
    //        public BCR DmdVArh { get; set; }
    //        [XmlIgnore]
    //        public DPL EEName { get; set; }
    //        [XmlIgnore]
    //        public INS EEHealth { get; set; }
    //        [XmlIgnore]
    //        public SPC SupRs { get; set; }
    //        [XmlIgnore]
    //        public SPS VArhFwdAlm { get; set; }
    //        [XmlIgnore]
    //        public SPS VArhRvAlm { get; set; }
    //        [XmlIgnore]
    //        public SPS WhFwdAlm { get; set; }
    //        [XmlIgnore]
    //        public SPS WhRvAlm { get; set; }
    //        [XmlIgnore]
    //        public MV VArsAccFwd { get; set; }
    //        [XmlIgnore]
    //        public MV VArsAccRev { get; set; }
    //        [XmlIgnore]
    //        public MV MaxVArFwdD { get; set; }
    //        [XmlIgnore]
    //        public MV MaxWFwdDmd { get; set; }
    //        [XmlIgnore]
    //        public MV MaxWRvDmd { get; set; }

    //        public MV MaxVArRvDm { get; set; }

    //        public MV WsAccRev { get; set; }
    //        public MV WsAccFwd { get; set; }





    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "MMXN" (Metering).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class MMXN : CommonLogicalNode
    //    {
    //        public MMXN()
    //        {
    //            this.lnClass = tLNClassEnum.MMXN.ToString();
    //        }
    //        public MMXN(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.MMXN.ToString();
    //            this.iedType = iedType;
    //            this.Imp = new CMV("Imp", lnType, iedType);
    //            this.EEName = new DPL("EEName", lnType, iedType);
    //            this.EEHealth = new INS("EEHealth", lnType, iedType);
    //            this.Amp = new MV("Amp", lnType, iedType);
    //            this.Vol = new MV("Vol", lnType, iedType);
    //            this.Watt = new MV("Watt", lnType, iedType);
    //            this.VolAmpr = new MV("VolAmpr", lnType, iedType);
    //            this.VolAmp = new MV("VolAmp", lnType, iedType);
    //            this.PwrFact = new MV("PwrFact", lnType, iedType);
    //            this.Hz = new MV("Hz", lnType, iedType);
    //        }

    //        public CMV Imp { get; set; }

    //        public DPL EEName { get; set; }

    //        public INS EEHealth { get; set; }

    //        public MV Amp { get; set; }

    //        public MV Vol { get; set; }

    //        public MV Watt { get; set; }

    //        public MV VolAmpr { get; set; }

    //        public MV VolAmp { get; set; }

    //        public MV PwrFact { get; set; }

    //        public MV Hz { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "MMXU" (Measurement).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class MMXU : CommonLogicalNode
    //    {
    //        public MMXU()
    //        {
    //            this.lnClass = tLNClassEnum.MMXU.ToString();
    //        }

    //        public MMXU(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.lnClass = tLNClassEnum.MMXU.ToString();
    //            //this.PPV = new DEL("PPV", lnType, iedType);
    //            //this.EEHealth = new INS("EEHealth", lnType, iedType);
    //            //this.TotW = new MV("TotW", lnType, iedType);
    //            //this.TotVAr = new MV("TotVAr", lnType, iedType);
    //            //this.TotVA = new MV("TotVA", lnType, iedType);
    //            //this.TotPF = new MV("TotPF", lnType, iedType);
    //            //this.Hz = new MV("Hz", lnType, iedType);
    //            //this.PhV = new WYE("PhV", lnType, iedType);
    //            //this.A = new WYE("A", lnType, iedType);
    //            //this.W = new WYE("W", lnType, iedType);
    //            //this.VAr = new WYE("VAr", lnType, iedType);
    //            //this.VA = new WYE("VA", lnType, iedType);
    //            //this.PF = new WYE("PF", lnType, iedType);
    //            //this.Z = new WYE("Z", lnType, iedType);
    //        }
    //        [XmlIgnore]
    //        public DEL PPV { get; set; }
    //        [XmlIgnore]
    //        public INS EEHealth { get; set; }
    //        [XmlIgnore]
    //        public MV TotW { get; set; }
    //        [XmlIgnore]
    //        public MV TotVAr { get; set; }
    //        [XmlIgnore]
    //        public MV Amp { get; set; }
    //        public MV Vol { get; set; }
    //        public MV TotVA { get; set; }
    //        [XmlIgnore]
    //        public MV TotPF { get; set; }
    //        [XmlIgnore]
    //        public MV Hz { get; set; }
    //        [XmlIgnore]
    //        public WYE phV { get; set; }
    //        [XmlIgnore]
    //        public WYE PhV { get { return phV; } set { phV = value; } }

    //        [XmlIgnore]
    //        public WYE A { get; set; }
    //        [XmlIgnore]
    //        public WYE W { get; set; }
    //        [XmlIgnore]
    //        public WYE VAr { get; set; }
    //        [XmlIgnore]
    //        public WYE VA { get; set; }
    //        [XmlIgnore]
    //        public WYE PF { get; set; }
    //        [XmlIgnore]
    //        public WYE Z { get; set; }
    //        [XmlIgnore]
    //        public SPS Ald { get; set; }
    //        [XmlIgnore]
    //        public SPS Alg { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "MSQI" (Sequence and imbalance).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class MSQI : CommonLogicalNode
    //    {
    //        public MSQI()
    //        {
    //            this.lnClass = tLNClassEnum.MSQI.ToString();
    //        }

    //        public MSQI(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.MSQI.ToString();
    //            this.iedType = iedType;
    //            //this.ImbPPVField = new DEL("ImbPPV", lnType, iedType);
    //            //this.EENameField = new DPL("EEName", lnType, iedType);
    //            //this.EEHealthField = new INS("EEHealth", lnType, iedType);
    //            //this.ImbNgAField = new MV("ImbNgA", lnType, iedType);
    //            //this.ImbNgVField = new MV("ImbNgV", lnType, iedType);
    //            //this.ImbZroAField = new MV("ImbZroA", lnType, iedType);
    //            //this.ImbZroVField = new MV("ImbZroV", lnType, iedType);
    //            //this.MaxImbAField = new MV("MaxImbA", lnType, iedType);
    //            //this.MaxImbPPVField = new MV("MaxImbPPV", lnType, iedType);
    //            //this.MaxImbVField = new MV("MaxImbV", lnType, iedType);
    //            //this.SeqAField = new SEQ("SeqA", lnType, iedType);
    //            //this.SeqVField = new SEQ("SeqV", lnType, iedType);
    //            //this.DQ0SeqField = new SEQ("DQ0Seq", lnType, iedType);
    //            //this.ImbAField = new WYE("ImbA", lnType, iedType);
    //            //this.ImbVField = new WYE("ImbV", lnType, iedType);
    //        }
    //        [XmlIgnore]
    //        public DEL ImbPPV { get; set; }
    //        [XmlIgnore]
    //        public DPL EEName { get; set; }
    //        [XmlIgnore]
    //        public INS EEHealth { get; set; }
    //        [XmlIgnore]
    //        public MV ImbNgA { get; set; }
    //        [XmlIgnore]
    //        public MV ImbNgV { get; set; }
    //        [XmlIgnore]
    //        public MV ImbZroA { get; set; }
    //        [XmlIgnore]
    //        public MV ImbZroV { get; set; }
    //        [XmlIgnore]
    //        public MV MaxImbA { get; set; }
    //        [XmlIgnore]
    //        public MV MaxImbPPV { get; set; }
    //        [XmlIgnore]
    //        public MV MaxImbV { get; set; }
    //        [XmlIgnore]
    //        public SEQ SeqA { get; set; }
    //        [XmlIgnore]
    //        public SEQ SeqV { get; set; }
    //        [XmlIgnore]
    //        public SEQ SeqU { get; set; }
    //        [XmlIgnore]
    //        public SEQ SeqT { get; set; }
    //        [XmlIgnore]
    //        public SEQ DQ0Seq { get; set; }
    //        [XmlIgnore]
    //        public WYE ImbA { get; set; }
    //        [XmlIgnore]
    //        public WYE ImbV { get; set; }

    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "MSTA" (Metering Statistics).
    //    /// </summary>			
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class MSTA : CommonLogicalNode
    //    {
    //        public MSTA()
    //        {
    //            this.lnClass = tLNClassEnum.MSTA.ToString();
    //        }
    //        public MSTA(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.MSTA.ToString();
    //            this.iedType = iedType;
    //            this.EvTmms = new ASG("EvTmms", lnType, iedType);
    //            this.EEName = new DPL("EEName", lnType, iedType);
    //            this.EEHealth = new INS("EEHealth", lnType, iedType);
    //            this.AvAmps = new MV("AvAmps", lnType, iedType);
    //            this.MaxAmps = new MV("MaxAmps", lnType, iedType);
    //            this.MinAmps = new MV("MinAmps", lnType, iedType);
    //            this.AvVolts = new MV("AvVolts", lnType, iedType);
    //            this.MaxVolts = new MV("MaxVolts", lnType, iedType);
    //            this.MinVolts = new MV("MinVolts", lnType, iedType);
    //            this.AvVA = new MV("AvVA", lnType, iedType);
    //            this.MaxVA = new MV("MaxVA", lnType, iedType);
    //            this.MinVA = new MV("MinVA", lnType, iedType);
    //            this.AvW = new MV("AvW", lnType, iedType);
    //            this.MaxW = new MV("MaxW", lnType, iedType);
    //            this.MinW = new MV("MinW", lnType, iedType);
    //            this.AvVAr = new MV("AvVAr", lnType, iedType);
    //            this.MaxVAr = new MV("MaxVAr", lnType, iedType);
    //            this.MinVAr = new MV("MinVAr", lnType, iedType);
    //            this.EvStr = new SPC("EvStr", lnType, iedType);
    //        }

    //        public ASG EvTmms { get; set; }

    //        public DPL EEName { get; set; }

    //        public INS EEHealth { get; set; }

    //        public MV AvAmps { get; set; }

    //        public MV MaxAmps { get; set; }

    //        public MV MinAmps { get; set; }

    //        public MV AvVolts { get; set; }

    //        public MV MaxVolts { get; set; }

    //        public MV MinVolts { get; set; }

    //        public MV AvVA { get; set; }

    //        public MV MaxVA { get; set; }

    //        public MV MinVA { get; set; }

    //        public MV AvW { get; set; }

    //        public MV MaxW { get; set; }

    //        public MV MinW { get; set; }

    //        public MV AvVAr { get; set; }

    //        public MV MaxVAr { get; set; }

    //        public MV MinVAr { get; set; }

    //        public SPC EvStr { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "PDIF" (Differential).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class PDIF : CommonLogicalNode
    //    {
    //        public PDIF()
    //        {
    //            this.lnClass = tLNClassEnum.PDIF.ToString();
    //        }

    //        public PDIF(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.PDIF.ToString();
    //            this.iedType = iedType;
    //            this.Str = new ACD("Str", lnType, iedType);
    //            this.Op = new ACT("Op", lnType, iedType);
    //            this.LinCapac = new ASG("LinCapac", lnType, iedType);
    //            this.TmASt = new CSD("TmASt", lnType, iedType);
    //            this.TmACrv = new CURVE("TmACrv", lnType, iedType);
    //            this.OpCntRs = new INC("OpCntRs", lnType, iedType);
    //            this.LoSet = new ING("LoSet", lnType, iedType);
    //            this.HiSet = new ING("HiSet", lnType, iedType);
    //            this.MinOpTmms = new ING("MinOpTmms", lnType, iedType);
    //            this.MaxOpTmms = new ING("MaxOpTmms", lnType, iedType);
    //            this.RstMod = new ING("RstMod", lnType, iedType);
    //            this.RsDlTmms = new ING("RsDlTmms", lnType, iedType);
    //            this.DifACIc = new WYE("DifAClc", lnType, iedType);
    //            this.RstA = new WYE("RstA", lnType, iedType);
    //        }

    //        public ACD Str { get; set; }

    //        [Required]
    //        public ACT Op { get; set; }

    //        public ASG LinCapac { get; set; }

    //        public CSD TmASt { get; set; }

    //        private CURVE TmACrv { get; set; }

    //        public INC OpCntRs { get; set; }

    //        public ING LoSet { get; set; }

    //        public ING HiSet { get; set; }

    //        public ING MinOpTmms { get; set; }

    //        public ING MaxOpTmms { get; set; }

    //        public ING RstMod { get; set; }

    //        public ING RsDlTmms { get; set; }

    //        public WYE DifACIc { get; set; }

    //        public WYE RstA { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "PDIR" (Direction comparison).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class PDIR : CommonLogicalNode
    //    {
    //        public PDIR()
    //        {
    //            this.lnClass = tLNClassEnum.PDIR.ToString();
    //        }

    //        public PDIR(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.PDIR.ToString();
    //            this.iedType = iedType;
    //            this.Str = new ACD("Str", lnType, iedType);
    //            this.Op = new ACT("Op", lnType, iedType);
    //            this.OpCntRs = new INC("OpCntRs", lnType, iedType);
    //            this.RsDlTmms = new ING("RsDlTmms", lnType, iedType);
    //        }

    //        [Required]
    //        public ACD Str { get; set; }

    //        [Required]
    //        public ACT Op { get; set; }

    //        public INC OpCntRs { get; set; }

    //        public ING RsDlTmms { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "PDIS" (Distance).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class PDIS : CommonLogicalNode
    //    {
    //        public PDIS()
    //        {
    //            this.lnClass = tLNClassEnum.PDIS.ToString();
    //            StrCndZ = new INS();
    //            StrCndZ.stVal = new stVal("StrCndZ");
    //        }

    //        public PDIS(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.lnClass = tLNClassEnum.PDIS.ToString();
    //            //this.Str = new ACD("Str", lnType, iedType);
    //            //this.Op = new ACT("Op", lnType, iedType);
    //            //this.PoRch = new ASG("PoRch", lnType, iedType);
    //            //this.PhStr = new ASG("PhStr", lnType, iedType);
    //            //this.GndStr = new ASG("GndStr", lnType, iedType);
    //            //this.PctRch = new ASG("PctRch", lnType, iedType);
    //            //this.Ofs = new ASG("Ofs", lnType, iedType);
    //            //this.PctOfs = new ASG("PctOfs", lnType, iedType);
    //            //this.RisLod = new ASG("RisLod", lnType, iedType);
    //            //this.AngLod = new ASG("AngLod", lnType, iedType);
    //            //this.X1 = new ASG("X1", lnType, iedType);
    //            //this.LinAng = new ASG("LinAng", lnType, iedType);
    //            //this.RisGndRch = new ASG("RisGndRch", lnType, iedType);
    //            //this.RisPhRch = new ASG("RisPhRch", lnType, iedType);
    //            //this.K0Fact = new ASG("K0Fact", lnType, iedType);
    //            //this.K0FactAng = new ASG("K0FactAng", lnType, iedType);
    //            //this.OpCntRs = new INC("OpCntRs", lnType, iedType);
    //            //this.DirMod = new ING("DirMod", lnType, iedType);
    //            //this.OpDlTmms = new ING("OpDlTmms", lnType, iedType);
    //            //this.PhDlTmms = new ING("PhDlTmms", lnType, iedType);
    //            //this.GndDlTmms = new ING("GndDlTmms", lnType, iedType);
    //            //this.RsDlTmms = new ING("RsDlTmms", lnType, iedType);
    //            //this.TmDlMod = new SPG("TmDlMod", lnType, iedType);
    //            //this.PhDlMod = new SPG("PhDlMod", lnType, iedType);
    //            //this.GndDlMod = new SPG("GndDlMod", lnType, iedType);
    //        }

    //        [Required, XmlIgnore]
    //        public ACD Str { get; set; }

    //        [Required, XmlIgnore]
    //        public ACT Op { get; set; }
    //        [XmlIgnore]
    //        public ASG PoRch { get; set; }
    //        [XmlIgnore]
    //        public ASG PhStr { get; set; }
    //        [XmlIgnore]
    //        public ASG GndStr { get; set; }
    //        [XmlIgnore]
    //        public ASG PctRch { get; set; }
    //        [XmlIgnore]
    //        public ASG Ofs { get; set; }
    //        [XmlIgnore]
    //        public ASG PctOfs { get; set; }
    //        [XmlIgnore]
    //        public ASG RisLod { get; set; }
    //        [XmlIgnore]
    //        public ASG AngLod { get; set; }
    //        [XmlIgnore]
    //        public ASG X1 { get; set; }
    //        [XmlIgnore]
    //        public ASG LinAng { get; set; }
    //        [XmlIgnore]
    //        public ASG RisGndRch { get; set; }
    //        [XmlIgnore]
    //        public ASG RisPhRch { get; set; }
    //        [XmlIgnore]
    //        public ASG K0Fact { get; set; }
    //        [XmlIgnore]
    //        public ASG K0FactAng { get; set; }
    //        [XmlIgnore]
    //        public INC OpCntRs { get; set; }
    //        [XmlIgnore]
    //        public ING DirMod { get; set; }
    //        [XmlIgnore]
    //        public ING OpDlTmms { get; set; }
    //        [XmlIgnore]
    //        public ING PhDlTmms { get; set; }
    //        [XmlIgnore]
    //        public ING GndDlTmms { get; set; }
    //        [XmlIgnore]
    //        public ING RsDlTmms { get; set; }
    //        [XmlIgnore]
    //        public SPG TmDlMod { get; set; }
    //        [XmlIgnore]
    //        public SPG PhDlMod { get; set; }
    //        [XmlIgnore]
    //        public SPG GndDlMod { get; set; }
    //        [XmlIgnore]
    //        public INS StrCndZ { get; set; }
    //        [XmlIgnore]
    //        public INS StrNDir { get; set; }
    //        public SPS StrPP { get; set; }
    //        public SPS StrPh { get; set; }
    //        public SPS OpPh { get; set; }
    //        public SPS OpPP { get; set; }

    //        public SPS OpZ1 { get; set; }
    //        public SPS OpZ2 { get; set; }
    //        public SPS StrZ1 { get; set; }
    //        public SPS StrZ2 { get; set; }
    //        public MV PriRis { get; set; }
    //        public MV PriReact { get; set; }


    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "PDOP" (Directional overpower).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class PDOP : CommonLogicalNode
    //    {
    //        public PDOP()
    //        {
    //            this.lnClass = tLNClassEnum.PDOP.ToString();
    //        }

    //        public PDOP(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.PDOP.ToString();
    //            this.iedType = iedType;
    //            this.Str = new ACD("Str", lnType, iedType);
    //            this.Op = new ACT("Op", lnType, iedType);
    //            this.StrVal = new ASG("StrVal", lnType, iedType);
    //            this.OpCntRs = new INC("OpCntRs", lnType, iedType);
    //            this.DirMod = new ING("DirMod", lnType, iedType);
    //            this.OpDlTmms = new ING("OpDlTmms", lnType, iedType);
    //            this.RsDlTmms = new ING("RsDlTmms", lnType, iedType);
    //        }

    //        [Required]
    //        public ACD Str { get; set; }

    //        [Required]
    //        public ACT Op { get; set; }

    //        public ASG StrVal { get; set; }

    //        public INC OpCntRs { get; set; }

    //        public ING DirMod { get; set; }

    //        public ING OpDlTmms { get; set; }

    //        public ING RsDlTmms { get; set; }
    //        public MV MVAr { get; set; }

    //        public MV MW { get; set; }


    //        public SPS OpZ1 { get; set; }
    //        public SPS OpZ2 { get; set; }
    //        public SPS OpZ3 { get; set; }
    //        public SPS OpZ4 { get; set; }
    //        public SPS OpZ5 { get; set; }
    //        public SPS OpZ6 { get; set; }
    //        public SPS StrZ1 { get; set; }
    //        public SPS StrZ2 { get; set; }
    //        public SPS StrZ3 { get; set; }
    //        public SPS StrZ4 { get; set; }
    //        public SPS StrZ5 { get; set; }
    //        public SPS StrZ6 { get; set; }

    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "PDUP" (Directional underpower).
    //    /// </summary>	
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class PDUP : CommonLogicalNode
    //    {
    //        public PDUP()
    //        {
    //            this.lnClass = tLNClassEnum.PDUP.ToString();
    //        }

    //        public PDUP(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.PDUP.ToString();
    //            this.iedType = iedType;
    //            this.Str = new ACD("Str", lnType, iedType);
    //            this.Op = new ACT("Op", lnType, iedType);
    //            this.StrVal = new ASG("StrVal", lnType, iedType);
    //            this.OpCntRs = new INC("OpCntRs", lnType, iedType);
    //            this.OpDlTmms = new ING("OpDlTmms", lnType, iedType);
    //            this.RsDlTmms = new ING("RsDlTmms", lnType, iedType);
    //            this.DirMod = new ING("DirMod", lnType, iedType);
    //        }

    //        [Required]
    //        public ACD Str { get; set; }

    //        [Required]
    //        public ACT Op { get; set; }

    //        public ASG StrVal { get; set; }

    //        public INC OpCntRs { get; set; }

    //        public ING OpDlTmms { get; set; }

    //        public ING RsDlTmms { get; set; }

    //        public ING DirMod { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "PFRC" (Rate of change of frequency).
    //    /// </summary>	
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class PFRC : CommonLogicalNode
    //    {
    //        public PFRC()
    //        {
    //            this.lnClass = tLNClassEnum.PFRC.ToString();
    //        }

    //        public PFRC(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.PFRC.ToString();
    //            this.iedType = iedType;
    //            this.Str = new ACD("Str", lnType, iedType);
    //            this.Op = new ACT("Op", lnType, iedType);
    //            this.StrVal = new ASG("StrVal", lnType, iedType);
    //            this.BlkVal = new ASG("BlkVal", lnType, iedType);
    //            this.OpCntRs = new INC("OpCntRs", lnType, iedType);
    //            this.OpDlTmms = new ING("OpDlTmms", lnType, iedType);
    //            this.RsDlTmms = new ING("RsDlTmms", lnType, iedType);
    //            this.BlkV = new SPS("BlkV", lnType, iedType);
    //        }

    //        [Required]
    //        public ACD Str { get; set; }

    //        [Required]
    //        public ACT Op { get; set; }

    //        public ASG StrVal { get; set; }

    //        public ASG BlkVal { get; set; }

    //        private INC OpCntRs { get; set; }

    //        public ING OpDlTmms { get; set; }

    //        public ING RsDlTmms { get; set; }

    //        public SPS BlkV { get; set; }


    //        public SPS BlkLoMg { get; set; }

    //        public SPS RestLd { get; set; }


    //    }

    //    /// <summary>
    //    /// Этот класс определяет логические атрибуты узлов, которые определены для его использования в LN
    //    /// класс "PHAR" (Harmonic restraint).
    //    /// </summary>
    //    public class PHAR : CommonLogicalNode
    //    {
    //        public PHAR() : base()
    //        {
    //            this.lnClass = tLNClassEnum.PHAR.ToString();
    //        }

    //        public PHAR(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.lnClass = tLNClassEnum.PHAR.ToString();
    //            //this.Str = new ACD("Str", lnType, iedType);
    //            //this.PhStr = new ASG("PhStr", lnType, iedType);
    //            //this.PhStop = new ASG("PhStop", lnType, iedType);
    //            //this.OpCntRs = new INC("OpCntRs", lnType, iedType);
    //            //this.HaRst = new ING("HaRst", lnType, iedType);
    //            //this.OpDlTmms = new ING("OpDlTmms", lnType, iedType);
    //            //this.RsDlTmms = new ING("RsDlTmms", lnType, iedType);
    //        }

    //        [Required, XmlIgnore]
    //        public ACD Str { get; set; }
    //        [XmlIgnore]
    //        public ASG PhStr { get; set; }
    //        [XmlIgnore]
    //        public ASG PhStop { get; set; }
    //        [XmlIgnore]
    //        public INC OpCntRs { get; set; }
    //        [XmlIgnore]
    //        public ING HaRst { get; set; }
    //        [XmlIgnore]
    //        public ING OpDlTmms { get; set; }
    //        [XmlIgnore]
    //        public ING RsDlTmms { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "PHIZ" (Ground detector).
    //    /// </summary>	
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class PHIZ : CommonLogicalNode
    //    {
    //        public PHIZ()
    //        {
    //            this.lnClass = tLNClassEnum.PHIZ.ToString();
    //        }

    //        public PHIZ(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.PHIZ.ToString();
    //            this.iedType = iedType;
    //            this.Str = new ACD("Str", lnType, iedType);
    //            this.Op = new ACT("Op", lnType, iedType);
    //            this.AStr = new ASG("AStr", lnType, iedType);
    //            this.VStr = new ASG("VStr", lnType, iedType);
    //            this.HVStr = new ASG("HVStr", lnType, iedType);
    //            this.OpCntRs = new INC("OpCntRs", lnType, iedType);
    //            this.OpDlTmms = new ING("OpDlTmms", lnType, iedType);
    //            this.RsDlTmms = new ING("RsDlTmms", lnType, iedType);
    //        }

    //        [Required]
    //        public ACD Str { get; set; }
    //        public ING RsDlTmms { get; set; }

    //        [Required]
    //        public ACT Op { get; set; }

    //        public ASG AStr { get; set; }

    //        public ASG VStr { get; set; }

    //        public ASG HVStr { get; set; }

    //        public INC OpCntRs { get; set; }

    //        public ING OpDlTmms { get; set; }

    //        public ING VT3rdH { get; set; }

    //        public MV VN3rdH { get; set; }

    //        public MV E3 { get; set; }
    //        public MV Ang { get; set; }
    //        public MV VDif3rdH { get; set; }
    //        public MV VBias3rdH { get; set; }
    //        public MV UN { get; set; }
    //        public SPS StrUN { get; set; }

    //        public SPS Str3rdH { get; set; }
    //        public SPS OpUN { get; set; }
    //        public SPS Op3rdH { get; set; }

    //    }



    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "PIOC" (Instantaneous overcurrent).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class PIOC : CommonLogicalNode
    //    {
    //        public PIOC()
    //        {
    //            this.lnClass = tLNClassEnum.PIOC.ToString();
    //        }

    //        public PIOC(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.lnClass = tLNClassEnum.PIOC.ToString();
    //            //this.Str = new ACD("Str", lnType, iedType);
    //            //this.Op = new ACT("Op", lnType, iedType);
    //            //this.StrVal = new ASG("StrVal", lnType, iedType);
    //            //this.OpCntRs = new INC("OpCntRs", lnType, iedType);
    //        }
    //        [XmlIgnore]
    //        public ACD Str { get; set; }

    //        [Required, XmlIgnore]
    //        public ACT Op { get; set; }
    //        [XmlIgnore]
    //        public ASG StrVal { get; set; }
    //        [XmlIgnore]
    //        public INC OpCntRs { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "PMRI" (Motor restart inhibition).
    //    /// </summary>	
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class PMRI : CommonLogicalNode
    //    {
    //        public PMRI()
    //        {
    //            this.lnClass = tLNClassEnum.PMRI.ToString();
    //        }
    //        public PMRI(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.PMRI.ToString();
    //            this.iedType = iedType;
    //            this.Op = new ACT("Op", lnType, iedType);
    //            this.SetA = new ASG("SetA", lnType, iedType);
    //            this.OpCntRs = new INC("OpCntRs", lnType, iedType);
    //            this.SetTms = new ING("SetTms", lnType, iedType);
    //            this.MaxNumStr = new ING("MaxNumStr", lnType, iedType);
    //            this.MaxWrmStr = new ING("MaxWrmStr", lnType, iedType);
    //            this.MaxStrTmm = new ING("MaxStrTmm", lnType, iedType);
    //            this.EqTmm = new ING("EqTmm", lnType, iedType);
    //            this.InhTmm = new ING("InhTmm", lnType, iedType);
    //            this.StrInhTmm = new INS("StrInhTmm", lnType, iedType);
    //            this.StrInh = new SPS("StrInh", lnType, iedType);
    //        }

    //        public ACT Op { get; set; }

    //        public ASG SetA { get; set; }

    //        public INC OpCntRs { get; set; }

    //        public ING SetTms { get; set; }

    //        public ING MaxNumStr { get; set; }

    //        public ING MaxWrmStr { get; set; }

    //        public ING MaxStrTmm { get; set; }

    //        public ING EqTmm { get; set; }

    //        public ING InhTmm { get; set; }

    //        public INS StrInhTmm { get; set; }

    //        public SPS StrInh { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "PMSS" (Motor starting time supervision).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class PMSS : CommonLogicalNode
    //    {
    //        public PMSS()
    //        {
    //            this.lnClass = tLNClassEnum.PMSS.ToString();
    //        }
    //        public PMSS(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.PMSS.ToString();
    //            this.iedType = iedType;
    //            this.Str = new ACD("Str", lnType, iedType);
    //            this.Op = new ACT("Op", lnType, iedType);
    //            this.SetA = new ASG("SetA", lnType, iedType);
    //            this.MotStr = new ASG("MotStr", lnType, iedType);
    //            this.OpCntRs = new INC("OpCntRs", lnType, iedType);
    //            this.SetTms = new ING("SetTms", lnType, iedType);
    //            this.LokRotTms = new ING("LokRotTms", lnType, iedType);
    //        }

    //        public ACD Str { get; set; }

    //        public ACT Op { get; set; }

    //        public ASG SetA { get; set; }

    //        public ASG MotStr { get; set; }

    //        public INC OpCntRs { get; set; }

    //        public ING SetTms { get; set; }

    //        public ING LokRotTms { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "POPF" (Over power factor).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class POPF : CommonLogicalNode
    //    {
    //        private ASG BlkValVField;
    //        public POPF()
    //        {
    //            this.lnClass = tLNClassEnum.POPF.ToString();
    //        }

    //        public POPF(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.POPF.ToString();
    //            this.iedType = iedType;
    //            this.Str = new ACD("Str", lnType, iedType);
    //            this.Op = new ACT("Op", lnType, iedType);
    //            this.StrVal = new ASG("StrVal", lnType, iedType);
    //            this.BlkValA = new ASG("BlkValA", lnType, iedType);
    //            this.BlkValVField = new ASG("BlkValV", lnType, iedType);
    //            this.OpCntRs = new INC("OpCntRs", lnType, iedType);
    //            this.OpDlTmms = new ING("OpDlTmms", lnType, iedType);
    //            this.RsDlTmms = new ING("RsDlTmms", lnType, iedType);
    //            this.BlkA = new SPS("BlkA", lnType, iedType);
    //            this.BlkV = new SPS("BlkV", lnType, iedType);
    //        }

    //        [Required]
    //        public ACD Str { get; set; }

    //        [Required]
    //        public ACT Op { get; set; }

    //        public ASG StrVal { get; set; }

    //        public ASG BlkValA { get; set; }

    //        public ASG BlkValV
    //        {
    //            get { return this.BlkValVField; }
    //            set { this.BlkValA = value; }
    //        }

    //        public INC OpCntRs { get; set; }

    //        public ING OpDlTmms { get; set; }

    //        public ING RsDlTmms { get; set; }

    //        public SPS BlkA { get; set; }

    //        public SPS BlkV { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "PPAM" (Phase angle measuring).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class PPAM : CommonLogicalNode
    //    {
    //        public PPAM()
    //        {
    //            this.lnClass = tLNClassEnum.PPAM.ToString();
    //        }

    //        public PPAM(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.PPAM.ToString();
    //            this.iedType = iedType;
    //            this.Str = new ACD("Str", lnType, iedType);
    //            this.Op = new ACT("Op", lnType, iedType);
    //            this.StrVal = new ASG("StrVal", lnType, iedType);
    //            this.OpCntRs = new INC("OpCntRs", lnType, iedType);
    //        }

    //        [Required]
    //        public ACD Str { get; set; }

    //        [Required]
    //        public ACT Op { get; set; }

    //        public ASG StrVal { get; set; }

    //        public INC OpCntRs { get; set; }


    //        public SPS Gen { get; set; }
    //        public SPS Mot { get; set; }
    //        public SPS N2SlpDet { get; set; }
    //        public SPS SlpZ1 { get; set; }
    //        public SPS N1SlpDet { get; set; }
    //        public SPS SlpZ2 { get; set; }

    //        public MV SlpHz { get; set; }
    //        public MV SlpZOhm { get; set; }
    //        public MV CosUkV { get; set; }






    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "PSCH" (Protection scheme).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class PSCH : CommonLogicalNode
    //    {
    //        public PSCH()
    //        {
    //            lnClass = tLNClassEnum.PSCH.ToString();
    //        }

    //        public PSCH(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            lnClass = tLNClassEnum.PSCH.ToString();
    //            // Common Logical Node
    //            //this.OpCntRs = new INC("OpCntRs", lnType, iedType);
    //            // Status Information
    //            //this.ProTx = new SPS("ProTx", lnType, iedType);
    //            //this.ProRx = new SPS("ProRx", lnType, iedType);
    //            //this.Str = new ACD("Str", lnType, iedType);
    //            //this.Op = new ACT("Op", lnType, iedType);
    //            //this.CarRx = new ACT("CarRx", lnType, iedType);
    //            //this.LosOfGrd = new SPS("LosOfGrd", lnType, iedType);
    //            //this.Echo = new ACT("Echo", lnType, iedType);
    //            //this.WeiOp = new ACT("WeiOp", lnType, iedType);
    //            //this.RvABlk = new ACT("RvABlk", lnType, iedType);
    //            //this.GrdRx = new SPS("GrdRx", lnType, iedType);
    //            //// Settings
    //            //this.SchTyp = new ING("SchTyp", lnType, iedType);
    //            //this.OpDlTmms = new ING("OpDlTmms", lnType, iedType);
    //            //this.CrdTmms = new ING("CrdTmms", lnType, iedType);
    //            //this.DurTmms = new ING("DurTmms", lnType, iedType);
    //            //this.UnBlkMod = new ING("UnBlkMod", lnType, iedType);
    //            //this.SecTmms = new ING("SecTmms", lnType, iedType);
    //            //this.WeiMod = new ING("WeiMod", lnType, iedType);
    //            //this.WeiTmms = new ING("WeiTmms", lnType, iedType);
    //            //this.PPVVal = new ASG("PPVVal", lnType, iedType);
    //            //this.PhGndVal = new ASG("PhGndVal", lnType, iedType);
    //            //this.RvAMod = new ING("RvAMod", lnType, iedType);
    //            //this.RvATmms = new ING("RvATmms", lnType, iedType);
    //            //this.RvRsTmms = new ING("RvRsTmms", lnType, iedType);
    //        }

    //        [XmlIgnore]
    //        public INC OpCntRs { get; set; }

    //        // Status Information
    //        [Required, XmlIgnore]
    //        public SPS ProRx { get; set; }

    //        [Required, XmlIgnore]
    //        public SPS ProTx { get; set; }

    //        [Required, XmlIgnore]
    //        public ACD Str { get; set; }

    //        [Required, XmlIgnore]
    //        public ACT Op { get; set; }
    //        [XmlIgnore]
    //        public ACT CarRx { get; set; }
    //        [XmlIgnore]
    //        public SPS LosOfGrd { get; set; }
    //        [XmlIgnore]
    //        public ACT Echo { get; set; }
    //        [XmlIgnore]
    //        public ACT WeiOp { get; set; }
    //        [XmlIgnore]
    //        public ACT RvABlk { get; set; }
    //        [XmlIgnore]
    //        public SPS GrdRx { get; set; }

    //        [XmlIgnore]
    //        public ING SchTyp { get; set; }
    //        [XmlIgnore]
    //        public ING OpDlTmms { get; set; }
    //        [XmlIgnore]
    //        public ING CrdTmms { get; set; }
    //        [XmlIgnore]
    //        public ING DurTmms { get; set; }
    //        [XmlIgnore]
    //        public ING UnBlkMod { get; set; }
    //        [XmlIgnore]
    //        public ING SecTmms { get; set; }
    //        [XmlIgnore]
    //        public ING WeiMod { get; set; }
    //        [XmlIgnore]
    //        public ING WeiTmms { get; set; }
    //        [XmlIgnore]
    //        public ASG PPVVal { get; set; }
    //        [XmlIgnore]
    //        public ASG PhGndVal { get; set; }
    //        [XmlIgnore]
    //        public ING RvAMod { get; set; }
    //        [XmlIgnore]
    //        public ING RvATmms { get; set; }
    //        [XmlIgnore]
    //        public ING RvRsTmms { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "PSDE" (Sensitive directional earthfault).
    //    /// </summary>			
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class PSDE : CommonLogicalNode
    //    {
    //        public PSDE()
    //        {
    //            this.lnClass = tLNClassEnum.PSDE.ToString();
    //        }

    //        public PSDE(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.lnClass = tLNClassEnum.CCGR.ToString();
    //            //this.Str = new ACD("Str", lnType, iedType);
    //            //this.Op = new ACT("Op", lnType, iedType);
    //            //this.Ang = new ASG("Ang", lnType, iedType);
    //            //this.GndStr = new ASG("GndStr", lnType, iedType);
    //            //this.GndOp = new ASG("GndOp", lnType, iedType);
    //            //this.OpCntRs = new INC("OpCntRs", lnType, iedType);
    //            //this.StrDlTmms = new ING("StrDlTmms", lnType, iedType);
    //            //this.OpDlTmms = new ING("OpDlTmms", lnType, iedType);
    //            //this.DirMod = new ING("DirMod", lnType, iedType);
    //        }

    //        [Required]
    //        public ACD Str { get; set; }

    //        public ACT Op { get; set; }

    //        public ASG Ang { get; set; }

    //        public ASG GndOp { get; set; }

    //        public ASG GndStr { get; set; }

    //        public INC OpCntRs { get; set; }

    //        public ING DirMod { get; set; }

    //        public ING OpDlTmms { get; set; }

    //        public ING StrDlTmms { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "PTEF" (Transient earth fault).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class PTEF : CommonLogicalNode
    //    {
    //        private ASG GndOpField;
    //        public PTEF()
    //        {
    //            lnClass = tLNClassEnum.PTEF.ToString();
    //        }
    //        public PTEF(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.CCGR.ToString();
    //            this.iedType = iedType;
    //            this.Str = new ACD("Str", lnType, iedType);
    //            this.Op = new ACT("Op", lnType, iedType);
    //            this.GndStr = new ASG("GndStr", lnType, iedType);
    //            this.GndOpField = new ASG("GndOp", lnType, iedType);
    //            this.OpCntRs = new INC("OpCntRs", lnType, iedType);
    //            this.DirMod = new ING("DirMod", lnType, iedType);
    //        }

    //        public ACD Str { get; set; }

    //        public ACT Op { get; set; }

    //        public ASG GndStr { get; set; }

    //        public INC OpCntRs { get; set; }

    //        public ING DirMod { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "PTOC" (Time overcurrent).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class PTOC : CommonLogicalNode
    //    {
    //        public PTOC()
    //        {
    //            this.lnClass = tLNClassEnum.CCGR.ToString();
    //            Str = new ACD();
    //            Str.dirGeneral = new dirGeneral();
    //        }

    //        public PTOC(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.lnClass = tLNClassEnum.CCGR.ToString();
    //            //this.Str = new ACD("Str", lnType, iedType);
    //            //this.Op = new ACT("Op", lnType, iedType);
    //            //this.StrVal = new ASG("StrVal", lnType, iedType);
    //            //this.TmMult = new ASG("TmMult", lnType, iedType);
    //            //this.TmASt = new CSD("TmASt", lnType, iedType);
    //            //this.TmACrv = new CURVE("TmACrv", lnType, iedType);
    //            //this.OpCntRs = new INC("OpCntRs", lnType, iedType);
    //            //this.MinOpTmms = new ING("MinOpTmms", lnType, iedType);
    //            //this.MaxOpTmms = new ING("MaxOpTmms", lnType, iedType);
    //            //this.OpDlTmms = new ING("OpDlTmms", lnType, iedType);
    //            //this.TypRsCrv = new ING("TypRsCrv", lnType, iedType);
    //            //this.RsDlTmms = new ING("RsDlTmms", lnType, iedType);
    //            //this.DirMod = new ING("DirMod", lnType, iedType);
    //        }

    //        [Required, XmlIgnore]
    //        public ACD Str { get; set; }

    //        [Required, XmlIgnore]
    //        public ACT Op { get; set; }
    //        [XmlIgnore]
    //        public ASG StrVal { get; set; }
    //        [XmlIgnore]
    //        public ASG TmMult { get; set; }
    //        [XmlIgnore]
    //        public CSD TmASt { get; set; }
    //        [XmlIgnore]
    //        public CURVE TmACrv { get; set; }
    //        [XmlIgnore]
    //        public INC OpCntRs { get; set; }
    //        [XmlIgnore]
    //        public ING DirMod { get; set; }
    //        [XmlIgnore]
    //        public ING MaxOpTmms { get; set; }
    //        [XmlIgnore]
    //        public ING MinOpTmms { get; set; }
    //        [XmlIgnore]
    //        public ING OpDlTmms { get; set; }
    //        [XmlIgnore]
    //        public ING RsDlTmms { get; set; }
    //        [XmlIgnore]
    //        public ING TypRsCrv { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "PTOF" (Overfrequency).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class PTOF : CommonLogicalNode
    //    {
    //        public PTOF()
    //        {
    //            lnClass = tLNClassEnum.PTOF.ToString();
    //        }
    //        public PTOF(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.CCGR.ToString();
    //            this.iedType = iedType;
    //            this.Str = new ACD("Str", lnType, iedType);
    //            this.Op = new ACT("Op", lnType, iedType);
    //            this.StrVal = new ASG("StrVal", lnType, iedType);
    //            this.BlkVal = new ASG("BlkVal", lnType, iedType);
    //            this.OpCntRs = new INC("OpCntRs", lnType, iedType);
    //            this.OpDlTmms = new ING("OpDlTmms", lnType, iedType);
    //            this.RsDlTmms = new ING("RsDlTmms", lnType, iedType);
    //            this.BlkV = new SPS("BlkV", lnType, iedType);
    //        }

    //        [Required]
    //        public ACD Str { get; set; }

    //        [Required]
    //        public ACT Op { get; set; }

    //        public ASG BlkVal { get; set; }

    //        public ASG StrVal { get; set; }

    //        public INC OpCntRs { get; set; }

    //        public ING OpDlTmms { get; set; }

    //        public ING RsDlTmms { get; set; }

    //        public SPS BlkV { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "PTOV" (Overvoltage).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class PTOV : CommonLogicalNode
    //    {
    //        public PTOV()
    //        {
    //            lnClass = tLNClassEnum.PTOV.ToString();
    //        }
    //        public PTOV(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.CCGR.ToString();
    //            this.iedType = iedType;
    //            this.Str = new ACD("Str", lnType, iedType);
    //            this.Op = new ACT("Op", lnType, iedType);
    //            this.StrVal = new ASG("StrVal", lnType, iedType);
    //            this.TmMult = new ASG("TmMult", lnType, iedType);
    //            this.TmVSt = new CSD("TmVSt", lnType, iedType);
    //            this.TmVCrv = new CURVE("TmVCrv", lnType, iedType);
    //            this.OpCntRs = new INC("OpCntRs", lnType, iedType);
    //            this.MinOpTmms = new ING("MinOpTmms", lnType, iedType);
    //            this.MaxOpTmms = new ING("MaxOpTmms", lnType, iedType);
    //            this.OpDlTmms = new ING("OpDlTmms", lnType, iedType);
    //            this.RsDlTmms = new ING("RsDlTmms", lnType, iedType);
    //        }

    //        [Required]
    //        public ACD Str { get; set; }

    //        [Required]
    //        public ACT Op { get; set; }

    //        public ASG StrVal { get; set; }

    //        public ASG TmMult { get; set; }

    //        public CSD TmVSt { get; set; }

    //        public CURVE TmVCrv { get; set; }

    //        public INC OpCntRs { get; set; }

    //        public ING MaxOpTmms { get; set; }

    //        public ING MinOpTmms { get; set; }

    //        public ING OpDlTmms { get; set; }

    //        public ING RsDlTmms { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "PTRC" (Protection trip conditioning).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class PTRC : CommonLogicalNode
    //    {
    //        public PTRC()
    //        {
    //            this.lnClass = tLNClassEnum.CCGR.ToString();
    //        }

    //        public PTRC(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.lnClass = tLNClassEnum.CCGR.ToString();
    //            //this.Str = new ACD("Str", lnType, iedType);
    //            //this.Tr = new ACT("Tr", lnType, iedType);
    //            //this.Op = new ACT("Op", lnType, iedType);
    //            //this.OpCntRs = new INC("OpCntRs", lnType, iedType);
    //            //this.TrMod = new ING("TrMod", lnType, iedType);
    //            //this.TrPlsTmms = new ING("TrPlsTmms", lnType, iedType);
    //        }
    //        [XmlIgnore]
    //        public ACD StrSOF { get; set; }
    //        [XmlIgnore]
    //        public ACT OpSOF { get; set; }

    //        [XmlIgnore]
    //        public ACD Str { get; set; }
    //        [XmlIgnore]
    //        public ACT Op { get; set; }

    //        [XmlIgnore]
    //        public ACT Tr { get; set; }
    //        [XmlIgnore]
    //        public INC OpCntRs { get; set; }
    //        [XmlIgnore]
    //        public ING TrMod { get; set; }
    //        [XmlIgnore]
    //        public ING TrPlsTmms { get; set; }
    //        [XmlIgnore]
    //        public SPS TwoPTr { get; set; }
    //        [XmlIgnore]
    //        public SPS SPTr { get; set; }
    //        [XmlIgnore]
    //        public SPS TPTr { get; set; }
    //        [XmlIgnore]
    //        public SPC LORs { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "PTTR" (Thermal overload).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class PTTR : CommonLogicalNode
    //    {
    //        public PTTR()
    //        {
    //            this.lnClass = tLNClassEnum.PTTR.ToString();

    //        }

    //        public PTTR(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.CCGR.ToString();
    //            this.iedType = iedType;
    //            this.Str = new ACD("Str", lnType, iedType);
    //            this.Op = new ACT("Op", lnType, iedType);
    //            this.AlmThm = new ACT("AlmThm", lnType, iedType);
    //            this.TmpMax = new ASG("TmpMax", lnType, iedType);
    //            this.StrVal = new ASG("StrVal", lnType, iedType);
    //            this.AlmVal = new ASG("AlmVal", lnType, iedType);
    //            this.TmTmpSt = new CSD("TmTmpSt", lnType, iedType);
    //            this.TmASt = new CSD("TmASt", lnType, iedType);
    //            this.TmTmpCrv = new CURVE("TmTmpCrv", lnType, iedType);
    //            this.TmACrv = new CURVE("TmACrv", lnType, iedType);
    //            this.OpCntRs = new INC("OpCntRs", lnType, iedType);
    //            this.OpDlTmms = new ING("OpDlTmms", lnType, iedType);
    //            this.MinOpTmms = new ING("MinOpTmms", lnType, iedType);
    //            this.MaxOpTmms = new ING("MaxOpTmms", lnType, iedType);
    //            this.RsDlTmms = new ING("RsDlTmms", lnType, iedType);
    //            this.ConsTms = new ING("ConsTms", lnType, iedType);
    //            this.Amp = new MV("Amp", lnType, iedType);
    //            this.Tmp = new MV("Tmp", lnType, iedType);
    //            this.TmpRl = new MV("TmpRl", lnType, iedType);
    //            this.LodRsvAlm = new MV("LodRsvAlm", lnType, iedType);
    //            this.LodRsvTr = new MV("LodRsvTr", lnType, iedType);
    //            this.AgeRat = new MV("AgeRat", lnType, iedType);
    //        }

    //        public ACD Str { get; set; }

    //        public ACT AlmThm { get; set; }

    //        [Required]
    //        public ACT Op { get; set; }

    //        public ASG AlmVal { get; set; }

    //        public ASG StrVal { get; set; }

    //        public ASG TmpMax { get; set; }

    //        public CSD TmASt { get; set; }

    //        public CSD TmTmpSt { get; set; }

    //        public CURVE TmACrv { get; set; }

    //        public CURVE TmTmpCrv { get; set; }

    //        public INC OpCntRs { get; set; }

    //        public ING ConsTms { get; set; }

    //        public ING MaxOpTmms { get; set; }

    //        public ING MinOpTmms { get; set; }

    //        public ING OpDlTmms { get; set; }

    //        public ING RsDlTmms { get; set; }

    //        public MV AgeRat { get; set; }

    //        public MV Amp { get; set; }

    //        public MV LodRsvAlm { get; set; }

    //        public MV LodRsvTr { get; set; }

    //        public MV Tmp { get; set; }

    //        public MV TmpRl { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "PTUC" (Undercurrent).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class PTUC : CommonLogicalNode
    //    {

    //        public PTUC()
    //        {
    //            this.lnClass = tLNClassEnum.PTUC.ToString();

    //        }


    //        public PTUC(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.PTUC.ToString();
    //            this.iedType = iedType;
    //            this.Str = new ACD("Str", lnType, iedType);
    //            this.Op = new ACT("Op", lnType, iedType);
    //            this.StrVal = new ASG("StrVal", lnType, iedType);
    //            this.TmMult = new ASG("TmMult", lnType, iedType);
    //            this.TmASt = new CSD("TmASt", lnType, iedType);
    //            this.TmACrv = new CURVE("TmACrv", lnType, iedType);
    //            this.OpCntRs = new INC("OpCntRs", lnType, iedType);
    //            this.OpDlTmms = new ING("OpDlTmms", lnType, iedType);
    //            this.MinOpTmms = new ING("MinOpTmms", lnType, iedType);
    //            this.MaxOpTmms = new ING("MaxOpTmms", lnType, iedType);
    //            this.TypRsCrv = new ING("TypRsCrv", lnType, iedType);
    //            this.RsDlTmms = new ING("RsDlTmms", lnType, iedType);
    //            this.DirMod = new ING("DirMod", lnType, iedType);
    //        }

    //        [Required]
    //        public ACD Str { get; set; }

    //        [Required]
    //        public ACT Op { get; set; }

    //        public ASG StrVal { get; set; }

    //        public ASG TmMult { get; set; }

    //        public CSD TmASt { get; set; }

    //        public CURVE TmACrv { get; set; }

    //        public INC OpCntRs { get; set; }

    //        public ING OpDlTmms { get; set; }

    //        public ING MinOpTmms { get; set; }

    //        public ING MaxOpTmms { get; set; }

    //        public ING TypRsCrv { get; set; }

    //        public ING RsDlTmms { get; set; }

    //        public ING DirMod { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "PTUF" (Underfrequency).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class PTUF : CommonLogicalNode
    //    {
    //        public PTUF()
    //        {
    //            lnClass = tLNClassEnum.PTUF.ToString();
    //        }
    //        public PTUF(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.PTUF.ToString();
    //            this.iedType = iedType;
    //            this.Str = new ACD("Str", lnType, iedType);
    //            this.Op = new ACT("Op", lnType, iedType);
    //            this.StrVal = new ASG("StrVal", lnType, iedType);
    //            this.BlkVal = new ASG("BlkVal", lnType, iedType);
    //            this.OpCntRs = new INC("OpCntRs", lnType, iedType);
    //            this.OpDlTmms = new ING("OpDlTmms", lnType, iedType);
    //            this.RsDlTmms = new ING("RsDlTmms", lnType, iedType);
    //            this.BlkV = new SPS("BlkV", lnType, iedType);
    //        }

    //        [Required]
    //        public ACD Str { get; set; }

    //        [Required]
    //        public ACT Op { get; set; }

    //        public ASG StrVal { get; set; }

    //        public ASG BlkVal { get; set; }

    //        public INC OpCntRs { get; set; }

    //        public ING OpDlTmms { get; set; }

    //        public ING RsDlTmms { get; set; }

    //        public SPS BlkV { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "PTUV" (Undervoltage).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class PTUV : CommonLogicalNode
    //    {
    //        public PTUV()
    //        {

    //            this.lnClass = tLNClassEnum.PTUV.ToString();
    //        }



    //        public PTUV(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.PTUV.ToString();
    //            this.iedType = iedType;
    //            this.Str = new ACD("Str", lnType, iedType);
    //            this.Op = new ACT("Op", lnType, iedType);
    //            this.StrVal = new ASG("StrVal", lnType, iedType);
    //            this.TmMult = new ASG("TmMult", lnType, iedType);
    //            this.TmVSt = new CSD("TmVSt", lnType, iedType);
    //            this.TmVCrv = new CURVE("TmVCrv", lnType, iedType);
    //            this.OpCntRs = new INC("OpCntRs", lnType, iedType);
    //            this.MinOpTmms = new ING("MinOpTmms", lnType, iedType);
    //            this.MaxOpTmms = new ING("MaxOpTmms", lnType, iedType);
    //            this.OpDlTmms = new ING("OpDlTmms", lnType, iedType);
    //            this.RsDlTmms = new ING("RsDlTmms", lnType, iedType);
    //        }

    //        [Required]
    //        public ACD Str { get; set; }

    //        [Required]
    //        public ACT Op { get; set; }

    //        public ASG StrVal { get; set; }

    //        public ASG TmMult { get; set; }

    //        public CSD TmVSt { get; set; }

    //        public CURVE TmVCrv { get; set; }

    //        public INC OpCntRs { get; set; }

    //        public ING MinOpTmms { get; set; }

    //        public ING MaxOpTmms { get; set; }

    //        public ING OpDlTmms { get; set; }

    //        public ING RsDlTmms { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "PUPF" (Underpower factor).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class PUPF : CommonLogicalNode
    //    {

    //        public PUPF()
    //        {
    //            this.lnClass = tLNClassEnum.PUPF.ToString();

    //        }


    //        public PUPF(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.PUPF.ToString();
    //            this.iedType = iedType;
    //            this.Str = new ACD("Str", lnType, iedType);
    //            this.Op = new ACT("Op", lnType, iedType);
    //            this.StrVal = new ASG("StrVal", lnType, iedType);
    //            this.BlkValA = new ASG("BlkValA", lnType, iedType);
    //            this.BlkValV = new ASG("BlkValV", lnType, iedType);
    //            this.OpCntRs = new INC("OpCntRs", lnType, iedType);
    //            this.OpDlTmms = new ING("OpDlTmms", lnType, iedType);
    //            this.RsDlTmms = new ING("RsDlTmms", lnType, iedType);
    //            this.BlkA = new SPS("BlkA", lnType, iedType);
    //            this.BlkV = new SPS("BlkV", lnType, iedType);
    //        }

    //        [Required]
    //        public ACD Str { get; set; }

    //        [Required]
    //        public ACT Op { get; set; }

    //        public ASG StrVal { get; set; }

    //        public ASG BlkValA { get; set; }

    //        public ASG BlkValV { get; set; }

    //        public INC OpCntRs { get; set; }

    //        public ING OpDlTmms { get; set; }

    //        public ING RsDlTmms { get; set; }

    //        public SPS BlkA { get; set; }

    //        public SPS BlkV { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "PVOC" (Voltage controlled time overcurrent).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class PVOC : CommonLogicalNode
    //    {

    //        public PVOC()
    //        {
    //            this.lnClass = tLNClassEnum.PVOC.ToString();

    //        }


    //        public PVOC(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.PVOC.ToString();
    //            this.iedType = iedType;
    //            this.Str = new ACD("Str", lnType, iedType);
    //            this.Op = new ACT("Op", lnType, iedType);
    //            this.TmMult = new ASG("TmMult", lnType, iedType);
    //            this.AVSt = new CSD("AVSt", lnType, iedType);
    //            this.TmASt = new CSD("TmASt", lnType, iedType);
    //            this.AVCrv = new CURVE("AVCrv", lnType, iedType);
    //            this.TmACrv = new CURVE("TmACrv", lnType, iedType);
    //            this.OpCntRs = new INC("OpCntRs", lnType, iedType);
    //            this.MinOpTmms = new ING("MinOpTmms", lnType, iedType);
    //            this.MaxOpTmms = new ING("MaxOpTmms", lnType, iedType);
    //            this.OpDlTmms = new ING("OpDlTmms", lnType, iedType);
    //            this.TypRsCrv = new ING("TypRsCrv", lnType, iedType);
    //            this.RsDlTmms = new ING("RsDlTmms", lnType, iedType);
    //        }

    //        [Required]
    //        public ACD Str { get; set; }

    //        [Required]
    //        public ACT Op { get; set; }

    //        public ASG TmMult { get; set; }

    //        public CSD AVSt { get; set; }

    //        public CSD TmASt { get; set; }

    //        public CURVE AVCrv { get; set; }

    //        public CURVE TmACrv { get; set; }

    //        public INC OpCntRs { get; set; }

    //        public ING MinOpTmms { get; set; }

    //        public ING MaxOpTmms { get; set; }

    //        public ING OpDlTmms { get; set; }

    //        public ING TypRsCrv { get; set; }

    //        public ING RsDlTmms { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "PVPH" (Volts per Hz).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class PVPH : CommonLogicalNode
    //    {
    //        public PVPH()
    //        {

    //            this.lnClass = tLNClassEnum.PVPH.ToString();
    //        }
    //        public PVPH(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.PVPH.ToString();
    //            this.iedType = iedType;
    //            this.Str = new ACD("Str", lnType, iedType);
    //            this.Op = new ACT("Op", lnType, iedType);
    //            this.StrVal = new ASG("StrVal", lnType, iedType);
    //            this.TmMult = new ASG("TmMult", lnType, iedType);
    //            this.VHzSt = new CSD("VHzSt", lnType, iedType);
    //            this.VHzCrv = new CURVE("VHzCrv", lnType, iedType);
    //            this.OpCntRs = new INC("OpCntRs", lnType, iedType);
    //            this.OpDlTmms = new ING("OpDlTmms", lnType, iedType);
    //            this.TypRsCrv = new ING("TypRsCrv", lnType, iedType);
    //            this.RsDlTmms = new ING("RsDlTmms", lnType, iedType);
    //            this.MinOpTmms = new ING("MinOpTmms", lnType, iedType);
    //            this.MaxOpTmms = new ING("MaxOpTmms", lnType, iedType);
    //        }

    //        [Required]
    //        public ACD Str { get; set; }

    //        [Required]
    //        public ACT Op { get; set; }

    //        public ASG StrVal { get; set; }

    //        public ASG TmMult { get; set; }

    //        public CSD VHzSt { get; set; }

    //        public CURVE VHzCrv { get; set; }

    //        public INC OpCntRs { get; set; }

    //        public ING OpDlTmms { get; set; }

    //        public ING TypRsCrv { get; set; }

    //        public ING RsDlTmms { get; set; }

    //        public ING MinOpTmms { get; set; }

    //        public ING MaxOpTmms { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "PZSU" (Zero speed or underspeed).
    //    /// </summary>	
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class PZSU : CommonLogicalNode
    //    {

    //        public PZSU()
    //        {

    //            this.lnClass = tLNClassEnum.PZSU.ToString();
    //        }

    //        public PZSU(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.PZSU.ToString();
    //            this.iedType = iedType;
    //            this.Str = new ACD("Str", lnType, iedType);
    //            this.Op = new ACT("Op", lnType, iedType);
    //            this.StrVal = new ASG("StrVal", lnType, iedType);
    //            this.OpCntRs = new INC("OpCntRs", lnType, iedType);
    //            this.OpDlTmms = new ING("OpDlTmms", lnType, iedType);
    //            this.RsDlTmms = new ING("RsDlTmms", lnType, iedType);
    //        }

    //        [Required]
    //        public ACD Str { get; set; }

    //        [Required]
    //        public ACT Op { get; set; }

    //        public ASG StrVal { get; set; }

    //        public INC OpCntRs { get; set; }

    //        public ING OpDlTmms { get; set; }

    //        public ING RsDlTmms { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "RADR" (Disturbance recorder channel analogue).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class RADR : CommonLogicalNode
    //    {
    //        public RADR()
    //        {

    //            this.lnClass = tLNClassEnum.RADR.ToString();
    //        }

    //        public RADR(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.RADR.ToString();
    //            this.iedType = iedType;
    //            this.HiTrgLev = new ASG("HiTrgLev", lnType, iedType);
    //            this.LoTrgLev = new ASG("LoTrgLev", lnType, iedType);
    //            this.OpCntRs = new INC("OpCntRs", lnType, iedType);
    //            this.ChNum = new ING("ChNum", lnType, iedType);
    //            this.TrgMod = new ING("TrgMod", lnType, iedType);
    //            this.LevMod = new ING("LevMod", lnType, iedType);
    //            this.PreTmms = new ING("PreTmms", lnType, iedType);
    //            this.PstTmms = new ING("PstTmms", lnType, iedType);
    //            this.ChTrg = new SPS("ChTrg", lnType, iedType);
    //        }

    //        public ASG HiTrgLev { get; set; }

    //        public ASG LoTrgLev { get; set; }

    //        public INC OpCntRs { get; set; }

    //        public ING ChNum { get; set; }

    //        public ING TrgMod { get; set; }

    //        public ING LevMod { get; set; }

    //        public ING PreTmms { get; set; }

    //        public ING PstTmms { get; set; }

    //        [Required]
    //        public SPS ChTrg { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "RBDR" (Disturbance recorder channel binary).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class RBDR : CommonLogicalNode
    //    {

    //        public RBDR()
    //        {

    //            this.lnClass = tLNClassEnum.RBDR.ToString();
    //        }


    //        public RBDR(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.RBDR.ToString();
    //            this.iedType = iedType;
    //            this.OpCntRs = new INC("OpCntRs", lnType, iedType);
    //            this.ChNum = new ING("ChNum", lnType, iedType);
    //            this.TrgMod = new ING("TrgMod", lnType, iedType);
    //            this.LevMod = new ING("LevMod", lnType, iedType);
    //            this.PreTmms = new ING("PreTmms", lnType, iedType);
    //            this.PstTmms = new ING("PstTmms", lnType, iedType);
    //            this.ChTrg = new SPS("ChTrg", lnType, iedType);
    //        }

    //        public INC OpCntRs { get; set; }

    //        public ING ChNum { get; set; }

    //        public ING TrgMod { get; set; }

    //        public ING LevMod { get; set; }

    //        public ING PreTmms { get; set; }

    //        public ING PstTmms { get; set; }

    //        [Required]
    //        public SPS ChTrg { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "RBRF" (Breaker failure).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class RBRF : CommonLogicalNode
    //    {
    //        public RBRF()
    //        {
    //            this.lnClass = tLNClassEnum.RBRF.ToString();
    //        }

    //        public RBRF(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.lnClass = tLNClassEnum.RBRF.ToString();
    //            //this.Str = new ACD("Str", lnType, iedType);
    //            //this.OpEx = new ACT("OpEx", lnType, iedType);
    //            //this.OpIn = new ACT("OpIn", lnType, iedType);
    //            //this.DetValA = new ASG("DetValA", lnType, iedType);
    //            //this.OpCntRs = new INC("OpCntRs", lnType, iedType);
    //            //this.FailMod = new ING("FailMod", lnType, iedType);
    //            //this.FailTmms = new ING("FailTmms", lnType, iedType);
    //            //this.SPlTrTmms = new ING("SPlTrTmms", lnType, iedType);
    //            //this.TPTrTmms = new ING("TPTrTmms", lnType, iedType);
    //            //this.ReTrMod = new ING("ReTrMod", lnType, iedType);
    //        }
    //        [XmlIgnore]
    //        public ACD Str { get; set; }
    //        [XmlIgnore]
    //        public ACT OpEx { get; set; }
    //        [XmlIgnore]
    //        public ACT OpEx2 { get; set; }
    //        [XmlIgnore]
    //        public ACT OpIn { get; set; }
    //        [XmlIgnore]
    //        public ASG DetValA { get; set; }
    //        [XmlIgnore]
    //        public INC OpCntRs { get; set; }
    //        [XmlIgnore]
    //        public ING FailMod { get; set; }
    //        [XmlIgnore]
    //        public ING FailTmms { get; set; }
    //        [XmlIgnore]
    //        public ING SPlTrTmms { get; set; }
    //        [XmlIgnore]
    //        public ING TPTrTmms { get; set; }
    //        [XmlIgnore]
    //        public ING ReTrMod { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "RDIR" (Directional element).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class RDIR : CommonLogicalNode
    //    {
    //        public RDIR()
    //        {
    //            this.lnClass = tLNClassEnum.RDIR.ToString();
    //        }

    //        public RDIR(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.lnClass = tLNClassEnum.RDIR.ToString();

    //            //this.Dir = new ACD("Dir", lnType, iedType);
    //            //this.ChrAng = new ASG("ChrAng", lnType, iedType);
    //            //this.MinFwdAng = new ASG("MinFwdAng", lnType, iedType);
    //            //this.MinRvAng = new ASG("MinRvAng", lnType, iedType);
    //            //this.MaxFwdAng = new ASG("MaxFwdAng", lnType, iedType);
    //            //this.MaxRvAng = new ASG("MaxRvAng", lnType, iedType);
    //            //this.BlkValA = new ASG("BlkValA", lnType, iedType);
    //            //this.BlkValV = new ASG("BlkValV", lnType, iedType);
    //            //this.MinPPV = new ASG("MinPPV", lnType, iedType);
    //            //this.PolQty = new ING("PolQty", lnType, iedType);
    //        }

    //        [Required, XmlIgnore]
    //        public ACT Op { get; set; }

    //        [Required, XmlIgnore]
    //        public ACD Dir { get; set; }
    //        [XmlIgnore]
    //        public ASG ChrAng { get; set; }
    //        [XmlIgnore]
    //        public ASG MinFwdAng { get; set; }
    //        [XmlIgnore]
    //        public ASG MinRvAng { get; set; }
    //        [XmlIgnore]
    //        public ASG MaxFwdAng { get; set; }
    //        [XmlIgnore]
    //        public ASG MaxRvAng { get; set; }
    //        [XmlIgnore]
    //        public ASG BlkValA { get; set; }
    //        [XmlIgnore]
    //        public ASG BlkValV { get; set; }
    //        [XmlIgnore]
    //        public ASG MinPPV { get; set; }
    //        [XmlIgnore]
    //        public ING PolQty { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "RDRE" (Disturbance recorder function).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class RDRE : CommonLogicalNode
    //    {
    //        public RDRE()
    //        {
    //            this.lnClass = tLNClassEnum.RDRE.ToString();
    //        }

    //        public RDRE(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.lnClass = tLNClassEnum.RDRE.ToString();
    //            //this.OpCntRs = new INC("OpCntRs", lnType, iedType);
    //            //this.TrgMod = new ING("TrgMod", lnType, iedType);
    //            //this.LevMod = new ING("LevMod", lnType, iedType);
    //            //this.PreTmms = new ING("PreTmms", lnType, iedType);
    //            //this.PstTmms = new ING("PstTmms", lnType, iedType);
    //            //this.MemFull = new ING("MemFull", lnType, iedType);
    //            //this.MaxNumRcd = new ING("MaxNumRcd", lnType, iedType);
    //            //this.ReTrgMod = new ING("ReTrgMod", lnType, iedType);
    //            //this.PerTrgTms = new ING("PerTrgTms", lnType, iedType);
    //            //this.ExclTmms = new ING("ExclTmms", lnType, iedType);
    //            //this.OpMod = new ING("OpMod", lnType, iedType);
    //            //this.FltNum = new INS("FltNum", lnType, iedType);
    //            //this.GriFltNum = new INS("GriFltNum", lnType, iedType);
    //            //this.MemUsed = new INS("MemUsed", lnType, iedType);
    //            //this.RcdTrg = new SPC("RcdTrg", lnType, iedType);
    //            //this.MemRs = new SPC("MemRs", lnType, iedType);
    //            //this.MemClr = new SPC("MemClr", lnType, iedType);
    //            //this.RcdMade = new SPC("RcdMade", lnType, iedType);
    //            //this.RcdStr = new SPC("RcdStr", lnType, iedType);
    //        }
    //        [XmlIgnore]
    //        public INC OpCntRs { get; set; }
    //        [XmlIgnore]
    //        public ING TrgMod { get; set; }
    //        [XmlIgnore]
    //        public ING LevMod { get; set; }
    //        [XmlIgnore]
    //        public ING PreTmms { get; set; }
    //        [XmlIgnore]
    //        public ING PstTmms { get; set; }
    //        [XmlIgnore]
    //        public ING MemFull { get; set; }
    //        [XmlIgnore]
    //        public ING MaxNumRcd { get; set; }
    //        [XmlIgnore]
    //        public ING ReTrgMod { get; set; }
    //        [XmlIgnore]
    //        public ING PerTrgTms { get; set; }
    //        [XmlIgnore]
    //        public ING ExclTmms { get; set; }
    //        [XmlIgnore]
    //        public ING OpMod { get; set; }

    //        [Required, XmlIgnore]
    //        public INS FltNum { get; set; }
    //        [XmlIgnore]
    //        public INS GriFltNum { get; set; }
    //        [XmlIgnore]
    //        public INS MemUsed { get; set; }
    //        [XmlIgnore]
    //        public SPC RcdTrg { get; set; }
    //        [XmlIgnore]
    //        public SPC MemRs { get; set; }
    //        [XmlIgnore]
    //        public SPC MemClr { get; set; }

    //        [Required, XmlIgnore]
    //        public SPC RcdMade { get; set; }
    //        [XmlIgnore]
    //        public SPC RcdStr { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "RDRS" (Disturbance record handling).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class RDRS : CommonLogicalNode
    //    {
    //        public RDRS()
    //        {

    //            this.lnClass = tLNClassEnum.RDRS.ToString();
    //        }

    //        public RDRS(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.RDRS.ToString();
    //            this.iedType = iedType;
    //            this.AutoUpLod = new SPC("AutoUpLod", lnType, iedType);
    //            this.DltRcd = new SPC("DltRcd", lnType, iedType);
    //        }

    //        public SPC AutoUpLod { get; set; }

    //        public SPC DltRcd { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "RFLO" (Fault locator).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class RFLO : CommonLogicalNode
    //    {
    //        public RFLO()
    //        {
    //            this.lnClass = tLNClassEnum.RFLO.ToString();
    //        }

    //        public RFLO(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.lnClass = tLNClassEnum.RFLO.ToString();
    //            //this.LinLenKm = new ASG("LinLenKm", lnType, iedType);
    //            //this.R1 = new ASG("R1", lnType, iedType);
    //            //this.X1 = new ASG("X1", lnType, iedType);
    //            //this.R0 = new ASG("R0", lnType, iedType);
    //            //this.X0 = new ASG("X0", lnType, iedType);
    //            //this.Z1Mod = new ASG("Z1Mod", lnType, iedType);
    //            //this.Z1Ang = new ASG("Z1Ang", lnType, iedType);
    //            //this.Z0Mod = new ASG("Z0Mod", lnType, iedType);
    //            //this.Z0Ang = new ASG("Z0Ang", lnType, iedType);
    //            //this.Rm0 = new ASG("Rm0", lnType, iedType);
    //            //this.Xm0 = new ASG("Xm0", lnType, iedType);
    //            //this.Zm0Mod = new ASG("Zm0Mod", lnType, iedType);
    //            //this.Zm0Ang = new ASG("Zm0Ang", lnType, iedType);
    //            //this.FltZ = new CMV("FltZ", lnType, iedType);
    //            //this.OpCntRs = new INC("OpCntRs", lnType, iedType);
    //            //this.FltLoop = new INS("FltLoop", lnType, iedType);
    //            //this.FltDiskm = new MV("FltDiskm", lnType, iedType);
    //        }
    //        [XmlIgnore]
    //        public ASG LinLenKm { get; set; }
    //        [XmlIgnore]
    //        public ASG R1 { get; set; }
    //        [XmlIgnore]
    //        public ASG X1 { get; set; }
    //        [XmlIgnore]
    //        public ASG R0 { get; set; }
    //        [XmlIgnore]
    //        public ASG X0 { get; set; }
    //        [XmlIgnore]
    //        public ASG Z1Mod { get; set; }
    //        [XmlIgnore]
    //        public ASG Z1Ang { get; set; }
    //        [XmlIgnore]
    //        public ASG Z0Mod { get; set; }
    //        [XmlIgnore]
    //        public ASG Z0Ang { get; set; }
    //        [XmlIgnore]
    //        public ASG Rm0 { get; set; }
    //        [XmlIgnore]
    //        public ASG Xm0 { get; set; }
    //        [XmlIgnore]
    //        public ASG Zm0Mod { get; set; }
    //        [XmlIgnore]
    //        public ASG Zm0Ang { get; set; }

    //        [XmlIgnore]
    //        public CMV FltZ { get; set; }
    //        [XmlIgnore]
    //        public CMV Fltz { get; set; }
    //        [XmlIgnore]
    //        public INC OpCntRs { get; set; }
    //        [XmlIgnore]
    //        public INS FltLoop { get; set; }
    //        [XmlIgnore]
    //        public MV FltDiskm { get; set; }

    //        [XmlIgnore]
    //        public SPS ClcFlt { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "RPSB" (Power swing detection/blocking).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class RPSB : CommonLogicalNode
    //    {
    //        public RPSB()
    //        {
    //            this.lnClass = tLNClassEnum.RPSB.ToString();
    //        }

    //        public RPSB(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.lnClass = tLNClassEnum.RPSB.ToString();
    //            //this.Str = new ACD("Str", lnType, iedType);
    //            //this.Op = new ACT("Op", lnType, iedType);
    //            //this.SwgVal = new ASG("SwgVal", lnType, iedType);
    //            //this.SwgRis = new ASG("SwgRis", lnType, iedType);
    //            //this.SwgReact = new ASG("SwgReact", lnType, iedType);
    //            //this.OpCntRs = new INC("OpCntRs", lnType, iedType);
    //            //this.SwgTmms = new ING("SwgTmms", lnType, iedType);
    //            //this.UnBlkTmms = new ING("UnBlkTmms", lnType, iedType);
    //            //this.MaxNumSlp = new ING("MaxNumSlp", lnType, iedType);
    //            //this.EvTmms = new ING("EvTmms", lnType, iedType);
    //            //this.ZeroEna = new SPG("ZeroEna", lnType, iedType);
    //            //this.NgEna = new SPG("NgEna", lnType, iedType);
    //            //this.MaxEna = new SPG("MaxEna", lnType, iedType);
    //            //this.BlkZn = new SPS("BlkZn", lnType, iedType);
    //        }
    //        [XmlIgnore]
    //        public ACD Str { get; set; }
    //        [XmlIgnore]
    //        public ACT Op { get; set; }
    //        [XmlIgnore]
    //        public ASG SwgVal { get; set; }
    //        [XmlIgnore]
    //        public ASG SwgRis { get; set; }
    //        [XmlIgnore]
    //        public ASG SwgReact { get; set; }
    //        [XmlIgnore]
    //        public INC OpCntRs { get; set; }
    //        [XmlIgnore]
    //        public ING SwgTmms { get; set; }
    //        [XmlIgnore]
    //        public ING UnBlkTmms { get; set; }
    //        [XmlIgnore]
    //        public ING MaxNumSlp { get; set; }
    //        [XmlIgnore]
    //        public ING EvTmms { get; set; }
    //        [XmlIgnore]
    //        public SPG ZeroEna { get; set; }
    //        [XmlIgnore]
    //        public SPG NgEna { get; set; }
    //        [XmlIgnore]
    //        public SPG MaxEna { get; set; }
    //        [XmlIgnore]
    //        public SPS BlkZn { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "RREC" (Autoreclosing).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class RREC : CommonLogicalNode
    //    {
    //        public RREC()
    //        {
    //            this.lnClass = tLNClassEnum.RREC.ToString();
    //            BlkRec = new SPC();
    //            BlkRec.stVal.bType = tBasicTypeEnum.BOOLEAN;
    //            AutoRecSt = new INS("AutoRecSt");
    //        }

    //        public RREC(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.lnClass = tLNClassEnum.RREC.ToString();
    //            //this.Op = new ACT("Op", lnType, iedType);
    //            //this.OpCntRs = new INC("OpCntRs", lnType, iedType);
    //            //this.Rec1Tmms = new ING("Rec1Tmms", lnType, iedType);
    //            //this.Rec2Tmms = new ING("Rec2Tmms", lnType, iedType);
    //            //this.Rec3Tmms = new ING("Rec3Tmms", lnType, iedType);
    //            //this.PlsTmms = new ING("PlsTmms", lnType, iedType);
    //            //this.RclTmms = new ING("RclTmms", lnType, iedType);
    //            //this.AutoRecSt = new INS("AutoRecSt", lnType, iedType);
    //            //this.BlkRec = new SPC("BlkRec", lnType, iedType);
    //            //this.ChkRec = new SPC("ChkRec", lnType, iedType);
    //            //this.Auto = new SPS("Auto", lnType, iedType);
    //        }

    //        [Required, XmlIgnore]
    //        public ACT Op { get; set; }
    //        [XmlIgnore]
    //        public INC OpCntRs { get; set; }
    //        [XmlIgnore]
    //        public ING Rec1Tmms { get; set; }
    //        [XmlIgnore]
    //        public ING Rec2Tmms { get; set; }
    //        [XmlIgnore]
    //        public ING Rec3Tmms { get; set; }
    //        [XmlIgnore]
    //        public ING PlsTmms { get; set; }
    //        [XmlIgnore]
    //        public ING RclTmms { get; set; }

    //        [Required, XmlIgnore]
    //        public INS AutoRecSt { get; set; }
    //        [XmlIgnore]
    //        public SPC BlkRec { get; set; }
    //        [XmlIgnore]
    //        public SPC ChkRec { get; set; }
    //        [XmlIgnore]
    //        public SPS Auto { get; set; }
    //        [XmlIgnore]
    //        public SPS EnaRec { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "RSYN" (Synchronism-check or synchronising).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class RSYN : CommonLogicalNode
    //    {
    //        public RSYN()
    //        {
    //            this.lnClass = tLNClassEnum.RSYN.ToString();
    //        }

    //        public RSYN(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.lnClass = tLNClassEnum.RSYN.ToString();
    //            //this.DifV = new ASG("DifV", lnType, iedType);
    //            //this.DifHz = new ASG("DifHz", lnType, iedType);
    //            //this.DifAng = new ASG("DifAng", lnType, iedType);
    //            //this.DeaLinVal = new ASG("DeaLinVal", lnType, iedType);
    //            //this.LivLinVal = new ASG("LivLinVal", lnType, iedType);
    //            //this.DeaBusVal = new ASG("DeaBusVal", lnType, iedType);
    //            //this.LivBusVal = new ASG("LivBusVal", lnType, iedType);
    //            //this.LivDeaMod = new ING("LivDeaMod", lnType, iedType);
    //            //this.PlsTmms = new ING("PlsTmms", lnType, iedType);
    //            //this.BkrTmms = new ING("BkrTmms", lnType, iedType);
    //            //this.DifVClc = new MV("DifVClc", lnType, iedType);
    //            //this.DifHzClc = new MV("DifHzClc", lnType, iedType);
    //            //this.DifAngClc = new MV("DifAngClc", lnType, iedType);
    //            //this.RHz = new SPC("RHz", lnType, iedType);
    //            //this.LHz = new SPC("LHz", lnType, iedType);
    //            //this.RV = new SPC("RV", lnType, iedType);
    //            //this.LV = new SPC("LV", lnType, iedType);
    //            //this.Rel = new SPS("Rel", lnType, iedType);
    //            //this.VInd = new SPS("VInd", lnType, iedType);
    //            //this.AngInd = new SPS("AngInd", lnType, iedType);
    //            //this.HzInd = new SPS("HzInd", lnType, iedType);
    //            //this.SynPrg = new SPS("SynPrg", lnType, iedType);
    //        }
    //        [XmlIgnore]
    //        public ASG DifV { get; set; }
    //        [XmlIgnore]
    //        public ASG DifHz { get; set; }
    //        [XmlIgnore]
    //        public ASG DifAng { get; set; }
    //        [XmlIgnore]
    //        public ASG DeaLinVal { get; set; }
    //        [XmlIgnore]
    //        public ASG LivLinVal { get; set; }
    //        [XmlIgnore]
    //        public ASG DeaBusVal { get; set; }
    //        [XmlIgnore]
    //        public ASG LivBusVal { get; set; }
    //        [XmlIgnore]
    //        public ING LivDeaMod { get; set; }
    //        [XmlIgnore]
    //        public ING PlsTmms { get; set; }
    //        [XmlIgnore]
    //        public ING BkrTmms { get; set; }
    //        [XmlIgnore]
    //        public MV DifVClc { get; set; }
    //        [XmlIgnore]
    //        public MV DifHzClc { get; set; }
    //        [XmlIgnore]
    //        public MV DifAngClc { get; set; }
    //        [XmlIgnore]
    //        public SPC RHz { get; set; }
    //        [XmlIgnore]
    //        public SPC LHz { get; set; }
    //        [XmlIgnore]
    //        public SPC RV { get; set; }
    //        [XmlIgnore]
    //        public SPC LV { get; set; }

    //        [Required, XmlIgnore]
    //        public SPS Rel { get; set; }
    //        [XmlIgnore]
    //        public SPS VInd { get; set; }
    //        [XmlIgnore]
    //        public SPS AngInd { get; set; }
    //        [XmlIgnore]
    //        public SPS HzInd { get; set; }
    //        [XmlIgnore]
    //        public SPS SynPrg { get; set; }
    //        [XmlIgnore]
    //        public SPS EnOk { get; set; }
    //        [XmlIgnore]
    //        public SPS FailSyn { get; set; }
    //        [XmlIgnore]
    //        public SPS TestSCOK { get; set; }
    //        [XmlIgnore]
    //        public ACT Op { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "SARC" (Monitoring and diagnostics for arcs).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class SARC : CommonLogicalNode
    //    {

    //        public SARC()
    //        {

    //            this.lnClass = tLNClassEnum.SARC.ToString();
    //        }


    //        public SARC(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.SARC.ToString();
    //            this.iedType = iedType;
    //            this.EEName = new DPL("EEName", lnType, iedType);
    //            this.OpCntRs = new INC("OpCntRs", lnType, iedType);
    //            this.FACntRs = new INC("FACntRs", lnType, iedType);
    //            this.ArcCntRs = new INC("ArcCntRs", lnType, iedType);
    //            this.EEHealth = new INS("EEHealth", lnType, iedType);
    //            this.FADet = new SPS("FADet", lnType, iedType);
    //            this.SwArcDet = new SPS("SwArcDet", lnType, iedType);
    //        }

    //        public DPL EEName { get; set; }

    //        public INC OpCntRs { get; set; }

    //        [Required]
    //        public INC FACntRs { get; set; }

    //        public INC ArcCntRs { get; set; }

    //        public INS EEHealth { get; set; }

    //        [Required]
    //        public SPS FADet { get; set; }

    //        public SPS SwArcDet { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "SIMG" (Insulation medium supervision (gas)).
    //    /// </summary>			
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class SIMG : CommonLogicalNode
    //    {

    //        public SIMG()
    //        {

    //            this.lnClass = tLNClassEnum.SIMG.ToString();
    //        }


    //        public SIMG(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.SIMG.ToString();
    //            this.iedType = iedType;
    //            this.EEName = new DPL("EEName", lnType, iedType);
    //            this.EEHealth = new INS("EEHealth", lnType, iedType);
    //            this.Pres = new MV("Pres", lnType, iedType);
    //            this.Den = new MV("Den", lnType, iedType);
    //            this.Tmp = new MV("Tmp", lnType, iedType);
    //            this.InsAlm = new SPS("InsAlm", lnType, iedType);
    //            this.InsBlk = new SPS("InsBlk", lnType, iedType);
    //            this.InsTr = new SPS("InsTr", lnType, iedType);
    //            this.PresAlm = new SPS("PresAlm", lnType, iedType);
    //            this.DenAlm = new SPS("DenAlm", lnType, iedType);
    //            this.TmpAlm = new SPS("TmpAlm", lnType, iedType);
    //            this.InsLevMax = new SPS("InsLevMax", lnType, iedType);
    //            this.InsLevMin = new SPS("InsLevMin", lnType, iedType);
    //        }

    //        public DPL EEName { get; set; }

    //        public INS EEHealth { get; set; }

    //        public MV Pres { get; set; }

    //        public MV Den { get; set; }

    //        public MV Tmp { get; set; }

    //        [Required]
    //        public SPS InsAlm { get; set; }

    //        public SPS InsBlk { get; set; }

    //        public SPS InsTr { get; set; }

    //        public SPS PresAlm { get; set; }

    //        public SPS DenAlm { get; set; }

    //        public SPS TmpAlm { get; set; }

    //        public SPS InsLevMax { get; set; }

    //        public SPS InsLevMin { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "SIML" (Insulation medium supervision (liquid)).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class SIML : CommonLogicalNode
    //    {

    //        public SIML()
    //        {

    //            this.lnClass = tLNClassEnum.SIML.ToString();
    //        }


    //        public SIML(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.SIML.ToString();
    //            this.iedType = iedType;
    //            this.EEName = new DPL("EEName", lnType, iedType);
    //            this.EEHealth = new INS("EEHealth", lnType, iedType);
    //            this.Tmp = new MV("Tmp", lnType, iedType);
    //            this.Lev = new MV("Lev", lnType, iedType);
    //            this.Pres = new MV("Pres", lnType, iedType);
    //            this.H2O = new MV("H2O", lnType, iedType);
    //            this.H2OTmp = new MV("H2OTmp", lnType, iedType);
    //            this.H2 = new MV("H2", lnType, iedType);
    //            this.InsAlm = new SPS("InsAlm", lnType, iedType);
    //            this.InsBlk = new SPS("InsBlk", lnType, iedType);
    //            this.InsTr = new SPS("InsTr", lnType, iedType);
    //            this.InsAlm = new SPS("InsAlm", lnType, iedType);
    //            this.PresTr = new SPS("PresTr", lnType, iedType);
    //            this.PresAlm = new SPS("PresAlm", lnType, iedType);
    //            this.GasInsAlm = new SPS("GasInsAlm", lnType, iedType);
    //            this.GasInsTr = new SPS("GasInsTr", lnType, iedType);
    //            this.GasFlwTr = new SPS("GasFlwTr", lnType, iedType);
    //            this.InsLevMax = new SPS("InsLevMax", lnType, iedType);
    //            this.InsLevMin = new SPS("InsLevMin", lnType, iedType);
    //            this.H2Alm = new SPS("H2Alm", lnType, iedType);
    //            this.MstAlm = new SPS("MstAlm", lnType, iedType);
    //            this.TmpAlm = new SPS("TmpAlm", lnType, iedType);
    //        }

    //        public DPL EEName { get; set; }

    //        public INS EEHealth { get; set; }

    //        public MV Tmp { get; set; }

    //        public MV Lev { get; set; }

    //        public MV Pres { get; set; }

    //        public MV H2O { get; set; }

    //        public MV H2OTmp { get; set; }

    //        public MV H2 { get; set; }

    //        [Required]
    //        public SPS InsAlm { get; set; }

    //        public SPS InsBlk { get; set; }

    //        public SPS InsTr { get; set; }

    //        public SPS TmpAlm { get; set; }

    //        public SPS PresTr { get; set; }

    //        public SPS PresAlm { get; set; }

    //        public SPS GasInsAlm { get; set; }

    //        public SPS GasInsTr { get; set; }

    //        public SPS GasFlwTr { get; set; }

    //        public SPS InsLevMax { get; set; }

    //        public SPS InsLevMin { get; set; }

    //        public SPS H2Alm { get; set; }

    //        public SPS MstAlm { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "SPDC" (Monitoring and diagnostics for partial discharges).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class SPDC : CommonLogicalNode
    //    {

    //        public SPDC()
    //        {

    //            this.lnClass = tLNClassEnum.SPDC.ToString();
    //        }

    //        public SPDC(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.SPDC.ToString();
    //            this.iedType = iedType;
    //            this.EEName = new DPL("EEName", lnType, iedType);
    //            this.EEHealth = new INS("EEHealth", lnType, iedType);
    //            this.OpCnt = new INS("OpCnt", lnType, iedType);
    //            this.AcuPaDsch = new MV("AcuPaDsch", lnType, iedType);
    //            this.PaDschAlm = new SPS("PaDschAlm", lnType, iedType);
    //        }

    //        public DPL EEName { get; set; }

    //        public INS EEHealth { get; set; }

    //        [Required]
    //        public INS OpCnt { get; set; }

    //        public MV AcuPaDsch { get; set; }

    //        public SPS PaDschAlm { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "TCTR" (Current transformer).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class TCTR : CommonLogicalNode
    //    {

    //        public TCTR()
    //        {

    //            this.lnClass = tLNClassEnum.TCTR.ToString();
    //        }

    //        public TCTR(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.TCTR.ToString();
    //            this.iedType = iedType;
    //            this.ARtg = new ASG("ARtg", lnType, iedType);
    //            this.HzRtg = new ASG("HzRtg", lnType, iedType);
    //            this.Rat = new ASG("Rat", lnType, iedType);
    //            this.Cor = new ASG("Cor", lnType, iedType);
    //            this.AngCor = new ASG("AngCor", lnType, iedType);
    //            this.EEName = new DPL("EEName", lnType, iedType);
    //            this.EEHealth = new INS("EEHealth", lnType, iedType);
    //            this.OpTmh = new INS("OpTmh", lnType, iedType);
    //            this.Amp = new SAV("Amp", lnType, iedType);
    //        }

    //        public ASG ARtg { get; set; }

    //        public ASG HzRtg { get; set; }

    //        public ASG Rat { get; set; }

    //        public ASG Cor { get; set; }

    //        public ASG AngCor { get; set; }

    //        public DPL EEName { get; set; }

    //        public INS EEHealth { get; set; }

    //        public INS OpTmh { get; set; }

    //        [Required]
    //        public SAV Amp { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "TVTR" (Voltage transformer).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class TVTR : CommonLogicalNode
    //    {

    //        public TVTR()
    //        {

    //            this.lnClass = tLNClassEnum.TVTR.ToString();
    //        }

    //        public TVTR(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.TVTR.ToString();
    //            this.iedType = iedType;
    //            this.VRtg = new ASG("VRtg", lnType, iedType);
    //            this.HzRtg = new ASG("HzRtg", lnType, iedType);
    //            this.Rat = new ASG("Rat", lnType, iedType);
    //            this.Cor = new ASG("Cor", lnType, iedType);
    //            this.AngCor = new ASG("AngCor", lnType, iedType);
    //            this.EEName = new DPL("EEName", lnType, iedType);
    //            this.EEHealth = new INS("EEHealth", lnType, iedType);
    //            this.OpTmh = new INS("OpTmh", lnType, iedType);
    //            this.Vol = new SAV("Vol", lnType, iedType);
    //            this.FuFail = new SPS("FuFail", lnType, iedType);
    //        }

    //        public ASG VRtg { get; set; }

    //        public ASG HzRtg { get; set; }

    //        public ASG Rat { get; set; }

    //        public ASG Cor { get; set; }

    //        public ASG AngCor { get; set; }

    //        public DPL EEName { get; set; }

    //        public INS EEHealth { get; set; }

    //        public INS OpTmh { get; set; }

    //        [Required]
    //        public SAV Vol { get; set; }

    //        public SPS FuFail { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "XCBR" (Circuit breaker).
    //    /// </summary>	
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class XCBR : CommonLogicalNode
    //    {
    //        public XCBR()
    //        {
    //            this.lnClass = tLNClassEnum.XCBR.ToString();
    //            this.OpCnt = new INS();
    //            this.OpCnt.stVal.bType = tBasicTypeEnum.INT32;
    //            this.Pos = new DPC();
    //            Pos.stVal.bType = tBasicTypeEnum.Dbpos;
    //            Pos.subVal.bType = tBasicTypeEnum.Dbpos;
    //            CBOpCap = new INS("CBOpCap");
    //            CBOpCap.stVal = new stVal();
    //            CBOpCap.stVal.bType = tBasicTypeEnum.INT32U;
    //        }

    //        public XCBR(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.lnClass = tLNClassEnum.XCBR.ToString();
    //            //this.SumSwARs = new BCR("SumSwARs", lnType, iedType);
    //            //this.Pos = new DPC("Pos", lnType, iedType);
    //            //this.EEName = new DPL("EEName", lnType, iedType);
    //            //this.EEHealth = new INS("EEHealth", lnType, iedType);

    //            //this.CBOpCap = new INS("CBOpCap", lnType, iedType);
    //            //this.POWCap = new INS("POWCap", lnType, iedType);
    //            //this.MaxOpCap = new INS("MaxOpCap", lnType, iedType);
    //            //this.BlkOpn = new SPC("BlkOpn", lnType, iedType);
    //            //this.BlkCls = new SPC("BlkCls", lnType, iedType);
    //            //this.ChaMotEna = new SPC("ChaMotEna", lnType, iedType);
    //            //this.Loc = new SPS("Loc", lnType, iedType);
    //        }
    //        [XmlIgnore]
    //        public BCR SumSwARs { get; set; }

    //        [Required, XmlIgnore]
    //        public DPC Pos { get; set; }

    //        [XmlIgnore]
    //        public DPL EEName { get; set; }

    //        [XmlIgnore]
    //        public INS EEHealth { get; set; }

    //        [Required, XmlIgnore]
    //        public INS OpCnt { get; set; }

    //        [Required, XmlIgnore]
    //        public INS CBOpCap { get; set; }

    //        [XmlIgnore]
    //        public INS POWCap { get; set; }

    //        [XmlIgnore]
    //        public INS MaxOpCap { get; set; }

    //        [Required, XmlIgnore]
    //        public SPC BlkOpn { get; set; }

    //        [Required, XmlIgnore]
    //        public SPC BlkCls { get; set; }
    //        [XmlIgnore]
    //        public SPC ChaMotEna { get; set; }

    //        [Required, XmlIgnore]
    //        public SPS Loc { get; set; }
    //        [Required, XmlIgnore]
    //        public SPS BlkUpd { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "XSWI" (Circuit switch).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class XSWI : CommonLogicalNode
    //    {

    //        public XSWI()
    //        {

    //            this.lnClass = tLNClassEnum.XSWI.ToString();
    //        }

    //        public XSWI(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.XSWI.ToString();
    //            this.iedType = iedType;
    //            this.Pos = new DPC("Pos", lnType, iedType);
    //            this.EEName = new DPL("EEName", lnType, iedType);
    //            this.EEHealth = new INS("EEHealth", lnType, iedType);
    //            this.OpCnt = new INS("OpCnt", lnType, iedType);
    //            this.SwTyp = new INS("SwTyp", lnType, iedType);
    //            this.SwOpCap = new INS("SwOpCap", lnType, iedType);
    //            this.MaxOpCap = new INS("MaxOpCap", lnType, iedType);
    //            this.BlkOpn = new SPC("BlkOpn", lnType, iedType);
    //            this.BlkCls = new SPC("BlkCls", lnType, iedType);
    //            this.ChaMotEna = new SPC("ChaMotEna", lnType, iedType);
    //            this.Loc = new SPS("Loc", lnType, iedType);
    //        }

    //        [Required]
    //        public DPC Pos { get; set; }

    //        public DPL EEName { get; set; }

    //        public INS EEHealth { get; set; }

    //        [Required]
    //        public INS OpCnt { get; set; }

    //        [Required]
    //        public INS SwTyp { get; set; }

    //        [Required]
    //        public INS SwOpCap { get; set; }

    //        public INS MaxOpCap { get; set; }

    //        [Required]
    //        public SPC BlkOpn { get; set; }

    //        [Required]
    //        public SPC BlkCls { get; set; }

    //        public SPC ChaMotEna { get; set; }

    //        [Required]
    //        public SPS Loc { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "YEFN" (Earth fault neutralizer (Petersen coil)).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class YEFN : CommonLogicalNode
    //    {
    //        public YEFN(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.YEFN.ToString();
    //            this.iedType = iedType;
    //            this.ColPos = new APC("ColPos", lnType, iedType);
    //            this.EEName = new DPL("EEName", lnType, iedType);
    //            this.EEHealth = new INS("EEHealth", lnType, iedType);
    //            this.OpTmh = new INS("OpTmh", lnType, iedType);
    //            this.ColTapPos = new ISC("ColTapPos", lnType, iedType);
    //            this.ECA = new MV("ECA", lnType, iedType);
    //            this.Loc = new SPS("Loc", lnType, iedType);
    //        }

    //        public APC ColPos { get; set; }

    //        public DPL EEName { get; set; }

    //        public INS EEHealth { get; set; }

    //        public INS OpTmh { get; set; }

    //        [Required]
    //        public ISC ColTapPos { get; set; }

    //        [Required]
    //        public MV ECA { get; set; }

    //        [Required]
    //        public SPS Loc { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "YLTC" (Tap changer).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class YLTC : CommonLogicalNode
    //    {


    //        public YLTC()
    //        {

    //            this.lnClass = tLNClassEnum.YLTC.ToString();
    //        }


    //        public YLTC(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.YLTC.ToString();
    //            this.iedType = iedType;
    //            this.TapChg = new BSC("TapChg", lnType, iedType);
    //            this.EEName = new DPL("EEName", lnType, iedType);
    //            this.EEHealth = new INS("EEHealth", lnType, iedType);
    //            this.OpCnt = new INS("OpCnt", lnType, iedType);
    //            this.TapPos = new ISC("TapPos", lnType, iedType);
    //            this.Torq = new MV("Torq", lnType, iedType);
    //            this.MotDrvA = new MV("MotDrvA", lnType, iedType);
    //            this.EndPosR = new SPS("EndPosR", lnType, iedType);
    //            this.EndPosL = new SPS("EndPosL", lnType, iedType);
    //            this.OilFil = new SPS("OilFil", lnType, iedType);
    //        }

    //        public BSC TapChg { get; set; }

    //        public DPL EEName { get; set; }

    //        public INS EEHealth { get; set; }

    //        public INS OpCnt { get; set; }

    //        public ISC TapPos { get; set; }

    //        public MV Torq { get; set; }

    //        public MV MotDrvA { get; set; }

    //        [Required]
    //        public SPS EndPosR { get; set; }

    //        [Required]
    //        public SPS EndPosL { get; set; }

    //        public SPS OilFil { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "YPSH" (Power shunt).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class YPSH : CommonLogicalNode
    //    {


    //        public YPSH()
    //        {

    //            this.lnClass = tLNClassEnum.YPSH.ToString();
    //        }
    //        public YPSH(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.YPSH.ToString();
    //            this.iedType = iedType;
    //            this.Pos = new DPC("Pos", lnType, iedType);
    //            this.EEName = new DPL("EEName", lnType, iedType);
    //            this.EEHealth = new INS("EEHealth", lnType, iedType);
    //            this.OpTmh = new INS("OpTmh", lnType, iedType);
    //            this.ShOpCap = new INS("ShOpCap", lnType, iedType);
    //            this.MaxOpCap = new INS("MaxOpCap", lnType, iedType);
    //            this.BlkOpn = new SPC("BlkOpn", lnType, iedType);
    //            this.BlkCls = new SPC("BlkCls", lnType, iedType);
    //            this.ChaMotEna = new SPC("ChaMotEna", lnType, iedType);
    //        }

    //        [Required]
    //        public DPC Pos { get; set; }

    //        public DPL EEName { get; set; }

    //        public INS EEHealth { get; set; }

    //        public INS OpTmh { get; set; }

    //        [Required]
    //        public INS ShOpCap { get; set; }

    //        public INS MaxOpCap { get; set; }

    //        [Required]
    //        public SPC BlkOpn { get; set; }

    //        [Required]
    //        public SPC BlkCls { get; set; }

    //        public SPC ChaMotEna { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "YPTR" (Power transformer).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class YPTR : CommonLogicalNode
    //    {

    //        public YPTR()
    //        {

    //            this.lnClass = tLNClassEnum.YPTR.ToString();
    //        }
    //        public YPTR(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.YPTR.ToString();
    //            this.iedType = iedType;
    //            this.HiVRtg = new ASG("HiVRtg", lnType, iedType);
    //            this.LoVRtg = new ASG("LoVRtg", lnType, iedType);
    //            this.PwrRtg = new ASG("PwrRtg", lnType, iedType);
    //            this.EEName = new DPL("EEName", lnType, iedType);
    //            this.EEHealth = new INS("EEHealth", lnType, iedType);
    //            this.OpTmh = new INS("OpTmh", lnType, iedType);
    //            this.HPTmp = new MV("HPTmp", lnType, iedType);
    //            this.HPTmpAlm = new SPS("HPTmpAlm", lnType, iedType);
    //            this.HPTmpTr = new SPS("HPTmpTr", lnType, iedType);
    //            this.OANL = new SPS("OANL", lnType, iedType);
    //            this.OpOvA = new SPS("OpOvA", lnType, iedType);
    //            this.OpOvV = new SPS("OpOvV", lnType, iedType);
    //            this.OpUnV = new SPS("OpUnV", lnType, iedType);
    //            this.CGAlm = new SPS("CGAlm", lnType, iedType);
    //        }

    //        public ASG HiVRtg { get; set; }

    //        public ASG LoVRtg { get; set; }

    //        public ASG PwrRtg { get; set; }

    //        public DPL EEName { get; set; }

    //        public INS EEHealth { get; set; }

    //        public INS OpTmh { get; set; }

    //        public MV HPTmp { get; set; }

    //        public SPS HPTmpAlm { get; set; }

    //        public SPS HPTmpTr { get; set; }

    //        public SPS OANL { get; set; }

    //        public SPS OpOvA { get; set; }

    //        public SPS OpOvV { get; set; }

    //        public SPS OpUnV { get; set; }

    //        public SPS CGAlm { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "ZAXN" (Auxiliary network).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class ZAXN : CommonLogicalNode
    //    {

    //        public ZAXN()
    //        {

    //            this.lnClass = tLNClassEnum.ZAXN.ToString();
    //        }
    //        public ZAXN(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.ZAXN.ToString();
    //            this.iedType = iedType;
    //            this.EEName = new DPL("EEName", lnType, iedType);
    //            this.EEHealth = new INS("EEHealth", lnType, iedType);
    //            this.OpTmh = new INS("OpTmh", lnType, iedType);
    //            this.Vol = new MV("Vol", lnType, iedType);
    //            this.Amp = new MV("Amp", lnType, iedType);
    //        }

    //        public DPL EEName { get; set; }

    //        public INS EEHealth { get; set; }

    //        public INS OpTmh { get; set; }

    //        public MV Vol { get; set; }

    //        public MV Amp { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "ZBAT" (Battery).
    //    /// </summary>	
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class ZBAT : CommonLogicalNode
    //    {
    //        public ZBAT()
    //        {

    //            this.lnClass = tLNClassEnum.ZBAT.ToString();
    //        }
    //        public ZBAT(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.ZBAT.ToString();
    //            this.iedType = iedType;
    //            this.LoBatVal = new ASG("LoBatVal", lnType, iedType);
    //            this.HiBatVal = new ASG("HiBatVal", lnType, iedType);
    //            this.EEName = new DPL("EEName", lnType, iedType);
    //            this.EEHealth = new INS("EEHealth", lnType, iedType);
    //            this.OpTmh = new INS("OpTmh", lnType, iedType);
    //            this.Vol = new MV("Vol", lnType, iedType);
    //            this.VolChgRte = new MV("VolChgRte", lnType, iedType);
    //            this.Amp = new MV("Amp", lnType, iedType);
    //            this.BatTest = new SPC("BatTest", lnType, iedType);
    //            this.TestRsl = new SPS("TestRsl", lnType, iedType);
    //            this.BatHi = new SPS("BatHi", lnType, iedType);
    //            this.BatLo = new SPS("BatLo", lnType, iedType);
    //        }

    //        public ASG LoBatVal { get; set; }

    //        public ASG HiBatVal { get; set; }

    //        public DPL EEName { get; set; }

    //        public INS EEHealth { get; set; }

    //        public INS OpTmh { get; set; }

    //        [Required]
    //        public MV Vol { get; set; }

    //        public MV VolChgRte { get; set; }

    //        public MV Amp { get; set; }

    //        public SPC BatTest { get; set; }

    //        public SPS TestRsl { get; set; }

    //        public SPS BatHi { get; set; }

    //        public SPS BatLo { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "ZBSH" (Bushing).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class ZBSH : CommonLogicalNode
    //    {
    //        public ZBSH()
    //        {

    //            this.lnClass = tLNClassEnum.ZBSH.ToString();
    //        }
    //        public ZBSH(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.ZBSH.ToString();
    //            this.iedType = iedType;
    //            this.RefReact = new ASG("RefReact", lnType, iedType);
    //            this.RefPF = new ASG("RefPF", lnType, iedType);
    //            this.RefV = new ASG("RefV", lnType, iedType);
    //            this.EEName = new DPL("EEName", lnType, iedType);
    //            this.EEHealth = new INS("EEHealth", lnType, iedType);
    //            this.OpTmh = new INS("OpTmh", lnType, iedType);
    //            this.React = new MV("React", lnType, iedType);
    //            this.LosFact = new MV("LosFact", lnType, iedType);
    //            this.Vol = new MV("Vol", lnType, iedType);
    //        }

    //        public ASG RefReact { get; set; }

    //        public ASG RefPF { get; set; }

    //        public ASG RefV { get; set; }

    //        public DPL EEName { get; set; }

    //        public INS EEHealth { get; set; }

    //        public INS OpTmh { get; set; }

    //        [Required]
    //        public MV React { get; set; }

    //        public MV LosFact { get; set; }

    //        public MV Vol { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "ZCAB" (Power cable).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class ZCAB : CommonLogicalNode
    //    {
    //        public ZCAB()
    //        {

    //            this.lnClass = tLNClassEnum.ZCAB.ToString();
    //        }
    //        public ZCAB(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.ZCAB.ToString();
    //            this.iedType = iedType;
    //            this.EEName = new DPL("EEName", lnType, iedType);
    //            this.EEHealth = new INS("EEHealth", lnType, iedType);
    //            this.OpTmh = new INS("OpTmh", lnType, iedType);
    //        }

    //        public DPL EEName { get; set; }

    //        public INS EEHealth { get; set; }

    //        public INS OpTmh { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "ZCAP" (Capacitor bank).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class ZCAP : CommonLogicalNode
    //    {
    //        public ZCAP()
    //        {

    //            this.lnClass = tLNClassEnum.ZCAP.ToString();
    //        }
    //        public ZCAP(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.ZCAP.ToString();
    //            this.iedType = iedType;
    //            this.EEName = new DPL("EEName", lnType, iedType);
    //            this.EEHealth = new INS("EEHealth", lnType, iedType);
    //            this.OpTmh = new INS("OpTmh", lnType, iedType);
    //            this.CapDS = new SPC("CapDS", lnType, iedType);
    //            this.DschBlk = new SPS("DschBlk", lnType, iedType);
    //        }

    //        public DPL EEName { get; set; }

    //        public INS EEHealth { get; set; }

    //        public INS OpTmh { get; set; }

    //        [Required]
    //        public SPC CapDS { get; set; }

    //        [Required]
    //        public SPS DschBlk { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "ZCON" (Converter).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class ZCON : CommonLogicalNode
    //    {
    //        public ZCON()
    //        {

    //            this.lnClass = tLNClassEnum.ZCON.ToString();
    //        }
    //        public ZCON(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.ZCON.ToString();
    //            this.iedType = iedType;
    //            this.EEName = new DPL("EEName", lnType, iedType);
    //            this.EEHealth = new INS("EEHealth", lnType, iedType);
    //            this.OpTmh = new INS("OpTmh", lnType, iedType);
    //        }

    //        public DPL EEName { get; set; }

    //        public INS EEHealth { get; set; }

    //        public INS OpTmh { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "ZGEN" (Generator).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class ZGEN : CommonLogicalNode
    //    {
    //        public ZGEN()
    //        {

    //            this.lnClass = tLNClassEnum.ZGEN.ToString();
    //        }
    //        public ZGEN(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.ZGEN.ToString();
    //            this.iedType = iedType;
    //            this.DmdPwr = new ASG("DmdPwr", lnType, iedType);
    //            this.PwrRtg = new ASG("PwrRtg", lnType, iedType);
    //            this.VRtg = new ASG("VRtg", lnType, iedType);
    //            this.GnCtl = new DPC("GnCtl", lnType, iedType);
    //            this.EEName = new DPL("EEName", lnType, iedType);
    //            this.EEHealth = new INS("EEHealth", lnType, iedType);
    //            this.OpTmh = new INS("OpTmh", lnType, iedType);
    //            this.GnSt = new INS("GnSt", lnType, iedType);
    //            this.GnSpd = new MV("GnSpd", lnType, iedType);
    //            this.DExt = new SPC("DExt", lnType, iedType);
    //            this.AuxSCO = new SPC("AuxSCO", lnType, iedType);
    //            this.StopVlv = new SPC("StopVlv", lnType, iedType);
    //            this.ReactPwrR = new SPC("ReactPwrR", lnType, iedType);
    //            this.ReactPwrL = new SPC("ReactPwrL", lnType, iedType);
    //            this.OANL = new SPS("OANL", lnType, iedType);
    //            this.ClkRot = new SPS("ClkRot", lnType, iedType);
    //            this.CntClkRot = new SPS("CntClkRot", lnType, iedType);
    //            this.OpUnExt = new SPS("OpUnExt", lnType, iedType);
    //            this.OpOvExt = new SPS("OpOvExt", lnType, iedType);
    //            this.LosOil = new SPS("LosOil", lnType, iedType);
    //            this.LosVac = new SPS("LosVac", lnType, iedType);
    //            this.PresAlm = new SPS("PresAlm", lnType, iedType);
    //        }

    //        public ASG DmdPwr { get; set; }

    //        public ASG PwrRtg { get; set; }

    //        public ASG VRtg { get; set; }

    //        [Required]
    //        public DPC GnCtl { get; set; }

    //        public DPL EEName { get; set; }

    //        public INS EEHealth { get; set; }

    //        public INS OpTmh { get; set; }

    //        [Required]
    //        public INS GnSt { get; set; }

    //        public MV GnSpd { get; set; }

    //        [Required]
    //        public SPC DExt { get; set; }

    //        public SPC AuxSCO { get; set; }

    //        public SPC StopVlv { get; set; }

    //        public SPC ReactPwrR { get; set; }

    //        public SPC ReactPwrL { get; set; }

    //        [Required]
    //        public SPS OANL { get; set; }

    //        [Required]
    //        public SPS ClkRot { get; set; }

    //        [Required]
    //        public SPS CntClkRot { get; set; }

    //        [Required]
    //        public SPS OpUnExt { get; set; }

    //        [Required]
    //        public SPS OpOvExt { get; set; }

    //        public SPS LosOil { get; set; }

    //        public SPS LosVac { get; set; }

    //        public SPS PresAlm { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "ZGIL" (Gas insulated line).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class ZGIL : CommonLogicalNode
    //    {
    //        public ZGIL()
    //        {

    //            this.lnClass = tLNClassEnum.ZGIL.ToString();
    //        }
    //        public ZGIL(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.ZGIL.ToString();
    //            this.iedType = iedType;
    //            this.EEName = new DPL("EEName", lnType, iedType);
    //            this.EEHealth = new INS("EEHealth", lnType, iedType);
    //            this.OpTmh = new INS("OpTmh", lnType, iedType);
    //        }

    //        public DPL EEName { get; set; }

    //        public INS EEHealth { get; set; }

    //        public INS OpTmh { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "ZLIN" (Power overhead line).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class ZLIN : CommonLogicalNode
    //    {
    //        public ZLIN(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.ZLIN.ToString();
    //            this.iedType = iedType;
    //            this.EEName = new DPL("EEName", lnType, iedType);
    //            this.EEHealth = new INS("EEHealth", lnType, iedType);
    //            this.OpTmh = new INS("OpTmh", lnType, iedType);
    //        }

    //        public DPL EEName { get; set; }

    //        public INS EEHealth { get; set; }

    //        public INS OpTmh { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "ZMOT" (Motor).
    //    /// </summary>	
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class ZMOT : CommonLogicalNode
    //    {
    //        public ZMOT()
    //        {

    //            this.lnClass = tLNClassEnum.ZMOT.ToString();
    //        }
    //        public ZMOT(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.ZMOT.ToString();
    //            this.iedType = iedType;
    //            this.EEName = new DPL("EEName", lnType, iedType);
    //            this.EEHealth = new INS("EEHealth", lnType, iedType);
    //            this.OpTmh = new INS("OpTmh", lnType, iedType);
    //            this.DExt = new SPC("DExt", lnType, iedType);
    //            this.LosOil = new SPS("LosOil", lnType, iedType);
    //            this.LosVac = new SPS("LosVac", lnType, iedType);
    //            this.PresAlm = new SPS("PresAlm", lnType, iedType);
    //        }

    //        public DPL EEName { get; set; }

    //        public INS EEHealth { get; set; }

    //        public INS OpTmh { get; set; }

    //        [Required]
    //        public SPC DExt { get; set; }

    //        public SPS LosOil { get; set; }

    //        public SPS LosVac { get; set; }

    //        public SPS PresAlm { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "ZREA" (Reactor).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class ZREA : CommonLogicalNode
    //    {
    //        public ZREA()
    //        {

    //            this.lnClass = tLNClassEnum.ZREA.ToString();
    //        }
    //        public ZREA(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.ZREA.ToString();
    //            this.iedType = iedType;
    //            this.EEName = new DPL("EEName", lnType, iedType);
    //            this.EEHealth = new INS("EEHealth", lnType, iedType);
    //            this.OpTmh = new INS("OpTmh", lnType, iedType);
    //        }

    //        public DPL EEName { get; set; }

    //        public INS EEHealth { get; set; }

    //        public INS OpTmh { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "ZRRC" (Rotating reactive component).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class ZRRC : CommonLogicalNode
    //    {
    //        public ZRRC(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.ZRRC.ToString();
    //            this.iedType = iedType;
    //            this.EEName = new DPL("EEName", lnType, iedType);
    //            this.EEHealth = new INS("EEHealth", lnType, iedType);
    //            this.OpTmh = new INS("OpTmh", lnType, iedType);
    //        }

    //        public DPL EEName { get; set; }

    //        public INS EEHealth { get; set; }

    //        public INS OpTmh { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "ZSAR" (Surge arrestor).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class ZSAR : CommonLogicalNode
    //    {
    //        public ZSAR()
    //        {

    //            this.lnClass = tLNClassEnum.ZSAR.ToString();
    //        }
    //        public ZSAR(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.ZSAR.ToString();
    //            this.iedType = iedType;
    //            this.EEName = new DPL("EEName", lnType, iedType);
    //            this.EEHealth = new INS("EEHealth", lnType, iedType);
    //            this.OpTmh = new INS("OpTmh", lnType, iedType);
    //            this.OPSA = new SPS("OPSA", lnType, iedType);
    //        }

    //        public DPL EEName { get; set; }

    //        public INS EEHealth { get; set; }

    //        public INS OpTmh { get; set; }

    //        [Required]
    //        public SPS OPSA { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "ZTCF" (Thyristor controlled frequency converter).
    //    /// </summary>		
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class ZTCF : CommonLogicalNode
    //    {
    //        public ZTCF()
    //        {

    //            this.lnClass = tLNClassEnum.ZTCF.ToString();
    //        }
    //        public ZTCF(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.ZTCF.ToString();
    //            this.iedType = iedType;
    //            this.PwrFrq = new ASG("PwrFrq", lnType, iedType);
    //            this.EEName = new DPL("EEName", lnType, iedType);
    //            this.EEHealth = new INS("EEHealth", lnType, iedType);
    //            this.OpTmh = new INS("OpTmh", lnType, iedType);
    //        }

    //        public ASG PwrFrq { get; set; }

    //        public DPL EEName { get; set; }

    //        public INS EEHealth { get; set; }

    //        public INS OpTmh { get; set; }
    //    }

    //    /// <summary>
    //    /// This class defines the logical node attributes that are defined for his use in the LN 
    //    /// class "ZTCR" (Thyristor controlled reactive component).
    //    /// </summary>	
    //    /// <remarks>
    //    /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
    //    /// has to be changed from Enum to String.
    //    /// </remarks>
    //    public class ZTCR : CommonLogicalNode
    //    {
    //        public ZTCR()
    //        {

    //            this.lnClass = tLNClassEnum.ZTCR.ToString();
    //        }
    //        public ZTCR(string lnType, string iedType) : base(lnType, iedType)
    //        {
    //            this.id = lnType;
    //            this.lnClass = tLNClassEnum.ZTCR.ToString();
    //            this.iedType = iedType;
    //            this.EEName = new DPL("EEName", lnType, iedType);
    //            this.EEHealth = new INS("EEHealth", lnType, iedType);
    //            this.OpTmh = new INS("OpTmh", lnType, iedType);
    //        }

    //        public DPL EEName { get; set; }

    //        public INS EEHealth { get; set; }

    //        public INS OpTmh { get; set; }
    //    }


    //    public class CBAY : CommonLogicalNode
    //    {

    //        public CBAY() : base()
    //        {
    //            this.lnClass = tLNClassEnum.CBAY.ToString();
    //            BlkCmd = new SPC();
    //            BlkCmd.Oper = new Oper("BlkCmd");
    //            BlkCmd.stVal = new stVal("BlkCmd");

    //            BlkMeas = new SPC();
    //            BlkMeas.Oper = new Oper("BlkMeas");
    //            BlkMeas.stVal = new stVal("BlkMeas");

    //            BlkUpd = new SPC();
    //            BlkUpd.Oper = new Oper("BlkUpd");
    //            BlkUpd.stVal = new stVal("BlkUpd");

    //            LocSwPos = new SPC();
    //            LocSwPos.stVal = new stVal("LocSwPos");

    //            SrcOpPrm = new SPC();
    //            SrcOpPrm.stVal = new stVal("SrcOpPrm");

    //            this.Mod.Oper.bType = tBasicTypeEnum.INT8;
    //        }

    //        [XmlIgnore]
    //        public SPC LocSwPos { get; set; }

    //        [XmlIgnore]
    //        public SPC SrcOpPrm { get; set; }

    //        [XmlIgnore]
    //        public SPC BlkCmd { get; set; }

    //        [XmlIgnore]
    //        public SPC BlkMeas { get; set; }

    //        [XmlIgnore]
    //        public SPC BlkUpd { get; set; }

    //    }



    //    public class RFUF : CommonLogicalNode
    //    {
    //        public RFUF() : base()
    //        {
    //            this.lnClass = tLNClassEnum.RFUF.ToString();
    //        }

    //        [XmlIgnore]
    //        public ACD Str3Ph { get; set; }

    //        [XmlIgnore]
    //        public ACD Str { get; set; }

    //        [XmlIgnore]
    //        public ACD StrRst { get; set; }

    //    }


    //    public class PSOF : CommonLogicalNode
    //    {
    //        public PSOF() : base()
    //        {
    //            this.lnClass = tLNClassEnum.PSOF.ToString();
    //        }

    //        [XmlIgnore]
    //        public ACT Op { get; set; }

    //    }



    //#endregion

}
