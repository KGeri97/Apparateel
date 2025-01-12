using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apparateel.Utilities.Timer;

namespace Apparateel.Crop {
    public interface ICrop {
        public SOCropData CropData { get; }

        public bool IsGrowing { get; }
    }
}
