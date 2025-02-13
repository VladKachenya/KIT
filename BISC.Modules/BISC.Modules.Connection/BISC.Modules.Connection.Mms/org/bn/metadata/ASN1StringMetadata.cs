/*
 * Copyright 2006 Abdulla G. Abdurakhmanov (abdulla.abdurakhmanov@gmail.com).
 * 
 * Licensed under the LGPL, Version 2 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *      http://www.gnu.org/copyleft/lgpl.html
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * 
 * With any your questions welcome to my e-mail 
 * or blog at http://abdulla-a.blogspot.com.
 */

using System;
using System.IO;
using System.Reflection;
using BISC.Modules.Connection.MMS.org.bn.attributes;
using BISC.Modules.Connection.MMS.org.bn.coders;

namespace BISC.Modules.Connection.MMS.org.bn.metadata
{
    public class ASN1StringMetadata : ASN1FieldMetadata
    {
        private bool isUCS = false;
        private int     stringType = UniversalTags.PrintableString ;
        //private bool hasDefaults = false;
        
        public ASN1StringMetadata() {
            //hasDefaults = true;
        }

        public ASN1StringMetadata(ASN1String annotation)
            : this(annotation.Name, annotation.IsUCS, annotation.StringType)
        {
        }

        public ASN1StringMetadata(String  name,
                                  bool isUCS,
                                  int     stringType): base(name)
        {
            this.isUCS = isUCS;
            this.stringType = stringType;
        }

        public bool IsUCS
        {
            get { return isUCS; }
        }

        public int StringType
        {
            get { return stringType; }
        }

        public override void setParentAnnotated(ICustomAttributeProvider parent) {
            if(parent!=null) {
                if(CoderUtils.isAttributePresent<ASN1String>(parent)) {
                    ASN1String value = CoderUtils.getAttribute<ASN1String>(parent);
                    stringType = value.StringType;
                }    
            }        
        }


        public override int encode(IASN1TypesEncoder encoder, object obj, Stream stream, ElementInfo elementInfo) 
        {
            return encoder.encodeString(obj, stream, elementInfo);
        }

        public override DecodedObject<object> decode(IASN1TypesDecoder decoder, DecodedObject<object> decodedTag, Type objectClass, ElementInfo elementInfo, Stream stream) 
        {
            return decoder.decodeString(decodedTag,objectClass,elementInfo,stream);
        }    
    }
}

