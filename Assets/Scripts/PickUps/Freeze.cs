using UnityEngine;

public class Freeze : PickUp
{
    public int freezTime = 10;

    public override void Picked()
    {
        GameManager.gameManager.FreezTime(freezTime);
        Destroy(this.gameObject);
    }

    void Update()
    {
        Rotation();
    }
}
