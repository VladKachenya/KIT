
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
    [ASN1Sequence ( Name = "ChangeAccessControl_Request", IsSet = false  )]
    public class ChangeAccessControl_Request : IASN1PreparedElement {
                    
	private ScopeOfChangeChoiceType scopeOfChange_ ;
	

    [ASN1PreparedElement]    
    [ASN1Choice ( Name = "scopeOfChange" )]
    public class ScopeOfChangeChoiceType : IASN1PreparedElement  {
	            
        
	private NullObject vMDOnly_ ;
        private bool  vMDOnly_selected = false ;
        
                
        
        [ASN1Null ( Name = "vMDOnly" )]
    
        [ASN1Element ( Name = "vMDOnly", IsOptional =  false , HasTag =  true, Tag = 0 , HasDefaultValue =  false )  ]
    
        public NullObject VMDOnly
        {
            get { return vMDOnly_; }
            set { selectVMDOnly(value); }
        }
        
                
          
        
	private ListOfObjectsSequenceType listOfObjects_ ;
        private bool  listOfObjects_selected = false ;
        
                
        
       [ASN1PreparedElement]
       [ASN1Sequence ( Name = "listOfObjects", IsSet = false  )]
       public class ListOfObjectsSequenceType : IASN1PreparedElement {
                        
	private ObjectClass objectClass_ ;
	
        [ASN1Element ( Name = "objectClass", IsOptional =  false , HasTag =  true, Tag = 0 , HasDefaultValue =  false )  ]
    
        public ObjectClass ObjectClass
        {
            get { return objectClass_; }
            set { objectClass_ = value;  }
        }
        
                
          
	private ObjectScopeChoiceType objectScope_ ;
	

    [ASN1PreparedElement]    
    [ASN1Choice ( Name = "objectScope" )]
    public class ObjectScopeChoiceType : IASN1PreparedElement  {
	            
        
	private System.Collections.Generic.ICollection<ObjectName> specific_ ;
        private bool  specific_selected = false ;
        
                
        
[ASN1SequenceOf( Name = "specific", IsSetOf = false  )]

    
        [ASN1Element ( Name = "specific", IsOptional =  false , HasTag =  true, Tag = 0 , HasDefaultValue =  false )  ]
    
        public System.Collections.Generic.ICollection<ObjectName> Specific
        {
            get { return specific_; }
            set { selectSpecific(value); }
        }
        
                
          
        
	private NullObject aa_specific_ ;
        private bool  aa_specific_selected = false ;
        
                
        
        [ASN1Null ( Name = "aa-specific" )]
    
        [ASN1Element ( Name = "aa-specific", IsOptional =  false , HasTag =  true, Tag = 1 , HasDefaultValue =  false )  ]
    
        public NullObject Aa_specific
        {
            get { return aa_specific_; }
            set { selectAa_specific(value); }
        }
        
                
          
        
	private Identifier domain_ ;
        private bool  domain_selected = false ;
        
                
        
        [ASN1Element ( Name = "domain", IsOptional =  false , HasTag =  true, Tag = 2 , HasDefaultValue =  false )  ]
    
        public Identifier Domain
        {
            get { return domain_; }
            set { selectDomain(value); }
        }
        
                
          
        
	private NullObject vmd_ ;
        private bool  vmd_selected = false ;
        
                
        
        [ASN1Null ( Name = "vmd" )]
    
        [ASN1Element ( Name = "vmd", IsOptional =  false , HasTag =  true, Tag = 3 , HasDefaultValue =  false )  ]
    
        public NullObject Vmd
        {
            get { return vmd_; }
            set { selectVmd(value); }
        }
        
                
          
        public bool isSpecificSelected () {
            return this.specific_selected ;
        }

        


        public void selectSpecific (System.Collections.Generic.ICollection<ObjectName> val) {
            this.specific_ = val;
            this.specific_selected = true;
            
            
                    this.aa_specific_selected = false;
                
                    this.domain_selected = false;
                
                    this.vmd_selected = false;
                            
        }
        
          
        public bool isAa_specificSelected () {
            return this.aa_specific_selected ;
        }

        
        public void selectAa_specific () {
            selectAa_specific (new NullObject());
	}
	


        public void selectAa_specific (NullObject val) {
            this.aa_specific_ = val;
            this.aa_specific_selected = true;
            
            
                    this.specific_selected = false;
                
                    this.aa_specific_selected = false;
                
                    this.domain_selected = false;
                
                    this.vmd_selected = false;
                            
        }
        
          
        public bool isDomainSelected () {
            return this.domain_selected ;
        }

        


        public void selectDomain (Identifier val) {
            this.domain_ = val;
            this.domain_selected = true;
            
            
                    this.specific_selected = false;
                
                    this.aa_specific_selected = false;
                
                    this.vmd_selected = false;
                            
        }
        
          
        public bool isVmdSelected () {
            return this.vmd_selected ;
        }

        
        public void selectVmd () {
            selectVmd (new NullObject());
	}
	


        public void selectVmd (NullObject val) {
            this.vmd_ = val;
            this.vmd_selected = true;
            
            
                    this.specific_selected = false;
                
                    this.aa_specific_selected = false;
                
                    this.domain_selected = false;
                            
        }
        
  

            public void initWithDefaults()
	    {
	    }

            private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(ObjectScopeChoiceType));
            public IASN1PreparedElementData PreparedData {
            	get { return preparedData; }
            }

    }
                
        [ASN1Element ( Name = "objectScope", IsOptional =  false , HasTag =  true, Tag = 1 , HasDefaultValue =  false )  ]
    
        public ObjectScopeChoiceType ObjectScope
        {
            get { return objectScope_; }
            set { objectScope_ = value;  }
        }
        
                
  
                
                public void initWithDefaults() {
            		
                }

            private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(ListOfObjectsSequenceType));
            public IASN1PreparedElementData PreparedData {
            	get { return preparedData; }
            }

                
       }
                
        [ASN1Element ( Name = "listOfObjects", IsOptional =  false , HasTag =  true, Tag = 1 , HasDefaultValue =  false )  ]
    
        public ListOfObjectsSequenceType ListOfObjects
        {
            get { return listOfObjects_; }
            set { selectListOfObjects(value); }
        }
        
                
          
        public bool isVMDOnlySelected () {
            return this.vMDOnly_selected ;
        }

        
        public void selectVMDOnly () {
            selectVMDOnly (new NullObject());
	}
	


        public void selectVMDOnly (NullObject val) {
            this.vMDOnly_ = val;
            this.vMDOnly_selected = true;
            
            
                    this.listOfObjects_selected = false;
                            
        }
        
          
        public bool isListOfObjectsSelected () {
            return this.listOfObjects_selected ;
        }

        


        public void selectListOfObjects (ListOfObjectsSequenceType val) {
            this.listOfObjects_ = val;
            this.listOfObjects_selected = true;
            
            
                    this.vMDOnly_selected = false;
                            
        }
        
  

            public void initWithDefaults()
	    {
	    }

            private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(ScopeOfChangeChoiceType));
            public IASN1PreparedElementData PreparedData {
            	get { return preparedData; }
            }

    }
                
        [ASN1Element ( Name = "scopeOfChange", IsOptional =  false , HasTag =  false  , HasDefaultValue =  false )  ]
    
        public ScopeOfChangeChoiceType ScopeOfChange
        {
            get { return scopeOfChange_; }
            set { scopeOfChange_ = value;  }
        }
        
                
          
	private Identifier accessControlListName_ ;
	
        [ASN1Element ( Name = "accessControlListName", IsOptional =  false , HasTag =  true, Tag = 2 , HasDefaultValue =  false )  ]
    
        public Identifier AccessControlListName
        {
            get { return accessControlListName_; }
            set { accessControlListName_ = value;  }
        }
        
                
  

            public void initWithDefaults() {
            	
            }


            private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(ChangeAccessControl_Request));
            public IASN1PreparedElementData PreparedData {
            	get { return preparedData; }
            }

            
    }
            
}
