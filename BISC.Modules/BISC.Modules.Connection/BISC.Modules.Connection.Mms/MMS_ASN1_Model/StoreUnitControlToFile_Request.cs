
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
    [ASN1Sequence ( Name = "StoreUnitControlToFile_Request", IsSet = false  )]
    public class StoreUnitControlToFile_Request : IASN1PreparedElement {
                    
	private Identifier unitControlName_ ;
	
        [ASN1Element ( Name = "unitControlName", IsOptional =  false , HasTag =  true, Tag = 0 , HasDefaultValue =  false )  ]
    
        public Identifier UnitControlName
        {
            get { return unitControlName_; }
            set { unitControlName_ = value;  }
        }
        
                
          
	private FileName fileName_ ;
	
        [ASN1Element ( Name = "fileName", IsOptional =  false , HasTag =  true, Tag = 1 , HasDefaultValue =  false )  ]
    
        public FileName FileName
        {
            get { return fileName_; }
            set { fileName_ = value;  }
        }
        
                
          
	private ApplicationReference thirdParty_ ;
	
        private bool  thirdParty_present = false ;
	
        [ASN1Element ( Name = "thirdParty", IsOptional =  true , HasTag =  true, Tag = 2 , HasDefaultValue =  false )  ]
    
        public ApplicationReference ThirdParty
        {
            get { return thirdParty_; }
            set { thirdParty_ = value; thirdParty_present = true;  }
        }
        
                
  
        public bool isThirdPartyPresent () {
            return this.thirdParty_present == true;
        }
        

            public void initWithDefaults() {
            	
            }


            private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(StoreUnitControlToFile_Request));
            public IASN1PreparedElementData PreparedData {
            	get { return preparedData; }
            }

            
    }
            
}
