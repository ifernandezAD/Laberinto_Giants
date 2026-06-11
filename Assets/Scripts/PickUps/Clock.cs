using UnityEngine;

public class Clock : PickUp
{
    public bool addTime; //true adds time, false substacts time
    public int time = 5;
    public override void Picked()
    {
        int sign;
        sign = addTime ? 1 : -1;

        GameManager.gameManager.AddTime(time * sign);
        
        Destroy(this.gameObject);
    }

    void Update()
    {
        Rotation();
    }
}
