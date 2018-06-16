using System;

namespace BISC.Model.Iec61850Ed2.Common
{	
	///<summary>
	/// This class defines a [Required] attribute, which one identify at SCL class if it 
	/// contains a required variable.
	/// </summary>		
    [AttributeUsage(AttributeTargets.Property)]
    public class RequiredAttribute : Attribute
    {
	    public bool Required { get; set; }

	    public string ErrorMessage { get; set; }

	    /// <summary>
        /// Constructor: This method is to indicate if the attribute is required
        /// </summary>
        public RequiredAttribute()
        {
            this.Required = true;
        }
      
        /// <summary>
        /// Constructor: This method identify if the attribute is required
        /// </summary>
        /// <param name="required">
        /// His value is true if the attribute is required and False in other case.
        /// </param>
        public RequiredAttribute(bool required)
        {
            this.Required = required;
        }

        /// <summary>
        /// Constructor: This method sends an error message when the attribute is empty
        /// </summary>
        /// <param name="errorMessage">
        /// Text to identify the error
        /// </param>
        public RequiredAttribute(string errorMessage)
        {
            this.ErrorMessage = errorMessage;
        }
    }
}
