using Hauschild.IK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour {

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

    private Item getItemWithRaycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Item item = hit.collider.GetComponent<Item>();
            return item;
        }
        return null;
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) {

            Vector2 v = new Vector2(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height);
            Quaternion rotation =
                transform.rotation
                * Quaternion.LookRotation(transform.right)
                * Quaternion.LookRotation(transform.right * v.x)
                * Quaternion.LookRotation(transform.up * v.y);
            
            IKInfo ikInfo = new IKInfo.IKInfoBuilder(transform, transform)
                .WithIKGoal(IKInfo.IKGoalTransform.RH(eyeTransform.position + eyeTransform.forward * 0.3f + eyeTransform.right * 0.05f - eyeTransform.up * 0.1f, rotation))
                .WithIKHint(IKInfo.IKHintTransform.RE(eyeTransform.position + eyeTransform.forward * 0.3f + eyeTransform.right * 0.1f - eyeTransform.up * 0.3f, transform.rotation))
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

            Vector2 v = new Vector2(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height);
            Quaternion rotation =
                transform.rotation
                * Quaternion.LookRotation(-transform.right)
                * Quaternion.LookRotation(-transform.right * v.x)
                * Quaternion.LookRotation(transform.up * v.y);

            IKInfo ikInfo = new IKInfo.IKInfoBuilder(transform, transform)
                .WithIKGoal(IKInfo.IKGoalTransform.LH(eyeTransform.position + eyeTransform.forward * 0.3f - eyeTransform.right * 0.05f - eyeTransform.up * 0.1f, rotation))
                .WithIKHint(IKInfo.IKHintTransform.LE(eyeTransform.position + eyeTransform.forward * 0.3f - eyeTransform.right * 0.1f - eyeTransform.up * 0.3f, transform.rotation))
                .Build();
            leftHand.IKInfo = ikInfo;
            leftVisualizer.Visualize(ikInfo);
            leftHand.HandValue += Time.deltaTime;
        }
        else
        {
            leftHand.HandValue -= Time.deltaTime;
        }




        //Item item;
        //if (Input.GetMouseButtonDown(1))
        //{
        //    if (rightHand.IKInfo == null)
        //    {
        //        if((item = getItem())!=null) {
        //            rightHand.IKInfo = item.GetIKInfo(eyeTransform);
        //        }
        //    }
        //}
        //else if (Input.GetMouseButton(1))
        //{
        //    rightHand.HandValue += Time.deltaTime;
        //}
        //else
        //{
        //    rightHand.HandValue -= Time.deltaTime;
        //}
        //if(rightHand.HandValue == 1 && rightHand.IKInfo != null)
        //{
        //    if(rightHand.GoalDistance < 0.05)
        //    {
        //        //attach
        //    }
        //    else
        //    {
        //        rightHand.IKInfo.Receiver.transform.position = Vector3.Lerp(rightHand.IKInfo.Receiver.transform.position, rightHand.HandTransform.position, Time.deltaTime);
        //    }
        //}
        //if(rightHand.HandValue <= 0 && !Input.GetMouseButton(1))
        //{
        //    rightHand.IKInfo = null;
        //}

        //if (Input.GetMouseButtonDown(0))
        //{
        //    if (leftHand.IKInfo == null)
        //    {
        //        if ((item = getItem()) != null)
        //        {
        //            leftHand.IKInfo = item.GetIKInfo(eyeTransform);
        //        }
        //    }
        //}
        //else if (Input.GetMouseButton(0))
        //{
        //    leftHand.HandValue += Time.deltaTime;
        //    Debug.Log("Distance left : " + leftHand.GoalDistance);
        //}
        //else
        //{
        //    leftHand.HandValue -= Time.deltaTime;
        //}

        //if (leftHand.HandValue <= 0 && !Input.GetMouseButton(0))
        //{
        //    leftHand.IKInfo = null;
        //}

    }
}
