using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Generation
{
    public class ModuleConnector : MonoBehaviour
    {
        [SerializeField]
        public string[] Tags;        

        [SerializeField]
        public bool IsDefault;

        //[HideInInspector]
        public bool IsConnected;

        void OnDrawGizmos()
        {
            var scale = 1.0f;

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * scale);

            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position - transform.right * scale);
            Gizmos.DrawLine(transform.position, transform.position + transform.right * scale);

            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.up * scale);

            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, 0.125f);
        }
    }
}