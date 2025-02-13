
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
    [ASN1Sequence ( Name = "ReportPoolSemaphoreStatus_Request", IsSet = false  )]
    public class ReportPoolSemaphoreStatus_Request : IASN1PreparedElement {
                    
	private ObjectName semaphoreName_ ;
	
        [ASN1Element ( Name = "semaphoreName", IsOptional =  false , HasTag =  true, Tag = 0 , HasDefaultValue =  false )  ]
    
        public ObjectName SemaphoreName
        {
            get { return semaphoreName_; }
            set { semaphoreName_ = value;  }
        }
        
                
          
	private Identifier nameToStartAfter_ ;
	
        private bool  nameToStartAfter_present = false ;
	
        [ASN1Element ( Name = "nameToStartAfter", IsOptional =  true , HasTag =  true, Tag = 1 , HasDefaultValue =  false )  ]
    
        public Identifier NameToStartAfter
        {
            get { return nameToStartAfter_; }
            set { nameToStartAfter_ = value; nameToStartAfter_present = true;  }
        }
        
                
  
        public bool isNameToStartAfterPresent () {
            return this.nameToStartAfter_present == true;
        }
        

            public void initWithDefaults() {
            	
            }


            private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(ReportPoolSemaphoreStatus_Request));
            public IASN1PreparedElementData PreparedData {
            	get { return preparedData; }
            }

            
    }
            
}
