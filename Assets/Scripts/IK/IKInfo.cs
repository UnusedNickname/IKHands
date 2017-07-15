using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Hauschild.IK
{

    [Serializable]
    public class IKInfo
    {

        [Serializable]
        public abstract class IKTransform<T>
        {
            [SerializeField]
            protected T value;
            [SerializeField]
            protected Vector3 position;
            [SerializeField]
            protected Quaternion rotation;

            protected IKTransform(T value, Vector3 position, Quaternion rotation)
            {
                this.value = value;
                this.position = position;
                this.rotation = rotation;
            }

            public T Value
            {
                get
                {
                    return value;
                }
            }

            public Vector3 Position
            {
                get
                {
                    return position;
                }
            }

            public Quaternion Rotation
            {
                get
                {
                    return rotation;
                }
            }

            public override string ToString()
            {
                return string.Format("{0} [IKValue : {1}] [Position : {2}] [Rotation : {3}]", GetType().Name, Value.ToString(), Position.ToString(), Rotation.ToString());
            }

        }

        [Serializable]
        public sealed class IKGoalTransform : IKTransform<AvatarIKGoal>
        {
            private IKGoalTransform(AvatarIKGoal value, Vector3 position, Quaternion rotation) : base(value, position, rotation) { }

            public static IKGoalTransform RH(Vector3 position, Quaternion rotation)
            {
                return new IKGoalTransform(AvatarIKGoal.RightHand, position, rotation);
            }

            public static IKGoalTransform LH(Vector3 position, Quaternion rotation)
            {
                return new IKGoalTransform(AvatarIKGoal.LeftHand, position, rotation);
            }

            public static IKGoalTransform RF(Vector3 position, Quaternion rotation)
            {
                return new IKGoalTransform(AvatarIKGoal.RightFoot, position, rotation);
            }

            public static IKGoalTransform LF(Vector3 position, Quaternion rotation)
            {
                return new IKGoalTransform(AvatarIKGoal.LeftFoot, position, rotation);
            }
        }

        [Serializable]
        public sealed class IKHintTransform : IKTransform<AvatarIKHint>
        {
            private IKHintTransform(AvatarIKHint value, Vector3 position, Quaternion rotation) : base(value, position, rotation) { }

            public static IKHintTransform RE(Vector3 position, Quaternion rotation)
            {
                return new IKHintTransform(AvatarIKHint.RightElbow, position, rotation);
            }

            public static IKHintTransform LE(Vector3 position, Quaternion rotation)
            {
                return new IKHintTransform(AvatarIKHint.LeftElbow, position, rotation);
            }

            public static IKHintTransform RK(Vector3 position, Quaternion rotation)
            {
                return new IKHintTransform(AvatarIKHint.RightKnee, position, rotation);
            }

            public static IKHintTransform LK(Vector3 position, Quaternion rotation)
            {
                return new IKHintTransform(AvatarIKHint.LeftKnee, position, rotation);
            }
        }

        public class IKInfoBuilder
        {

            private IDictionary<AvatarIKGoal, IKGoalTransform> ikGoalTransformDict = new Dictionary<AvatarIKGoal, IKGoalTransform>();
            private IDictionary<AvatarIKHint, IKHintTransform> ikHintTransformDict = new Dictionary<AvatarIKHint, IKHintTransform>();
            private Transform receiver;
            private Transform sender;

            public IKInfoBuilder(Transform receiver, Transform sender)
            {
                this.receiver = receiver;
                this.sender = sender;
            }

            public IKInfoBuilder WithIKGoal(IKGoalTransform ikGoal)
            {
                ikGoalTransformDict[ikGoal.Value] = ikGoal;
                return this;
            }

            public IKInfoBuilder WithIKHint(IKHintTransform ikHint)
            {
                ikHintTransformDict[ikHint.Value] = ikHint;
                return this;
            }

            public IKInfo Build()
            {
                IKInfo ikInfo = new IKInfo();
                ikInfo.receiver = receiver;
                ikInfo.sender = sender;
                ikInfo.ikGoalTransforms = ikGoalTransformDict.Values.ToArray();
                ikInfo.ikHintTransforms = ikHintTransformDict.Values.ToArray();
                return ikInfo;
            }

        }

        [SerializeField]
        private Transform receiver;
        [SerializeField]
        private Transform sender;
        [SerializeField]
        private IKGoalTransform[] ikGoalTransforms;
        [SerializeField]
        private IKHintTransform[] ikHintTransforms;

        private IKInfo() { }

        public Transform Receiver
        {
            get
            {
                return receiver;
            }
        }

        public Transform Sender
        {
            get
            {
                return sender;
            }
        }

        public IEnumerable<IKGoalTransform> IKGoalTransforms
        {
            get
            {
                return ikGoalTransforms;
            }
        }

        public IEnumerable<IKHintTransform> IKHintTransforms
        {
            get
            {
                return ikHintTransforms;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(GetType().Name);
            sb.AppendFormat("Sender [Transform : {0}]\n", Sender.ToString());
            sb.AppendFormat("Receiver [Transform : {0}]\n", Receiver.ToString());
            foreach(IKGoalTransform ikgt in IKGoalTransforms)
            {
                sb.AppendLine(ikgt.ToString());
            }
            foreach (IKHintTransform ikht in IKHintTransforms)
            {
                sb.AppendLine(ikht.ToString());
            }
            return sb.ToString();
        }

    }

}