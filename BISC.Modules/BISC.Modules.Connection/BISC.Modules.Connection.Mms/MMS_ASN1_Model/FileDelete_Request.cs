
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
    [ASN1BoxedType ( Name = "FileDelete_Request") ]
    public class FileDelete_Request: IASN1PreparedElement {
            
           
        private FileName  val;

        
        [ASN1Element ( Name = "FileDelete-Request", IsOptional =  false , HasTag =  false  , HasDefaultValue =  false )  ]
    
        public FileName Value
        {
                get { return val; }        
                    
                set { val = value; }
                        
        }            

                    
        
        public FileDelete_Request ()
        {
        }

            public void initWithDefaults()
	    {
	    }


            private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(FileDelete_Request));
            public IASN1PreparedElementData PreparedData {
            	get { return preparedData; }
            }

        
    }
            
}
