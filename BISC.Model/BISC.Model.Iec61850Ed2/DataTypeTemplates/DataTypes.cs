using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;
using BISC.Model.Iec61850Ed2.Common;
using BISC.Model.Iec61850Ed2.SclModelTemplates;
using BISC.Modules.Connection.Infrastructure.Connection;

namespace BISC.Model.Iec61850Ed2.DataTypeTemplates
{
  
    /// <summary>
    /// This data type structure is could be used by a personalized or composed DAData class. 
    /// </summary>
    public class DADataType : tDA
    {
        [XmlIgnore]
        public bool CheckSelection { get; set; }
        [XmlIgnore]
        public bool Visible { get; set; }
        public string id { get; set; }
        public string iedType { get; set; }
        [XmlIgnore]
        public List<tEnumVal> EnumVal { get; set; }

        private tVal _val;

        public DADataType()
        {
            this.Visible = true;
            Val = new List<tVal>();
        }

        public void FullEnumList(Type type)
        {
            if (EnumVal == null)
                EnumVal = new List<tEnumVal>();
            string[] strs = Enum.GetNames(type);
            List<int> ints = Enum.GetValues(type).Cast<int>().ToList();
            for (int i = 0; i < strs.Length; i++)
            {
                tEnumVal enumval = new tEnumVal();
                enumval.ord = ints[i].ToString();
                enumval.Value = strs[i];
                this.EnumVal.Add(enumval);
            }
        }
  

        [XmlIgnore]
        [Category("DA"), Description("Value of attribute.")]
        public string Value
        {
            get
            {
                if (Val.Count==0)
                {
                    return null;
                }
                return Val[0].Value;
            }
            set
            {
                this._val = new tVal();
                _val.Value=value;
                if (Val.Count == 0) Val.Add(_val);             
            }
        }
    }

    [XmlInclude(typeof(ctlModel))]
    /// <summary>
    /// This data type structure is could be used by a personalized or composed SDI class. 
    /// </summary>
    public class SDIDADataTypeBDA : tDA
    {
        public bool CheckSelection { get; set; }
        public bool Visible { get; set; }
        public string id { get; set; }
        public string iedType { get; set; }
        public List<tEnumVal> EnumVal { get; set; }
        private tVal _val;

        public SDIDADataTypeBDA()
        {
            this.Visible = true;
            Val = new List<tVal>();
        }

        [Category("DA"), Description("Value of attribute.")]
        public string Value
        {
            get
            {
                if (Val[0] == null)
                {
                    return null;
                }
                return Val[0].Value;
            }
            set
            {
                this._val = new tVal();
                Val[0] = this._val;
                Val[0].Value = value;
            }
        }

        public void FullEnumList(Type type)
        {
            if(EnumVal==null)
                EnumVal=new List<tEnumVal>();
            string[] strs = Enum.GetNames(type);
            List<int> ints = Enum.GetValues(type).Cast<int>().ToList();
            for (int i = 0; i < strs.Length; i++)
            {
                tEnumVal enumval = new tEnumVal();
                enumval.ord = ints[i].ToString();
                enumval.Value = strs[i];
                this.EnumVal.Add(enumval);
            }
        }
    }

    /// <summary>
    /// This data type structure is could be used by a personalized or composed SDO class. 
    /// </summary>
    public class SDOSDIDOTypeDA : tDA
    {
        public bool CheckSelection { get; set; }
        public bool Visible { get; set; }
        public string id { get; set; }
        public string iedType { get; set; }
        public string cdc { get; set; }

        public SDOSDIDOTypeDA()
        {
            this.Visible = true;
            this.CheckSelection = false;
        }
    }

    /// <summary>
    /// The parameter T (Transient_Data) shall be the time when the client sends the control request.
    /// </summary>
    public class Transient_Data : SDIDADataTypeBDA
    {
        public Transient_Data(string iedType, string lnType)
        {
            name = "Transient_Data";
            bType = tBasicTypeEnum.Struct;
            id = type = lnType + "Transient_Data";
            this.iedType = iedType;
            this.test = new BOOLEAN("test");
            this.test.CheckSelection = true;
        }
        public Transient_Data()
        {
            name = "Transient_Data";
            bType = tBasicTypeEnum.Struct;
        }


        public BOOLEAN test { get; set; }
    }

    /// <summary>
    /// The parameter Check shall specify the kind of checks a control object shall perform 
    /// before issuing the control operation if common DATA class is DPC.
    /// </summary>
    public class Check : SDIDADataTypeBDA
    {
        public Check(string iedType, string lnType)
        {
            name = "Check";
            bType = tBasicTypeEnum.Struct;
            id = type = lnType + "Check";
            this.iedType = iedType;
            this.synchrocheck = new BOOLEAN("synchrocheck");
            this.interlockcheck = new BOOLEAN("interlockcheck");
        }
        public Check()
        {
            name = "Check";
            bType = tBasicTypeEnum.Check;
        }
        public BOOLEAN synchrocheck { get; set; }
        public BOOLEAN interlockcheck { get; set; }
    }

