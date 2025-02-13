
//
// This file was generated by the BinaryNotes compiler.
// See http://bnotes.sourceforge.net 
// Any modifications to this file will be lost upon recompilation of the source ASN.1. 
//

using BISC.Modules.Connection.MMS.org.bn;
using BISC.Modules.Connection.MMS.org.bn.attributes;
using BISC.Modules.Connection.MMS.org.bn.coders;
using BISC.Modules.Connection.MMS.org.bn.types;

namespace BISC.Modules.Connection.MMS.MMS_ASN1_Model
{


    [ASN1PreparedElement]
    [ASN1Choice(Name = "Data")]
    public class Data : IASN1PreparedElement
    {

        private System.Collections.Generic.ICollection<Data> array_;
        private bool array_selected = false;

        [ASN1SequenceOf(Name = "array", IsSetOf = false)]
        [ASN1_MMSDataArray]
        [ASN1Element(Name = "array", IsOptional = false, HasTag = true, Tag = 1, HasDefaultValue = false)]
        public System.Collections.Generic.ICollection<Data> Array
        {
            get { return array_; }
            set { selectArray(value); }
        }

        private System.Collections.Generic.ICollection<Data> structure_;
        private bool structure_selected = false;



        [ASN1SequenceOf(Name = "structure", IsSetOf = false)]
        [ASN1_MMSDataStructure]
        [ASN1Element(Name = "structure", IsOptional = false, HasTag = true, Tag = 2, HasDefaultValue = false)]

        public System.Collections.Generic.ICollection<Data> Structure
        {
            get { return structure_; }
            set { selectStructure(value); }
        }




        private bool boolean_;
        private bool boolean_selected = false;


        [ASN1Boolean(Name = "")]

        [ASN1Element(Name = "boolean", IsOptional = false, HasTag = true, Tag = 3, HasDefaultValue = false)]

        public bool Boolean
        {
            get { return boolean_; }
            set { selectBoolean(value); }
        }




        private BitString bit_string_;
        private bool bit_string_selected = false;


        [ASN1BitString(Name = "")]

        [ASN1Element(Name = "bit-string", IsOptional = false, HasTag = true, Tag = 4, HasDefaultValue = false)]

        public BitString Bit_string
        {
            get { return bit_string_; }
            set { selectBit_string(value); }
        }




        private long integer_;
        private bool integer_selected = false;


        [ASN1Integer(Name = "")]

        [ASN1Element(Name = "integer", IsOptional = false, HasTag = true, Tag = 5, HasDefaultValue = false)]

        public long Integer
        {
            get { return integer_; }
            set { selectInteger(value); }
        }




        private long unsigned_;
        private bool unsigned_selected = false;


        [ASN1Integer(Name = "")]

        [ASN1Element(Name = "unsigned", IsOptional = false, HasTag = true, Tag = 6, HasDefaultValue = false)]

        public long Unsigned
        {
            get { return unsigned_; }
            set { selectUnsigned(value); }
        }




        private FloatingPoint floating_point_;
        private bool floating_point_selected = false;



        [ASN1Element(Name = "floating-point", IsOptional = false, HasTag = true, Tag = 7, HasDefaultValue = false)]

        public FloatingPoint Floating_point
        {
            get { return floating_point_; }
            set { selectFloating_point(value); }
        }




        private byte[] octet_string_;
        private bool octet_string_selected = false;


        [ASN1OctetString(Name = "")]

        [ASN1Element(Name = "octet-string", IsOptional = false, HasTag = true, Tag = 9, HasDefaultValue = false)]

        public byte[] Octet_string
        {
            get { return octet_string_; }
            set { selectOctet_string(value); }
        }




        private string visible_string_;
        private bool visible_string_selected = false;


        [ASN1String(Name = "",
        StringType = UniversalTags.VisibleString, IsUCS = false)]
        [ASN1Element(Name = "visible-string", IsOptional = false, HasTag = true, Tag = 10, HasDefaultValue = false)]

        public string Visible_string
        {
            get { return visible_string_; }
            set { selectVisible_string(value); }
        }




        private string generalized_time_;
        private bool generalized_time_selected = false;


        [ASN1String(Name = "",
        StringType = UniversalTags.GeneralizedTime, IsUCS = false)]
        [ASN1Element(Name = "generalized-time", IsOptional = false, HasTag = true, Tag = 11, HasDefaultValue = false)]

        public string Generalized_time
        {
            get { return generalized_time_; }
            set { selectGeneralized_time(value); }
        }




        private TimeOfDay binary_time_;
        private bool binary_time_selected = false;



