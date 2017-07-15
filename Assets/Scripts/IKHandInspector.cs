using Hauschild.IK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKHandInspector : MonoBehaviour {

    [SerializeField]
    private IKHand leftHand;
    [SerializeField]
    private IKHand rightHand;
    [SerializeField]
    private Transform eyeTransform;
    [SerializeField]
    private IKInfoVisualizer rightVisualizer;
    [SerializeField]
    private IKInfoVisualizer leftVisualizer;
    [SerializeField]
    private Vector3 rightHandOffsetFactor = new Vector3(0.05f, 0.1f, 0.3f);
    [SerializeField]
    private Vector3 rightElbowOffsetFactor = new Vector3(0.1f, 0.3f, 0.3f);
    [SerializeField]
    private Vector3 leftHandOffsetFactor = new Vector3(-0.05f, 0.1f, 0.3f);
    [SerializeField]
    private Vector3 leftElbowOffsetFactor = new Vector3(-0.1f, 0.3f, 0.3f);
    [SerializeField]
    private Vector3 rightHandRotationFactor = new Vector3(3, 2, 1);
    [SerializeField]
    private Vector3 leftHandRotationFactor = new Vector3(-3, 2, 1);


    void Update()
    {
        if (Input.GetMouseButton(0)) {

            Vector2 v = new Vector2(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height) - Vector2.one * 0.5f;
            Vector3 rhrf = rightHandRotationFactor;
            Quaternion rotation =
                transform.rotation
                * Quaternion.LookRotation(new Vector3(v.x * rhrf.x, v.y * rhrf.y, rhrf.z))
                * Quaternion.LookRotation(-Vector3.forward)
                * Quaternion.LookRotation(Vector3.up);
                

            Vector3 rHOF = rightHandOffsetFactor;
            Vector3 rEOF = rightElbowOffsetFactor;
            IKInfo ikInfo = new IKInfo.IKInfoBuilder(transform, transform)
                .WithIKGoal(IKInfo.IKGoalTransform.RH(eyeTransform.position + eyeTransform.forward * rHOF.z + eyeTransform.right * rHOF.x - eyeTransform.up * rHOF.y, rotation))
                .WithIKHint(IKInfo.IKHintTransform.RE(eyeTransform.position + eyeTransform.forward * rEOF.z + eyeTransform.right * rEOF.x - eyeTransform.up * rEOF.y, transform.rotation))
                .Build();
            rightHand.IKInfo = ikInfo;
            rightVisualizer.Visualize(ikInfo);
            rightHand.HandValue += Time.deltaTime;
        }
        else
        {
            rightHand.HandValue -= Time.deltaTime;
        }

        if (Input.GetMouseButton(1))
        {

            Vector2 v = new Vector2(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height) - Vector2.one * 0.5f;
            Vector3 lhrf = leftHandRotationFactor;
            Quaternion rotation =
                                transform.rotation
                * Quaternion.LookRotation(new Vector3(v.x * lhrf.x, v.y * lhrf.y, lhrf.z))
                * Quaternion.LookRotation(-Vector3.forward)
                * Quaternion.LookRotation(Vector3.up);

            //Quaternion.LookRotation(-Vector3.forward)
            //    * Quaternion.LookRotation(Vector3.up);

            Vector3 lHOF = leftHandOffsetFactor;
            Vector3 lEOF = leftElbowOffsetFactor;
            IKInfo ikInfo = new IKInfo.IKInfoBuilder(transform, transform)
                .WithIKGoal(IKInfo.IKGoalTransform.LH(eyeTransform.position + eyeTransform.forward * lHOF.z + eyeTransform.right * lHOF.x - eyeTransform.up * lHOF.y, rotation))
                .WithIKHint(IKInfo.IKHintTransform.LE(eyeTransform.position + eyeTransform.forward * lEOF.z + eyeTransform.right * lEOF.x - eyeTransform.up * lEOF.y, transform.rotation))
                .Build();
            leftHand.IKInfo = ikInfo;
            leftVisualizer.Visualize(ikInfo);
            leftHand.HandValue += Time.deltaTime;
        }
        else
        {
            leftHand.HandValue -= Time.deltaTime;
        }

    }

}
