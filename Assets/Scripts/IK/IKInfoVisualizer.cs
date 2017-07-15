using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hauschild.IK
{
    public class IKInfoVisualizer : MonoBehaviour
    {

        [Header("Debug")]
        [SerializeField]
        private IKInfo ikInfo;
        [SerializeField]
        private bool visualize;
        [SerializeField]
        [Range(0.01f,1f)]
        private float gizmoSize = 0.01f;

        public void Visualize(IKInfo ikInfo)
        {
            this.ikInfo = ikInfo;
            visualize = ikInfo != null;
        }

        void OnDrawGizmos()
        {
            if (!visualize) return;
            Gizmos.color = Color.green;
            foreach (IKInfo.IKGoalTransform ikgt in ikInfo.IKGoalTransforms)
            {
                Gizmos.DrawSphere(ikgt.Position, gizmoSize);
            }
            Gizmos.color = Color.yellow;
            foreach (IKInfo.IKHintTransform ikht in ikInfo.IKHintTransforms)
            {
                Gizmos.DrawSphere(ikht.Position, gizmoSize);
            }            
        }

    }

}