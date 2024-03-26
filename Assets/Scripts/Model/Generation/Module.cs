using UnityEngine;

namespace Model.Generation
{
    public class Module : MonoBehaviour
    {
        [SerializeField]
        public string[] Tags;
        
        
        public bool Colliding;

        public ModuleConnector[] GetExits()
        {
            return GetComponentsInChildren<ModuleConnector>();
        }
    }
}