    /// <summary>
    /// Source shall give information related to the origin of a value. The value may be acquired 
    /// from the process or be a substituted value.
    /// </summary>
    public enum sourceEnum
    {
        process,
        substituted
    }

    /// <summary>
    /// Analogue values may be represented as a basic data type INTEGER (attribute i) or as FLOATING
    /// POINT (attribute f)
    /// </summary>
    public class AnalogueValue : SDIDADataTypeBDA
    {
        public AnalogueValue(string iedType, string lnType)
        {
            name = "AnalogueValue";
            bType = tBasicTypeEnum.Struct;
            id = type = lnType + "AnalogueValue";
            this.iedType = iedType;
            this.i = new INT32("i");
            this.f = new FLOAT32("f");
        }
        public AnalogueValue()
        {
            name = "AnalogueValue";
            bType = tBasicTypeEnum.Struct;
            f=new FLOAT32();
            i= new INT32();
        }
        public INT32 i { get; set; }
        public FLOAT32 f { get; set; }
    }

    /// <summary>
    /// Configuration of analogue value type.
    /// </summary>
    public class ScaledValueConfig : SDIDADataTypeBDA
    {
        public ScaledValueConfig(string iedType, string lnType)
        {
            name = "ScaledValueConfig";
            bType = tBasicTypeEnum.Struct;
            id = type = lnType + "ScaledValueConfig";
            this.iedType = iedType;
            this.scaleFactor = new FLOAT32("scaleFactor");
            this.offset = new FLOAT32("offset");
        }
        public ScaledValueConfig()
        {
            name = "ScaledValueConfig";
            bType = tBasicTypeEnum.Struct;
        }
        [Required]
        public FLOAT32 scaleFactor { get; set; }

        [Required]
        public FLOAT32 offset { get; set; }
    }

    /// <summary>
    /// These attributes shall be the configuration parameters used in the context with the range 
    /// attribute.
    /// </summary>
    public class hhLim : SDIDADataTypeBDA
    {
        public hhLim(string iedType, string lnType)
        {
            name = "hhLim";
            bType = tBasicTypeEnum.Struct;
            id = type = lnType + "hhLim";
            this.iedType = iedType;
            this.AnalogueValue = new AnalogueValue(iedType, id);
            this.AnalogueValue.CheckSelection = true;
        }
        public hhLim()
        {
            name = "hhLim";
            bType = tBasicTypeEnum.Struct;
            this.AnalogueValue = new AnalogueValue(iedType, id);
        }
        public AnalogueValue AnalogueValue { get; set; }
        public FLOAT32 f
        {
            get { return AnalogueValue.f; }
            set { AnalogueValue.f = value; }
        }
    }

    /// <summary>
    /// These attributes shall be the configuration parameters used in the context with the range 
    /// attribute.
    /// </summary>
    public class hLim : SDIDADataTypeBDA
    {
        public hLim(string iedType, string lnType)
        {
            name = "hLim";
            bType = tBasicTypeEnum.Struct;
            id = type = lnType + "hLim";
            this.iedType = iedType;
            this.AnalogueValue = new AnalogueValue(iedType, id);
            this.AnalogueValue.CheckSelection = true;
        }
        public hLim()
        {
            name = "hLim";
            bType = tBasicTypeEnum.Struct;
            this.AnalogueValue = new AnalogueValue(iedType, id);

        }

        public AnalogueValue AnalogueValue { get; set; }
        public FLOAT32 f
        {
            get { return AnalogueValue.f; }
            set { AnalogueValue.f = value; }
        }
    }

    /// <summary>
    /// These attributes shall be the configuration parameters used in the context with the range 
    /// attribute.
    /// </summary>
    public class lLim : SDIDADataTypeBDA
    {
        public lLim(string iedType, string lnType)
        {
            name = "lLim";
            bType = tBasicTypeEnum.Struct;
            id = type = lnType + "lLim";
            this.iedType = iedType;
            this.AnalogueValue = new AnalogueValue(iedType, id);
            this.AnalogueValue.CheckSelection = true;
        }
        public lLim()
        {
            name = "lLim";
            bType = tBasicTypeEnum.Struct;
            this.AnalogueValue = new AnalogueValue(iedType, id);
        }

        public AnalogueValue AnalogueValue { get; set; }
        public FLOAT32 f
        {
            get { return AnalogueValue.f; }
            set { AnalogueValue.f = value; }
        }
    }

