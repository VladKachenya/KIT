using System.ComponentModel;
using BISC.Model.Iec61850Ed2.Common;

namespace BISC.Model.Iec61850Ed2.DataTypeTemplates
{
    public class CDCTypesEd2
    {


        public class ENC : DOData
        {
            public ENC()
            {
                cdc = tCDCEnumEd2.ENC;
            }


            public Originator origin { get; set; }

            public INT8U ctlNum { get; set; }

            public ENUMERATED stVal { get; set; }

            public Quality q { get; set; }

            public TimeStamp t { get; set; }

            public BOOLEAN stSeld { get; set; }

            public BOOLEAN opRcvd { get; set; }

            public BOOLEAN opOk { get; set; }

            public TimeStamp tOpOk { get; set; }

            public BOOLEAN subEna { get; set; }

            public ENUMERATED subVal { get; set; }

            public Quality subQ { get; set; }

            public VisString64 subID { get; set; }

            public BOOLEAN blkEna { get; set; }

            public ctlModels ctlModel { get; set; }

            public INT32U sboTimeout { get; set; }

            public SboClasses sboClass { get; set; }

            public INT32U operTimeout { get; set; }

            public VisString255 d { get; set; }
            public Unicode255 dU { get; set; }


            public VisString255 cdcNs { get; set; }
            public VisString255 cdcName { get; set; }
            public VisString255 dataNs { get; set; }


            public ENUMERATED ctlVal { get; set; }


        }


        public class ENS : DOData
        {
            public ENS()
            {
                cdc = tCDCEnumEd2.ENC;

            }

            public ENUMERATED stVal { get; set; }
            public Quality q { get; set; }
            public TimeStamp t { get; set; }
            public BOOLEAN subEna { get; set; }
            public ENUMERATED subVal { get; set; }
            public Quality subQ { get; set; }
            public VisString64 subID { get; set; }
            public BOOLEAN blkEna { get; set; }
            public VisString255 d { get; set; }
            public Unicode255 dU { get; set; }
            public VisString255 cdcNs { get; set; }
            public VisString255 cdcName { get; set; }
            public VisString255 dataNs { get; set; }
        }

        public class LPL : DOData
        {
            public LPL()
            {
                cdc = tCDCEnumEd2.LPL;
            }

            public VisString255 vendor { get; set; }
            public VisString255 swRev { get; set; }
            public VisString255 d { get; set; }
            public Unicode255 dU { get; set; }
            public VisString255 configRev { get; set; }
            public INT32 paramRev { get; set; }
            public INT32 valRev { get; set; }
            public VisString255 ldNs { get; set; }
            public VisString255 lnNs { get; set; }
            public VisString255 cdcNs { get; set; }
            public VisString255 cdcName { get; set; }
            public VisString255 dataNs { get; set; }

        }


        public class SPS : DOData
        {
            public SPS()
            {
                cdc = tCDCEnumEd2.SPS;
            }

            public BOOLEAN stVal { get; set; }
            public Quality q { get; set; }
            public TimeStamp t { get; set; }
            public BOOLEAN subEna { get; set; }
            public BOOLEAN subVal { get; set; }
            public Quality subQ { get; set; }
            public VisString64 subID { get; set; }
            public BOOLEAN blkEna { get; set; }
            public VisString255 d { get; set; }
            public Unicode255 dU { get; set; }
            public VisString255 cdcNs { get; set; }
            public VisString255 cdcName { get; set; }
            public VisString255 dataNs { get; set; }

        }

        public class SPC : DOData
        {
            public SPC()
            {
                cdc = tCDCEnumEd2.SPC;
            }

