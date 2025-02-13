
//
// This file was generated by the BinaryNotes compiler.
// See http://bnotes.sourceforge.net 
// Any modifications to this file will be lost upon recompilation of the source ASN.1. 
//

using System;
using BISC.Modules.Connection.MMS.org.bn;
using BISC.Modules.Connection.MMS.org.bn.attributes;
using BISC.Modules.Connection.MMS.org.bn.coders;
using BISC.Modules.Connection.MMS.org.bn.types;

namespace BISC.Modules.Connection.MMS.Goose_ASN1_Model {


    [ASN1PreparedElement]
    [ASN1Choice ( Name = "Data") ]
    public class Data : IASN1PreparedElement {

    public event EventHandler ValueChanged;

	private DataSequence array_ ;
        private bool  array_selected = false ;
        
                
        
        [ASN1Element ( Name = "array", IsOptional =  false , HasTag =  true, Tag = 1 , HasDefaultValue =  false )  ]
    
        public DataSequence Array
        {
            get { return array_; }
            set { selectArray(value); }
        }
        
                
          
        
	private DataSequence structure_ ;
        private bool  structure_selected = false ;
        
                
        
        [ASN1Element ( Name = "structure", IsOptional =  false , HasTag =  true, Tag = 2 , HasDefaultValue =  false )  ]
    
        public DataSequence Structure
        {
            get { return structure_; }
            set { selectStructure(value); }
        }
        
                
          
        
	private bool boolean_ ;
        private bool  boolean_selected = false ;
        
                
        [ASN1Boolean( Name = "" )]
    
        [ASN1Element ( Name = "boolean", IsOptional =  false , HasTag =  true, Tag = 3 , HasDefaultValue =  false )  ]
    
        public bool Boolean
        {
            get { 
                    lock(this)
                        return boolean_; 
                }
            set 
            { 
                    lock(this)
                        selectBoolean(value); 
            }
        }
        
                
          
        
	private BitString bitstring_ ;
        private bool  bitstring_selected = false ;
        
                
        [ASN1BitString( Name = "" )]
    
        [ASN1Element ( Name = "bitstring", IsOptional =  false , HasTag =  true, Tag = 4 , HasDefaultValue =  false )  ]
    
        public BitString Bitstring
        {
            get { return bitstring_; }
            set { selectBitstring(value); }
        }
        
                
          
        
	private long integer_ ;
        private bool  integer_selected = false ;
        
                
        [ASN1Integer( Name = "" )]
    
        [ASN1Element ( Name = "integer", IsOptional =  false , HasTag =  true, Tag = 5 , HasDefaultValue =  false )  ]
    
        public long Integer
        {
            get {
                    lock(this)
                        return integer_; 
                }
            set {
                    lock (this)
                        selectInteger(value);
                }
        }
        
                
          
        
	private long unsigned_ ;
        private bool  unsigned_selected = false ;
        
                
        [ASN1Integer( Name = "" )]
    
        [ASN1Element ( Name = "unsigned", IsOptional =  false , HasTag =  true, Tag = 6 , HasDefaultValue =  false )  ]
    
        public long Unsigned
        {
            get { return unsigned_; }
            set { selectUnsigned(value); }
        }
        
                
          
        
	private FloatingPoint floatingpoint_ ;
        private bool  floatingpoint_selected = false ;
        
                
        
        [ASN1Element ( Name = "floatingpoint", IsOptional =  false , HasTag =  true, Tag = 7 , HasDefaultValue =  false )  ]
    
        public FloatingPoint Floatingpoint
        {
            get { return floatingpoint_; }
            set { selectFloatingpoint(value); }
        }
        
                
          
        
	private byte[] octetstring_ ;
        private bool  octetstring_selected = false ;
        
                
        [ASN1OctetString( Name = "" )]
    
        [ASN1Element ( Name = "octetstring", IsOptional =  false , HasTag =  true, Tag = 9 , HasDefaultValue =  false )  ]
    
