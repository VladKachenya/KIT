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
	        public static string GooseMatrixFtpKey => "GooseMatrixFtp";
	        public static string GoCbFtpEntityKey => "GoCbFtpEntity";
	        public static string GooseRowFtpEntityKey => "GooseRowFtpEntity";
	        public static string MacAddressEntityKey => "MacAddressEntity";
	        public static string GooseRowQualityFtpEntityKey => "GooseRowQualityFtpEntity";
	        public static string GooseInputModelInfoKey => "GooseInputModelInfo";

            public static string GooseDeviceInputKey => "GooseDeviceInput";

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
            public static string GooseControlUnsavedWarningTagKey => "GooseControlUnsavedWarningTagKey";
            public static string GooseSubscriptionUnsavedWarningTagKey => "GooseSubscriptionUnsavedWarningTagKey";

        }

        public static class GooseSubscriptionPresentationKeys
        {
            public static string Validity = nameof(Validity);
            public static string Quality = nameof(Quality);
            public static string State = nameof(State);
            public static string Goose = nameof(Goose);
        }

        public static class GoInNameKeys
        {
            public static string IndicationResetKey = "Сброс индикации";
            public static string FaultResetKey = "Сброс неисправности";
            public static string SystemLogResetKey = "Сброс ЖС";
            public static string AlarmLogResetKey = "Сброс ЖА";
            public static string TurnOffBreaker = "Откл. В";
            public static string TurnOnBreaker = "Вкл. В";
        }

        public enum GooseSubscriptionRowType
        {
            State,
            Quality,
            Validity,
            Goose,
            Separator
        }
    }
}
