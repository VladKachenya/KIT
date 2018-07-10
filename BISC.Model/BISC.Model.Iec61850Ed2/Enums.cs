using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Xml.Serialization;

namespace BISC.Model.Iec61850Ed2
{
	[GeneratedCode("xsd", "2.0.50727.42")]
	[Serializable]
	[XmlType(Namespace="http://www.iec.ch/61850/2003/SCL")]
	public enum tServiceSettingsEnum 
	{		
		Dyn,
		Conf,
		Fix
	}
	
	[GeneratedCode("xsd", "2.0.50727.42")]
	[Serializable]
	[XmlType(Namespace="http://www.iec.ch/61850/2003/SCL")]
	public enum tBasicTypeEnum
	{
	    //tPredefinedBasicTypeEnum
	    BOOLEAN,
	    INT8,
	    INT16,
	    INT24,
	    INT32,
	    INT128,
	    INT8U,
	    INT16U,
	    INT24U,
	    INT64,

	    INT32U,
	    FLOAT32,
	    FLOAT64,
	    Enum,
	    Dbpos,
	    Tcmd,
	    Quality,
	    Timestamp,
	    VisString32,
	    VisString64,
	    VisString255,
	    VisString129,
	    Octet64,
	    Struct,
	    EntryTime,
	    Unicode255,
	    //tExtensionBasicTypeEnum is missing	
	    VisString65,
	    Check,
	    Extension, // Used for custom types
	    bit_string,
	    ObjRef,
	    OptFlds, TrgOps, EntryID, PhyComAddr,
	    //unset value
	    Unset
	}

    public enum tFCEnum
	{
        /** Status information */
        ST = 0,
        /** Measurands - analog values */
        MX = 1,
        /** Setpoint */
        SP = 2,
        /** Substitution */
        SV = 3,
        /** Configuration */
        CF = 4,
        /** Description */
        DC = 5,
        /** Setting group */
        SG = 6,
        /** Setting group editable */
        SE = 7,
        /** Service response / Service tracking */
        SR = 8,
        /** Operate received */
        OR = 9,
        /** Blocking */
        BL = 10,
        /** Extended definition */
        EX = 11,
        /** Control */
        CO = 12,
        /** Unicast SV */
        US = 13,
        /** Multicast SV */
        MS = 14,
        /** Unbuffered report */
        RP = 15,
        /** Buffered report */
        BR = 16,

        /** All FCs - wildcard value */
        ALL = 99,
        NONE = -1
    }

	[GeneratedCode("xsd", "2.0.50727.42")]
	[Serializable]
	[XmlType(Namespace="http://www.iec.ch/61850/2003/SCL")]
	public enum tAssociationKindEnum 
	{		
		[XmlEnum("pre-established")]
		preestablished,
		predefined
	}






	[GeneratedCode("xsd", "2.0.50727.42")]
	[Serializable]
	[XmlType(AnonymousType=true, Namespace="http://www.iec.ch/61850/2003/SCL")]
	public enum tHeaderNameStructure 
	{		
		IEDName,
		FuncName
	}
    
	[GeneratedCode("xsd", "2.0.50727.42")]
	[Serializable]
	[XmlType(Namespace="http://www.iec.ch/61850/2003/SCL")]
	public enum tSIUnitEnum
	{		
		none = 1,
		m = 2,
		kg = 3,
		s = 4,
		A = 5,
		K = 6,
		mol = 7,
		cd = 8,
		deg = 9,
		rad = 10,
		sr = 11,
		Gy = 21,
		q = 22,
		[XmlEnum("°C")]
		C = 23,
		Sv = 24,
		F = 25,
		[XmlEnum("C")]
		C1 = 26,
		
        S = 27,
		H = 28,
		V = 29,
		ohm = 30,
		J = 31,
		N = 32,
		Hz = 33,
		lx = 34,
		Lm = 35,
		Wb = 36,
		T = 37,
		W = 38,
		Pa = 39,
		[XmlEnum("m^2")]
		m2 = 41,
		
