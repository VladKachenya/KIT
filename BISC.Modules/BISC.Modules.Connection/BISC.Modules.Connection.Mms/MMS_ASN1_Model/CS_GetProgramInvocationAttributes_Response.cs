
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
    [ASN1Sequence ( Name = "CS_GetProgramInvocationAttributes_Response", IsSet = false  )]
    public class CS_GetProgramInvocationAttributes_Response : IASN1PreparedElement {
                    
	private long errorCode_ ;
	[ASN1Integer( Name = "" )]
    
        [ASN1Element ( Name = "errorCode", IsOptional =  false , HasTag =  true, Tag = 0 , HasDefaultValue =  false )  ]
    
        public long ErrorCode
        {
            get { return errorCode_; }
            set { errorCode_ = value;  }
        }
        
                
          
	private ControlChoiceType control_ ;
	

    [ASN1PreparedElement]    
    [ASN1Choice ( Name = "control" )]
    public class ControlChoiceType : IASN1PreparedElement  {
	            
        
	private ControllingSequenceType controlling_ ;
        private bool  controlling_selected = false ;
        
                
        
       [ASN1PreparedElement]
       [ASN1Sequence ( Name = "controlling", IsSet = false  )]
       public class ControllingSequenceType : IASN1PreparedElement {
                        
	private System.Collections.Generic.ICollection<Identifier> controlledPI_ ;
	
[ASN1SequenceOf( Name = "controlledPI", IsSetOf = false  )]

    
        [ASN1Element ( Name = "controlledPI", IsOptional =  false , HasTag =  true, Tag = 0 , HasDefaultValue =  false )  ]
    
        public System.Collections.Generic.ICollection<Identifier> ControlledPI
        {
            get { return controlledPI_; }
            set { controlledPI_ = value;  }
        }
        
                
          
	private string programLocation_ ;
	
        private bool  programLocation_present = false ;
	[ASN1String( Name = "", 
        StringType =  UniversalTags.VisibleString , IsUCS = false )]
        [ASN1Element ( Name = "programLocation", IsOptional =  true , HasTag =  true, Tag = 1 , HasDefaultValue =  false )  ]
    
        public string ProgramLocation
        {
            get { return programLocation_; }
            set { programLocation_ = value; programLocation_present = true;  }
        }
        
                
          
	private RunningModeChoiceType runningMode_ ;
	

    [ASN1PreparedElement]    
    [ASN1Choice ( Name = "runningMode" )]
    public class RunningModeChoiceType : IASN1PreparedElement  {
	            
        
	private NullObject freeRunning_ ;
        private bool  freeRunning_selected = false ;
        
                
        
        [ASN1Null ( Name = "freeRunning" )]
    
        [ASN1Element ( Name = "freeRunning", IsOptional =  false , HasTag =  true, Tag = 0 , HasDefaultValue =  false )  ]
    
        public NullObject FreeRunning
        {
            get { return freeRunning_; }
            set { selectFreeRunning(value); }
        }
        
                
          
        
	private long cycleLimited_ ;
        private bool  cycleLimited_selected = false ;
        
                
        [ASN1Integer( Name = "" )]
    
        [ASN1Element ( Name = "cycleLimited", IsOptional =  false , HasTag =  true, Tag = 1 , HasDefaultValue =  false )  ]
    
        public long CycleLimited
        {
            get { return cycleLimited_; }
            set { selectCycleLimited(value); }
        }
        
                
          
        
	private long stepLimited_ ;
        private bool  stepLimited_selected = false ;
        
                
        [ASN1Integer( Name = "" )]
    
        [ASN1Element ( Name = "stepLimited", IsOptional =  false , HasTag =  true, Tag = 2 , HasDefaultValue =  false )  ]
    
        public long StepLimited
        {
            get { return stepLimited_; }
            set { selectStepLimited(value); }
        }
        
                
          
        public bool isFreeRunningSelected () {
            return this.freeRunning_selected ;
        }

        
        public void selectFreeRunning () {
            selectFreeRunning (new NullObject());
	}
	


        public void selectFreeRunning (NullObject val) {
            this.freeRunning_ = val;
            this.freeRunning_selected = true;
            
            
                    this.cycleLimited_selected = false;
                
                    this.stepLimited_selected = false;
                            
        }
        
          
        public bool isCycleLimitedSelected () {
            return this.cycleLimited_selected ;
        }

        


        public void selectCycleLimited (long val) {
            this.cycleLimited_ = val;
            this.cycleLimited_selected = true;
            
            
                    this.freeRunning_selected = false;
                
                    this.stepLimited_selected = false;
                            
        }
        
          
        public bool isStepLimitedSelected () {
            return this.stepLimited_selected ;
        }

        


        public void selectStepLimited (long val) {
            this.stepLimited_ = val;
            this.stepLimited_selected = true;
            
            
                    this.freeRunning_selected = false;
                
                    this.cycleLimited_selected = false;
                            
        }
        
  

            public void initWithDefaults()
	    {
	    }

            private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(RunningModeChoiceType));
            public IASN1PreparedElementData PreparedData {
            	get { return preparedData; }
            }

    }
                
        [ASN1Element ( Name = "runningMode", IsOptional =  false , HasTag =  true, Tag = 2 , HasDefaultValue =  false )  ]
    
        public RunningModeChoiceType RunningMode
        {
            get { return runningMode_; }
            set { runningMode_ = value;  }
        }
        
                
  
        public bool isProgramLocationPresent () {
            return this.programLocation_present == true;
        }
        
                
                public void initWithDefaults() {
            		
                }

            private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(ControllingSequenceType));
            public IASN1PreparedElementData PreparedData {
            	get { return preparedData; }
            }

                
       }
                
        [ASN1Element ( Name = "controlling", IsOptional =  false , HasTag =  true, Tag = 0 , HasDefaultValue =  false )  ]
    
        public ControllingSequenceType Controlling
        {
            get { return controlling_; }
            set { selectControlling(value); }
        }
        
                
          
        
	private ControlledChoiceType controlled_ ;
        private bool  controlled_selected = false ;
        
                
        

    [ASN1PreparedElement]    
    [ASN1Choice ( Name = "controlled" )]
    public class ControlledChoiceType : IASN1PreparedElement  {
	            
        
	private Identifier controllingPI_ ;
        private bool  controllingPI_selected = false ;
        
                
        
        [ASN1Element ( Name = "controllingPI", IsOptional =  false , HasTag =  true, Tag = 0 , HasDefaultValue =  false )  ]
    
        public Identifier ControllingPI
        {
            get { return controllingPI_; }
            set { selectControllingPI(value); }
        }
        
                
          
        
	private NullObject none_ ;
        private bool  none_selected = false ;
        
                
        
        [ASN1Null ( Name = "none" )]
    
        [ASN1Element ( Name = "none", IsOptional =  false , HasTag =  true, Tag = 1 , HasDefaultValue =  false )  ]
    
        public NullObject None
        {
            get { return none_; }
            set { selectNone(value); }
        }
        
                
          
        public bool isControllingPISelected () {
            return this.controllingPI_selected ;
        }

        


        public void selectControllingPI (Identifier val) {
            this.controllingPI_ = val;
            this.controllingPI_selected = true;
            
            
                    this.none_selected = false;
                            
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
            
            
                    this.controllingPI_selected = false;
                            
        }
        
  

            public void initWithDefaults()
	    {
	    }

            private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(ControlledChoiceType));
            public IASN1PreparedElementData PreparedData {
            	get { return preparedData; }
            }

    }
                
        [ASN1Element ( Name = "controlled", IsOptional =  false , HasTag =  true, Tag = 1 , HasDefaultValue =  false )  ]
    
        public ControlledChoiceType Controlled
        {
            get { return controlled_; }
            set { selectControlled(value); }
        }
        
                
          
        
	private NullObject normal_ ;
        private bool  normal_selected = false ;
        
                
        
        [ASN1Null ( Name = "normal" )]
    
        [ASN1Element ( Name = "normal", IsOptional =  false , HasTag =  true, Tag = 2 , HasDefaultValue =  false )  ]
    
        public NullObject Normal
        {
            get { return normal_; }
            set { selectNormal(value); }
        }
        
                
          
        public bool isControllingSelected () {
            return this.controlling_selected ;
        }

        


        public void selectControlling (ControllingSequenceType val) {
            this.controlling_ = val;
            this.controlling_selected = true;
            
            
                    this.controlled_selected = false;
                
                    this.normal_selected = false;
                            
        }
        
          
        public bool isControlledSelected () {
            return this.controlled_selected ;
        }

        


        public void selectControlled (ControlledChoiceType val) {
            this.controlled_ = val;
            this.controlled_selected = true;
            
            
                    this.controlling_selected = false;
                
                    this.normal_selected = false;
                            
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
                
                    this.controlled_selected = false;
                            
        }
        
  

            public void initWithDefaults()
	    {
	    }

            private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(ControlChoiceType));
            public IASN1PreparedElementData PreparedData {
            	get { return preparedData; }
            }

    }
                
        [ASN1Element ( Name = "control", IsOptional =  false , HasTag =  true, Tag = 1 , HasDefaultValue =  false )  ]
    
        public ControlChoiceType Control
        {
            get { return control_; }
            set { control_ = value;  }
        }
        
                
  

            public void initWithDefaults() {
            	
            }


            private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(CS_GetProgramInvocationAttributes_Response));
            public IASN1PreparedElementData PreparedData {
            	get { return preparedData; }
            }

            
    }
            
}
