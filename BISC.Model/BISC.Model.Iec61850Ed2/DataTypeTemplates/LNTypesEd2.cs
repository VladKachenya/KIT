using System.ComponentModel;
using System.Xml.Serialization;
using BISC.Model.Iec61850Ed2.Common;

namespace BISC.Model.Iec61850Ed2.DataTypeTemplates
{
    /// <summary>
    /// Description of LNTypes.
    /// </summary>	
    public class DOData : tDOType
    {
        public bool CheckSelection = false;
        public bool Visible = true;

        public DOData(string name, string type)
        {
            this.name = name;
            this.type = type;
        }

        public DOData()
        {

        }
        [XmlIgnore]
        public Oper Oper { get; set; }

        [ReadOnly(true)]
        public string name { get; set; }

        [ReadOnly(true)]
        public string type { get; set; }

  

    }

    public class LNTypesEd2
    {


        /// <summary>
        /// This class defines the logical node attributes that are defined for his use in the LN 
        /// class "LLN0" (Logical node zero).
        /// </summary>		
        /// <remarks>
        /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
        /// has to be changed from Enum to String.
        /// </remarks>
        public class LLN0 : CommonLogicalNode
        {

            public LLN0()
            {
                this.lnClass = tLNClassEnum.LLN0.ToString();
            }

            [XmlIgnore]
            public CDCTypesEd2.INS OpTmh { get; set; }

