
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
    [ASN1Choice ( Name = "AccessCondition") ]
    public class AccessCondition : IASN1PreparedElement {
                    
        
	private NullObject never_ ;
        private bool  never_selected = false ;
        
                
        
        [ASN1Null ( Name = "never" )]
    
        [ASN1Element ( Name = "never", IsOptional =  false , HasTag =  true, Tag = 0 , HasDefaultValue =  false )  ]
    
        public NullObject Never
        {
            get { return never_; }
            set { selectNever(value); }
        }
        
                
          
        
	private Identifier semaphore_ ;
        private bool  semaphore_selected = false ;
        
                
        
        [ASN1Element ( Name = "semaphore", IsOptional =  false , HasTag =  true, Tag = 1 , HasDefaultValue =  false )  ]
    
        public Identifier Semaphore
        {
            get { return semaphore_; }
            set { selectSemaphore(value); }
        }
        
                
          
        
	private UserChoiceType user_ ;
        private bool  user_selected = false ;
        
                
        

    [ASN1PreparedElement]    
    [ASN1Choice ( Name = "user" )]
    public class UserChoiceType : IASN1PreparedElement  {
	            
        
	private ApplicationReference association_ ;
        private bool  association_selected = false ;
        
                
        
        [ASN1Element ( Name = "association", IsOptional =  false , HasTag =  false  , HasDefaultValue =  false )  ]
    
        public ApplicationReference Association
        {
            get { return association_; }
            set { selectAssociation(value); }
        }
        
                
          
        
	private NullObject none_ ;
        private bool  none_selected = false ;
        
                
        
        [ASN1Null ( Name = "none" )]
    
        [ASN1Element ( Name = "none", IsOptional =  false , HasTag =  false  , HasDefaultValue =  false )  ]
    
        public NullObject None
        {
            get { return none_; }
            set { selectNone(value); }
        }
        
                
          
        public bool isAssociationSelected () {
            return this.association_selected ;
        }

        


        public void selectAssociation (ApplicationReference val) {
            this.association_ = val;
            this.association_selected = true;
            
            
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
            
            
                    this.association_selected = false;
                            
        }
        
  

            public void initWithDefaults()
	    {
	    }

            private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(UserChoiceType));
            public IASN1PreparedElementData PreparedData {
            	get { return preparedData; }
            }

    }
                
        [ASN1Element ( Name = "user", IsOptional =  false , HasTag =  true, Tag = 2 , HasDefaultValue =  false )  ]
    
        public UserChoiceType User
        {
            get { return user_; }
            set { selectUser(value); }
        }
        
                
          
        
	private Authentication_value password_ ;
        private bool  password_selected = false ;
        
                
        
        [ASN1Element ( Name = "password", IsOptional =  false , HasTag =  true, Tag = 3 , HasDefaultValue =  false )  ]
    
        public Authentication_value Password
        {
            get { return password_; }
            set { selectPassword(value); }
        }
        
                
          
        
	private System.Collections.Generic.ICollection<AccessCondition> joint_ ;
        private bool  joint_selected = false ;
        
                
        
[ASN1SequenceOf( Name = "joint", IsSetOf = false  )]

    
        [ASN1Element ( Name = "joint", IsOptional =  false , HasTag =  true, Tag = 4 , HasDefaultValue =  false )  ]
    
        public System.Collections.Generic.ICollection<AccessCondition> Joint
        {
            get { return joint_; }
            set { selectJoint(value); }
        }
        
                
          
        
	private System.Collections.Generic.ICollection<AccessCondition> alternate_ ;
        private bool  alternate_selected = false ;
        
                
        
[ASN1SequenceOf( Name = "alternate", IsSetOf = false  )]

    
        [ASN1Element ( Name = "alternate", IsOptional =  false , HasTag =  true, Tag = 5 , HasDefaultValue =  false )  ]
    
        public System.Collections.Generic.ICollection<AccessCondition> Alternate
        {
            get { return alternate_; }
            set { selectAlternate(value); }
        }
        
                
          
        public bool isNeverSelected () {
            return this.never_selected ;
        }

        
        public void selectNever () {
            selectNever (new NullObject());
	}
	


        public void selectNever (NullObject val) {
            this.never_ = val;
            this.never_selected = true;
            
            
                    this.semaphore_selected = false;
                
                    this.user_selected = false;
                
                    this.password_selected = false;
                
                    this.joint_selected = false;
                
                    this.alternate_selected = false;
                            
        }
        
          
        public bool isSemaphoreSelected () {
            return this.semaphore_selected ;
        }

        


        public void selectSemaphore (Identifier val) {
            this.semaphore_ = val;
            this.semaphore_selected = true;
            
            
                    this.never_selected = false;
                
                    this.user_selected = false;
                
                    this.password_selected = false;
                
                    this.joint_selected = false;
                
                    this.alternate_selected = false;
                            
        }
        
          
        public bool isUserSelected () {
            return this.user_selected ;
        }

        


        public void selectUser (UserChoiceType val) {
            this.user_ = val;
            this.user_selected = true;
            
            
                    this.never_selected = false;
                
                    this.semaphore_selected = false;
                
                    this.password_selected = false;
                
                    this.joint_selected = false;
                
                    this.alternate_selected = false;
                            
        }
        
          
        public bool isPasswordSelected () {
            return this.password_selected ;
        }

        


        public void selectPassword (Authentication_value val) {
            this.password_ = val;
            this.password_selected = true;
            
            
                    this.never_selected = false;
                
                    this.semaphore_selected = false;
                
                    this.user_selected = false;
                
                    this.joint_selected = false;
                
                    this.alternate_selected = false;
                            
        }
        
          
        public bool isJointSelected () {
            return this.joint_selected ;
        }

        


        public void selectJoint (System.Collections.Generic.ICollection<AccessCondition> val) {
            this.joint_ = val;
            this.joint_selected = true;
            
            
                    this.never_selected = false;
                
                    this.semaphore_selected = false;
                
                    this.user_selected = false;
                
                    this.password_selected = false;
                
                    this.alternate_selected = false;
                            
        }
        
          
        public bool isAlternateSelected () {
            return this.alternate_selected ;
        }

        


        public void selectAlternate (System.Collections.Generic.ICollection<AccessCondition> val) {
            this.alternate_ = val;
            this.alternate_selected = true;
            
            
                    this.never_selected = false;
                
                    this.semaphore_selected = false;
                
                    this.user_selected = false;
                
                    this.password_selected = false;
                
                    this.joint_selected = false;
                            
        }
        
  

            public void initWithDefaults()
	    {
	    }

            private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(AccessCondition));
            public IASN1PreparedElementData PreparedData {
            	get { return preparedData; }
            }

    }
            
}
