namespace BISC.Modules.InformationModel.Presentation.Helpers
{
    public class ValueValidatorsHelper
    {
        private object validateDbOrZeroDbLockObject = new object();
        private int minDbValue = 0;
        private int maxDbValue = 100000;
        public bool ValidateDbOrZeroDb(string value)
        {
            lock (validateDbOrZeroDbLockObject)
            {
                int intValue;

                if (!int.TryParse(value, out intValue))
                {
                    return false;
                }

                if (intValue < minDbValue || intValue > maxDbValue)
                {
                    return false;
                }

                return true;
            }
        }

        public string DbOrZeroDbToolTip => $"Диапазон значений [{minDbValue} - {maxDbValue}]";

    }
}