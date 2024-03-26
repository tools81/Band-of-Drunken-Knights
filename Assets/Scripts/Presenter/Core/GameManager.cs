using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Presenter.Generation;

namespace Presenter.Core
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;

        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError(("Game Manager is null"));
                }

                return _instance;
            }
        }

        private void Awake()
        {
            _instance = this;
        }
    }
}