    /// <summary>
    /// These attributes shall be the configuration parameters used in the context with the range 
    /// attribute.
    /// </summary>
    public class llLim : SDIDADataTypeBDA
    {
        public llLim(string iedType, string lnType)
        {
            name = "llLim";
            bType = tBasicTypeEnum.Struct;
            id = type = lnType + "llLim";
            this.iedType = iedType;
            this.AnalogueValue = new AnalogueValue(iedType, id);
            this.AnalogueValue.CheckSelection = true;
        }
        public llLim()
        {
            name = "llLim";
            bType = tBasicTypeEnum.Struct;
            this.AnalogueValue = new AnalogueValue(iedType, id);
        }
        public AnalogueValue AnalogueValue { get; set; }
        public FLOAT32 f
        {
            get { return AnalogueValue.f; }
            set { AnalogueValue.f = value; }
        }
    }

    /// <summary>
    /// Range configuration type is used to configure the limits that define the range of a 
    /// measured value.
    /// </summary>
    public class RangeConfig : SDIDADataTypeBDA
    {
        public RangeConfig(string iedType, string lnType)
        {
            name = "RangeConfig";
            bType = tBasicTypeEnum.Struct;
            id = type = lnType + "RangeConfig";
            this.iedType = iedType;
            this.hhLim = new hhLim(iedType, id);
            this.hLim = new hLim(iedType, id);
            this.lLim = new lLim(iedType, id);
            this.min = new min(iedType, id);
            this.max = new max(iedType, id);
            this.llLim = new llLim(iedType, id);
        }
        public RangeConfig()
        {
            name = "RangeConfig";
            bType = tBasicTypeEnum.Struct;

            this.hhLim = new hhLim(iedType, id);
            this.hLim = new hLim(iedType, id);
            this.lLim = new lLim(iedType, id);
            this.min = new min(iedType, id);
            this.max = new max(iedType, id);
            this.llLim = new llLim(iedType, id);
        }
        [Required]
        public hhLim hhLim { get; set; }

        [Required]
        public hLim hLim { get; set; }

        [Required]
        public lLim lLim { get; set; }

        [Required]
        public llLim llLim { get; set; }

        [Required]
        public min min { get; set; }

        [Required]
        public max max { get; set; }
    }

    /// <summary>
    /// Step position with transient indication type is for example used to indicate the position 
    /// of tap changers.
    /// </summary>
    public class ValWithTrans : SDIDADataTypeBDA
    {
        public ValWithTrans(string iedType, string lnType)
        {
            name = "ValWithTrans";
            bType = tBasicTypeEnum.Struct;
            id = type = lnType + "ValWithTrans";
            this.iedType = iedType;
            this.posVal = new INT8("posVal");
            this.transInd = new BOOLEAN("transInd");
        }
        public ValWithTrans()
        {
            name = "ValWithTrans";
            bType = tBasicTypeEnum.Struct;
        }
        [Required]
        public INT8 posVal { get; set; }

        public BOOLEAN transInd { get; set; }
    }

    /// <summary>
    /// This identifier shall define if the control output is a pulse output or if it is a 
    /// persistent output.
    /// tBasicTypeEnum.Enum
    /// </summary>
    public class cmdQual : DADataType
    {
        public cmdQual()
        {
            name = "cmdQual";
            bType = tBasicTypeEnum.Enum;
            id = type = "cmdQualEnum";
        }
    }

    /// <summary>
    /// Pulse configuration type is used to configure the output pulse generated with a command.
    /// </summary>
    public class PulseConfig : SDIDADataTypeBDA
    {
        public PulseConfig(string iedType, string lnType, tFCEnum fCEnum)
        {
            name = "PulseConfig";
            bType = tBasicTypeEnum.Struct;
            fc = fCEnum;
            id = type = lnType + "PulseConfig";
            this.iedType = iedType;
            this.cmdQual = new cmdQual();
            this.onDur = new INT32U("onDur");
            this.offDur = new INT32U("offDur");
            this.numPls = new INT32U("numPls");
        }

        public PulseConfig(string iedType, string lnType)
        {
            name = "PulseConfig";
            bType = tBasicTypeEnum.Struct;
            id = type = lnType + "PulseConfig";
            this.iedType = iedType;
            this.cmdQual = new cmdQual();
            this.onDur = new INT32U("onDur");
            this.offDur = new INT32U("offDur");
            this.numPls = new INT32U("numPls");
        }
        public PulseConfig()
        {
            name = "PulseConfig";
            bType = tBasicTypeEnum.Struct;
        }
        [Required]
        public cmdQual cmdQual { get; set; }