        public byte[] Octetstring
        {
            get { return octetstring_; }
            set { selectOctetstring(value); }
        }
        
                
          
        
	private string visiblestring_ ;
        private bool  visiblestring_selected = false ;
        
                
        [ASN1String( Name = "", 
        StringType =  UniversalTags.VisibleString , IsUCS = false )]
        [ASN1Element ( Name = "visiblestring", IsOptional =  false , HasTag =  true, Tag = 10 , HasDefaultValue =  false )  ]
    
        public string Visiblestring
        {
            get { return visiblestring_; }
            set { selectVisiblestring(value); }
        }
        
                
          
        
	private string generalizedtime_ ;
        private bool  generalizedtime_selected = false ;
        
                
        [ASN1String( Name = "", 
        StringType = UniversalTags.GeneralizedTime , IsUCS = false )]
        [ASN1Element ( Name = "generalizedtime", IsOptional =  false , HasTag =  true, Tag = 11 , HasDefaultValue =  false )  ]
    
        public string Generalizedtime
        {
            get { return generalizedtime_; }
            set { selectGeneralizedtime(value); }
        }
        
                
          
        
	private TimeOfDay binarytime_ ;
        private bool  binarytime_selected = false ;
        
                
        
        [ASN1Element ( Name = "binarytime", IsOptional =  false , HasTag =  true, Tag = 12 , HasDefaultValue =  false )  ]
    
        public TimeOfDay Binarytime
        {
            get { return binarytime_; }
            set { selectBinarytime(value); }
        }
        
                
          
        
	private long bcd_ ;
        private bool  bcd_selected = false ;
        
                
        [ASN1Integer( Name = "" )]
    
        [ASN1Element ( Name = "bcd", IsOptional =  false , HasTag =  true, Tag = 13 , HasDefaultValue =  false )  ]
    
        public long Bcd
        {
            get { return bcd_; }
            set { selectBcd(value); }
        }
        
                
          
        
	private BitString booleanArray_ ;
        private bool  booleanArray_selected = false ;
        
                
        [ASN1BitString( Name = "" )]
    
        [ASN1Element ( Name = "booleanArray", IsOptional =  false , HasTag =  true, Tag = 14 , HasDefaultValue =  false )  ]
    
        public BitString BooleanArray
        {
            get { return booleanArray_; }
            set { selectBooleanArray(value); }
        }
        
                
          
        
	private MMSString mMSString_ ;
        private bool  mMSString_selected = false ;
        
                
        
        [ASN1Element ( Name = "mMSString", IsOptional =  false , HasTag =  true, Tag = 16 , HasDefaultValue =  false )  ]
    
        public MMSString MMSString
        {
            get { return mMSString_; }
            set { selectMMSString(value); }
        }
        
                
          
        
	private UtcTime utctime_ ;
        private bool  utctime_selected = false ;
        
                
        
        [ASN1Element ( Name = "utctime", IsOptional =  false , HasTag =  true, Tag = 17 , HasDefaultValue =  false )  ]
    
        public UtcTime Utctime
        {
            get { return utctime_; }
            set { selectUtctime(value); }
        }
        
                
          
        public bool isArraySelected () {
            return this.array_selected ;
        }

        


        public void selectArray (DataSequence val) {
            this.array_ = val;
            this.array_selected = true;
            
            
                    this.structure_selected = false;
                
                    this.boolean_selected = false;
                
                    this.bitstring_selected = false;
                
                    this.integer_selected = false;
                
                    this.unsigned_selected = false;
                
                    this.floatingpoint_selected = false;
                
                    this.octetstring_selected = false;
                
                    this.visiblestring_selected = false;
                
                    this.generalizedtime_selected = false;
                
                    this.binarytime_selected = false;
                
                    this.bcd_selected = false;
                
                    this.booleanArray_selected = false;
                
                    this.mMSString_selected = false;
                
                    this.utctime_selected = false;
                            
        }
        
          
        public bool isStructureSelected () {
            return this.structure_selected ;
        }

        


