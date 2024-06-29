using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nemuge.Core {
    public static class Utility {
        private static Camera _mainCamera;

        public static Camera MainCamera {
            get {
                if (_mainCamera == null)
                    _mainCamera = Camera.main;

                return _mainCamera;
            }
        }
    }
}