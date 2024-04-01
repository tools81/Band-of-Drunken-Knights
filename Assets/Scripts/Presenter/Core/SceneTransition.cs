using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Presenter.Core
{
    public class SceneTransition : MonoBehaviour
    {
        void Awake()
        {
            Scene scene = SceneManager.GetActiveScene();

            if (scene.buildIndex == 0)
            {
                SceneManager.LoadScene(1);
            }
        }
    }
}