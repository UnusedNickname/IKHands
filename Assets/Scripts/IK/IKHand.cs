using UnityEngine;
using System.Linq;

namespace Hauschild.IK
{

    public class IKHand : MonoBehaviour
    {

        [SerializeField]
        private Animator animator;
        [SerializeField]
        private Transform handTransform;
        [SerializeField]
        private AvatarIKGoal ikGoal;
        [SerializeField]
        private AvatarIKHint ikHint;

        [Header("Debug")]
        [SerializeField]
        private float handValue;
        public float HandValue
        {
            get
            {
                return handValue;
            }

            set
            {
                handValue = Mathf.Clamp01(value);
            }
        }

        private IKInfo ikInfo;
        public IKInfo IKInfo
        {
            get
            {
                return ikInfo;
            }
            set
            {
                ikInfo = value; 
                if (ikInfo == null) return;
                hintTransform = ikInfo.IKHintTransforms.Where(ikht => ikht.Value == ikHint).First();
                goalTransform = ikInfo.IKGoalTransforms.Where(ikgt => ikgt.Value == ikGoal).First();
            }
        }

        public float GoalDistance
        {
            get
            {
                return (handTransform.position - goalTransform.Position).magnitude;
            }
        }

        public Transform HandTransform
        {
            get
            {
                return handTransform;
            }

            set
            {
                handTransform = value;
            }
        }

        private IKInfo.IKHintTransform hintTransform;
        private IKInfo.IKGoalTransform goalTransform;

        void OnAnimatorIK()
        {
            if (IKInfo != null)
            {
                if (hintTransform!=null) animator.SetIKHintPosition(ikHint, hintTransform.Position);
                animator.SetIKHintPositionWeight(ikHint, handValue);
                if (goalTransform != null)
                {
                    animator.SetIKPosition(ikGoal, goalTransform.Position);
                    animator.SetIKRotation(ikGoal, goalTransform.Rotation);
                }
                animator.SetIKPositionWeight(ikGoal, handValue);
                animator.SetIKRotationWeight(ikGoal, handValue);
            }
        }
       
    }

}