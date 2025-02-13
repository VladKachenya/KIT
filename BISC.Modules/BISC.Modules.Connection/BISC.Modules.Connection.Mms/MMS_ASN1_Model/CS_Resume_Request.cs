
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
    [ASN1BoxedType ( Name = "CS_Resume_Request") ]
    public class CS_Resume_Request: IASN1PreparedElement {
            
           
        private CS_Resume_RequestChoiceType  val;

        

    [ASN1PreparedElement]    
    [ASN1Choice ( Name = "CS-Resume-Request" )]
    public class CS_Resume_RequestChoiceType : IASN1PreparedElement  {
	            
        
	private NullObject normal_ ;
        private bool  normal_selected = false ;
        
                
        
        [ASN1Null ( Name = "normal" )]
    
        [ASN1Element ( Name = "normal", IsOptional =  false , HasTag =  false  , HasDefaultValue =  false )  ]
    
        public NullObject Normal
        {
            get { return normal_; }
            set { selectNormal(value); }
        }
        
                
          
        
	private ControllingSequenceType controlling_ ;
        private bool  controlling_selected = false ;
        
                
        
       [ASN1PreparedElement]
       [ASN1Sequence ( Name = "controlling", IsSet = false  )]
       public class ControllingSequenceType : IASN1PreparedElement {
                        
	private ModeTypeChoiceType modeType_ ;
	

    [ASN1PreparedElement]    
    [ASN1Choice ( Name = "modeType" )]
    public class ModeTypeChoiceType : IASN1PreparedElement  {
	            
        
	private NullObject continueMode_ ;
        private bool  continueMode_selected = false ;
        
                
        
        [ASN1Null ( Name = "continueMode" )]
    
        [ASN1Element ( Name = "continueMode", IsOptional =  false , HasTag =  true, Tag = 0 , HasDefaultValue =  false )  ]
    
        public NullObject ContinueMode
        {
            get { return continueMode_; }
            set { selectContinueMode(value); }
        }
        
                
          
        
	private StartCount changeMode_ ;
        private bool  changeMode_selected = false ;
        
                
        
        [ASN1Element ( Name = "changeMode", IsOptional =  false , HasTag =  true, Tag = 1 , HasDefaultValue =  false )  ]
    
        public StartCount ChangeMode
        {
            get { return changeMode_; }
            set { selectChangeMode(value); }
        }
        
                
          
        public bool isContinueModeSelected () {
            return this.continueMode_selected ;
        }

        
        public void selectContinueMode () {
            selectContinueMode (new NullObject());
	}
	


        public void selectContinueMode (NullObject val) {
            this.continueMode_ = val;
            this.continueMode_selected = true;
            
            
                    this.changeMode_selected = false;
                            
        }
        
          
        public bool isChangeModeSelected () {
            return this.changeMode_selected ;
        }

        


        public void selectChangeMode (StartCount val) {
            this.changeMode_ = val;
            this.changeMode_selected = true;
            
            
                    this.continueMode_selected = false;
                            
        }
        
  

            public void initWithDefaults()
	    {
	    }

            private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(ModeTypeChoiceType));
            public IASN1PreparedElementData PreparedData {
            	get { return preparedData; }
            }

    }
                
        [ASN1Element ( Name = "modeType", IsOptional =  false , HasTag =  false  , HasDefaultValue =  false )  ]
    
        public ModeTypeChoiceType ModeType
        {
            get { return modeType_; }
            set { modeType_ = value;  }
        }
        
                
  
                
                public void initWithDefaults() {
            		
                }

            private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(ControllingSequenceType));
            public IASN1PreparedElementData PreparedData {
            	get { return preparedData; }
            }

                
       }
                
        [ASN1Element ( Name = "controlling", IsOptional =  false , HasTag =  false  , HasDefaultValue =  false )  ]
    
        public ControllingSequenceType Controlling
        {
            get { return controlling_; }
            set { selectControlling(value); }
        }
        
                
          
        public bool isNormalSelected () {
            return this.normal_selected ;
        }

        
        public void selectNormal () {
            selectNormal (new NullObject());
	}
	


        public void selectNormal (NullObject val) {
            this.normal_ = val;
            this.normal_selected = true;
            
            
                    this.controlling_selected = false;
                            
        }
        
          
        public bool isControllingSelected () {
            return this.controlling_selected ;
        }

        


        public void selectControlling (ControllingSequenceType val) {
            this.controlling_ = val;
            this.controlling_selected = true;
            
            
                    this.normal_selected = false;
                            
        }
        
  

            public void initWithDefaults()
	    {
	    }

            private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(CS_Resume_RequestChoiceType));
            public IASN1PreparedElementData PreparedData {
            	get { return preparedData; }
            }

    }
                
        [ASN1Element ( Name = "CS-Resume-Request", IsOptional =  false , HasTag =  true, Tag = 0 , HasDefaultValue =  false )  ]
    
        public CS_Resume_RequestChoiceType Value
        {
                get { return val; }        
                    
                set { val = value; }
                        
        }            

                    
        
        public CS_Resume_Request ()
        {
        }

            public void initWithDefaults()
	    {
	    }


            private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(CS_Resume_Request));
            public IASN1PreparedElementData PreparedData {
            	get { return preparedData; }
            }

        
    }
            
}
