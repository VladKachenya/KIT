
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
    [ASN1BoxedType ( Name = "InitiateUploadSequence_Request") ]
    public class InitiateUploadSequence_Request: IASN1PreparedElement {
            
           
        private Identifier  val;

        
        [ASN1Element ( Name = "InitiateUploadSequence-Request", IsOptional =  false , HasTag =  false  , HasDefaultValue =  false )  ]
    
        public Identifier Value
        {
                get { return val; }        
                    
                set { val = value; }
                        
        }            

                    
        
        public InitiateUploadSequence_Request ()
        {
        }

            public void initWithDefaults()
	    {
	    }


            private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(InitiateUploadSequence_Request));
            public IASN1PreparedElementData PreparedData {
            	get { return preparedData; }
            }

        
    }
            
}
