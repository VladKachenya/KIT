
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
    [ASN1BoxedType ( Name = "FileRead_Request") ]
    public class FileRead_Request: IASN1PreparedElement {
            
           
        private Integer32  val;

        
        [ASN1Element ( Name = "FileRead-Request", IsOptional =  false , HasTag =  false  , HasDefaultValue =  false )  ]
    
        public Integer32 Value
        {
                get { return val; }        
                    
                set { val = value; }
                        
        }            

                    
        
        public FileRead_Request ()
        {
        }

            public void initWithDefaults()
	    {
	    }


            private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(FileRead_Request));
            public IASN1PreparedElementData PreparedData {
            	get { return preparedData; }
            }

        
    }
            
}
