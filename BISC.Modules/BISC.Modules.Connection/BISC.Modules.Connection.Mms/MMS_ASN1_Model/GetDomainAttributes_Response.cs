
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
    [ASN1Sequence ( Name = "GetDomainAttributes_Response", IsSet = false  )]
    public class GetDomainAttributes_Response : IASN1PreparedElement {
                    
	private System.Collections.Generic.ICollection<MMSString> listOfCapabilities_ ;
	
[ASN1SequenceOf( Name = "listOfCapabilities", IsSetOf = false  )]

    
        [ASN1Element ( Name = "listOfCapabilities", IsOptional =  false , HasTag =  true, Tag = 0 , HasDefaultValue =  false )  ]
    
        public System.Collections.Generic.ICollection<MMSString> ListOfCapabilities
        {
            get { return listOfCapabilities_; }
            set { listOfCapabilities_ = value;  }
        }
        
                
          
	private DomainState state_ ;
	
        [ASN1Element ( Name = "state", IsOptional =  false , HasTag =  true, Tag = 1 , HasDefaultValue =  false )  ]
    
        public DomainState State
        {
            get { return state_; }
            set { state_ = value;  }
        }
        
                
          
	private bool mmsDeletable_ ;
	[ASN1Boolean( Name = "" )]
    
        [ASN1Element ( Name = "mmsDeletable", IsOptional =  false , HasTag =  true, Tag = 2 , HasDefaultValue =  false )  ]
    
        public bool MmsDeletable
        {
            get { return mmsDeletable_; }
            set { mmsDeletable_ = value;  }
        }
        
                
          
	private bool sharable_ ;
	[ASN1Boolean( Name = "" )]
    
        [ASN1Element ( Name = "sharable", IsOptional =  false , HasTag =  true, Tag = 3 , HasDefaultValue =  false )  ]
    
        public bool Sharable
        {
            get { return sharable_; }
            set { sharable_ = value;  }
        }
        
                
          
	private System.Collections.Generic.ICollection<Identifier> listOfProgramInvocations_ ;
	
[ASN1SequenceOf( Name = "listOfProgramInvocations", IsSetOf = false  )]

    
        [ASN1Element ( Name = "listOfProgramInvocations", IsOptional =  false , HasTag =  true, Tag = 4 , HasDefaultValue =  false )  ]
    
        public System.Collections.Generic.ICollection<Identifier> ListOfProgramInvocations
        {
            get { return listOfProgramInvocations_; }
            set { listOfProgramInvocations_ = value;  }
        }
        
                
          
	private Integer8 uploadInProgress_ ;
	
        [ASN1Element ( Name = "uploadInProgress", IsOptional =  false , HasTag =  true, Tag = 5 , HasDefaultValue =  false )  ]
    
        public Integer8 UploadInProgress
        {
            get { return uploadInProgress_; }
            set { uploadInProgress_ = value;  }
        }
        
                
          
	private Identifier accessControlList_ ;
	
        private bool  accessControlList_present = false ;
	
        [ASN1Element ( Name = "accessControlList", IsOptional =  true , HasTag =  true, Tag = 6 , HasDefaultValue =  false )  ]
    
        public Identifier AccessControlList
        {
            get { return accessControlList_; }
            set { accessControlList_ = value; accessControlList_present = true;  }
        }
        
                
  
        public bool isAccessControlListPresent () {
            return this.accessControlList_present == true;
        }
        

            public void initWithDefaults() {
            	
            }


            private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(GetDomainAttributes_Response));
            public IASN1PreparedElementData PreparedData {
            	get { return preparedData; }
            }

            
    }
            
}