        [ASN1Element(Name = "binary-time", IsOptional = false, HasTag = true, Tag = 12, HasDefaultValue = false)]

        public TimeOfDay Binary_time
        {
            get { return binary_time_; }
            set { selectBinary_time(value); }
        }




        private long bcd_;
        private bool bcd_selected = false;


        [ASN1Integer(Name = "")]

        [ASN1Element(Name = "bcd", IsOptional = false, HasTag = true, Tag = 13, HasDefaultValue = false)]

        public long Bcd
        {
            get { return bcd_; }
            set { selectBcd(value); }
        }




        private BitString booleanArray_;
        private bool booleanArray_selected = false;


        [ASN1BitString(Name = "")]

        [ASN1Element(Name = "booleanArray", IsOptional = false, HasTag = true, Tag = 14, HasDefaultValue = false)]

        public BitString BooleanArray
        {
            get { return booleanArray_; }
            set { selectBooleanArray(value); }
        }




        private ObjectIdentifier objId_;
        private bool objId_selected = false;


        [ASN1ObjectIdentifier(Name = "")]

        [ASN1Element(Name = "objId", IsOptional = false, HasTag = true, Tag = 15, HasDefaultValue = false)]

        public ObjectIdentifier ObjId
        {
            get { return objId_; }
            set { selectObjId(value); }
        }




        private MMSString mMSString_;
        private bool mMSString_selected = false;



        [ASN1Element(Name = "mMSString", IsOptional = false, HasTag = true, Tag = 16, HasDefaultValue = false)]

        public MMSString MMSString
        {
            get { return mMSString_; }
            set { selectMMSString(value); }
        }




        private UtcTime utc_time_;
        private bool utc_time_selected = false;



        [ASN1Element(Name = "utc-time", IsOptional = false, HasTag = true, Tag = 17, HasDefaultValue = false)]

        public UtcTime Utc_time
        {
            get { return utc_time_; }
            set { selectUtc_time(value); }
        }



        public bool isArraySelected()
        {
            return this.array_selected;
        }




        public void selectArray(System.Collections.Generic.ICollection<Data> val)
        {
            this.array_ = val;
            this.array_selected = true;


            this.structure_selected = false;

            this.boolean_selected = false;

            this.bit_string_selected = false;

            this.integer_selected = false;

            this.unsigned_selected = false;

            this.floating_point_selected = false;

            this.octet_string_selected = false;

            this.visible_string_selected = false;

            this.generalized_time_selected = false;

            this.binary_time_selected = false;

            this.bcd_selected = false;

            this.booleanArray_selected = false;

            this.objId_selected = false;

            this.mMSString_selected = false;

            this.utc_time_selected = false;

        }


        public bool isStructureSelected()
        {
            return this.structure_selected;
        }




        public void selectStructure(System.Collections.Generic.ICollection<Data> val)
        {
            this.structure_ = val;
            this.structure_selected = true;


            this.array_selected = false;

            this.boolean_selected = false;

            this.bit_string_selected = false;

            this.integer_selected = false;

            this.unsigned_selected = false;

            this.floating_point_selected = false;

            this.octet_string_selected = false;

            this.visible_string_selected = false;

            this.generalized_time_selected = false;

            this.binary_time_selected = false;

            this.bcd_selected = false;

            this.booleanArray_selected = false;

            this.objId_selected = false;

            this.mMSString_selected = false;

            this.utc_time_selected = false;

        }


        public bool isBooleanSelected()
        {
            return this.boolean_selected;
        }




        public void selectBoolean(bool val)
        {
            this.boolean_ = val;
            this.boolean_selected = true;


            this.array_selected = false;

            this.structure_selected = false;

            this.bit_string_selected = false;

            this.integer_selected = false;

            this.unsigned_selected = false;

            this.floating_point_selected = false;

            this.octet_string_selected = false;

            this.visible_string_selected = false;

            this.generalized_time_selected = false;

            this.binary_time_selected = false;

            this.bcd_selected = false;

            this.booleanArray_selected = false;

            this.objId_selected = false;

            this.mMSString_selected = false;

            this.utc_time_selected = false;

        }


        public bool isBit_stringSelected()
        {
            return this.bit_string_selected;
        }




