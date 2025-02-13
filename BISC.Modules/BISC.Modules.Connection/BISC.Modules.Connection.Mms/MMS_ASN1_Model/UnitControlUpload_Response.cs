
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
    [ASN1Sequence ( Name = "UnitControlUpload_Response", IsSet = false  )]
    public class UnitControlUpload_Response : IASN1PreparedElement {
                    
	private System.Collections.Generic.ICollection<ControlElement> controlElements_ ;
	
[ASN1SequenceOf( Name = "controlElements", IsSetOf = false  )]

    
        [ASN1Element ( Name = "controlElements", IsOptional =  false , HasTag =  true, Tag = 0 , HasDefaultValue =  false )  ]
    
        public System.Collections.Generic.ICollection<ControlElement> ControlElements
        {
            get { return controlElements_; }
            set { controlElements_ = value;  }
        }
        
                
          
	private NextElementChoiceType nextElement_ ;
	
        private bool  nextElement_present = false ;
	

    [ASN1PreparedElement]    
    [ASN1Choice ( Name = "nextElement" )]
    public class NextElementChoiceType : IASN1PreparedElement  {
	            
        
	private Identifier domain_ ;
        private bool  domain_selected = false ;
        
                
        
        [ASN1Element ( Name = "domain", IsOptional =  false , HasTag =  true, Tag = 1 , HasDefaultValue =  false )  ]
    
        public Identifier Domain
        {
            get { return domain_; }
            set { selectDomain(value); }
        }
        
                
          
        
	private long ulsmID_ ;
        private bool  ulsmID_selected = false ;
        
                
        [ASN1Integer( Name = "" )]
    
        [ASN1Element ( Name = "ulsmID", IsOptional =  false , HasTag =  true, Tag = 2 , HasDefaultValue =  false )  ]
    
        public long UlsmID
        {
            get { return ulsmID_; }
            set { selectUlsmID(value); }
        }
        
                
          
        
	private Identifier programInvocation_ ;
        private bool  programInvocation_selected = false ;
        
                
        
        [ASN1Element ( Name = "programInvocation", IsOptional =  false , HasTag =  true, Tag = 3 , HasDefaultValue =  false )  ]
    
        public Identifier ProgramInvocation
        {
            get { return programInvocation_; }
            set { selectProgramInvocation(value); }
        }
        
                
          
        public bool isDomainSelected () {
            return this.domain_selected ;
        }

        


        public void selectDomain (Identifier val) {
            this.domain_ = val;
            this.domain_selected = true;
            
            
                    this.ulsmID_selected = false;
                
                    this.programInvocation_selected = false;
                            
        }
        
          
        public bool isUlsmIDSelected () {
            return this.ulsmID_selected ;
        }

        


        public void selectUlsmID (long val) {
            this.ulsmID_ = val;
            this.ulsmID_selected = true;
            
            
                    this.domain_selected = false;
                
                    this.programInvocation_selected = false;
                            
        }
        
          
        public bool isProgramInvocationSelected () {
            return this.programInvocation_selected ;
        }

        


        public void selectProgramInvocation (Identifier val) {
            this.programInvocation_ = val;
            this.programInvocation_selected = true;
            
            
                    this.domain_selected = false;
                
                    this.ulsmID_selected = false;
                            
        }
        
  

            public void initWithDefaults()
	    {
	    }

            private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(NextElementChoiceType));
            public IASN1PreparedElementData PreparedData {
            	get { return preparedData; }
            }

    }
                
        [ASN1Element ( Name = "nextElement", IsOptional =  true , HasTag =  false  , HasDefaultValue =  false )  ]
    
        public NextElementChoiceType NextElement
        {
            get { return nextElement_; }
            set { nextElement_ = value; nextElement_present = true;  }
        }
        
                
  
        public bool isNextElementPresent () {
            return this.nextElement_present == true;
        }
        

            public void initWithDefaults() {
            	
            }


            private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(UnitControlUpload_Response));
            public IASN1PreparedElementData PreparedData {
            	get { return preparedData; }
            }

            
    }
            
}
