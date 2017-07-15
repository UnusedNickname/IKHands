using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hauschild.IK
{

    public interface IIKReceiver
    {
        IKInfo GetIKInfo(Transform sender);
    }

}