
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
    [ASN1Sequence ( Name = "InitiateDownloadSequence_Request", IsSet = false  )]
    public class InitiateDownloadSequence_Request : IASN1PreparedElement {
                    
	private Identifier domainName_ ;
	
        [ASN1Element ( Name = "domainName", IsOptional =  false , HasTag =  true, Tag = 0 , HasDefaultValue =  false )  ]
    
        public Identifier DomainName
        {
            get { return domainName_; }
            set { domainName_ = value;  }
        }
        
                
          
	private System.Collections.Generic.ICollection<MMSString> listOfCapabilities_ ;
	
[ASN1SequenceOf( Name = "listOfCapabilities", IsSetOf = false  )]

    
        [ASN1Element ( Name = "listOfCapabilities", IsOptional =  false , HasTag =  true, Tag = 1 , HasDefaultValue =  false )  ]
    
        public System.Collections.Generic.ICollection<MMSString> ListOfCapabilities
        {
            get { return listOfCapabilities_; }
            set { listOfCapabilities_ = value;  }
        }
        
                
          
	private bool sharable_ ;
	[ASN1Boolean( Name = "" )]
    
        [ASN1Element ( Name = "sharable", IsOptional =  false , HasTag =  true, Tag = 2 , HasDefaultValue =  false )  ]
    
        public bool Sharable
        {
            get { return sharable_; }
            set { sharable_ = value;  }
        }
        
                
  

            public void initWithDefaults() {
            	
            }


            private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(InitiateDownloadSequence_Request));
            public IASN1PreparedElementData PreparedData {
            	get { return preparedData; }
            }

            
    }
            
}
