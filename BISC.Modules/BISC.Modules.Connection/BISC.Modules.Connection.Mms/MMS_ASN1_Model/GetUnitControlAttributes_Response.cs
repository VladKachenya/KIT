
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
    [ASN1Sequence ( Name = "GetUnitControlAttributes_Response", IsSet = false  )]
    public class GetUnitControlAttributes_Response : IASN1PreparedElement {
                    
	private System.Collections.Generic.ICollection<Identifier> domains_ ;
	
[ASN1SequenceOf( Name = "domains", IsSetOf = false  )]

    
        [ASN1Element ( Name = "domains", IsOptional =  false , HasTag =  true, Tag = 0 , HasDefaultValue =  false )  ]
    
        public System.Collections.Generic.ICollection<Identifier> Domains
        {
            get { return domains_; }
            set { domains_ = value;  }
        }
        
                
          
	private System.Collections.Generic.ICollection<Identifier> programInvocations_ ;
	
[ASN1SequenceOf( Name = "programInvocations", IsSetOf = false  )]

    
        [ASN1Element ( Name = "programInvocations", IsOptional =  false , HasTag =  true, Tag = 1 , HasDefaultValue =  false )  ]
    
        public System.Collections.Generic.ICollection<Identifier> ProgramInvocations
        {
            get { return programInvocations_; }
            set { programInvocations_ = value;  }
        }
        
                
  

            public void initWithDefaults() {
            	
            }


            private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(GetUnitControlAttributes_Response));
            public IASN1PreparedElementData PreparedData {
            	get { return preparedData; }
            }

            
    }
            
}
