
//
// This file was generated by the BinaryNotes compiler.
// See http://bnotes.sourceforge.net 
// Any modifications to this file will be lost upon recompilation of the source ASN.1. 
//

using BISC.Modules.Connection.MMS.org.bn;
using BISC.Modules.Connection.MMS.org.bn.attributes;
using BISC.Modules.Connection.MMS.org.bn.coders;
using BISC.Modules.Connection.MMS.org.bn.types;

namespace BISC.Modules.Connection.MMS.MMS_ASN1_Model {


    [ASN1PreparedElement]
    [ASN1Sequence ( Name = "DomainManagementParameters", IsSet = false  )]
    public class DomainManagementParameters : IASN1PreparedElement {
                    
	private MMSString loadDataOctet_ ;
	
        [ASN1Element ( Name = "loadDataOctet", IsOptional =  false , HasTag =  true, Tag = 0 , HasDefaultValue =  false )  ]
    
        public MMSString LoadDataOctet
        {
            get { return loadDataOctet_; }
            set { loadDataOctet_ = value;  }
        }
        
                
          
	private System.Collections.Generic.ICollection<ObjectIdentifier> loadDataSyntax_ ;
	[ASN1ObjectIdentifier( Name = "" )]
    
[ASN1SequenceOf( Name = "loadDataSyntax", IsSetOf = false  )]

    
        [ASN1Element ( Name = "loadDataSyntax", IsOptional =  false , HasTag =  true, Tag = 1 , HasDefaultValue =  false )  ]
    
        public System.Collections.Generic.ICollection<ObjectIdentifier> LoadDataSyntax
        {
            get { return loadDataSyntax_; }
            set { loadDataSyntax_ = value;  }
        }
        
                
          
	private long maxUploads_ ;
	[ASN1Integer( Name = "" )]
    
        [ASN1Element ( Name = "maxUploads", IsOptional =  false , HasTag =  true, Tag = 2 , HasDefaultValue =  false )  ]
    
        public long MaxUploads
        {
            get { return maxUploads_; }
            set { maxUploads_ = value;  }
        }
        
                
  

            public void initWithDefaults() {
            	
            }


            private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(DomainManagementParameters));
            public IASN1PreparedElementData PreparedData {
            	get { return preparedData; }
            }

            
    }
            
}
