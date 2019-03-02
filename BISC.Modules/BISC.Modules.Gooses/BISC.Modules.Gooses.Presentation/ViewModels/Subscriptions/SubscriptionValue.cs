namespace BISC.Modules.Gooses.Presentation.ViewModels.Subscriptions
{
   public class SubscriptionValue
    {

        public SubscriptionValue(bool? isSelected, bool isValueEditable=true)
        {
            IsSelected = isSelected;
            IsValueEditable = isValueEditable;
        }
        public bool? IsSelected { get; set; }
        public bool IsValueEditable { get;  } 
    }
}