		[XmlEnum("m^3")]
		m3 = 42,
		
		[XmlEnum("m/s")]
		ms = 43,
		
		[XmlEnum("m/s^2")]
		ms2 = 44,
		
		[XmlEnum("m^3/s")]
		m3s = 45,
		
		[XmlEnum("m/m^3")]
		mm3 = 46,
		
		M = 47,
		
		[XmlEnum("kg/m^3")]
		kgm3 = 48,
		
		[XmlEnum("m^2/s")]
		m2s = 49,

		[XmlEnum("W/m K")]
		WmK = 50,

		[XmlEnum("J/K")]
		JK = 51,

		ppm = 52,

		[XmlEnum("s^-1")]
		s1 = 53,

		[XmlEnum("rad/s")]
		rads = 54,

		VA = 61,
		Watts = 62,
		VAr = 63,
		phi = 64,
		cos_phi = 65,
		Vs = 66,
		
		[XmlEnum("V^2")]
		V2 = 67,

		As = 68,

		[XmlEnum("A^2")]
		A2 = 69,

		[XmlEnum("A^2 s")]
		A2s = 70,

		VAh = 71,
		Wh = 72,
		VArh = 73,

		[XmlEnum("V/Hz")]
		VHz = 74,

		[XmlEnum("b/s")]
		bs = 75
	}

	[GeneratedCode("xsd", "2.0.50727.42")]
	[Serializable]
	[XmlType(Namespace="http://www.iec.ch/61850/2003/SCL")]
	public enum tUnitMultiplierEnum
	{		
		[XmlEnum("")]
		Item = 0,
		m = -3,
		k = 3,
		M = 6,
		mu = -6,
		y = -24,
		z = -21,
		a = -18,
		f = -15,
		p = -12,
		n = -9,
		c = -2,
		d = -1,
		da = 1,
		h = 2,
		G = 9,
		T = 12,
		P = 15,
		E = 18,
		Z = 21,
		Y = 24
	}

	
	
	[GeneratedCode("xsd", "2.0.50727.42")]
	[Serializable]
	[XmlType(Namespace="http://www.iec.ch/61850/2003/SCL")]
	public enum tValKindEnum 
	{
		Spec,
		Conf,
		RO,
		Set
	}
    public enum orCatEnum
    {
        not_supported=0,
        bay_control,
        station_control,
        remote_control,
        automatic_bay,
        automatic_station,
        automatic_remote,
        maintenance,
        process

    }
	
	[GeneratedCode("xsd", "2.0.50727.42")]
	[Serializable]
	[XmlType(Namespace="http://www.iec.ch/61850/2003/SCL")]
	[Category("GSEControl"), Description("GSE Type")]
	public enum tGSEControlTypeEnum 
	{		
		GSSE,
		GOOSE
	}
	/*
	 * The enumeration "tPredefinedLNClassEnum" was added to fulfill standard IEC 61850 Ed. 1.0.
	 * This is composed by following Enums: tLPHDEnum, tLLN0Enum y tDomainLNEnum
	 * (que incluyen a tDomainLNGroupAEnum, tDomainLNGroupCEnum, tDomainLNGroupGEnum, 
	 * tDomainLNGroupIEnum, tDomainLNGroupMEnum, tDomainLNGroupPEnum, 
	 * tDomainLNGroupREnum, tDomainLNGroupSEnum, tDomainLNGroupTEnum, 
	 * tDomainLNGroupXEnum, tDomainLNGroupYEnum, tDomainLNGroupZEnum)
	*/	
	[Serializable]
	[XmlType(Namespace="http://www.iec.ch/61850/2003/SCL")]	
	public enum tPredefinedLNClassEnum 
	{
		LPHD,		
		LLN0,
		ANCR,ARCO,ATCC,AVCO,
		CILO, CSWI,CALH,CCGR,CPOW,
		GAPC,GGIO,GSAL,
		IHMI,IARC,ITCI,ITMI,
		MMXU,MDIF,MHAI,MHAN,MMTR,MMXN,MSQI,MSTA,
		PDIF,PDIS,PDIR,PDOP,PDUP,PFRC,PHAR,PHIZ,PIOC,PMRI,PMSS,POPF,PPAM,PSCH,PSDE,PTEF,PTOC,PTOF,PTOV,PTRC,PTTR,PTUC,PTUV,PUPF,PTUF,PVOC,PVPH,PZSU,
		RSYN,RDRE,RADR,RBDR,RDRS,RBRF,RDIR,RFLO,RPSB,RREC,
		SARC,SIMG,SIML,SPDC,
		TCTR,TVTR,
		XCBR,XSWI,
		YPTR,YEFN,YLTC,YPSH,
		ZAXN,ZBAT,ZBSH,ZCAB,ZCAP,ZCON,ZGEN,ZGIL,ZLIN,ZMOT,ZREA,ZRRC,ZSAR,ZTCF,ZTCR
	}
	