        [Required]
        public INT32U onDur { get; set; }

        [Required]
        public INT32U offDur { get; set; }

        [Required]
        public INT32U numPls { get; set; }
    }

    /// <summary>
    /// It shall define the SI unit.
    /// </summary>
    public class Unit : SDIDADataTypeBDA
    {
        public Unit(string iedType, string lnType)
        {
            name = "Unit";
            bType = tBasicTypeEnum.Struct;
            id = type = lnType + "Unit";
            this.iedType = iedType;
            this.SIUnit = new SIUnit();
            this.multiplier = new multiplier();
        }
        public Unit()
        {
            name = "Unit";
            bType = tBasicTypeEnum.Struct;
        }
        [Required]
        public SIUnit SIUnit { get; set; }

        public multiplier multiplier { get; set; }
    }

    /// <summary>
    /// It shall define the SI unit.
    /// </summary>
    public class SIUnit : DADataType
    {
        private ConversionObject _conversionObject;

        public SIUnit()
        {
            this._conversionObject = new ConversionObject();
            if (Value == null)
            {
                Value = this._conversionObject.SetEnumObjectToString(tSIUnitEnum.A);
            }
            name = "SIUnit";
            bType = tBasicTypeEnum.Enum;
            id = type = "SIUnitEnum";
        }

        [DisplayName("Value"), Description("It´s the value of attribute.")]
        public tSIUnitEnum tValue
        {
            get { return (tSIUnitEnum)this._conversionObject.SetStringToEnumObject(Value, typeof(tSIUnitEnum)); }
            set { Value = this._conversionObject.SetEnumObjectToString(value); }
        }
    }

    /// <summary>
    /// It shall define the multiplier value.
    /// </summary>
    public class multiplier : DADataType
    {
        private ConversionObject _conversionObject;

        public multiplier()
        {
            this._conversionObject = new ConversionObject();
            if (Value == null)
            {
                Value = this._conversionObject.SetEnumObjectToString(tUnitMultiplierEnum.a);
            }
            name = "multiplier";
            bType = tBasicTypeEnum.Enum;
            id = type = "multiplierEnum";
        }

        [Category("DA"), DisplayName("Value"), Description("Value of attribute.")]
        public tUnitMultiplierEnum tValue
        {
            get
            {
                return
                    (tUnitMultiplierEnum)
                        this._conversionObject.SetStringToEnumObject(Value, typeof(tUnitMultiplierEnum));
            }
            set { Value = this._conversionObject.SetEnumObjectToString(value); }
        }
    }

    /// <summary>
    /// The angle reference is defined in the context where the Vector type is used.
    /// </summary>
    public class ang : SDIDADataTypeBDA
    {
        public ang(string iedType, string lnType)
        {
            name = "ang";
            bType = tBasicTypeEnum.Struct;
            id = type = lnType + "ang";
            this.iedType = iedType;
            this.AnalogueValue = new AnalogueValue(iedType, id);
            this.AnalogueValue.CheckSelection = true;
        }
        public ang()
        {
            name = "ang";
            bType = tBasicTypeEnum.Struct;
        }
        public AnalogueValue AnalogueValue { get; set; }
        public FLOAT32 f
        {
            get { return AnalogueValue.f; }
            set { AnalogueValue.f = value; }
        }
    }

    /// <summary>
    /// Vector type.
    /// </summary>
    public class Vector : SDIDADataTypeBDA
    {
        public Vector(string iedType, string lnType)
        {
            name = "Vector";
            bType = tBasicTypeEnum.Struct;
            id = type = lnType + "Vector";
            this.iedType = iedType;
            this.mag = new mag(iedType, id);
            this.ang = new ang(iedType, id);
        }
        public Vector()
        {
            name = "Vector";
            bType = tBasicTypeEnum.Struct;
            mag = new mag();
        }
        [Required]
        public mag mag { get; set; }

        public ang ang { get; set; }
    }

    /// <summary>
    /// Point type.
    /// </summary>
    public class Point : SDIDADataTypeBDA
    {
        public Point(string iedType, string lnType)
        {
            name = "Point";
            bType = tBasicTypeEnum.Struct;
            id = type = lnType + "Point";
            this.iedType = iedType;
            this.xVal = new FLOAT32("xVal");
            this.yVal = new FLOAT32("yVal");
        }
        public Point()
        {
            name = "Point";
            bType = tBasicTypeEnum.Struct;
        }
        [Required]
        public FLOAT32 xVal { get; set; }

        [Required]
        public FLOAT32 yVal { get; set; }
    }

    /// <summary>
    /// CtlModels type.
    /// tBasicTypeEnum.Enum
    /// </summary>
    public class ctlModels : DADataType
    {
        private ConversionObject _conversionObject;

