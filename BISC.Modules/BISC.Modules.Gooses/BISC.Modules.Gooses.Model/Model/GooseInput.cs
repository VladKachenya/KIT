﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Model;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.Gooses.Infrastructure.Keys;
using BISC.Modules.Gooses.Infrastructure.Model;

namespace BISC.Modules.Gooses.Model.Model
{
    public class GooseInput : ModelElement, IGooseInput
    {
        public GooseInput()
        {
            ElementName = GooseKeys.GooseModelKeys.GooseInputKey;
        }

        #region Implementation of IGooseInput

        public ChildModelsList<IExternalGooseRef> ExternalGooseReferences =>new ChildModelsList<IExternalGooseRef>(this, GooseKeys.GooseModelKeys.ExternalGooseRefKey);

        #endregion
    }
}