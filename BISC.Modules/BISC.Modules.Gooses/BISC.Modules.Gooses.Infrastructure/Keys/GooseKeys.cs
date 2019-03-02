using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.Gooses.Infrastructure.Keys
{
   public static class GooseKeys
    {
        public static class GooseModelKeys
        {
            public static string GooseInputKey => "Inputs";
            public static string ExternalGooseRefKey => "ExtRef";
            public static string GooseControlKey => "GSEControl";
            public static string SubscriberDeviceKey => "IEDName";
            public static string GooseRowKey => "GooseRow";
            public static string GooseMatrixKey => "GooseMatrix";

	        public static string GooseMatrixFtpKey => "GooseMatrixFtp";
	        public static string GoCbFtpEntityKey => "GoCbFtpEntity";
	        public static string GooseRowFtpEntityKey => "GooseRowFtpEntity";
	        public static string MacAddressEntityKey => "MacAddressEntity";
	        public static string GooseRowQualityFtpEntityKey => "GooseRowQualityFtpEntity";


		}
		public static class GoosePresentationKeys
        {
  
            public static string GooseGroupTreeItemViewKey => "GooseGroupTreeItemView";
            public static string GooseSubscriptionTabKey => "GooseSubscriptionTab";
            public static string GooseControlsTabKey => "GooseControlsTab";
            public static string GooseMatrixTabKey => "GooseMatrixTab";
            public static string GooseMatrixTabFieldKey => "GooseMatrixTabField";
            public static string GooseControlAssignmentViewKey => "GooseControlAssignmentView";
            public static string GooseControlsConflictsView => "GooseControlsConflictsView";
            public static string GooseControlsConflictContext => "GooseControlsConflictContext";

            public static string GooseMatrixViewKey => "GooseMatrixView";



        }
        public static class GooseWarningKeys
        {

            public static string GooseSavedFtpKey => "GooseSavedFtp";
            public static string ErrorGettingGooseOutOfDeviceKey => "ErrorGettingGooseOutOfDevice";



        }
    }
}