        public ctlModels()
        {
            this._conversionObject = new ConversionObject();
            if (Value == null)
            {
                Value = this._conversionObject.SetEnumObjectToString(ctlModelsEnum.status_only);
            }
            name = "ctlModels";
            bType = tBasicTypeEnum.Enum;
            FullEnumList(typeof(ctlModelsEnum));
            id = type = "ctlModelsEnum";
        }

        [Category("DA"), DisplayName("Value"), Description("Value of attribute.")]
        public ctlModelsEnum tValue
        {
            get { return (ctlModelsEnum)this._conversionObject.SetStringToEnumObject(Value, typeof(ctlModelsEnum)); }
            set { Value = this._conversionObject.SetEnumObjectToString(value); }
        }
    }

    /// <summary>
    /// SboClasses type.
    /// tBasicTypeEnum.Enum
    /// </summary>
    public class SboClasses : DADataType
    {
        public SboClasses()
        {
            name = "SboClasses";
            bType = tBasicTypeEnum.Enum;
        }
    }

    /// <summary>
    /// Enums of angRef
    /// </summary>
    public enum angRefEnum
    {
        Va,
        Vb,
        Vc,
        Aa,
        Ab,
        Ac,
        Vab,
        Vbc,
        Vca,
        Vother,
        Aother
    }

    /// <summary>
    /// Enums of dirGeneral
    /// </summary>
    public enum dirGeneralEnum
    {
        unknown,
        forward,
        backward,
        both
    }

    /// <summary>
    /// Enums of dir
    /// </summary>

    /// <summary>
    /// Enum of hvRef
    /// </summary>
    public enum hvRefEnum
    {
        fundamental,
        rms,
        absolute
    }

    /// <summary>
    /// The originator category shall specify the category of the originator that caused a change 
    /// of a value.
    /// tBasicTypeEnum.Enum
    /// </summary>
    public class orCat : DADataType
    {
        public orCat()
        {
            
            name = "orCat";
            bType = tBasicTypeEnum.Enum;
            FullEnumList(typeof(orCatEnum));    
        }
    }

    /// <summary>
    /// Enum of phsRef
    /// </summary>
    public enum phsRefEnum
    {
        A,
        B,
        C
    }


    /// <summary>
    /// Enum of sev
    /// </summary>	
    public enum sevEnum
    {
        unknown,
        critical,
        major,
        minor,
        warning
    }

    /// <summary>
    /// Enum of ctlVal
    /// </summary>
    public enum ctlValEnum
    {
        stop,
        lower,
        higher,
        reserved
    }

    /// <summary>
    /// Enum of val
    /// </summary>
    public enum valEnum
    {
        intermediate_state,
        off,
        on,
        bad_state
    }


    /// <summary>
    /// Class to define a boolean data type.
    /// </summary>
    public class BOOLEAN : DADataType
    {
        public BOOLEAN()
        {
            bType = tBasicTypeEnum.BOOLEAN;
        }

        public BOOLEAN(string name, tFCEnum fCEnum)
        {
            this.name = name;
            bType = tBasicTypeEnum.BOOLEAN;
            fc = fCEnum;
        }

        public BOOLEAN(string name)
        {
            this.name = name;
            bType = tBasicTypeEnum.BOOLEAN;
        }
    }

    /// <summary>
    /// Class to define an INT8 data type.
    /// </summary>
    public class INT8 : DADataType
    {
        public INT8()
        {
            bType = tBasicTypeEnum.INT8;
        }

        public INT8(string name, tFCEnum fCEnum)
        {
            this.name = name;
            bType = tBasicTypeEnum.INT8;
            fc = fCEnum;
        }

        public INT8(string name)
        {
            this.name = name;
            bType = tBasicTypeEnum.INT8;
        }
    }

    /// <summary>
    /// Class to define an INT16 data type.
    /// </summary>	
    public class INT16 : DADataType
    {
        public INT16()
        {
            bType = tBasicTypeEnum.INT16;
        }

        public INT16(string name, tFCEnum fCEnum)
        {
            this.name = name;
            bType = tBasicTypeEnum.INT16;
            fc = fCEnum;
        }

        public INT16(string name)
        {
            this.name = name;
            bType = tBasicTypeEnum.INT16;
        }
    }

    /// <summary>
    /// Class to define an INT24 data type.
    /// </summary>	
    public class INT24 : DADataType
    {
        public INT24()
        {
            bType = tBasicTypeEnum.INT24;
        }

        public INT24(string name, tFCEnum fCEnum)
        {
            this.name = name;
            bType = tBasicTypeEnum.INT24;
            fc = fCEnum;
        }