            public Originator origin { get; set; }
            public INT8U ctlNum { get; set; }
            public BOOLEAN stVal { get; set; }
            public Quality q { get; set; }
            public TimeStamp t { get; set; }
            public BOOLEAN stSeld { get; set; }
            public BOOLEAN opRcvd { get; set; }
            public BOOLEAN opOk { get; set; }
            public TimeStamp tOpOk { get; set; }
            public BOOLEAN subEna { get; set; }
            public BOOLEAN subVal { get; set; }
            public Quality subQ { get; set; }
            public VisString64 subID { get; set; }
            public BOOLEAN blkEna { get; set; }
            public PulseConfig pulseConfig { get; set; }
            public ctlModels ctlModel { get; set; }
            public INT32U sboTimeout { get; set; }
            public SboClasses sboClass { get; set; }
            public INT32U operTimeout { get; set; }
            public VisString255 d { get; set; }
            public Unicode255 dU { get; set; }
            public VisString255 cdcNs { get; set; }
            public VisString255 cdcName { get; set; }
            public VisString255 dataNs { get; set; }
            public BOOLEAN ctlVal { get; set; }

        }
        public class BAC : DOData
        {
            public BAC()
            {
                cdc = tCDCEnumEd2.SPC;
            }

            public Originator origin { get; set; }
            public INT8U ctlNum { get; set; }
            public BOOLEAN stVal { get; set; }
            public Quality q { get; set; }
            public TimeStamp t { get; set; }
            public BOOLEAN stSeld { get; set; }
            public BOOLEAN opRcvd { get; set; }
            public BOOLEAN opOk { get; set; }
            public TimeStamp tOpOk { get; set; }
            public BOOLEAN subEna { get; set; }
            public BOOLEAN subVal { get; set; }
            public Quality subQ { get; set; }
            public VisString64 subID { get; set; }
            public BOOLEAN blkEna { get; set; }

            public BOOLEAN persistent { get; set; }

            public ctlModels ctlModel { get; set; }
            public INT32U sboTimeout { get; set; }
            public SboClasses sboClass { get; set; }


            public Unit units { get; set; }

            public INT32U dB { get; set; }

            public ScaledValueConfig sVC { get; set; }


            public AnalogueValue minVal { get; set; }

            public AnalogueValue maxVal { get; set; }
            public AnalogueValue stepSize { get; set; }

            public INT32U operTimeout { get; set; }
            public VisString255 d { get; set; }
            public Unicode255 dU { get; set; }
            public VisString255 cdcNs { get; set; }
            public VisString255 cdcName { get; set; }
            public VisString255 dataNs { get; set; }
            public BOOLEAN ctlVal { get; set; }

        }
        public class SPG : DOData
        {
            public SPG()
            {
                cdc = tCDCEnumEd2.SPC;
            }

            public BOOLEAN stVal { get; set; }
            public VisString255 d { get; set; }
            public Unicode255 dU { get; set; }
            public VisString255 cdcNs { get; set; }
            public VisString255 cdcName { get; set; }
            public VisString255 dataNs { get; set; }

        }



        public class ENG : DOData
        {
            public ENG()
            {
                cdc = tCDCEnumEd2.ENG;
            }

            public ENUMERATED setVal { get; set; }

            public VisString255 d { get; set; }
            public Unicode255 dU { get; set; }
            public VisString255 cdcNs { get; set; }
            public VisString255 cdcName { get; set; }
            public VisString255 dataNs { get; set; }

        }



        public class ING : DOData
        {
            public ING()
            {
                cdc = tCDCEnumEd2.ING;
            }

            public INT32 setVal { get; set; }

            public INT32 minVal { get; set; }
            public INT32 maxVal { get; set; }
            public INT32U stepSize { get; set; }
            public Unit units { get; set; }
            public VisString255 d { get; set; }
            public Unicode255 dU { get; set; }
            public VisString255 cdcNs { get; set; }
            public VisString255 cdcName { get; set; }
            public VisString255 dataNs { get; set; }

        }


        public class INS : DOData
        {
            public INS()
            {
                cdc = tCDCEnumEd2.INS;
            }

