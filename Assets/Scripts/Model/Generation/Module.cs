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