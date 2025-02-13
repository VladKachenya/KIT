
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
    [ASN1Sequence ( Name = "FileOpen_Response", IsSet = false  )]
    public class FileOpen_Response : IASN1PreparedElement {
                    
	private Integer32 frsmID_ ;
	
        [ASN1Element ( Name = "frsmID", IsOptional =  false , HasTag =  true, Tag = 0 , HasDefaultValue =  false )  ]
    
        public Integer32 FrsmID
        {
            get { return frsmID_; }
            set { frsmID_ = value;  }
        }
        
                
          
	private FileAttributes fileAttributes_ ;
	
        [ASN1Element ( Name = "fileAttributes", IsOptional =  false , HasTag =  true, Tag = 1 , HasDefaultValue =  false )  ]
    
        public FileAttributes FileAttributes
        {
            get { return fileAttributes_; }
            set { fileAttributes_ = value;  }
        }
        
                
  

            public void initWithDefaults() {
            	
            }


            private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(FileOpen_Response));
            public IASN1PreparedElementData PreparedData {
            	get { return preparedData; }
            }

            
    }
            
}