            public INT32 stVal { get; set; }
            public Quality q { get; set; }
            public TimeStamp t { get; set; }
            public BOOLEAN subEna { get; set; }
            public BOOLEAN subVal { get; set; }
            public Quality subQ { get; set; }
            public VisString64 subID { get; set; }
            public BOOLEAN blkEna { get; set; }

            public Unit units { get; set; }
            public VisString255 d { get; set; }
            public Unicode255 dU { get; set; }
            public VisString255 cdcNs { get; set; }
            public VisString255 cdcName { get; set; }
            public VisString255 dataNs { get; set; }

        }


        public class ORG : DOData
        {
            public ORG()
            {
                cdc = tCDCEnumEd2.ORG;
            }

            public ObjectReference setSrcRef { get; set; }
            public ObjectReference setTstRef { get; set; }
            public ObjectReference setSrcCB { get; set; }
            public ObjectReference setTstCB { get; set; }
            public VisString255 intAddr { get; set; }
            public BOOLEAN tstEna { get; set; }

            public VisString255 purpose { get; set; }
            public VisString255 d { get; set; }
            public Unicode255 dU { get; set; }
            public VisString255 cdcNs { get; set; }
            public VisString255 cdcName { get; set; }
            public VisString255 dataNs { get; set; }

        }


        public class INC : DOData
        {
            public INC()
            {
                cdc = tCDCEnumEd2.INC;
            }

            public Originator origin { get; set; }
            public INT8U ctlNum { get; set; }
            public BOOLEAN stVal { get; set; }
            public Quality q { get; set; }
            public TimeStamp t { get; set; }
            public BOOLEAN stSeld { get; set; }
            public BOOLEAN opRcvd { get; set; }
            public BOOLEAN opOk { get; set; }
            public TimeStamp tOpOk { get; set; }
            public BOOLEAN subEna { get; set; }
            public BOOLEAN subVal { get; set; }
            public Quality subQ { get; set; }
            public VisString64 subID { get; set; }
            public BOOLEAN blkEna { get; set; }

            public ctlModels ctlModel { get; set; }

            public INT32U sboTimeout { get; set; }

            public SboClasses sboClass { get; set; }

            public INT32 minVal { get; set; }
            public INT32 maxVal { get; set; }
            public INT32U stepSize { get; set; }
            public INT32U operTimeout { get; set; }
            public Unit units { get; set; }

            public VisString255 d { get; set; }
            public Unicode255 dU { get; set; }
            public VisString255 cdcNs { get; set; }
            public VisString255 cdcName { get; set; }
            public VisString255 dataNs { get; set; }

        }







        public class DPL : DOData
        {
            public DPL()
            {
                cdc = tCDCEnumEd2.DPL;
            }

            public VisString255 vendor { get; set; }
            public VisString255 hwRev { get; set; }
            public VisString255 swRev { get; set; }
            public VisString255 serNum { get; set; }
            public VisString255 model { get; set; }
            public VisString255 location { get; set; }
            public VisString255 name { get; set; }
            public VisString255 owner { get; set; }
            public VisString255 ePSName { get; set; }
            public VisString255 primeOper { get; set; }
            public VisString255 secondOper { get; set; }
            public FLOAT32 latitude { get; set; }
            public FLOAT32 longitude { get; set; }
            public FLOAT32 altitude { get; set; }
            public VisString255 mRID { get; set; }
            public VisString255 d { get; set; }
            public Unicode255 dU { get; set; }
            public VisString255 cdcNs { get; set; }
            public VisString255 cdcName { get; set; }
            public VisString255 dataNs { get; set; }

        }



        public class MV : DOData
        {
            public MV()
            {
                cdc = tCDCEnumEd2.MV;
            }

