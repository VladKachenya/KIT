
//
// This file was generated by the BinaryNotes compiler.
// See http://bnotes.sourceforge.net 
// Any modifications to this file will be lost upon recompilation of the source ASN.1. 
//

using BISC.Modules.Connection.MMS.org.bn;
using BISC.Modules.Connection.MMS.org.bn.attributes;
using BISC.Modules.Connection.MMS.org.bn.coders;

namespace BISC.Modules.Connection.MMS.MMS_ASN1_Model {


    [ASN1PreparedElement]
    [ASN1Sequence ( Name = "DataParameters", IsSet = false  )]
    public class DataParameters : IASN1PreparedElement {
                    
	private long bit_string_ ;
	
        private bool  bit_string_present = false ;
	[ASN1Integer( Name = "" )]
    
        [ASN1Element ( Name = "bit-string", IsOptional =  true , HasTag =  true, Tag = 0 , HasDefaultValue =  false )  ]
    
        public long Bit_string
        {
            get { return bit_string_; }
            set { bit_string_ = value; bit_string_present = true;  }
        }
        
                
          
	private long integer_ ;
	
        private bool  integer_present = false ;
	[ASN1Integer( Name = "" )]
    
        [ASN1Element ( Name = "integer", IsOptional =  true , HasTag =  true, Tag = 1 , HasDefaultValue =  false )  ]
    
        public long Integer
        {
            get { return integer_; }
            set { integer_ = value; integer_present = true;  }
        }
        
                
          
	private long unsigned_ ;
	
        private bool  unsigned_present = false ;
	[ASN1Integer( Name = "" )]
    
        [ASN1Element ( Name = "unsigned", IsOptional =  true , HasTag =  true, Tag = 2 , HasDefaultValue =  false )  ]
    
        public long Unsigned
        {
            get { return unsigned_; }
            set { unsigned_ = value; unsigned_present = true;  }
        }
        
                
          
	private Floating_pointSequenceType floating_point_ ;
	
        private bool  floating_point_present = false ;
	
       [ASN1PreparedElement]
       [ASN1Sequence ( Name = "floating-point", IsSet = false  )]
       public class Floating_pointSequenceType : IASN1PreparedElement {
                        
	private long total_ ;
	[ASN1Integer( Name = "" )]
    
        [ASN1Element ( Name = "total", IsOptional =  false , HasTag =  true, Tag = 4 , HasDefaultValue =  false )  ]
    
        public long Total
        {
            get { return total_; }
            set { total_ = value;  }
        }
        
                
          
	private long exponent_ ;
	[ASN1Integer( Name = "" )]
    
        [ASN1Element ( Name = "exponent", IsOptional =  false , HasTag =  true, Tag = 5 , HasDefaultValue =  false )  ]
    
        public long Exponent
        {
            get { return exponent_; }
            set { exponent_ = value;  }
        }
        
                
  
                
                public void initWithDefaults() {
            		
                }

            private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(Floating_pointSequenceType));
            public IASN1PreparedElementData PreparedData {
            	get { return preparedData; }
            }

                
       }
                
        [ASN1Element ( Name = "floating-point", IsOptional =  true , HasTag =  true, Tag = 3 , HasDefaultValue =  false )  ]
    
        public Floating_pointSequenceType Floating_point
        {
            get { return floating_point_; }
            set { floating_point_ = value; floating_point_present = true;  }
        }
        
                
          
	private long octet_string_ ;
	
        private bool  octet_string_present = false ;
	[ASN1Integer( Name = "" )]
    
        [ASN1Element ( Name = "octet-string", IsOptional =  true , HasTag =  true, Tag = 10 , HasDefaultValue =  false )  ]
    
        public long Octet_string
        {
            get { return octet_string_; }
            set { octet_string_ = value; octet_string_present = true;  }
        }
        
                
          
	private long visible_string_ ;
	
        private bool  visible_string_present = false ;
	[ASN1Integer( Name = "" )]
    
        [ASN1Element ( Name = "visible-string", IsOptional =  true , HasTag =  true, Tag = 11 , HasDefaultValue =  false )  ]
    
        public long Visible_string
        {
            get { return visible_string_; }
            set { visible_string_ = value; visible_string_present = true;  }
        }
        
                
          
	private bool binary_time_ ;
	
        private bool  binary_time_present = false ;
	[ASN1Boolean( Name = "" )]
    
        [ASN1Element ( Name = "binary-time", IsOptional =  true , HasTag =  true, Tag = 12 , HasDefaultValue =  false )  ]
    
        public bool Binary_time
        {
            get { return binary_time_; }
            set { binary_time_ = value; binary_time_present = true;  }
        }
        
                
          
	private long bcd_ ;
	
        private bool  bcd_present = false ;
	[ASN1Integer( Name = "" )]
    
        [ASN1Element ( Name = "bcd", IsOptional =  true , HasTag =  true, Tag = 13 , HasDefaultValue =  false )  ]
    
        public long Bcd
        {
            get { return bcd_; }
            set { bcd_ = value; bcd_present = true;  }
        }
        
                
          
	private long mmsString_ ;
	
        private bool  mmsString_present = false ;
	[ASN1Integer( Name = "" )]
    
        [ASN1Element ( Name = "mmsString", IsOptional =  true , HasTag =  true, Tag = 14 , HasDefaultValue =  false )  ]
    
        public long MmsString
        {
            get { return mmsString_; }
            set { mmsString_ = value; mmsString_present = true;  }
        }
        
                
  
        public bool isBit_stringPresent () {
            return this.bit_string_present == true;
        }
        
        public bool isIntegerPresent () {
            return this.integer_present == true;
        }
        
        public bool isUnsignedPresent () {
            return this.unsigned_present == true;
        }
        
        public bool isFloating_pointPresent () {
            return this.floating_point_present == true;
        }
        
        public bool isOctet_stringPresent () {
            return this.octet_string_present == true;
        }
        
        public bool isVisible_stringPresent () {
            return this.visible_string_present == true;
        }
        
        public bool isBinary_timePresent () {
            return this.binary_time_present == true;
        }
        
        public bool isBcdPresent () {
            return this.bcd_present == true;
        }
        
        public bool isMmsStringPresent () {
            return this.mmsString_present == true;
        }
        

            public void initWithDefaults() {
            	
            }


            private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(DataParameters));
            public IASN1PreparedElementData PreparedData {
            	get { return preparedData; }
            }

            
    }
            
}
