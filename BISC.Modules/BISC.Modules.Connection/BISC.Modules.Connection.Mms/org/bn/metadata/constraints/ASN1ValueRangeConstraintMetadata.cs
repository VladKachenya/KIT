/*
 * Copyright 2007 Abdulla G. Abdurakhmanov (abdulla.abdurakhmanov@gmail.com).
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

using BISC.Modules.Connection.MMS.org.bn.attributes.constraints;

namespace BISC.Modules.Connection.MMS.org.bn.metadata.constraints
{
    public class ASN1ValueRangeConstraintMetadata : IASN1ConstraintMetadata 
    {
        private long minValue, maxValue;
        
        public ASN1ValueRangeConstraintMetadata(ASN1ValueRangeConstraint annotation) 
        {
            this.minValue = annotation.Min;
            this.maxValue = annotation.Max;
        }
        
        public long Min {
            get { return minValue; }
        }
        
        public long Max {
            get { return maxValue; }
        }
        
        public bool checkValue(long value) 
        {
            return value<= maxValue && value>= minValue;
        }
    }
}