            public AnalogueValue instMag { get; set; }
            public AnalogueValue mag { get; set; }
            public range range { get; set; }
            public Quality q { get; set; }
            public TimeStamp t { get; set; }
            public BOOLEAN subEna { get; set; }
            public BOOLEAN subVal { get; set; }
            public Quality subQ { get; set; }
            public VisString64 subID { get; set; }
            public BOOLEAN blkEna { get; set; }
            public Unit units { get; set; }
            public INT32U db { get; set; }
            public INT32U zeroDb { get; set; }
            public ScaledValueConfig sVC { get; set; }
            public RangeConfig rangeC { get; set; }

            public INT32U smpRate { get; set; }

            public VisString255 d { get; set; }
            public Unicode255 dU { get; set; }
            public VisString255 cdcNs { get; set; }
            public VisString255 cdcName { get; set; }
            public VisString255 dataNs { get; set; }

        }




        public class ACD : DOData
        {
            public ACD()
            {
                cdc = tCDCEnumEd2.ACD;
            }


            public BOOLEAN general { get; set; }
            public dirGeneral dirGeneral { get; set; }
            public BOOLEAN phsA { get; set; }
            public dirPhs dirPhsA { get; set; }
            public BOOLEAN phsB { get; set; }
            public dirPhs dirPhsB { get; set; }
            public BOOLEAN phsC { get; set; }
            public dirPhs dirPhsC { get; set; }
            public BOOLEAN neut { get; set; }
            public dirPhs dirNeut { get; set; }
            public Quality q { get; set; }
            public TimeStamp t { get; set; }
            public VisString255 d { get; set; }
            public Unicode255 dU { get; set; }
            public VisString255 cdcNs { get; set; }
            public VisString255 cdcName { get; set; }
            public VisString255 dataNs { get; set; }

        }


        public class ACT : DOData
        {
            public ACT()
            {
                cdc = tCDCEnumEd2.ACT;
            }


            public BOOLEAN general { get; set; }
            public BOOLEAN phsA { get; set; }
            public BOOLEAN phsB { get; set; }
            public BOOLEAN phsC { get; set; }
            public BOOLEAN neut { get; set; }
            public Quality q { get; set; }
            public TimeStamp t { get; set; }
            public Originator originSrc { get; set; }
            public TimeStamp operTmPhsA { get; set; }
            public TimeStamp operTmPhsB { get; set; }
            public TimeStamp operTmPhsC { get; set; }

            public VisString255 d { get; set; }
            public Unicode255 dU { get; set; }
            public VisString255 cdcNs { get; set; }
            public VisString255 cdcName { get; set; }
            public VisString255 dataNs { get; set; }

        }

        public class ASG : DOData
        {
            public ASG()
            {
                cdc = tCDCEnumEd2.ASG;
            }

            public VisString255 cdcName { get; set; }

            public VisString255 cdcNs { get; set; }

            public VisString255 d { get; set; }
            public VisString255 dataNs { get; set; }

            public Unicode255 dU { get; set; }

            public AnalogueValue maxVal { get; set; }

            public AnalogueValue minVal { get; set; }

            [Browsable(false)]
            public AnalogueValue setMag { get; set; }

            public Unit units { get; set; }

            public AnalogueValue stepSize { get; set; }

            public ScaledValueConfig sVC { get; set; }


        }






        public class CSD : DOData
        {

            public CSD()
            {
                cdc = tCDCEnumEd2.CSD;
            }

            public VisString255 cdcName { get; set; }


            public VisString255 cdcNs { get; set; }

            public crvPts crvPts { get; set; }


            public VisString255 d { get; set; }


            public VisString255 dataNs { get; set; }

            public Unicode255 dU { get; set; }

            public VisString255 xD { get; set; }
            public VisString255 xDU { get; set; }

            public Unit xUnits { get; set; }


            public VisString255 yD { get; set; }

            public VisString255 yDU { get; set; }

            public Unit yUnits { get; set; }

            public VisString255 zD { get; set; }

            public VisString255 zDU { get; set; }

            public Unit zUnits { get; set; }
            public INT16U numPts { get; set; }

        }







        public class CURVE : DOData
        {