	/* 
	 * The enumeration "tLNClassEnum" was added to fulfill standard IEC 61850 Ed. 1.0.
	 * This is composed by following Enums: tPredefinedLNClassEnum y tExtensionLNClassEnum	 	 
	 *
	 * To add a Logical Node personalized and defined by the enum "Custom" was added.
	*/	
	public enum tLNClassEnum
	{
		LPHD,		
		LLN0,
		ANCR,ARCO,ATCC,AVCO,
		CILO, CSWI,CALH,CCGR,CPOW,
		GAPC,GGIO,GSAL,
		IHMI,IARC,ITCI,ITMI,
		MMXU,MDIF,MHAI,MHAN,MMTR,MMXN,MSQI,MSTA,
		PDIF,PDIS,PDIR,PDOP,PDUP,PFRC,PHAR,PHIZ,PIOC,PMRI,PMSS,POPF,PPAM,PSCH,PSDE,PTEF,PTOC,PTOF,PTOV,PTRC,PTTR,PTUC,PTUV,PUPF,PTUF,PVOC,PVPH,PZSU,
		RSYN,RDRE,RADR,RBDR,RDRS,RBRF,RDIR,RFLO,RPSB,RREC,
		SARC,SIMG,SIML,SPDC,
		TCTR,TVTR,
		XCBR,XSWI,
		YPTR,YEFN,YLTC,YPSH,
		ZAXN,ZBAT,ZBSH,ZCAB,ZCAP,ZCON,ZGEN,ZGIL,ZLIN,ZMOT,ZREA,ZRRC,ZSAR,ZTCF,ZTCR,
	PDPR,
        CBAY,RFUF,PSOF,  //новые
		Custom
	}	
	
	/* 
	 * The enumeration "tAttributeNameEnum" was added to fulfill standard IEC 61850 Ed. 1.0.
	 * This is composed by following Enums: tPredefinedAttributeNameEnum tExtensionAttributeNameEnum
	 *
	 * The value "Check" was deleted because it was redundant.
	*/	
	public enum tAttributeNameEnum
	{
		T,		
		Test,		
		Check,
		SIUnit,
		Oper,
		SBO,
		SBOw,
		Cancel,			
		ctlVal,
		operTm,
		origin,
		ctlNum,
		stVal,
		q,
		t,
		stSeld,
		subEna,
		subVal,
		subQ,
		subID,
		ctlModel,
		sboTimeout,
		sboClass,
		minVal,
		maxVal,
		stepSize,
		d,
		dU,
		cdcNs,
		cdcName,
		dataNs
	}

    public enum ctlModelsEnum
    {
        status_only, direct_with_normal_security, sbo_with_normal_security, sbo_with_enhanced_security, direct_with_enhanced_security
    }

