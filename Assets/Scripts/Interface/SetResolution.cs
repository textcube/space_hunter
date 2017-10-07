using UnityEngine;
using System.Collections;

namespace RpgSystem
{
    public class SetResolution : MonoBehaviour
    {
        public int width = 480;
        public int height = 800;

        void Start()
        {
#if UNITY_STANDALONE
            //Application.targetFrameRate = 60;
            if (Screen.width != width || Screen.height != height)
                Screen.SetResolution(width, height, false);
#endif
            Destroy(this);
        }

    }
}