        public INT24(string name)
        {
            this.name = name;
            bType = tBasicTypeEnum.INT24;
        }
    }

    /// <summary>
    /// Class to define an INT32 data type.
    /// </summary>	
    public class INT32 : DADataType
    {
        public INT32()
        {
            bType = tBasicTypeEnum.INT32;
        }

        public INT32(string name, tFCEnum fCEnum)
        {
            this.name = name;
            bType = tBasicTypeEnum.INT32;
            fc = fCEnum;
        }

        public INT32(string name)
        {
            this.name = name;
            bType = tBasicTypeEnum.INT32;
        }
    }
    public class INT64 : DADataType
    {
        public INT64()
        {
            bType = tBasicTypeEnum.INT64;
        }

        public INT64(string name, tFCEnum fCEnum)
        {
            this.name = name;
            bType = tBasicTypeEnum.INT32;
            fc = fCEnum;
        }

        public INT64(string name)
        {
            this.name = name;
            bType = tBasicTypeEnum.INT32;
        }
    }

    /// <summary>
    /// Class to define an INT128 data type.
    /// </summary>	
    public class INT128 : DADataType
    {
        public INT128()
        {
            bType = tBasicTypeEnum.INT128;
        }

        public INT128(string name, tFCEnum fCEnum)
        {
            this.name = name;
            bType = tBasicTypeEnum.INT128;
            fc = fCEnum;
        }

        public INT128(string name)
        {
            this.name = name;
            bType = tBasicTypeEnum.INT128;
        }
    }

    /// <summary>
    /// Class to define an INT8U data type.
    /// </summary>	
    public class INT8U : DADataType
    {

        public INT8U(string name, tFCEnum fCEnum)
        {
            this.name = name;
            bType = tBasicTypeEnum.INT8U;
            fc = fCEnum;
        }

        public INT8U(string name)
        {
            this.name = name;
            bType = tBasicTypeEnum.INT8U;
        }

        public INT8U()
        {
            bType = tBasicTypeEnum.INT8U;
        }
    }

    /// <summary>
    /// Class to define an INT16U data type.
    /// </summary>	
    public class INT16U : DADataType
    {
        public INT16U()
        {
            bType = tBasicTypeEnum.INT16U;
        }

        public INT16U(string name, tFCEnum fCEnum)
        {
            this.name = name;
            bType = tBasicTypeEnum.INT16U;
            fc = fCEnum;
        }

        public INT16U(string name)
        {
            this.name = name;
            bType = tBasicTypeEnum.INT16U;
        }
    }

    /// <summary>
    /// Class to define an INT24U data type.
    /// </summary>	
    public class INT24U : DADataType
    {
        public INT24U()
        {
            bType = tBasicTypeEnum.INT24U;
        }

        public INT24U(string name, tFCEnum fCEnum)
        {
            this.name = name;
            bType = tBasicTypeEnum.INT24U;
            fc = fCEnum;
        }

        public INT24U(string name)
        {
            this.name = name;
            bType = tBasicTypeEnum.INT24U;
        }
    }

    /// <summary>
    /// Class to define an INT32U data type.
    /// </summary>	
    public class INT32U : DADataType
    {
        public INT32U(string name, tFCEnum fCEnum)
        {
            this.name = name;
            bType = tBasicTypeEnum.INT32U;
            fc = fCEnum;
        }

        public INT32U(string name)
        {
            this.name = name;
            bType = tBasicTypeEnum.INT32U;
        }

        public INT32U()
        {
            bType = tBasicTypeEnum.INT32U;
        }
    }

    /// <summary>
    /// Class to define an FLOAT32 data type.
    /// </summary>	
    public class FLOAT32 : DADataType
    {
        public FLOAT32(string name, tFCEnum fCEnum)
        {
            this.name = name;
            bType = tBasicTypeEnum.FLOAT32;
            fc = fCEnum;
        }

        public FLOAT32(string name)
        {
            this.name = name;
            bType = tBasicTypeEnum.FLOAT32;
        }

        public FLOAT32()
        {
            bType = tBasicTypeEnum.FLOAT32;
        }
    }

    /// <summary>
    /// Class to define an FLOAT64 data type.
    /// </summary>	
    public class FLOAT64 : DADataType
    {
        public FLOAT64(string name, tFCEnum fCEnum)
        {
            this.name = name;
            bType = tBasicTypeEnum.FLOAT64;
            fc = fCEnum;
        }

        public FLOAT64(string name)
        {
            this.name = name;
            bType = tBasicTypeEnum.FLOAT64;
        }

        public FLOAT64()
        {
            bType = tBasicTypeEnum.FLOAT64;
        }
    }

