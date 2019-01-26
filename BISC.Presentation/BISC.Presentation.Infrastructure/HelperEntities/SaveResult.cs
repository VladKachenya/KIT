namespace BISC.Presentation.Infrastructure.HelperEntities
{
    public class SaveResult
    {
        public bool IsSaved { get; set; }
        public bool IsDeclined { get; set; }
        public bool IsCancelled { get; set; }
        public  bool IsValidationFailed { get; set; }
    }
}