        public void selectBit_string(BitString val)
        {
            this.bit_string_ = val;
            this.bit_string_selected = true;


            this.array_selected = false;

            this.structure_selected = false;

            this.boolean_selected = false;

            this.integer_selected = false;

            this.unsigned_selected = false;

            this.floating_point_selected = false;

            this.octet_string_selected = false;

            this.visible_string_selected = false;

            this.generalized_time_selected = false;

            this.binary_time_selected = false;

            this.bcd_selected = false;

            this.booleanArray_selected = false;

            this.objId_selected = false;

            this.mMSString_selected = false;

            this.utc_time_selected = false;

        }


        public bool isIntegerSelected()
        {
            return this.integer_selected;
        }




        public void selectInteger(long val)
        {
            this.integer_ = val;
            this.integer_selected = true;


            this.array_selected = false;

            this.structure_selected = false;

            this.boolean_selected = false;

            this.bit_string_selected = false;

            this.unsigned_selected = false;

            this.floating_point_selected = false;

            this.octet_string_selected = false;

            this.visible_string_selected = false;

            this.generalized_time_selected = false;

            this.binary_time_selected = false;

            this.bcd_selected = false;

            this.booleanArray_selected = false;

            this.objId_selected = false;

            this.mMSString_selected = false;

            this.utc_time_selected = false;

        }


        public bool isUnsignedSelected()
        {
            return this.unsigned_selected;
        }




        public void selectUnsigned(long val)
        {
            this.unsigned_ = val;
            this.unsigned_selected = true;


            this.array_selected = false;

            this.structure_selected = false;

            this.boolean_selected = false;

            this.bit_string_selected = false;

            this.integer_selected = false;

            this.floating_point_selected = false;

            this.octet_string_selected = false;

            this.visible_string_selected = false;

            this.generalized_time_selected = false;

            this.binary_time_selected = false;

            this.bcd_selected = false;

            this.booleanArray_selected = false;

            this.objId_selected = false;

            this.mMSString_selected = false;

            this.utc_time_selected = false;

        }


        public bool isFloating_pointSelected()
        {
            return this.floating_point_selected;
        }




        public void selectFloating_point(FloatingPoint val)
        {
            this.floating_point_ = val;
            this.floating_point_selected = true;


            this.array_selected = false;

            this.structure_selected = false;

            this.boolean_selected = false;

            this.bit_string_selected = false;

            this.integer_selected = false;

            this.unsigned_selected = false;

            this.octet_string_selected = false;

            this.visible_string_selected = false;

            this.generalized_time_selected = false;

            this.binary_time_selected = false;

            this.bcd_selected = false;

            this.booleanArray_selected = false;

            this.objId_selected = false;

            this.mMSString_selected = false;

            this.utc_time_selected = false;

        }


        public bool isOctet_stringSelected()
        {
            return this.octet_string_selected;
        }




        public void selectOctet_string(byte[] val)
        {
            this.octet_string_ = val;
            this.octet_string_selected = true;


            this.array_selected = false;

            this.structure_selected = false;

            this.boolean_selected = false;

            this.bit_string_selected = false;

            this.integer_selected = false;

            this.unsigned_selected = false;

            this.floating_point_selected = false;

            this.visible_string_selected = false;

            this.generalized_time_selected = false;

            this.binary_time_selected = false;

            this.bcd_selected = false;

            this.booleanArray_selected = false;

            this.objId_selected = false;

            this.mMSString_selected = false;

            this.utc_time_selected = false;

        }


        public bool isVisible_stringSelected()
        {
            return this.visible_string_selected;
        }




        public void selectVisible_string(string val)
        {
            this.visible_string_ = val;
            this.visible_string_selected = true;


            this.array_selected = false;

            this.structure_selected = false;

            this.boolean_selected = false;

            this.bit_string_selected = false;

            this.integer_selected = false;

            this.unsigned_selected = false;

            this.floating_point_selected = false;

            this.octet_string_selected = false;

            this.generalized_time_selected = false;

            this.binary_time_selected = false;

            this.bcd_selected = false;

            this.booleanArray_selected = false;

            this.objId_selected = false;

            this.mMSString_selected = false;

            this.utc_time_selected = false;

        }


        public bool isGeneralized_timeSelected()
        {
            return this.generalized_time_selected;
        }




        public void selectGeneralized_time(string val)
        {
            this.generalized_time_ = val;
            this.generalized_time_selected = true;


            this.array_selected = false;

            this.structure_selected = false;

            this.boolean_selected = false;

            this.bit_string_selected = false;

            this.integer_selected = false;

            this.unsigned_selected = false;

            this.floating_point_selected = false;

            this.octet_string_selected = false;

            this.visible_string_selected = false;

            this.binary_time_selected = false;

            this.bcd_selected = false;

            this.booleanArray_selected = false;

            this.objId_selected = false;

            this.mMSString_selected = false;

            this.utc_time_selected = false;

        }


