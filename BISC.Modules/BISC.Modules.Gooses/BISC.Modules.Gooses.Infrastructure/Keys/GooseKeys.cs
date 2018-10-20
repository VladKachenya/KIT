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


        }
        public static class GoosePresentationKeys
        {
  
            public static string GooseGroupTreeItemViewKey => "GooseGroupTreeItemView";
            public static string GooseSubscriptionTabKey => "GooseSubscriptionTab";
            public static string GooseControlsTabKey => "GooseControlsTab";
            public static string GooseMatrixTabKey => "GooseMatrixTab";
            public static string GooseMatrixTabFieldKey => "GooseMatrixTabField";
            public static string GooseControlAssignmentViewKey => "GooseControlAssignmentView";

            public static string GooseMatrixViewKey => "GooseMatrixView";



        }
    }
}
