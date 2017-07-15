using Hauschild.IK;
using UnityEngine;

public class Item : MonoBehaviour, IIKReceiver, IAttachable
{
    [SerializeField]
    private Rigidbody rigidBody;
    [SerializeField]
    private IKInfoGenerator ikInfoGenerator;
    [SerializeField]
    private IKInfoVisualizer ikInfoVisualizer;

    private bool attached = false;
    public void Attach(Transform transform)
    {
        if (attached) return;
        this.transform.SetParent(transform);
        if (rigidBody)
        {
            //rigidBody.detectCollisions = false;
            rigidBody.isKinematic = true;
        }
        attached = true;
    }

    public void Detach()
    {
        if (!attached) return;
        this.transform.SetParent(null);
        if (rigidBody)
        {
            //rigidBody.detectCollisions = true;
            rigidBody.isKinematic = false;
        }
        attached = false;
    }

    public IKInfo GetIKInfo(Transform sender)
    {
        IKInfo ikInfo = ikInfoGenerator.GetIKInfo(sender);
        if (ikInfoVisualizer != null)
        {
            ikInfoVisualizer.Visualize(ikInfo);
        }
        return ikInfoGenerator.GetIKInfo(sender);
    }

}