            public CURVE()
            {
                cdc = tCDCEnumEd2.CURVE;
            }
            public ENUMERATED setCharact { get; set; }
            public ENUMERATED setCharact2 { get; set; }




            public FLOAT32 setParA { get; set; }


            public FLOAT32 setParA2 { get; set; }


            public FLOAT32 setParB { get; set; }


            public FLOAT32 setParB2 { get; set; }


            public FLOAT32 setParC { get; set; }


            public FLOAT32 setParC2 { get; set; }


            public FLOAT32 setParD { get; set; }


            public FLOAT32 setParD2 { get; set; }


            public FLOAT32 setParE { get; set; }


            public FLOAT32 setParE2 { get; set; }

            public FLOAT32 setParF { get; set; }

            public FLOAT32 setParF2 { get; set; }



            public VisString255 cdcName { get; set; }


            public VisString255 cdcNs { get; set; }


            public VisString255 d { get; set; }

            public VisString255 dataNs { get; set; }


            public Unicode255 dU { get; set; }
        }





        public class CSG : DOData
        {

            public CSG()
            {
                cdc = tCDCEnumEd2.CSG;
            }

            public FLOAT32 pointZ { get; set; }

            public crvPts crvPts { get; set; }





            public VisString255 xD { get; set; }
            public VisString255 xDU { get; set; }

            public Unit xUnits { get; set; }


            public VisString255 yD { get; set; }

            public VisString255 yDU { get; set; }

            public Unit yUnits { get; set; }

            public VisString255 zD { get; set; }

            public VisString255 zDU { get; set; }

            public Unit zUnits { get; set; }
            public INT16U numPts { get; set; }



            public VisString255 cdcName { get; set; }


            public VisString255 cdcNs { get; set; }


            public VisString255 d { get; set; }

            public VisString255 dataNs { get; set; }


            public Unicode255 dU { get; set; }
        }






        public class DPC : DOData
        {
            public DPC()
            {
                cdc = tCDCEnumEd2.DPC;
            }
            public Quality q { get; set; }
            public TimeStamp t { get; set; }

            public VisString255 cdcName { get; set; }

            public VisString255 cdcNs { get; set; }

            public INT8U ctlNum { get; set; }

            public BOOLEAN ctlVal { get; set; }

            public VisString255 d { get; set; }

            public VisString255 dataNs { get; set; }

            public Unicode255 dU { get; set; }

            public stVal stVal { get; set; }

            public BOOLEAN stSeld { get; set; }

            public BOOLEAN opRcvd { get; set; }

            public BOOLEAN opOk { get; set; }

            public TimeStamp tOpOk { get; set; }

            public BOOLEAN subEna { get; set; }

            public ENUMERATED subVal { get; set; }

            public Quality subQ { get; set; }

            public VisString64 subID { get; set; }

            public BOOLEAN blkEna { get; set; }

            public ctlModels ctlModel { get; set; }

            public INT32U sboTimeout { get; set; }

            public SboClasses sboClass { get; set; }

            public INT32U operTimeout { get; set; }

        }


        public class APC : DOData
        {
            public APC()
            {
                cdc = tCDCEnumEd2.APC;
            }
            public Originator origin { get; set; }

            public INT8U ctlNum { get; set; }

            public AnalogueValue mxVal { get; set; }

            public Quality q { get; set; }

            public TimeStamp t { get; set; }

            public BOOLEAN stSeld { get; set; }

            public BOOLEAN opRcvd { get; set; }

            public BOOLEAN opOk { get; set; }

            public TimeStamp tOpOk { get; set; }

            public BOOLEAN subEna { get; set; }

            public AnalogueValue subVal { get; set; }

            public Quality subQ { get; set; }

            public VisString64 subID { get; set; }

            public BOOLEAN blkEna { get; set; }

            public ctlModels ctlModel { get; set; }

            public INT32U sboTimeout { get; set; }

            public SboClasses sboClass { get; set; }
            public Unit units { get; set; }

