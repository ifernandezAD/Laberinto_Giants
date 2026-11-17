using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    [SerializeField] int timeToEnd;
    
    void Start()
    {
       if(gameManager == null){gameManager = this;}

       InvokeRepeating("Stopper", 2f, 1f);
    } 

    void Stopper()
    {
        timeToEnd--;
        Debug.Log("Time to end: " + timeToEnd);

        if (timeToEnd <= 0)
        {
            timeToEnd = 0;
        }
    }


}
