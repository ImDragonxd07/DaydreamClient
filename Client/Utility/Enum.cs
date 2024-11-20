using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Daydream.Client.Utility
{
    internal class Enum
    {
        public static IEnumerator waitForSceneLoad(int sceneNumber)
        {
            while (SceneManager.GetActiveScene().buildIndex != sceneNumber)
            {
                yield return null;
            }

            // Do anything after proper scene has been loaded
            if (SceneManager.GetActiveScene().buildIndex == sceneNumber)
            {
                yield return true;
            }
        }
    }
}
