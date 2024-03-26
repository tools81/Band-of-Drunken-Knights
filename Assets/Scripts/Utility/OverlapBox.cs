using UnityEngine;
using Model.Generation;
using System.Collections;
using System.Linq;

namespace Utility
{
    public class OverlapBox : MonoBehaviour
    {
        public LayerMask layerMask;
        public Module module;

        private Bounds bounds;

        private void CalcBounds()
        {
            bounds = gameObject.GetComponent<Renderer>().bounds;
            foreach (Transform child in gameObject.transform)
            {
                if (child.gameObject.TryGetComponent(out Renderer renderer))
                {
                    bounds.Encapsulate(renderer.bounds);
                }
            }
        }

        public void DetectCollisions()
        {
            CalcBounds();

            Collider[] hitColliders = Physics.OverlapBox(bounds.center, bounds.extents - bounds.extents.normalized, Quaternion.identity, layerMask);
            int i = 0;

            if (hitColliders.Length > 0 && hitColliders.Any(c => c.transform.parent != gameObject.transform))
            {
                module.Colliding = true;

                while (i < hitColliders.Length)
                {
                    Debug.Log($"Hit : {hitColliders[i].name + i} on Module : {gameObject.name} At : {gameObject.transform.position}");

                    if (Debug.isDebugBuild)
                    {
                        hitColliders[i].gameObject.GetComponent<Renderer>().material.color = Color.red;
                    }

                    i++;
                }
            }          
        }

        void OnDrawGizmos()
        {
            Gizmos.color = module.Colliding ? Color.red : Color.yellow;
            Gizmos.matrix = Matrix4x4.identity;

            CalcBounds();        

            //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
            Gizmos.DrawWireCube(bounds.center, bounds.extents * 2f - bounds.extents.normalized);
        }
    }
}