    /// <summary>
    /// Class to define an VisString255 data type.
    /// </summary>	
    public class VisString255 : DADataType
    {
        public VisString255(string name, tFCEnum fCEnum)
        {
            this.name = name;
            bType = tBasicTypeEnum.VisString255;
            fc = fCEnum;
        }

        public VisString255(string name)
        {
            this.name = name;
            bType = tBasicTypeEnum.VisString255;
        }

        public VisString255()
        {
            bType = tBasicTypeEnum.VisString255;
        }
    }



    public class VisString129 : DADataType
    {
        public VisString129()
        {
            bType = tBasicTypeEnum.VisString129;
        }
    }
    /// <summary>
    /// Class to define an Octet64 data type.
    /// </summary>	
    public class Octet64 : DADataType
    {
        public Octet64(string name, tFCEnum fCEnum)
        {
            this.name = name;
            bType = tBasicTypeEnum.Octet64;
            fc = fCEnum;
        }

        public Octet64(string name)
        {
            this.name = name;
            bType = tBasicTypeEnum.Octet64;
        }

        public Octet64()
        {
            bType = tBasicTypeEnum.Octet64;
        }
    }

    /// <summary>
    /// Class to define an VisString32 data type.
    /// </summary>	
    public class VisString32 : DADataType
    {
        public VisString32(string name, tFCEnum fCEnum)
        {
            this.name = name;
            bType = tBasicTypeEnum.VisString32;
            fc = fCEnum;
        }

        public VisString32(string name)
        {
            this.name = name;
            bType = tBasicTypeEnum.VisString32;
        }

        public VisString32()
        {
            bType = tBasicTypeEnum.VisString32;
        }
    }



    public class ObjReference : DADataType
    {
        public VisString129 ObjectReference { get; set; }
    }


  public class CalendarTime : DADataType
    {
        public INT16U occ { get; set; }
      //  public occType { get; set; }
        public VisString129 occPer { get; set; }
        public VisString129 weekDay { get; set; }
        public VisString129 month { get; set; }
        public VisString129 day { get; set; }
        public VisString129 hr { get; set; }
        public VisString129 mn { get; set; }






    }



    /// <summary>
    /// Class to define an TimeStamp data type.
    /// </summary>	
    public class TimeStamp : DADataType
    {
        public TimeStamp(string name, tFCEnum fCEnum)
        {
            this.name = name;
            bType = tBasicTypeEnum.Timestamp;
            fc = fCEnum;
        }

        public TimeStamp(string name)
        {
            this.name = name;
            bType = tBasicTypeEnum.Timestamp;
        }

        public TimeStamp()
        {
            bType = tBasicTypeEnum.Timestamp;
        }
    }

    /// <summary>
    /// Class to define an Unicode255 data type.
    /// </summary>	
    public class Unicode255 : DADataType
    {
        public Unicode255()
        {
            bType = tBasicTypeEnum.Unicode255;
        }

        public Unicode255(string name, tFCEnum fCEnum)
        {
            this.name = name;
            bType = tBasicTypeEnum.Unicode255;
            fc = fCEnum;
        }

        public Unicode255(string name)
        {
            this.name = name;
            bType = tBasicTypeEnum.Unicode255;
        }
    }

    /// <summary>
    /// The quality identifier shall reflect the quality of the information in the server, as it 
    /// is supplied to the client.
    /// </summary>
    public class Quality : DADataType
    {
        public Quality(string name, tFCEnum fCEnum)
        {
            this.name = name;
            bType = tBasicTypeEnum.Quality;
            fc = fCEnum;
        }

        public Quality(string name)
        {
            this.name = name;
            bType = tBasicTypeEnum.Quality;
        }

        public Quality()
        {
            bType = tBasicTypeEnum.Quality;
        }
    }

    /// <summary>
    /// Class to define a VisString64 data type.
    /// </summary>	
    public class VisString64 : DADataType
    {
        public VisString64(string name, tFCEnum fCEnum)
        {
            this.name = name;
            bType = tBasicTypeEnum.VisString64;
            fc = fCEnum;
        }

        public VisString64(string name)
        {
            this.name = name;
            bType = tBasicTypeEnum.VisString64;
        }

        public VisString64()
        {
            bType = tBasicTypeEnum.VisString64;
        }
    }

    /// <summary>
    /// Class to define a VisString65 data type.
    /// </summary>	
    public class VisString65 : DADataType
    {
        public VisString65()
        {
            bType = tBasicTypeEnum.VisString65;
        }

        public VisString65(string name, tFCEnum fCEnum)
        {
            this.name = name;
            bType = tBasicTypeEnum.VisString65;
            fc = fCEnum;
        }
    }

