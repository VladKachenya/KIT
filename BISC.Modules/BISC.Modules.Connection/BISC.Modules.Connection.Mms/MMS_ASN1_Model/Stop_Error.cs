
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
    [ASN1BoxedType ( Name = "Stop_Error") ]
    public class Stop_Error: IASN1PreparedElement {
            
           
        private ProgramInvocationState  val;

        
        [ASN1Element ( Name = "Stop-Error", IsOptional =  false , HasTag =  false  , HasDefaultValue =  false )  ]
    
        public ProgramInvocationState Value
        {
                get { return val; }        
                    
                set { val = value; }
                        
        }            

                    
        
        public Stop_Error ()
        {
        }

            public void initWithDefaults()
	    {
	    }


            private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(Stop_Error));
            public IASN1PreparedElementData PreparedData {
            	get { return preparedData; }
            }

        
    }
            
}
