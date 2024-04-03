using System.Linq;
using UnityEngine;

namespace Model.Generation
{
    public class Module : MonoBehaviour
    {
        [SerializeField]
        public string[] Tags;        

        [HideInInspector]
        public bool Colliding;
        [HideInInspector]
        public Bounds Bounds;
        public LayerMask _layerMask;

        private void FixedUpdate() 
        {
            if (_layerMask != gameObject.layer)
            {
                var shroudLayer = LayerMask.NameToLayer("Shroud");

                if (gameObject.layer == shroudLayer)
                {
                    foreach (Transform child in gameObject.transform.GetComponentsInChildren<Transform>())
                    {
                        child.gameObject.layer = shroudLayer;
                    }
                }
                else
                {
                    foreach (Transform child in gameObject.transform.GetComponentsInChildren<Transform>())
                    {
                        child.gameObject.layer = 0; //Default
                    }
                }
            } 

            _layerMask = gameObject.layer;
        }

        public ModuleConnector[] GetConnectors()
        {
            return GetComponentsInChildren<ModuleConnector>();
        }

        public ModuleConnector[] GetExitConnectors()
        {
            return GetComponentsInChildren<ModuleConnector>().Where(m => m.tag == "Exit").ToArray();
        }
    }
}