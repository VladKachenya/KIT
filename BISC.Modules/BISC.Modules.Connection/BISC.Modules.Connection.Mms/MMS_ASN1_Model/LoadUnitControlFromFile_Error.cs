
//
// This file was generated by the BinaryNotes compiler.
// See http://bnotes.sourceforge.net 
// Any modifications to this file will be lost upon recompilation of the source ASN.1. 
//

using BISC.Modules.Connection.MMS.org.bn;
using BISC.Modules.Connection.MMS.org.bn.attributes;
using BISC.Modules.Connection.MMS.org.bn.coders;
using BISC.Modules.Connection.MMS.org.bn.types;

namespace BISC.Modules.Connection.MMS.MMS_ASN1_Model {


    [ASN1PreparedElement]
    [ASN1Choice ( Name = "LoadUnitControlFromFile_Error") ]
    public class LoadUnitControlFromFile_Error : IASN1PreparedElement {
                    
        
	private NullObject none_ ;
        private bool  none_selected = false ;
        
                
        
        [ASN1Null ( Name = "none" )]
    
        [ASN1Element ( Name = "none", IsOptional =  false , HasTag =  true, Tag = 0 , HasDefaultValue =  false )  ]
    
        public NullObject None
        {
            get { return none_; }
            set { selectNone(value); }
        }
        
                
          
        
	private Identifier domain_ ;
        private bool  domain_selected = false ;
        
                
        
        [ASN1Element ( Name = "domain", IsOptional =  false , HasTag =  true, Tag = 1 , HasDefaultValue =  false )  ]
    
        public Identifier Domain
        {
            get { return domain_; }
            set { selectDomain(value); }
        }
        
                
          
        
	private Identifier programInvocation_ ;
        private bool  programInvocation_selected = false ;
        
                
        
        [ASN1Element ( Name = "programInvocation", IsOptional =  false , HasTag =  true, Tag = 2 , HasDefaultValue =  false )  ]
    
        public Identifier ProgramInvocation
        {
            get { return programInvocation_; }
            set { selectProgramInvocation(value); }
        }
        
                
          
        public bool isNoneSelected () {
            return this.none_selected ;
        }

        
        public void selectNone () {
            selectNone (new NullObject());
	}
	


        public void selectNone (NullObject val) {
            this.none_ = val;
            this.none_selected = true;
            
            
                    this.domain_selected = false;
                
                    this.programInvocation_selected = false;
                            
        }
        
          
        public bool isDomainSelected () {
            return this.domain_selected ;
        }

        


        public void selectDomain (Identifier val) {
            this.domain_ = val;
            this.domain_selected = true;
            
            
                    this.none_selected = false;
                
                    this.programInvocation_selected = false;
                            
        }
        
          
        public bool isProgramInvocationSelected () {
            return this.programInvocation_selected ;
        }

        


        public void selectProgramInvocation (Identifier val) {
            this.programInvocation_ = val;
            this.programInvocation_selected = true;
            
            
                    this.none_selected = false;
                
                    this.domain_selected = false;
                            
        }
        
  

            public void initWithDefaults()
	    {
	    }

            private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(LoadUnitControlFromFile_Error));
            public IASN1PreparedElementData PreparedData {
            	get { return preparedData; }
            }

    }
            
}