    /// <summary>
    /// The select with value service shall be performed through the use of an MMS write to the 
    /// SBOw attribute.
    /// </summary>	
    public class SBOw : SDIDADataTypeBDA
    {
        public SBOw(string iedType, string lnType, tFCEnum fCEnum)
        {
            Visible = false;
            name = "SBOw";
            bType = tBasicTypeEnum.Struct;
            fc = fCEnum;
            id = type = lnType + "SBOw";
            this.iedType = iedType;
            this.operTm = new TimeStamp("operTm");
            this.ctlNum = new INT8U("ctlNum");
            this.test = new BOOLEAN("test");
            this.Originator = new Originator(iedType, id);
            this.Transient_Data = new Transient_Data(iedType, id);
            this.Check = new Check(iedType, id);
        }
        public SBOw()
        {
            Visible = false;
            name = "SBOw";
            bType = tBasicTypeEnum.Struct;
        }
        [Browsable(false)]
        public INT8U ctlNum { get; set; }

        [Required]
        [Browsable(false)]
        public BOOLEAN test { get; set; }

        [Browsable(false)]
        public TimeStamp operTm { get; set; }

        [Required]
        [Browsable(false)]
        public Originator Originator { get; set; }

        [Required]
        [Browsable(false)]
        public Transient_Data Transient_Data { get; set; }

        [Required]
        [Browsable(false)]
        public Check Check { get; set; }
    }

    /// <summary>
    /// The receives service parameters and control values shall be performed through 
    /// the use of an MMS write to the Oper attribute.
    /// </summary>
    public class Oper : SDIDADataTypeBDA
    {
        public Oper()
        {
            bType = tBasicTypeEnum.Struct;
           
        }

        public Oper(string objtype)
        {
            bType = tBasicTypeEnum.Struct;
            ctlVal = new ctlVal(objtype);
            Originator = new Originator();
        }

        public Oper(string iedType, string lnType, tFCEnum fCEnum)
        {
            Visible = false;
            name = "Oper";
            bType = tBasicTypeEnum.Struct;
            fc = fCEnum;
            id = type = lnType + "Oper";
            this.iedType = iedType;
            this.operTm = new TimeStamp("operTm");
            this.ctlNum = new INT8U("ctlNum");
            this.Test = new BOOLEAN("test");
            this.Originator = new Originator(iedType, id);
            this.Transient_Data = new Transient_Data(iedType, id);
            this.Check = new Check(iedType, id);
        }

        [Browsable(false)]
        public TimeStamp operTm { get; set; }

        [Browsable(false)]
        public INT8U ctlNum { get; set; }

        [Required]
        [Browsable(false)]
        public BOOLEAN Test { get; set; }

        [Required]
        [Browsable(false)]
        public Originator Originator { get; set; }
        [Required]
        [Browsable(false)]
        public Originator origin { get; set; }
        [Required]
        [Browsable(false)]
        public Transient_Data Transient_Data { get; set; }

        [Required]
        [Browsable(false)]
        public Check Check { get; set; }

        [Required]
        [Browsable(false)]
        public TimeStamp T { get; set; }
        [Required]
        [Browsable(false)]
        public ctlVal ctlVal { get; set; }
    }

    /// <summary>
    /// The receives service parameters and control values shall be performed through 
    /// the use of an MMS write to the Cancel attribute.
    /// </summary>	
    public class Cancel : SDIDADataTypeBDA
    {
        public Cancel(string iedType, string lnType, tFCEnum fCEnum)
        {
            Visible = false;
            name = "Cancel";
            bType = tBasicTypeEnum.Struct;
            fc = fCEnum;
            id = type = lnType + "Cancel";
            this.iedType = iedType;
            this.operTm = new TimeStamp("operTm");
            this.Originator = new Originator(iedType, id);
            this.ctlNum = new INT8U("ctlNum");
            this.Transient_Data = new Transient_Data(iedType, id);
            this.test = new BOOLEAN("test");
        }
        public Cancel()
        {
            bType = tBasicTypeEnum.Struct;
        }

        [Browsable(false)]
        public TimeStamp operTm { get; set; }

        [Required]
        [Browsable(false)]
        public BOOLEAN test { get; set; }

        [Browsable(false)]
        public INT8U ctlNum { get; set; }

        [Required]
        [Browsable(false)]
        public Originator Originator { get; set; }

        [Required]
        [Browsable(false)]
        public Transient_Data Transient_Data { get; set; }
    }

    /// <summary>
    /// Enum of valMod
    /// </summary>
    /// <remarks>
    /// This enum was created to be used by stVal attribute.
    /// </remarks>
    public enum valModEnum
    {
        on=1,
        blocked,
        test,
        test_blocked,
        off
    }
}