
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
    [ASN1Sequence ( Name = "ApplicationReference", IsSet = false  )]
    public class ApplicationReference : IASN1PreparedElement {
                    
	private AP_title ap_title_ ;
	
        private bool  ap_title_present = false ;
	
        [ASN1Element ( Name = "ap-title", IsOptional =  true , HasTag =  true, Tag = 0 , HasDefaultValue =  false )  ]
    
        public AP_title Ap_title
        {
            get { return ap_title_; }
            set { ap_title_ = value; ap_title_present = true;  }
        }
        
                
          
	private AP_invocation_identifier ap_invocation_id_ ;
	
        private bool  ap_invocation_id_present = false ;
	
        [ASN1Element ( Name = "ap-invocation-id", IsOptional =  true , HasTag =  true, Tag = 1 , HasDefaultValue =  false )  ]
    
        public AP_invocation_identifier Ap_invocation_id
        {
            get { return ap_invocation_id_; }
            set { ap_invocation_id_ = value; ap_invocation_id_present = true;  }
        }
        
                
          
	private AE_qualifier ae_qualifier_ ;
	
        private bool  ae_qualifier_present = false ;
	
        [ASN1Element ( Name = "ae-qualifier", IsOptional =  true , HasTag =  true, Tag = 2 , HasDefaultValue =  false )  ]
    
        public AE_qualifier Ae_qualifier
        {
            get { return ae_qualifier_; }
            set { ae_qualifier_ = value; ae_qualifier_present = true;  }
        }
        
                
          
	private AE_invocation_identifier ae_invocation_id_ ;
	
        private bool  ae_invocation_id_present = false ;
	
        [ASN1Element ( Name = "ae-invocation-id", IsOptional =  true , HasTag =  true, Tag = 3 , HasDefaultValue =  false )  ]
    
        public AE_invocation_identifier Ae_invocation_id
        {
            get { return ae_invocation_id_; }
            set { ae_invocation_id_ = value; ae_invocation_id_present = true;  }
        }
        
                
  
        public bool isAp_titlePresent () {
            return this.ap_title_present == true;
        }
        
        public bool isAp_invocation_idPresent () {
            return this.ap_invocation_id_present == true;
        }
        
        public bool isAe_qualifierPresent () {
            return this.ae_qualifier_present == true;
        }
        
        public bool isAe_invocation_idPresent () {
            return this.ae_invocation_id_present == true;
        }
        

            public void initWithDefaults() {
            	
            }


            private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(ApplicationReference));
            public IASN1PreparedElementData PreparedData {
            	get { return preparedData; }
            }

            
    }
            
}
