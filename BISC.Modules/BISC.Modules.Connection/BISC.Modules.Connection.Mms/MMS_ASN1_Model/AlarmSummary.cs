
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
    [ASN1Sequence ( Name = "AlarmSummary", IsSet = false  )]
    public class AlarmSummary : IASN1PreparedElement {
                    
	private ObjectName eventConditionName_ ;
	
        [ASN1Element ( Name = "eventConditionName", IsOptional =  false , HasTag =  true, Tag = 0 , HasDefaultValue =  false )  ]
    
        public ObjectName EventConditionName
        {
            get { return eventConditionName_; }
            set { eventConditionName_ = value;  }
        }
        
                
          
	private Unsigned8 severity_ ;
	
        [ASN1Element ( Name = "severity", IsOptional =  false , HasTag =  true, Tag = 1 , HasDefaultValue =  false )  ]
    
        public Unsigned8 Severity
        {
            get { return severity_; }
            set { severity_ = value;  }
        }
        
                
          
	private EC_State currentState_ ;
	
        [ASN1Element ( Name = "currentState", IsOptional =  false , HasTag =  true, Tag = 2 , HasDefaultValue =  false )  ]
    
        public EC_State CurrentState
        {
            get { return currentState_; }
            set { currentState_ = value;  }
        }
        
                
          
	private long unacknowledgedState_ ;
	[ASN1Integer( Name = "" )]
    
        [ASN1Element ( Name = "unacknowledgedState", IsOptional =  false , HasTag =  true, Tag = 3 , HasDefaultValue =  false )  ]
    
        public long UnacknowledgedState
        {
            get { return unacknowledgedState_; }
            set { unacknowledgedState_ = value;  }
        }
        
                
          
	private EN_Additional_Detail displayEnhancement_ ;
	
        private bool  displayEnhancement_present = false ;
	
        [ASN1Element ( Name = "displayEnhancement", IsOptional =  true , HasTag =  true, Tag = 4 , HasDefaultValue =  false )  ]
    
        public EN_Additional_Detail DisplayEnhancement
        {
            get { return displayEnhancement_; }
            set { displayEnhancement_ = value; displayEnhancement_present = true;  }
        }
        
                
          
	private EventTime timeOfLastTransitionToActive_ ;
	
        private bool  timeOfLastTransitionToActive_present = false ;
	
        [ASN1Element ( Name = "timeOfLastTransitionToActive", IsOptional =  true , HasTag =  true, Tag = 5 , HasDefaultValue =  false )  ]
    
        public EventTime TimeOfLastTransitionToActive
        {
            get { return timeOfLastTransitionToActive_; }
            set { timeOfLastTransitionToActive_ = value; timeOfLastTransitionToActive_present = true;  }
        }
        
                
          
	private EventTime timeOfLastTransitionToIdle_ ;
	
        private bool  timeOfLastTransitionToIdle_present = false ;
	
        [ASN1Element ( Name = "timeOfLastTransitionToIdle", IsOptional =  true , HasTag =  true, Tag = 6 , HasDefaultValue =  false )  ]
    
        public EventTime TimeOfLastTransitionToIdle
        {
            get { return timeOfLastTransitionToIdle_; }
            set { timeOfLastTransitionToIdle_ = value; timeOfLastTransitionToIdle_present = true;  }
        }
        
                
  
        public bool isDisplayEnhancementPresent () {
            return this.displayEnhancement_present == true;
        }
        
        public bool isTimeOfLastTransitionToActivePresent () {
            return this.timeOfLastTransitionToActive_present == true;
        }
        
        public bool isTimeOfLastTransitionToIdlePresent () {
            return this.timeOfLastTransitionToIdle_present == true;
        }
        

            public void initWithDefaults() {
            	
            }


            private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(AlarmSummary));
            public IASN1PreparedElementData PreparedData {
            	get { return preparedData; }
            }

            
    }
            
}