            public INT32U db { get; set; }
            public ScaledValueConfig sVC { get; set; }
            public AnalogueValue minVal { get; set; }
            public AnalogueValue maxVal { get; set; }
            public AnalogueValue stepSize { get; set; }
            public INT32U operTimeout { get; set; }
            public VisString255 d { get; set; }
            public Unicode255 dU { get; set; }


            public VisString255 cdcNs { get; set; }
            public VisString255 cdcName { get; set; }
            public VisString255 dataNs { get; set; }


            public ENUMERATED ctlVal { get; set; }
        }









        public class BCR : DOData
        {
            public BCR()
            {
                cdc = tCDCEnumEd2.BCR;
            }
            public INT64 actVal { get; set; }

            public INT64 frVal { get; set; }

            public TimeStamp frTm { get; set; }

            public Quality q { get; set; }

            public TimeStamp t { get; set; }



            public BOOLEAN units { get; set; }

            public FLOAT32 pulsQty { get; set; }

            public BOOLEAN frEna { get; set; }

            public TimeStamp strTm { get; set; }

            public INT32 frPd { get; set; }

            public BOOLEAN frRs { get; set; }

            public VisString255 d { get; set; }
            public Unicode255 dU { get; set; }


            public VisString255 cdcNs { get; set; }
            public VisString255 cdcName { get; set; }
            public VisString255 dataNs { get; set; }


            public ENUMERATED ctlVal { get; set; }
        }









        public class WYE : DOData
        {


            public WYE()
            {
                cdc = tCDCEnumEd2.WYE;


            }
            [Browsable(false)]
            public angRef angRef { get; set; }

            [Browsable(false)]
            public VisString255 cdcName { get; set; }

            [Browsable(false)]
            public VisString255 cdcNs { get; set; }

            [Browsable(false)]
            public VisString255 d { get; set; }

            [Browsable(false)]
            public VisString255 dataNs { get; set; }

            [Browsable(false)]
            public Unicode255 dU { get; set; }

            [Browsable(false)]
            public CMV net { get; set; }

            [Browsable(false)]
            public CMV neut { get; set; }
            [Browsable(false)]
            public CMV neut1 { get; set; }
            [Browsable(false)]
            public CMV neut2 { get; set; }
            [Browsable(false)]
            public CMV neut3 { get; set; }
            [Browsable(false)]
            public CMV neut4 { get; set; }
            [Browsable(false)]
            public CMV neut5 { get; set; }
            [Browsable(false)]
            public CMV neut6 { get; set; }
            [Browsable(false)]
            public CMV neut7 { get; set; }

            [Browsable(false)]
            public CMV phsA { get; set; }

            [Browsable(false)]
            public CMV phsB { get; set; }

            [Browsable(false)]
            public CMV phsC { get; set; }

            [Browsable(false)]
            public CMV res { get; set; }

            public Quality q { get; set; }
            public TimeStamp t { get; set; }
            public INT32 db { get; set; }
            public INT32 zeroDb { get; set; }

        }
        public class CMV : DOData
        {

            public CMV()
            {
                cdc = tCDCEnumEd2.CMV;
            }

            [Browsable(false)]
            public angRef angRef { get; set; }

            [Browsable(false)]
            public angSVC angSVC { get; set; }

            [Browsable(false)]
            public VisString255 cdcName { get; set; }

            [Browsable(false)]
            public VisString255 cdcNs { get; set; }

            [Required]
            [Browsable(false)]
            public cVal cVal { get; set; }

            [Browsable(false)]
            public VisString255 d { get; set; }

            [Browsable(false)]
            public VisString255 dataNs { get; set; }

            [Browsable(false)]
            public INT32U db { get; set; }

            [Browsable(false)]
            public Unicode255 dU { get; set; }

            [Browsable(false)]
            public instCVal instCVal { get; set; }

            [Browsable(false)]
            public magSVC magSVC { get; set; }

