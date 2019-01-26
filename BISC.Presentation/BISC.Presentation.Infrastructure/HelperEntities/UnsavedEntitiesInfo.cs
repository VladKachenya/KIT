using System.Collections.Generic;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Presentation.Infrastructure.HelperEntities
{
    public class UnsavedEntitiesInfo
    {
        public UnsavedEntitiesInfo(bool isEntitiesSaved, List<SaveCheckingEntity> unsavedCheckingEntities)
        {
            IsEntitiesSaved = isEntitiesSaved;
            UnsavedCheckingEntities = unsavedCheckingEntities;
        }

        public bool IsEntitiesSaved { get; }
        public List<SaveCheckingEntity> UnsavedCheckingEntities { get; }
    }
}