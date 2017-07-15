using UnityEngine;

public interface IAttachable
{
    void Attach(Transform transform);
    void Detach();
}