            [Required]
            [Browsable(false)]
            public Quality q { get; set; }

            [Browsable(false)]
            public range range { get; set; }

            [Browsable(false)]
            public rangeC rangeC { get; set; }

            [Browsable(false)]
            public INT32U smpRate { get; set; }

            [Browsable(false)]
            public subCVal subCVal { get; set; }

            [Browsable(false)]
            public BOOLEAN subEna { get; set; }

            [Browsable(false)]
            public VisString64 subID { get; set; }

            [Browsable(false)]
            public Quality subQ { get; set; }

            [Required]
            [Browsable(false)]
            public TimeStamp t { get; set; }

            [Browsable(false)]
            public Unit units { get; set; }

            [Browsable(false)]
            public INT32U zeroDb { get; set; }
        }



        public class DEL : DOData
        {
            /// <summary>
            /// Phase to phase related measured values of a three phase system (DEL)
            /// </summary>
            /// <param name="name">
            /// A name identifying the DO.
            /// </param>
            /// <param name="lnType">
            /// Containing more detailed functional specification about LN.
            /// </param>
            /// <param name="iedType">
            /// The manufacturer IED type of the IED to which this LN type belongs.
            /// </param>


            public DEL()
            {
                cdc = tCDCEnumEd2.DEL;
            }
            [Browsable(false)]
            public angRef angRef { get; set; }

            [Browsable(false)]
            public VisString255 cdcName { get; set; }

            [Browsable(false)]
            public VisString255 cdcNs { get; set; }

            [Browsable(false)]
            public VisString255 d { get; set; }

            [Browsable(false)]
            public VisString255 dataNs { get; set; }

            [Browsable(false)]
            public Unicode255 dU { get; set; }

            [Browsable(false)]
            public CMV phsAB { get; set; }

            [Browsable(false)]
            public CMV phsBC { get; set; }

            [Browsable(false)]
            public CMV phsCA { get; set; }
        }




        public class SEQ : DOData
        {

            public SEQ()
            {
                cdc = tCDCEnumEd2.SEQ;
            }
            [Required]
            [Browsable(false)]
            public CMV c1 { get; set; }

            [Required]
            [Browsable(false)]
            public CMV c2 { get; set; }

            [Required]
            [Browsable(false)]
            public CMV c3 { get; set; }

            [Browsable(false)]
            public VisString255 cdcName { get; set; }

            [Browsable(false)]
            public VisString255 cdcNs { get; set; }

            [Browsable(false)]
            public VisString255 d { get; set; }

            [Browsable(false)]
            public VisString255 dataNs { get; set; }

            [Browsable(false)]
            public Unicode255 dU { get; set; }

            [Browsable(false)]
            public phsRef phsRef { get; set; }

            [Required]
            [Browsable(false)]
            public seqT SeqT { get; set; }
            [Required]
            [Browsable(false)]
            public seqT seqT { get; set; }
        }


        public class BSC : DOData
        {

            public BSC()
            {
                cdc = tCDCEnumEd2.BSC;
            }
            public BOOLEAN blkEna { get; set; }


            public BOOLEAN opRcvd { get; set; }
            public BOOLEAN opOk { get; set; }
            public TimeStamp tOpOk { get; set; }

            public BOOLEAN stSeld { get; set; }
            public VisString255 cdcName { get; set; }

            [Browsable(false)]
            public VisString255 cdcNs { get; set; }

            [Required]
            [Browsable(false)]
            public ctlModel ctlModel { get; set; }

            [Browsable(false)]
            public INT8U ctlNum { get; set; }

            [Browsable(false)]
            public ctlVal ctlVal { get; set; }

            [Browsable(false)]
            public VisString255 d { get; set; }

            [Browsable(false)]
            public VisString255 dataNs { get; set; }

            [Browsable(false)]
            public Unicode255 dU { get; set; }

            [Browsable(false)]
            public INT8 maxVal { get; set; }

            [Browsable(false)]
            public INT8 minVal { get; set; }