        public void selectStructure (DataSequence val) {
            this.structure_ = val;
            this.structure_selected = true;
            
            
                    this.array_selected = false;
                
                    this.boolean_selected = false;
                
                    this.bitstring_selected = false;
                
                    this.integer_selected = false;
                
                    this.unsigned_selected = false;
                
                    this.floatingpoint_selected = false;
                
                    this.octetstring_selected = false;
                
                    this.visiblestring_selected = false;
                
                    this.generalizedtime_selected = false;
                
                    this.binarytime_selected = false;
                
                    this.bcd_selected = false;
                
                    this.booleanArray_selected = false;
                
                    this.mMSString_selected = false;
                
                    this.utctime_selected = false;
                            
        }
        
          
        public bool isBooleanSelected () {
            return this.boolean_selected ;
        }

        


        public void selectBoolean (bool val) {
            bool fire = false;
            
            if (this.boolean_ != val)
                fire = true;                        

            this.boolean_ = val;
            this.boolean_selected = true;
            
            
                    this.array_selected = false;
                
                    this.structure_selected = false;
                
                    this.bitstring_selected = false;
                
                    this.integer_selected = false;
                
                    this.unsigned_selected = false;
                
                    this.floatingpoint_selected = false;
                
                    this.octetstring_selected = false;
                
                    this.visiblestring_selected = false;
                
                    this.generalizedtime_selected = false;
                
                    this.binarytime_selected = false;
                
                    this.bcd_selected = false;
                
                    this.booleanArray_selected = false;
                
                    this.mMSString_selected = false;
                
                    this.utctime_selected = false;

                if (ValueChanged != null && fire)
                {
                    ValueChanged(this, new EventArgs());
                }    
        }
        
          
        public bool isBitstringSelected () {
            return this.bitstring_selected ;
        }

        


        public void selectBitstring (BitString val) {
            bool fire = false;

            if (this.bitstring_ == null || this.bitstring_.Value != val.Value || this.bitstring_.TrailBitsCnt != val.TrailBitsCnt)
                fire = true;   
            
            this.bitstring_ = val;
            this.bitstring_selected = true;
            
            
                    this.array_selected = false;
                
                    this.structure_selected = false;
                
                    this.boolean_selected = false;
                
                    this.integer_selected = false;
                
                    this.unsigned_selected = false;
                
                    this.floatingpoint_selected = false;
                
                    this.octetstring_selected = false;
                
                    this.visiblestring_selected = false;
                
                    this.generalizedtime_selected = false;
                
                    this.binarytime_selected = false;
                
                    this.bcd_selected = false;
                
                    this.booleanArray_selected = false;
                
                    this.mMSString_selected = false;
                
                    this.utctime_selected = false;

                    if (ValueChanged != null && fire)
                    {
                        ValueChanged(this, new EventArgs());
                    }   
                            
        }
        
          
        public bool isIntegerSelected () {
            return this.integer_selected ;
        }

        


        public void selectInteger (long val) {
            bool fire = false;

            if (this.integer_ != val)
                fire = true;   

            this.integer_ = val;
            this.integer_selected = true;          
            
                    this.array_selected = false;
                
                    this.structure_selected = false;
                
                    this.boolean_selected = false;
                
                    this.bitstring_selected = false;
                
                    this.unsigned_selected = false;
                
                    this.floatingpoint_selected = false;
                
                    this.octetstring_selected = false;
                
                    this.visiblestring_selected = false;
                
                    this.generalizedtime_selected = false;
                
                    this.binarytime_selected = false;
                
                    this.bcd_selected = false;
                
                    this.booleanArray_selected = false;
                
                    this.mMSString_selected = false;
                
                    this.utctime_selected = false;

                    if (ValueChanged != null && fire)
                    {
                        ValueChanged(this, new EventArgs());
                    }                              
        }
        
          
        public bool isUnsignedSelected () {
            return this.unsigned_selected ;
        }

        


