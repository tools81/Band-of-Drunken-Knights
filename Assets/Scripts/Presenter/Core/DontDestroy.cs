using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Presenter.Core
{
    public class DontDestroy : MonoBehaviour
    {
        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}