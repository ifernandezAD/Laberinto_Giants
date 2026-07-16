using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Vector3 rotationAxis = Vector3.forward;

    public virtual void Picked()
    {
        Debug.Log("Picked Up");
        Destroy(this.gameObject);
    }

    public void Rotation()
    {
        transform.Rotate(rotationAxis);
    }
}
