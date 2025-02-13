
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
    [ASN1Sequence ( Name = "DefineEventAction_Request", IsSet = false  )]
    public class DefineEventAction_Request : IASN1PreparedElement {
                    
	private ObjectName eventActionName_ ;
	
        [ASN1Element ( Name = "eventActionName", IsOptional =  false , HasTag =  true, Tag = 0 , HasDefaultValue =  false )  ]
    
        public ObjectName EventActionName
        {
            get { return eventActionName_; }
            set { eventActionName_ = value;  }
        }
        
                
          
	private System.Collections.Generic.ICollection<Modifier> listOfModifier_ ;
	
        private bool  listOfModifier_present = false ;
	
[ASN1SequenceOf( Name = "listOfModifier", IsSetOf = false  )]

    
        [ASN1Element ( Name = "listOfModifier", IsOptional =  true , HasTag =  true, Tag = 1 , HasDefaultValue =  false )  ]
    
        public System.Collections.Generic.ICollection<Modifier> ListOfModifier
        {
            get { return listOfModifier_; }
            set { listOfModifier_ = value; listOfModifier_present = true;  }
        }
        
                
          
	private ConfirmedServiceRequest confirmedServiceRequest_ ;
	
        [ASN1Element ( Name = "confirmedServiceRequest", IsOptional =  false , HasTag =  true, Tag = 2 , HasDefaultValue =  false )  ]
    
        public ConfirmedServiceRequest ConfirmedServiceRequest
        {
            get { return confirmedServiceRequest_; }
            set { confirmedServiceRequest_ = value;  }
        }
        
                
          
	private Request_Detail cs_extension_ ;
	
        private bool  cs_extension_present = false ;
	
        [ASN1Element ( Name = "cs-extension", IsOptional =  true , HasTag =  true, Tag = 79 , HasDefaultValue =  false )  ]
    
        public Request_Detail Cs_extension
        {
            get { return cs_extension_; }
            set { cs_extension_ = value; cs_extension_present = true;  }
        }
        
                
  
        public bool isListOfModifierPresent () {
            return this.listOfModifier_present == true;
        }
        
        public bool isCs_extensionPresent () {
            return this.cs_extension_present == true;
        }
        

            public void initWithDefaults() {
            	
            }


            private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(DefineEventAction_Request));
            public IASN1PreparedElementData PreparedData {
            	get { return preparedData; }
            }

            
    }
            
}