            [XmlIgnore]
            public CDCTypesEd2.SPS LocKey { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPS Loc { get; set; }

            [XmlIgnore]
            public CDCTypesEd2.SPC LocSta { get; set; }

            [XmlIgnore]
            public CDCTypesEd2.SPC Diag { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPC LEDRs { get; set; }


            [XmlIgnore]
            public CDCTypesEd2.ORG GrRef { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPG MltLev { get; set; }
            [XmlIgnore]
            public SGCB SGCB { get; set; }
        }

        public class LPHD : CommonLogicalNode
        {
            public LPHD()
            {
                this.lnClass = tLNClassEnum.LPHD.ToString();
            }
            [Required, XmlIgnore]
            public CDCTypesEd2.DPL PhyNam { get; set; }

            [Required, XmlIgnore]
            public CDCTypesEd2.ENS PhyHealth { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPS OutOv { get; set; }
            [Required, XmlIgnore]
            public CDCTypesEd2.SPS Proxy { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPS InOv { get; set; }

            [XmlIgnore]
            public CDCTypesEd2.INS NumPwrUp { get; set; }

            [XmlIgnore]
            public CDCTypesEd2.INS WrmStr { get; set; }

            [XmlIgnore]
            public CDCTypesEd2.INS WacTrg { get; set; }

            [XmlIgnore]
            public CDCTypesEd2.SPS PwrUp { get; set; }

            [XmlIgnore]
            public CDCTypesEd2.SPS PwrDn { get; set; }

            [XmlIgnore]
            public CDCTypesEd2.SPS PwrSupAlm { get; set; }

            [XmlIgnore]
            public CDCTypesEd2.SPC RsStat { get; set; }

        }


        public class RDRE : CommonLogicalNode
        {
            public RDRE()
            {
                this.lnClass = tLNClassEnum.RDRE.ToString();
            }



            [Required, XmlIgnore]
            public CDCTypesEd2.SPS RcdMade { get; set; }
            [Required, XmlIgnore]
            public CDCTypesEd2.INS FltNum { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.INS GriFltNum { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPS RcdStr { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.INS MemUsed { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPC RcdTrg { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPC MemRs { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPC MemClr { get; set; }

            [XmlIgnore]
            public CDCTypesEd2.INC OpCntRs { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ENG TrgMod { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ENG LevMod { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ING PreTmms { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ING PstTmms { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ING MemFull { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ING MaxNumRcd { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPG ReTrgMod { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ING PerTrgTms { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ING ExclTmms { get; set; }

            [XmlIgnore]
            public CDCTypesEd2.ENG RcdMod { get; set; }

            [XmlIgnore]
            public CDCTypesEd2.ING StoRte { get; set; }

            [XmlIgnore]
            public CDCTypesEd2.ING OpMod { get; set; }

        }

        public class PTOC : CommonLogicalNode
        {
            public PTOC()
            {
                this.lnClass = tLNClassEnum.PTOC.ToString();
            }

            [Required, XmlIgnore]
            public CDCTypesEd2.ACD Str { get; set; }

            [Required, XmlIgnore]
            public CDCTypesEd2.ACT Op { get; set; }

            [XmlIgnore]
            public CDCTypesEd2.CURVE TmACrv { get; set; }


            [XmlIgnore]
            public CDCTypesEd2.ASG StrVal { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ASG TmMult { get; set; }

            [XmlIgnore]
            public CDCTypesEd2.CSD TmAChr33 { get; set; }

            [XmlIgnore]
            public CDCTypesEd2.CSD TmASt { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.INC OpCntRs { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ENG DirMod { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ING MaxOpTmms { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ING MinOpTmms { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ING OpDlTmms { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ING RsDlTmms { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ENG TypRsCrv { get; set; }
        }


        public class GGIO : CommonLogicalNode
        {

            public GGIO()
            {
                this.lnClass = tLNClassEnum.GGIO.ToString();
            }

            [XmlIgnore]
            public CDCTypesEd2.DPL EEName { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ENS EEHealth { get; set; }

            [XmlIgnore]
            public CDCTypesEd2.SPS Loc { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPS LocKey { get; set; }

            [XmlIgnore]
            public CDCTypesEd2.INS IntIn1 { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPS Alm1 { get; set; }


            [XmlIgnore]
            public CDCTypesEd2.SPS Wrn1 { get; set; }






            [XmlIgnore]
            public CDCTypesEd2.DPC DPCSO1 { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.DPC DPCSO2 { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.DPC DPCSO3 { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.DPC DPCSO4 { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.DPC DPCSO5 { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.DPC DPCSO6 { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.INC ISCSO { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.INC OpCntRs { get; set; }

            [XmlIgnore]
            public CDCTypesEd2.INS IntIn { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.MV AnIn1 { get; set; }


            [XmlIgnore]
            public CDCTypesEd2.APC AnOut1 { get; set; }

            [XmlIgnore]
            public CDCTypesEd2.BCR CntRs1 { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPC SPCSO { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPS Alm { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPS Ind { get; set; }


            [XmlIgnore]
            public CDCTypesEd2.SPS Ind1 { get; set; }

            [XmlIgnore]
            public CDCTypesEd2.SPS Ind2 { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPS Ind3 { get; set; }

            [XmlIgnore]
            public CDCTypesEd2.SPS Ind4 { get; set; }

            [XmlIgnore]
            public CDCTypesEd2.SPS Ind5 { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPS Ind6 { get; set; }

            [XmlIgnore]
            public CDCTypesEd2.SPS Ind7 { get; set; }

            [XmlIgnore]
            public CDCTypesEd2.SPS Ind8 { get; set; }

            public CDCTypesEd2.SPS Ind9 { get; set; }

            [XmlIgnore]
            public CDCTypesEd2.SPS Ind10 { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPS Ind11 { get; set; }

            [XmlIgnore]
            public CDCTypesEd2.SPS Ind12 { get; set; }

            [XmlIgnore]
            public CDCTypesEd2.SPS Ind13 { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPS Ind14 { get; set; }

            [XmlIgnore]
            public CDCTypesEd2.SPS Ind15 { get; set; }

            [XmlIgnore]
            public CDCTypesEd2.SPS Ind16 { get; set; }

            public CDCTypesEd2.SPS Ind17 { get; set; }

            [XmlIgnore]
            public CDCTypesEd2.SPS Ind18 { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPS Ind19 { get; set; }

            [XmlIgnore]
            public CDCTypesEd2.SPS Ind20 { get; set; }

            [XmlIgnore]
            public CDCTypesEd2.SPS Ind21 { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPS Ind22 { get; set; }

            [XmlIgnore]
            public CDCTypesEd2.SPS Ind23 { get; set; }

            [XmlIgnore]
            public CDCTypesEd2.SPS Ind24 { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPS GrInd { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPC SPCSO1 { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPC SPCSO2 { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPC SPCSO3 { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPC SPCSO4 { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPC SPCSO5 { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPC SPCSO6 { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPC SPCSO7 { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPC SPCSO8 { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPC SPCSO9 { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPC SPCS10 { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPC SPCS11 { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPC SPCS12 { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPC ISCSO1 { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPC ISCSO2 { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPC ISCSO3 { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPC ISCSO4 { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPC ISCSO5 { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPC ISCSO6 { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPC ISCSO7 { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPC ISCSO8 { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPC ISCSO9 { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPC ISCS10 { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPC ISCS11 { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPC ISCS12 { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPC Out1 { get; set; }

            [XmlIgnore]
            public CDCTypesEd2.SPC Out2 { get; set; }

            [XmlIgnore]
            public CDCTypesEd2.SPC Out3 { get; set; }

            [XmlIgnore]
            public CDCTypesEd2.SPC Out4 { get; set; }

            [XmlIgnore]
            public CDCTypesEd2.SPC Out5 { get; set; }

            [XmlIgnore]
            public CDCTypesEd2.SPC Out6 { get; set; }


        }



        public class PTOV : CommonLogicalNode
        {
            public PTOV()
            {
                lnClass = tLNClassEnum.PTOV.ToString();
            }

            [Required]
            public CDCTypesEd2.ACD Str { get; set; }

            [Required]
            public CDCTypesEd2.CSG TmVChr33 { get; set; }

            [Required]
            public CDCTypesEd2.ACT Op { get; set; }

            public CDCTypesEd2.ASG StrVal { get; set; }

            public CDCTypesEd2.ASG TmMult { get; set; }

            public CDCTypesEd2.CSD TmVSt { get; set; }

            public CDCTypesEd2.CURVE TmVCrv { get; set; }

            public CDCTypesEd2.INC OpCntRs { get; set; }

            public CDCTypesEd2.ING MaxOpTmms { get; set; }

            public CDCTypesEd2.ING MinOpTmms { get; set; }

            public CDCTypesEd2.ING OpDlTmms { get; set; }

            public CDCTypesEd2.ING RsDlTmms { get; set; }
        }





        public class PTOF : CommonLogicalNode
        {
            public PTOF()
            {
                lnClass = tLNClassEnum.PTOF.ToString();
            }

            [Required]
            public CDCTypesEd2.ACD Str { get; set; }

            [Required]
            public CDCTypesEd2.ACT Op { get; set; }

            public CDCTypesEd2.ASG BlkVal { get; set; }

            public CDCTypesEd2.ASG StrVal { get; set; }

            public CDCTypesEd2.INC OpCntRs { get; set; }

            public CDCTypesEd2.ING OpDlTmms { get; set; }

            public CDCTypesEd2.ING RsDlTmms { get; set; }

            public CDCTypesEd2.SPS BlkV { get; set; }
        }


        public class PTUF : CommonLogicalNode
        {
            public PTUF()
            {
                lnClass = tLNClassEnum.PTUF.ToString();
            }

            [Required]
            public CDCTypesEd2.ACD Str { get; set; }

            [Required]
            public CDCTypesEd2.ACT Op { get; set; }

            public CDCTypesEd2.ASG StrVal { get; set; }

            public CDCTypesEd2.ASG BlkVal { get; set; }

            public CDCTypesEd2.INC OpCntRs { get; set; }

            public CDCTypesEd2.ING OpDlTmms { get; set; }

            public CDCTypesEd2.ING RsDlTmms { get; set; }

            public CDCTypesEd2.SPS BlkV { get; set; }
        }


        public class PDUP : CommonLogicalNode
        {
            public PDUP()
            {
                this.lnClass = tLNClassEnum.PDUP.ToString();
            }

            public CDCTypesEd2.ACD Str { get; set; }

            public CDCTypesEd2.ACT Op { get; set; }

            public CDCTypesEd2.ASG StrVal { get; set; }

            public CDCTypesEd2.INC OpCntRs { get; set; }

            public CDCTypesEd2.ING OpDlTmms { get; set; }

            public CDCTypesEd2.ING RsDlTmms { get; set; }

            public CDCTypesEd2.ENG DirMod { get; set; }
        }


        public class PTTR : CommonLogicalNode
        {
            public PTTR()
            {
                this.lnClass = tLNClassEnum.PTTR.ToString();
            }

            public CDCTypesEd2.ACD Str { get; set; }

            public CDCTypesEd2.SPS AlmThm { get; set; }

            public CDCTypesEd2.SPS BlkThm { get; set; }


            public CDCTypesEd2.ACT Op { get; set; }

            public CDCTypesEd2.ASG AlmVal { get; set; }
            public CDCTypesEd2.ASG DropoutVal { get; set; }

            public CDCTypesEd2.ASG StrVal { get; set; }

            public CDCTypesEd2.ASG TmpMax { get; set; }

            public CDCTypesEd2.CSD TmASt { get; set; }

            public CDCTypesEd2.CSD TmTmpSt { get; set; }

            public CDCTypesEd2.CURVE TmACrv { get; set; }

            public CDCTypesEd2.CURVE TmTmpCrv { get; set; }
            public CDCTypesEd2.CSG TmAChr33 { get; set; }

            public CDCTypesEd2.INC OpCntRs { get; set; }

            public CDCTypesEd2.ING ConsTms { get; set; }

            public CDCTypesEd2.ING MaxOpTmms { get; set; }

            public CDCTypesEd2.ING MinOpTmms { get; set; }

            public CDCTypesEd2.ING OpDlTmms { get; set; }

            public CDCTypesEd2.ING RsDlTmms { get; set; }

            public CDCTypesEd2.MV AgeRat { get; set; }

            public CDCTypesEd2.MV Amp { get; set; }

            public CDCTypesEd2.MV LodRsvAlm { get; set; }

            public CDCTypesEd2.MV LodRsvTr { get; set; }

            public CDCTypesEd2.MV Tmp { get; set; }

            public CDCTypesEd2.MV TmpRl { get; set; }

        }




        public class RREC : CommonLogicalNode
        {
            public RREC()
            {
                this.lnClass = tLNClassEnum.RREC.ToString();

            }
            [Required, XmlIgnore]
            public CDCTypesEd2.ENS TrBeh { get; set; }
            [Required, XmlIgnore]
            public CDCTypesEd2.INS RecCyc { get; set; }

            [Required, XmlIgnore]
            public CDCTypesEd2.ACT OpCls { get; set; }
            public CDCTypesEd2.ENG CycTrMod1 { get; set; }

            [XmlIgnore]
            public CDCTypesEd2.ING MaxCyc { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ING UseCyc { get; set; }


            [Required, XmlIgnore]

            public CDCTypesEd2.ACT Op { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.INC OpCntRs { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ING Rec1Tmms1 { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ING Rec2Tmms1 { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ING Rec3Tmms1 { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ING Rec13Tmms1 { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ING PlsTmms { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ING RclTmms { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ING RdyTmms { get; set; }
            [Required, XmlIgnore]
            public CDCTypesEd2.ENS AutoRecSt { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPC BlkRec { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPC ChkRec { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPS Auto { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPS EnaRec { get; set; }
        }

        /// <summary>
        /// This class defines the logical node attributes that are defined for his use in the LN 
        /// class "RSYN" (Synchronism-check or synchronising).
        /// </summary>		
        /// <remarks>
        /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
        /// has to be changed from Enum to String.
        /// </remarks>
        public class RSYN : CommonLogicalNode
        {
            public RSYN()
            {
                this.lnClass = tLNClassEnum.RSYN.ToString();
            }


            [XmlIgnore]
            public CDCTypesEd2.ASG DifV { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ASG DifHz { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ASG DifAng { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ASG DeaLinVal { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ASG LivLinVal { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ASG DeaBusVal { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ASG LivBusVal { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ENG LivDeaMod { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ING PlsTmms { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ING BkrTmms { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.MV DifVClc { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.MV DifHzClc { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.MV DifAngClc { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPC RHz { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPC LHz { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPC RV { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPC LV { get; set; }

            [Required, XmlIgnore]
            public CDCTypesEd2.SPS Rel { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPS VInd { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPS AngInd { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPS HzInd { get; set; }

            [XmlIgnore]
            public CDCTypesEd2.SPS EnOk { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPS FailSyn { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPS TestSCOK { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ACT Op { get; set; }
            public CDCTypesEd2.SPC SynPrg { get; set; }
            public CDCTypesEd2.ING TotTmms { get; set; }

        }


        public class RBRF : CommonLogicalNode
        {
            public RBRF()
            {
                this.lnClass = tLNClassEnum.RBRF.ToString();
            }


            [XmlIgnore]
            public CDCTypesEd2.ACD Str { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ACT OpEx { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ACT OpEx2 { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ACT OpIn { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ASG DetValA { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.INC OpCntRs { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ENG FailMod { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ING FailTmms { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ING SPlTrTmms { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ING TPTrTmms { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ENG ReTrMod { get; set; }
        }



        public class RPSB : CommonLogicalNode
        {
            public RPSB()
            {
                this.lnClass = tLNClassEnum.RPSB.ToString();
            }
            [XmlIgnore]
            public CDCTypesEd2.ACD Str { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ACT Op { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ASG SwgVal { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ASG SwgRis { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ASG SwgReact { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.INC OpCntRs { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ING SwgTmms { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ING UnBlkTmms { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ING MaxNumSlp { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ING EvTmms { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPG ZeroEna { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPG NgEna { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPG MaxEna { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPS BlkZn { get; set; }
        }




        public class PDIS : CommonLogicalNode
        {
            public PDIS()
            {
                this.lnClass = tLNClassEnum.PDIS.ToString();
            }
            
            [Required, XmlIgnore]
            public CDCTypesEd2.ACD Str { get; set; }

            [Required, XmlIgnore]
            public CDCTypesEd2.ACT Op { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ASG PoRch { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ASG PhStr { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ASG GndStr { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ASG PctRch { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ASG Ofs { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ASG PctOfs { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ASG RisLod { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ASG AngLod { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ASG X1 { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ASG LinAng { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ASG RisGndRch { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ASG RisPhRch { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ASG K0Fact { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ASG K0FactAng { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.INC OpCntRs { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ING DirMod { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ING OpDlTmms { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ING PhDlTmms { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ING GndDlTmms { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ING RsDlTmms { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPG TmDlMod { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPG PhDlMod { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPG GndDlMod { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.INS StrCndZ { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.INS StrNDir { get; set; }
            public CDCTypesEd2.SPS StrPP { get; set; }
            public CDCTypesEd2.SPS StrPh { get; set; }
            public CDCTypesEd2.SPS OpPh { get; set; }
            public CDCTypesEd2.SPS OpPP { get; set; }

            public CDCTypesEd2.SPS OpZ1 { get; set; }
            public CDCTypesEd2.SPS OpZ2 { get; set; }
            public CDCTypesEd2.SPS StrZ1 { get; set; }
            public CDCTypesEd2.SPS StrZ2 { get; set; }
            public CDCTypesEd2.MV PriRis { get; set; }
            public CDCTypesEd2.MV PriReact { get; set; }


        }





        public class CSWI : CommonLogicalNode
        {
          

            public CSWI() 
            {
                this.lnClass = tLNClassEnum.CSWI.ToString();
            }
            [XmlIgnore]
            public CDCTypesEd2.ACT OpCls { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ACT OpOpn { get; set; }

            [Required, XmlIgnore]
            public CDCTypesEd2.DPC Pos { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.DPC PosA { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.DPC PosB { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.DPC PosC { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.INC OpCntRs { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPS Loc { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPC BlkCmd { get; set; }
        }



        public class XCBR : CommonLogicalNode
        {
    

            public XCBR()
            {
                this.lnClass = tLNClassEnum.XCBR.ToString();
                Pos = new CDCTypesEd2.DPC();
                Pos.stVal = new stVal();

                Pos.stVal.bType = tBasicTypeEnum.Dbpos;
            }
            [XmlIgnore]
            public CDCTypesEd2.BCR SumSwARs { get; set; }

            [Required, XmlIgnore]
            public CDCTypesEd2.DPC Pos { get; set; }

            [XmlIgnore]
            public CDCTypesEd2.DPL EEName { get; set; }

            [XmlIgnore]
            public CDCTypesEd2.ENS EEHealth { get; set; }

            [Required, XmlIgnore]
            public CDCTypesEd2.INS OpCnt { get; set; }

            [Required, XmlIgnore]
            public CDCTypesEd2.ENC CBOpCap { get; set; }

            [XmlIgnore]
            public CDCTypesEd2.ENS POWCap { get; set; }

            [XmlIgnore]
            public CDCTypesEd2.INS MaxOpCap { get; set; }

            [Required, XmlIgnore]
            public CDCTypesEd2.SPC BlkOpn { get; set; }

            [Required, XmlIgnore]
            public CDCTypesEd2.SPC BlkCls { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPC ChaMotEna { get; set; }

            [Required, XmlIgnore]
            public CDCTypesEd2.SPS Loc { get; set; }

            [Required, XmlIgnore]
            public CDCTypesEd2.SPS LocKey { get; set; }
            [Required, XmlIgnore]
            public CDCTypesEd2.SPS BlkUpd { get; set; }
            [Required, XmlIgnore]
            public CDCTypesEd2.SPS Dsc { get; set; }
            [Required, XmlIgnore]
            public CDCTypesEd2.ING CBTmms { get; set; }
        }
        public class PTRC : CommonLogicalNode
        {
            public PTRC()
            {
                this.lnClass = tLNClassEnum.CCGR.ToString();
            }
            
            [XmlIgnore]
            public CDCTypesEd2.ACD StrSOF { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ACT OpSOF { get; set; }

            [XmlIgnore]
            public CDCTypesEd2.ACD Str { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ACT Op { get; set; }

            [XmlIgnore]
            public CDCTypesEd2.ACT Tr { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.INC OpCntRs { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ENG TrMod { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ING TrPlsTmms { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPS TwoPTr { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPS SPTr { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPS TPTr { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPC LORs { get; set; }
        }




        public class MMXU : CommonLogicalNode
        {
            public MMXU()
            {
                this.lnClass = tLNClassEnum.MMXU.ToString();
            }


            [XmlIgnore]
            public CDCTypesEd2.DEL PPV { get; set; }

            [XmlIgnore]
            public CDCTypesEd2.INS EEHealth { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.MV TotW { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.MV TotVAr { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.MV Amp { get; set; }
            public CDCTypesEd2.MV Vol { get; set; }
            public CDCTypesEd2.MV TotVA { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.MV TotPF { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.MV Hz { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.WYE phV { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.WYE PhV { get { return phV; } set { phV = value; } }

            [XmlIgnore]
            public CDCTypesEd2.WYE A { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.WYE W { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.WYE VAr { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.WYE VA { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.WYE PF { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.WYE Z { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPS Ald { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SPS Alg { get; set; }
        }




        public class MSQI : CommonLogicalNode
        {
            public MSQI()
            {
                this.lnClass = tLNClassEnum.MSQI.ToString();
            }

            [XmlIgnore]
            public CDCTypesEd2.DPL EEName { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.INS EEHealth { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.MV ImbNgA { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.MV ImbNgV { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.MV ImbZroA { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.MV ImbZroV { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.MV MaxImbA { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.MV MaxImbPPV { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.MV MaxImbV { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SEQ SeqA { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SEQ SeqV { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SEQ SeqU { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SEQ SeqT { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.SEQ DQ0Seq { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.WYE ImbA { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.WYE ImbV { get; set; }

        }

        public class RFLO : CommonLogicalNode
        {
            public RFLO()
            {
                this.lnClass = tLNClassEnum.RFLO.ToString();
            }

            [XmlIgnore]
            public CDCTypesEd2.ASG LinLenKm { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ASG R1 { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ASG X1 { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ASG R0 { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ASG X0 { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ASG Z1Mod { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ASG Z1Ang { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ASG Z0Mod { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ASG Z0Ang { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ASG Rm0 { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ASG Xm0 { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ASG Zm0Mod { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ASG Zm0Ang { get; set; }

            [XmlIgnore]
            public CDCTypesEd2.CMV FltZ { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.CMV Fltz { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.INC OpCntRs { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.ENS FltLoop { get; set; }
            [XmlIgnore]
            public CDCTypesEd2.MV FltDiskm { get; set; }

            [XmlIgnore]
            public CDCTypesEd2.SPS ClcFlt { get; set; }
        }
        /// <summary>
        /// This class defines the logical node attributes that are defined for his use in the LN 
        /// class "PTUC" (Undercurrent).
        /// </summary>		
        /// <remarks>
        /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
        /// has to be changed from Enum to String.
        /// </remarks>
        public class PTUC : CommonLogicalNode
        {

            public PTUC()
            {
                this.lnClass = tLNClassEnum.PTUC.ToString();

            }


            [Required]
            public CDCTypesEd2.ACD Str { get; set; }

            [Required]
            public CDCTypesEd2.ACT Op { get; set; }

            public CDCTypesEd2.ASG StrVal { get; set; }

            public CDCTypesEd2.ASG TmMult { get; set; }

            public CDCTypesEd2.CSD TmASt { get; set; }

            public CDCTypesEd2.CURVE TmACrv { get; set; }

            public CDCTypesEd2.INC OpCntRs { get; set; }

            public CDCTypesEd2.ING OpDlTmms { get; set; }

            public CDCTypesEd2.ING MinOpTmms { get; set; }

            public CDCTypesEd2.ING MaxOpTmms { get; set; }

            public CDCTypesEd2.ENG TypRsCrv { get; set; }

            public CDCTypesEd2.ING RsDlTmms { get; set; }

            public CDCTypesEd2.ING DirMod { get; set; }
        }






        public class PTUV : CommonLogicalNode
        {
            public PTUV()
            {

                this.lnClass = tLNClassEnum.PTUV.ToString();
            }

            [Required]
            public CDCTypesEd2.ACD Str { get; set; }

            [Required]
            public CDCTypesEd2.ACT Op { get; set; }

            public CDCTypesEd2.ASG StrVal { get; set; }

            public CDCTypesEd2.ASG TmMult { get; set; }

            public CDCTypesEd2.CSD TmVSt { get; set; }

            public CDCTypesEd2.CURVE TmVCrv { get; set; }

            public CDCTypesEd2.INC OpCntRs { get; set; }

            public CDCTypesEd2.ING MinOpTmms { get; set; }

            public CDCTypesEd2.ING MaxOpTmms { get; set; }

            public CDCTypesEd2.ING OpDlTmms { get; set; }

            public CDCTypesEd2.ING RsDlTmms { get; set; }
        }

        /// <summary>
        /// This class defines the logical node attributes that are defined for his use in the LN 
        /// class "PUPF" (Underpower factor).
        /// </summary>		
        /// <remarks>
        /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
        /// has to be changed from Enum to String.
        /// </remarks>
        public class PUPF : CommonLogicalNode
        {

            public PUPF()
            {
                this.lnClass = tLNClassEnum.PUPF.ToString();

            }

            [Required]
            public CDCTypesEd2.ACD Str { get; set; }

            [Required]
            public CDCTypesEd2.ACT Op { get; set; }

            public CDCTypesEd2.ASG StrVal { get; set; }

            public CDCTypesEd2.ASG BlkValA { get; set; }

            public CDCTypesEd2.ASG BlkValV { get; set; }

            public CDCTypesEd2.INC OpCntRs { get; set; }

            public CDCTypesEd2.ING OpDlTmms { get; set; }

            public CDCTypesEd2.ING RsDlTmms { get; set; }

            public CDCTypesEd2.SPS BlkA { get; set; }

            public CDCTypesEd2.SPS BlkV { get; set; }
        }






        /// <summary>
        /// This class defines the logical node attributes that are defined for his use in the LN 
        /// class "PDIF" (Differential).
        /// </summary>		
        /// <remarks>
        /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
        /// has to be changed from Enum to String.
        /// </remarks>
        public class PDIF : CommonLogicalNode
        {
            public PDIF()
            {
                this.lnClass = tLNClassEnum.PDIF.ToString();
            }

          
            public CDCTypesEd2.ACD Str { get; set; }

            [Required]
            public CDCTypesEd2.ACT Op { get; set; }

            public CDCTypesEd2.ASG LinCapac { get; set; }

            public CDCTypesEd2.CSD TmASt { get; set; }

            public CDCTypesEd2.CURVE TmACrv { get; set; }

            public CDCTypesEd2.INC OpCntRs { get; set; }

            public CDCTypesEd2.ING LoSet { get; set; }

            public CDCTypesEd2.ING HiSet { get; set; }

            public CDCTypesEd2.ING MinOpTmms { get; set; }

            public CDCTypesEd2.ING MaxOpTmms { get; set; }

            public CDCTypesEd2.ENG RstMod { get; set; }

            public CDCTypesEd2.ING RsDlTmms { get; set; }

            public CDCTypesEd2.WYE DifACIc { get; set; }

            public CDCTypesEd2.WYE RstA { get; set; }
        }

        /// <summary>
        /// This class defines the logical node attributes that are defined for his use in the LN 
        /// class "MMXN" (Metering).
        /// </summary>		
        /// <remarks>
        /// To accept any Logical Node without shows an error on the tree, the type of lnClass attribute 
        /// has to be changed from Enum to String.
        /// </remarks>
        public class MMXN : CommonLogicalNode
        {
            public MMXN()
            {
                this.lnClass = tLNClassEnum.MMXN.ToString();
            }
       
            public CDCTypesEd2.CMV Imp { get; set; }

            public CDCTypesEd2.DPL EEName { get; set; }

            public CDCTypesEd2.INS EEHealth { get; set; }

            public CDCTypesEd2.MV Amp { get; set; }

            public CDCTypesEd2.MV Vol { get; set; }

            public CDCTypesEd2.MV Watt { get; set; }

            public CDCTypesEd2.MV VolAmpr { get; set; }

            public CDCTypesEd2.MV VolAmp { get; set; }

            public CDCTypesEd2.MV PwrFact { get; set; }

            public CDCTypesEd2.MV Hz { get; set; }
        }


    public class ATCC : CommonLogicalNode
    {
        private CDCTypesEd2.MV HiDmdAField;
        public ATCC()
        {
            this.lnClass = tLNClassEnum.ATCC.ToString();
        }


        public CDCTypesEd2.SPS Loc { get; set; }
        public CDCTypesEd2.SPS LocKey { get; set; }


        public CDCTypesEd2.INS HiTapPos { get; set; }

        public CDCTypesEd2.INS LoTapPos { get; set; }

        public CDCTypesEd2.SPS TapOpR { get; set; }
        public CDCTypesEd2.SPS TapOpL { get; set; }

        public CDCTypesEd2.SPS TapOpStop { get; set; }
        public CDCTypesEd2.SPS TapOpErr { get; set; }
        public CDCTypesEd2.SPC LTCBlk { get; set; }
        public CDCTypesEd2.SPC LTCDragRs { get; set; }

        public CDCTypesEd2.SPC LTCBlkVLo { get; set; }
        public CDCTypesEd2.SPC LTCBlkVHi { get; set; }


        public CDCTypesEd2.SPC LTCBlkAHi { get; set; }
        public CDCTypesEd2.SPC ErrPar { get; set; }
        public CDCTypesEd2.SPC EndPosL { get; set; }
        public CDCTypesEd2.SPC EndPosR { get; set; }

        public CDCTypesEd2.MV CtlV { get; set; }

        public CDCTypesEd2.MV CircA { get; set; }

        public CDCTypesEd2.MV LodA { get; set; }

        public CDCTypesEd2.MV PhAng { get; set; }


        public CDCTypesEd2.MV HiCtlV { get; set; }

        public CDCTypesEd2.MV HiDmdA { get; set; }

        public CDCTypesEd2.MV LoCtlV { get; set; }


        public CDCTypesEd2.INC OpCntRs { get; set; }

        public CDCTypesEd2.SPC LocSta { get; set; }
        public CDCTypesEd2.BAC BndCtrChg { get; set; }

        public CDCTypesEd2.ASG BlkLV { get; set; }

        public CDCTypesEd2.ASG BlkRV { get; set; }

        public CDCTypesEd2.ASG BndCtr { get; set; }

        public CDCTypesEd2.ASG BndWid { get; set; }


        public CDCTypesEd2.ASG BlkVLo { get; set; }

        public CDCTypesEd2.ASG BlkVHi { get; set; }

        public CDCTypesEd2.ENG ParTraMod { get; set; }



        public CDCTypesEd2.ASG LDCR { get; set; }

        public CDCTypesEd2.ASG LDCX { get; set; }

        public CDCTypesEd2.ASG LDCZ { get; set; }

        public CDCTypesEd2.ASG LimLodA { get; set; }

        public CDCTypesEd2.ASG RnbkRV { get; set; }

        public CDCTypesEd2.ASG VRedVal { get; set; }

        public CDCTypesEd2.BSC TapChg { get; set; }


        public CDCTypesEd2.SPC ParOp { get; set; }

     


        public CDCTypesEd2.ING CtlDlTmms { get; set; }

        public CDCTypesEd2.ING TapBlkL { get; set; }

        public CDCTypesEd2.ING TapBlkR { get; set; }



        public CDCTypesEd2.ISC TapPos { get; set; }

   





 

        public CDCTypesEd2.SPC VRed1 { get; set; }

        public CDCTypesEd2.SPC VRed2 { get; set; }

        public CDCTypesEd2.SPG LDC { get; set; }

        public CDCTypesEd2.SPG TmDlChr { get; set; }

        public CDCTypesEd2.SPS Auto { get; set; }

       
    }
    }



}