    public enum sboClassEnum
    {
        operate_once, operate_many
    }

    public enum dirEnum
    {
        unknown, forward, backward,both
    }
    public enum dirPhsEnum
    {
        unknown, forward, backward
    }

    /// <summary>
    /// Enum of phsRef
    /// </summary>
    public enum phsRefEnum
    {
        A, B, C
    }

    /// <summary>
    /// Enum of range
    /// </summary>
    public enum rangeEnum
    {
        normal, high, low, high_high, low_low
    }

    /// <summary>
    /// Enum of seqT
    /// </summary>
    public enum seqTEnum
    {
        pos_neg_zero,
        dir_quad_zero
    }

    /// <summary>
    /// Enum of sev
    /// </summary>	
    public enum sevEnum
    {
        unknown, critical, major, minor, warning
    }


    /// <summary>
    /// Enum of ctlVal
    /// </summary>
    public enum ctlValEnum
    {
        stop, lower, higher, reserved
    }

    /// <summary>
    /// Enum of val
    /// </summary>
    public enum valEnum
    {
        intermediate_state, off, on, bad_state
    }
    public enum modEnum
    {
        on=1, blocked, test, test_blocked,off
    }
    public enum healthEnum
    {
        Ok=1, Warning, Alarm
    }

    public enum autoRecStEnum
    {
        Ready = 1,
        In_Progress,
        Successful,
        Waiting_for_trip,
        Trip_issued_by_protection,
        Fault_disappeared,
        Wait_to_complete,
        Circuit_breaker_closed,
        Cycle_unsuccessful,
        Unsuccessful,
        Aborted
    }

    public enum fltLoopEnum
    {
        PhaseAtoGround=1, PhaseBtoGround, PhaseCtoGround, PhaseAtoB, PhaseBtoC, PhaseCtoA, Others
    }
     public enum CBOpCapEnum
    {
        None=1, Open, Close_Open, Open_Close_Open, Close_Open_Close_Open
    }
    public enum behEnum
    {
        on=1, blocked, test, test_blocked, off
    }

    public enum dbPosEnum
    {
        intermediate,
        off,
        on,
        bad
    }

    /*
	 * The enumeration "tPredefinedGeneralEquipmentEnum" was added to fulfill standard IEC 61850 Ed. 1.0.	 
	*/
    public enum tPredefinedGeneralEquipmentEnum
	{
		AXN,		
		BAT,		
		MOT	
	}
	
	/* 
	 * The enumeration "tGeneralEquipmentEnum" was added to fulfill standard IEC 61850 Ed. 1.0.
	 * This is composed by following Enums: tPredefinedGeneralEquipmentEnum tExtensionGeneralEquipmentEnum
	*/	
	public enum tGeneralEquipmentEnum
	{
		//tPredefinedGeneralEquipmentEnum
		AXN,		
		BAT,		
		MOT			
		// Missing:
		// tExtensionGeneralEquipmentEnum
	}
	
	/*
	 * The enumeration "tPredefinedCommonConductingEquipmentEnum" was added to fulfill standard IEC 61850 Ed. 1.0.	 
	*/		
	public enum tPredefinedCommonConductingEquipmentEnum
	{
		CBR,		
		DIS,		
		VTR,	
		CTR,
		GEN,
		CAP,
		REA,
		CON,
		MOT,
		EFN,
		PSH,
		BAT,
		BSH,
		CAB,
		GIL,
		LIN,
		RRC,
		SAR,
		TCF,
		TCR,
		IFL
	}
	
	/* 
	 * The enumeration "tCommonConductingEquipmentEnum" was added to fulfill standard IEC 61850 Ed. 1.0.
	 * This is composed by following Enums: tPredefinedCommonConductingEquipmentEnum tExtensionEquipmentEnum
	*/	
	public enum tCommonConductingEquipmentEnum
	{
		CBR,		
		DIS,		
		VTR,	
		CTR,
		GEN,
		CAP,
		REA,
		CON,
		MOT,
		EFN,
		PSH,
		BAT,
		BSH,
		CAB,
		GIL,
		LIN,
		RRC,
		SAR,
		TCF,
		TCR,
		IFL
	}
	
