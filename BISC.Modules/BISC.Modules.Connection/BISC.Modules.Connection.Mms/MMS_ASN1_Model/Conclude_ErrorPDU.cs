
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
    [ASN1BoxedType ( Name = "Conclude_ErrorPDU") ]
    public class Conclude_ErrorPDU: IASN1PreparedElement {
            
           
        private ServiceError  val;

        
        [ASN1Element ( Name = "Conclude-ErrorPDU", IsOptional =  false , HasTag =  false  , HasDefaultValue =  false )  ]
    
        public ServiceError Value
        {
                get { return val; }        
                    
                set { val = value; }
                        
        }            

                    
        
        public Conclude_ErrorPDU ()
        {
        }

            public void initWithDefaults()
	    {
	    }


            private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(Conclude_ErrorPDU));
            public IASN1PreparedElementData PreparedData {
            	get { return preparedData; }
            }

        
    }
            
}
