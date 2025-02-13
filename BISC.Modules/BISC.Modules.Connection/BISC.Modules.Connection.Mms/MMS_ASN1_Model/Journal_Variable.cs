
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
    [ASN1Sequence ( Name = "Journal_Variable", IsSet = false  )]
    public class Journal_Variable : IASN1PreparedElement {
                    
	private MMS255String variableTag_ ;
	
        [ASN1Element ( Name = "variableTag", IsOptional =  false , HasTag =  false  , HasDefaultValue =  false )  ]
    
        public MMS255String VariableTag
        {
            get { return variableTag_; }
            set { variableTag_ = value;  }
        }
        
                
          
	private Data valueSpecification_ ;
	
        [ASN1Element ( Name = "valueSpecification", IsOptional =  false , HasTag =  false  , HasDefaultValue =  false )  ]
    
        public Data ValueSpecification
        {
            get { return valueSpecification_; }
            set { valueSpecification_ = value;  }
        }
        
                
  

            public void initWithDefaults() {
            	
            }


            private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(Journal_Variable));
            public IASN1PreparedElementData PreparedData {
            	get { return preparedData; }
            }

            
    }
            
}