        public void selectUnsigned (long val) {
            this.unsigned_ = val;
            this.unsigned_selected = true;
            
            
                    this.array_selected = false;
                
                    this.structure_selected = false;
                
                    this.boolean_selected = false;
                
                    this.bitstring_selected = false;
                
                    this.integer_selected = false;
                
                    this.floatingpoint_selected = false;
                
                    this.octetstring_selected = false;
                
                    this.visiblestring_selected = false;
                
                    this.generalizedtime_selected = false;
                
                    this.binarytime_selected = false;
                
                    this.bcd_selected = false;
                
                    this.booleanArray_selected = false;
                
                    this.mMSString_selected = false;
                
                    this.utctime_selected = false;
                            
        }
        
          
        public bool isFloatingpointSelected () {
            return this.floatingpoint_selected ;
        }

        


        public void selectFloatingpoint (FloatingPoint val) {
            this.floatingpoint_ = val;
            this.floatingpoint_selected = true;
            
            
                    this.array_selected = false;
                
                    this.structure_selected = false;
                
                    this.boolean_selected = false;
                
                    this.bitstring_selected = false;
                
                    this.integer_selected = false;
                
                    this.unsigned_selected = false;
                
                    this.octetstring_selected = false;
                
                    this.visiblestring_selected = false;
                
                    this.generalizedtime_selected = false;
                
                    this.binarytime_selected = false;
                
                    this.bcd_selected = false;
                
                    this.booleanArray_selected = false;
                
                    this.mMSString_selected = false;
                
                    this.utctime_selected = false;
                            
        }
        
          
        public bool isOctetstringSelected () {
            return this.octetstring_selected ;
        }

        


        public void selectOctetstring (byte[] val) {
            this.octetstring_ = val;
            this.octetstring_selected = true;
            
            
                    this.array_selected = false;
                
                    this.structure_selected = false;
                
                    this.boolean_selected = false;
                
                    this.bitstring_selected = false;
                
                    this.integer_selected = false;
                
                    this.unsigned_selected = false;
                
                    this.floatingpoint_selected = false;
                
                    this.visiblestring_selected = false;
                
                    this.generalizedtime_selected = false;
                
                    this.binarytime_selected = false;
                
                    this.bcd_selected = false;
                
                    this.booleanArray_selected = false;
                
                    this.mMSString_selected = false;
                
                    this.utctime_selected = false;
                            
        }
        
          
        public bool isVisiblestringSelected () {
            return this.visiblestring_selected ;
        }

        


        public void selectVisiblestring (string val) {
            this.visiblestring_ = val;
            this.visiblestring_selected = true;
            
            
                    this.array_selected = false;
                
                    this.structure_selected = false;
                
                    this.boolean_selected = false;
                
                    this.bitstring_selected = false;
                
                    this.integer_selected = false;
                
                    this.unsigned_selected = false;
                
                    this.floatingpoint_selected = false;
                
                    this.octetstring_selected = false;
                
                    this.generalizedtime_selected = false;
                
                    this.binarytime_selected = false;
                
                    this.bcd_selected = false;
                
                    this.booleanArray_selected = false;
                
                    this.mMSString_selected = false;
                
                    this.utctime_selected = false;
                            
        }
        
          
        public bool isGeneralizedtimeSelected () {
            return this.generalizedtime_selected ;
        }

        


        public void selectGeneralizedtime (string val) {
            this.generalizedtime_ = val;
            this.generalizedtime_selected = true;
            
            
                    this.array_selected = false;
                
                    this.structure_selected = false;
                
                    this.boolean_selected = false;
                
                    this.bitstring_selected = false;
                
                    this.integer_selected = false;
                
                    this.unsigned_selected = false;
                
                    this.floatingpoint_selected = false;
                
                    this.octetstring_selected = false;
                
                    this.visiblestring_selected = false;
                
                    this.binarytime_selected = false;
                
                    this.bcd_selected = false;
                
                    this.booleanArray_selected = false;
                
                    this.mMSString_selected = false;
                
                    this.utctime_selected = false;
                            
        }
        
          
        public bool isBinarytimeSelected () {
            return this.binarytime_selected ;
        }

        


