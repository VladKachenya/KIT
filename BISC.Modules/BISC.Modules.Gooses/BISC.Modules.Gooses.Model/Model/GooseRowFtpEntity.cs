using BISC.Model.Global.Model;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.Gooses.Infrastructure.Keys;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;

namespace BISC.Modules.Gooses.Model.Model
{
   public class GooseRowFtpEntity:ModelElement, IGooseRowFtpEntity
    {
        public GooseRowFtpEntity()
        {
            ElementName = GooseKeys.GooseModelKeys.GooseRowFtpEntityKey;
        }


        #region Implementation of IGooseRowFtpEntity

        public int IndexOfGoose { get; set; }
        public int NumberOfFcdaInDataSetOfGoose { get; set; }
        public int BitIndex { get; set; }

        #endregion
        /// <summary>
        /// Сравниваются все свойства кроме IndexOfGoose
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool ModelElementCompareTo(IModelElement obj)
        {
            if (!base.ModelElementCompareTo(obj)) return false;
            if (!(obj is IGooseRowFtpEntity)) return false;
            var element = obj as IGooseRowFtpEntity;
            //if (element.IndexOfGoose != IndexOfGoose) return false;
            if (element.NumberOfFcdaInDataSetOfGoose != NumberOfFcdaInDataSetOfGoose) return false;
            if (element.BitIndex != BitIndex) return false;
            return true;
        }
    }
}
