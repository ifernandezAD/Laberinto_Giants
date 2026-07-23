using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform closePosition;
    public Transform openPosition;
    public Transform door;

    public bool open = false;
    public float speed = 5;

    void Start()
    {
        door.position = closePosition.position;
    }

    public void Open()
    {
        open = true;
    }

    void Update()
    {
        if (open && Vector3.Distance(door.position, openPosition.position) > 0.001)
        {
            door.position = Vector3.MoveTowards(door.position, openPosition.position,
                Time.deltaTime * speed);
        }
    }
}
