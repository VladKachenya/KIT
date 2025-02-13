
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
    [ASN1Sequence ( Name = "JournalEntry", IsSet = false  )]
    public class JournalEntry : IASN1PreparedElement {
                    
	private byte[] entryIdentifier_ ;
	[ASN1OctetString( Name = "" )]
    
        [ASN1Element ( Name = "entryIdentifier", IsOptional =  false , HasTag =  true, Tag = 0 , HasDefaultValue =  false )  ]
    
        public byte[] EntryIdentifier
        {
            get { return entryIdentifier_; }
            set { entryIdentifier_ = value;  }
        }
        
                
          
	private ApplicationReference originatingApplication_ ;
	
        [ASN1Element ( Name = "originatingApplication", IsOptional =  false , HasTag =  true, Tag = 1 , HasDefaultValue =  false )  ]
    
        public ApplicationReference OriginatingApplication
        {
            get { return originatingApplication_; }
            set { originatingApplication_ = value;  }
        }
        
                
          
	private EntryContent entryContent_ ;
	
        [ASN1Element ( Name = "entryContent", IsOptional =  false , HasTag =  true, Tag = 2 , HasDefaultValue =  false )  ]
    
        public EntryContent EntryContent
        {
            get { return entryContent_; }
            set { entryContent_ = value;  }
        }
        
                
  

            public void initWithDefaults() {
            	
            }


            private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(JournalEntry));
            public IASN1PreparedElementData PreparedData {
            	get { return preparedData; }
            }

            
    }
            
}