            [Browsable(false)]
            public TimeStamp operTm { get; set; }

            [Browsable(false)]
            public Originator origin { get; set; }

            [Required]
            [Browsable(false)]
            public BOOLEAN persistent { get; set; }

            [Browsable(false)]
            public Quality q { get; set; }

            [Browsable(false)]
            public sboClass sboClass { get; set; }

            [Browsable(false)]
            public INT32U sboTimeout { get; set; }

            [Browsable(false)]
            public INT8U stepSize { get; set; }

            [Browsable(false)]
            public BOOLEAN stSeldField { get; set; }

            [Browsable(false)]
            public BOOLEAN subEna { get; set; }

            [Browsable(false)]
            public VisString64 subID { get; set; }

            [Browsable(false)]
            public Quality subQ { get; set; }

            [Browsable(false)]
            public subVal subVal { get; set; }

            [Browsable(false)]
            public TimeStamp t { get; set; }

            [Browsable(false)]
            public valWTr valWTr { get; set; }

            [Browsable(false)]
            public VisString65 SBO { get; set; }

            [Browsable(false)]
            public SBOw SBOw { get; set; }

          
            public Oper Oper { get; set; }

            public INT32U operTimeout { get; set; }
            public Cancel Cancel { get; set; }
        }


        public class SEC : DOData
        {
            public SEC()
            {
                cdc = tCDCEnumEd2.SEC;
            }
            public INT32U cnt { get; set; }
            public ENUMERATED sev { get; set; }
            public TimeStamp T { get; set; }
            public Octet64 addr { get; set; }
            public VisString64 addInfo { get; set; }
            public VisString255 d { get; set; }
            public Unicode255 dU { get; set; }
            public VisString255 cdcNs { get; set; }
            public VisString255 cdcName { get; set; }
            public VisString255 dataNs { get; set; }





        }





        public class ISC : DOData
        {
       
            public ISC()
            {
                cdc = tCDCEnumEd2.ISC;
            }
            [Browsable(false)]
            public VisString255 cdcName { get; set; }

            [Browsable(false)]
            public VisString255 cdcNs { get; set; }

            [Required]
            [Browsable(false)]
            public ctlModel ctlModel { get; set; }

            [Browsable(false)]
            public INT8U ctlNum { get; set; }

            [Browsable(false)]
            public INT8 ctlVal { get; set; }

            [Browsable(false)]
            public VisString255 d { get; set; }

            [Browsable(false)]
            public VisString255 dataNs { get; set; }

            [Browsable(false)]
            public Unicode255 dU { get; set; }

            [Browsable(false)]
            public INT8 maxVal { get; set; }

            [Browsable(false)]
            public INT8 minVal { get; set; }

            [Browsable(false)]
            public TimeStamp operTm { get; set; }

            [Browsable(false)]
            public Originator origin { get; set; }

            [Browsable(false)]
            public Quality q { get; set; }

            [Browsable(false)]
            public sboClass sboClass { get; set; }

            [Browsable(false)]
            public INT32U sboTimeout { get; set; }

            [Browsable(false)]
            public INT8U stepSize { get; set; }

            [Browsable(false)]
            public BOOLEAN stSeld { get; set; }

            [Browsable(false)]
            public BOOLEAN subEna { get; set; }

            [Browsable(false)]
            public VisString64 subID { get; set; }

            [Browsable(false)]
            public Quality subQ { get; set; }

            [Browsable(false)]
            public subVal subVal { get; set; }

            [Browsable(false)]
            public TimeStamp t { get; set; }

            [Browsable(false)]
            public valWTr valWTr { get; set; }

            [Browsable(false)]
            public VisString65 SBO { get; set; }

            [Browsable(false)]
            public SBOw SBOw { get; set; }

            [Browsable(false)]
            public Oper Oper { get; set; }

            [Browsable(false)]
            public Cancel Cancel { get; set; }
        }
    }
}
