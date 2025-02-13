
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
    [ASN1Sequence ( Name = "AlterProgramInvocationAttributes_Request", IsSet = false  )]
    public class AlterProgramInvocationAttributes_Request : IASN1PreparedElement {
                    
	private Identifier programInvocation_ ;
	
        [ASN1Element ( Name = "programInvocation", IsOptional =  false , HasTag =  true, Tag = 0 , HasDefaultValue =  false )  ]
    
        public Identifier ProgramInvocation
        {
            get { return programInvocation_; }
            set { programInvocation_ = value;  }
        }
        
                
          
	private StartCount startCount_ ;
	
        [ASN1Element ( Name = "startCount", IsOptional =  false , HasTag =  true, Tag = 1 , HasDefaultValue =  true )  ]
    
        public StartCount StartCount
        {
            get { return startCount_; }
            set { startCount_ = value;  }
        }
        
                
  

            public void initWithDefaults() {
            	StartCount param_StartCount =         
            null;
        StartCount = param_StartCount;
    
            }


            private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(AlterProgramInvocationAttributes_Request));
            public IASN1PreparedElementData PreparedData {
            	get { return preparedData; }
            }

            
    }
            
}