        public void selectBinarytime (TimeOfDay val) {
            this.binarytime_ = val;
            this.binarytime_selected = true;
            
            
                    this.array_selected = false;
                
                    this.structure_selected = false;
                
                    this.boolean_selected = false;
                
                    this.bitstring_selected = false;
                
                    this.integer_selected = false;
                
                    this.unsigned_selected = false;
                
                    this.floatingpoint_selected = false;
                
                    this.octetstring_selected = false;
                
                    this.visiblestring_selected = false;
                
                    this.generalizedtime_selected = false;
                
                    this.bcd_selected = false;
                
                    this.booleanArray_selected = false;
                
                    this.mMSString_selected = false;
                
                    this.utctime_selected = false;
                            
        }
        
          
        public bool isBcdSelected () {
            return this.bcd_selected ;
        }

        


        public void selectBcd (long val) {
            this.bcd_ = val;
            this.bcd_selected = true;
            
            
                    this.array_selected = false;
                
                    this.structure_selected = false;
                
                    this.boolean_selected = false;
                
                    this.bitstring_selected = false;
                
                    this.integer_selected = false;
                
                    this.unsigned_selected = false;
                
                    this.floatingpoint_selected = false;
                
                    this.octetstring_selected = false;
                
                    this.visiblestring_selected = false;
                
                    this.generalizedtime_selected = false;
                
                    this.binarytime_selected = false;
                
                    this.booleanArray_selected = false;
                
                    this.mMSString_selected = false;
                
                    this.utctime_selected = false;
                            
        }
        
          
        public bool isBooleanArraySelected () {
            return this.booleanArray_selected ;
        }

        


        public void selectBooleanArray (BitString val) {
            this.booleanArray_ = val;
            this.booleanArray_selected = true;
            
            
                    this.array_selected = false;
                
                    this.structure_selected = false;
                
                    this.boolean_selected = false;
                
                    this.bitstring_selected = false;
                
                    this.integer_selected = false;
                
                    this.unsigned_selected = false;
                
                    this.floatingpoint_selected = false;
                
                    this.octetstring_selected = false;
                
                    this.visiblestring_selected = false;
                
                    this.generalizedtime_selected = false;
                
                    this.binarytime_selected = false;
                
                    this.bcd_selected = false;
                
                    this.mMSString_selected = false;
                
                    this.utctime_selected = false;
                            
        }
        
          
        public bool isMMSStringSelected () {
            return this.mMSString_selected ;
        }

        


        public void selectMMSString (MMSString val) {
            this.mMSString_ = val;
            this.mMSString_selected = true;
            
            
                    this.array_selected = false;
                
                    this.structure_selected = false;
                
                    this.boolean_selected = false;
                
                    this.bitstring_selected = false;
                
                    this.integer_selected = false;
                
                    this.unsigned_selected = false;
                
                    this.floatingpoint_selected = false;
                
                    this.octetstring_selected = false;
                
                    this.visiblestring_selected = false;
                
                    this.generalizedtime_selected = false;
                
                    this.binarytime_selected = false;
                
                    this.bcd_selected = false;
                
                    this.booleanArray_selected = false;
                
                    this.utctime_selected = false;
                            
        }
        
          
        public bool isUtctimeSelected () {
            return this.utctime_selected ;
        }

        


        public void selectUtctime (UtcTime val) {
            this.utctime_ = val;
            this.utctime_selected = true;
            
            
                    this.array_selected = false;
                
                    this.structure_selected = false;
                
                    this.boolean_selected = false;
                
                    this.bitstring_selected = false;
                
                    this.integer_selected = false;
                
                    this.unsigned_selected = false;
                
                    this.floatingpoint_selected = false;
                
                    this.octetstring_selected = false;
                
                    this.visiblestring_selected = false;
                
                    this.generalizedtime_selected = false;
                
                    this.binarytime_selected = false;
                
                    this.bcd_selected = false;
                
                    this.booleanArray_selected = false;
                
                    this.mMSString_selected = false;
                            
        }
        
  

            public void initWithDefaults()
	    {
	    }

            private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(Data));
            public IASN1PreparedElementData PreparedData {
            	get { return preparedData; }
            }

        public string Description
        {
            get;
            set;
        }
    }
            
}