        public bool isBinary_timeSelected()
        {
            return this.binary_time_selected;
        }




        public void selectBinary_time(TimeOfDay val)
        {
            this.binary_time_ = val;
            this.binary_time_selected = true;


            this.array_selected = false;

            this.structure_selected = false;

            this.boolean_selected = false;

            this.bit_string_selected = false;

            this.integer_selected = false;

            this.unsigned_selected = false;

            this.floating_point_selected = false;

            this.octet_string_selected = false;

            this.visible_string_selected = false;

            this.generalized_time_selected = false;

            this.bcd_selected = false;

            this.booleanArray_selected = false;

            this.objId_selected = false;

            this.mMSString_selected = false;

            this.utc_time_selected = false;

        }


        public bool isBcdSelected()
        {
            return this.bcd_selected;
        }




        public void selectBcd(long val)
        {
            this.bcd_ = val;
            this.bcd_selected = true;


            this.array_selected = false;

            this.structure_selected = false;

            this.boolean_selected = false;

            this.bit_string_selected = false;

            this.integer_selected = false;

            this.unsigned_selected = false;

            this.floating_point_selected = false;

            this.octet_string_selected = false;

            this.visible_string_selected = false;

            this.generalized_time_selected = false;

            this.binary_time_selected = false;

            this.booleanArray_selected = false;

            this.objId_selected = false;

            this.mMSString_selected = false;

            this.utc_time_selected = false;

        }


        public bool isBooleanArraySelected()
        {
            return this.booleanArray_selected;
        }




        public void selectBooleanArray(BitString val)
        {
            this.booleanArray_ = val;
            this.booleanArray_selected = true;


            this.array_selected = false;

            this.structure_selected = false;

            this.boolean_selected = false;

            this.bit_string_selected = false;

            this.integer_selected = false;

            this.unsigned_selected = false;

            this.floating_point_selected = false;

            this.octet_string_selected = false;

            this.visible_string_selected = false;

            this.generalized_time_selected = false;

            this.binary_time_selected = false;

            this.bcd_selected = false;

            this.objId_selected = false;

            this.mMSString_selected = false;

            this.utc_time_selected = false;

        }


        public bool isObjIdSelected()
        {
            return this.objId_selected;
        }




        public void selectObjId(ObjectIdentifier val)
        {
            this.objId_ = val;
            this.objId_selected = true;


            this.array_selected = false;

            this.structure_selected = false;

            this.boolean_selected = false;

            this.bit_string_selected = false;

            this.integer_selected = false;

            this.unsigned_selected = false;

            this.floating_point_selected = false;

            this.octet_string_selected = false;

            this.visible_string_selected = false;

            this.generalized_time_selected = false;

            this.binary_time_selected = false;

            this.bcd_selected = false;

            this.booleanArray_selected = false;

            this.mMSString_selected = false;

            this.utc_time_selected = false;

        }


        public bool isMMSStringSelected()
        {
            return this.mMSString_selected;
        }




        public void selectMMSString(MMSString val)
        {
            this.mMSString_ = val;
            this.mMSString_selected = true;


            this.array_selected = false;

            this.structure_selected = false;

            this.boolean_selected = false;

            this.bit_string_selected = false;

            this.integer_selected = false;

            this.unsigned_selected = false;

            this.floating_point_selected = false;

            this.octet_string_selected = false;

            this.visible_string_selected = false;

            this.generalized_time_selected = false;

            this.binary_time_selected = false;

            this.bcd_selected = false;

            this.booleanArray_selected = false;

            this.objId_selected = false;

            this.utc_time_selected = false;

        }


        public bool isUtc_timeSelected()
        {
            return this.utc_time_selected;
        }




        public void selectUtc_time(UtcTime val)
        {
            this.utc_time_ = val;
            this.utc_time_selected = true;


            this.array_selected = false;

            this.structure_selected = false;

            this.boolean_selected = false;

            this.bit_string_selected = false;

            this.integer_selected = false;

            this.unsigned_selected = false;

            this.floating_point_selected = false;

            this.octet_string_selected = false;

            this.visible_string_selected = false;

            this.generalized_time_selected = false;

            this.binary_time_selected = false;

            this.bcd_selected = false;

            this.booleanArray_selected = false;

            this.objId_selected = false;

            this.mMSString_selected = false;

        }



        public void initWithDefaults()
        {
        }

        private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(Data));
        public IASN1PreparedElementData PreparedData
        {
            get { return preparedData; }
        }

    }

}
