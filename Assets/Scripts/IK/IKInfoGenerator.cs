using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hauschild.IK
{

    public class IKInfoGenerator : MonoBehaviour, IIKReceiver
    {

        public MeshFilter meshFilter;
        public float maxAngle = 30;

        public IKInfo GetIKInfo(Transform sender)
        {
            Ray ray = new Ray(sender.position, transform.position - sender.position);
            RaycastHit hit;
            if (!Physics.Raycast(ray, out hit)) return null;
            if (!hit.collider.gameObject.Equals(gameObject)) return null;

            Mesh mesh = meshFilter.mesh;
            float r = Mathf.Max(mesh.bounds.size.x, mesh.bounds.size.y);
            Vector3 inverseLookDirection = sender.position - hit.point;

            IKInfo.IKInfoBuilder builder = new IKInfo.IKInfoBuilder(transform, sender);

            Quaternion rightElbowRotation = Quaternion.LookRotation(-inverseLookDirection);
            Vector3 rightElbowPosition = pointOnSphere(Quaternion.LookRotation(inverseLookDirection), -maxAngle) * r + hit.point;
            builder.WithIKHint(IKInfo.IKHintTransform.RE(rightElbowPosition, rightElbowRotation));

            Quaternion leftElbowRotation = Quaternion.LookRotation(-inverseLookDirection);
            Vector3 leftElbowPosition = pointOnSphere(Quaternion.LookRotation(inverseLookDirection), maxAngle) * r + hit.point;
            builder.WithIKHint(IKInfo.IKHintTransform.LE(leftElbowPosition, leftElbowRotation));

            Ray rayLeft = new Ray(leftElbowPosition, transform.position - leftElbowPosition);
            Ray rayRight = new Ray(rightElbowPosition, transform.position - rightElbowPosition);
            RaycastHit hitLeft;
            RaycastHit hitRight;
            if (Physics.Raycast(rayLeft, out hitLeft))
            {
                Quaternion leftHandRotation = Quaternion.LookRotation(-inverseLookDirection, leftElbowPosition - transform.position );
                Vector3 leftHandPosition = hitLeft.point;
                builder.WithIKGoal(IKInfo.IKGoalTransform.LH(leftHandPosition, leftHandRotation));
            }
            if (Physics.Raycast(rayRight, out hitRight))
            {
                Quaternion rightHandRotation = Quaternion.LookRotation(-inverseLookDirection, rightElbowPosition - transform.position);
                Vector3 rightHandPosition = hitRight.point;
                builder.WithIKGoal(IKInfo.IKGoalTransform.RH(rightHandPosition, rightHandRotation));
            }

            return builder.Build();
        }

        private Vector3 pointOnSphere(Quaternion targetDirection, float angle)
        {
            float angleInRad = angle * Mathf.Deg2Rad;
            Vector3 pointOnCircle = Vector2.one * Mathf.Sin(angleInRad);
            Vector3 v = new Vector3(pointOnCircle.x, 0, Mathf.Cos(angleInRad));
            return targetDirection * v;
        }

    }

}