	/*
	 * The enumeration "tPredefinedCDCEnum" was added to fulfill standard IEC 61850 Ed. 1.0.	 
	*/		
	public enum tPredefinedCDCEnum
	{
		SPS,
		DPS,
		INS,
		ACT,
		ACD,
		SEC,
		BCR,
		MV,
		CMV,
		SAV,
		WYE,
		DEL,
		SEQ,
		HMV,
		HWYE,
		HDEL,
		SPC,
		DPC,
		INC,
		BSC,
		ISC,
		APC,
		SPG,
		ING,
		ASG,
		CURVE,
		DPL,
		LPL,
		CSD	,
        RSS		
	}
	
	/* 
	 * The enumeration "tCDCEnumEd1" was added to fulfill standard IEC 61850 Ed. 1.0.
	 * This is composed by the following Enums: tPredefinedCDCEnum and tExtensionCDCEnum
	*/	
	public enum tCDCEnumEd1
	{
		//tPredefinedCDCEnum
		SPS,
		DPS,
		INS,
		ACT,
		ACD,
		SEC,
		BCR,
		MV,
		CMV,ENC,
        ENS,
		SAV,
		WYE,
		DEL,
		SEQ,
		HMV,
		HWYE,
		HDEL,
		SPC,
		DPC,
		INC,
		BSC,
		ISC,
		APC,
		SPG,
		ING,
		ASG,
		CURVE,
		DPL,
		LPL,
		CSD,
        RSS
        //FIXME : tExtensionCDCEnum is missing
    }
    public enum tCDCEnumEd2
    {
        SEC,
        CTS,CST,UTS,BTS,LTS,GTS, STS,VSS,TSG,//не добавлены
        ENC,
        ENS,
        LPL,
        SPS,
        SPC,
        ENG,
        ORG,
        ING,
        INS,
        SPG,
        DPL,
        INC,
        ACD,
        ACT,
        CSD,
        ASG,
        DPC,
        MV,
        APC,
        BCR,
        CSG,
        CMV,
        WYE,
        DEL,
        SEQ,
        CURVE,
        BSC,
        ISC,
        BAC
    }
    /*
	 * The enumeration "tPredefinedBasicTypeEnum" was added to fulfill standard IEC 61850 Ed. 1.0.	 
	*/
    public enum  tPredefinedBasicTypeEnum 
	{
		BOOLEAN,		
		INT8,
		INT16,
		INT24,
		INT32,
		INT128,
		INT8U,
		INT16U,
		INT24U,
		INT32U,
		FLOAT32,
		FLOAT64,
		Enum,
		Dbpos,
		Tcmd,
		Quality,
		Timestamp,
		VisString32,
		VisString64,
		VisString129,
		VisString255,
		Octet64,
		Struct,
		EntryTime,
		Unicode255,
		Check
	}
	
	


	[GeneratedCode("xsd", "2.0.50727.42")]
	[Serializable]
	[XmlType(Namespace="http://www.iec.ch/61850/2003/SCL")]
	public enum tTransformerWindingEnum 
	{		
		PTW
	}
	
	[GeneratedCode("xsd", "2.0.50727.42")]
	[Serializable]
	[XmlType(Namespace="http://www.iec.ch/61850/2003/SCL")]
	public enum tPhaseEnum 
	{		
		A,
		B,
		C,
		N,
		all,
		none
	}
	
	[GeneratedCode("xsd", "2.0.50727.42")]
	[Serializable]
	[XmlType(Namespace="http://www.iec.ch/61850/2003/SCL")]
	public enum tPowerTransformerEnum 
	{
		PTR
	}

}

