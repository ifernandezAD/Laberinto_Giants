using System.Threading;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    [SerializeField] int timeToEnd;

    bool gamePaused = false;
    bool endGame = false;
    bool win = false;

    public int points = 0;

    public int redKey = 0;
    public int greenKey = 0;
    public int goldKey = 0;


    [Header("Debug")]
    public TextMeshProUGUI debugCounterText;
    public GameObject debugCounter;
    public GameObject debugPausePopup;


    void Start()
    {
        if (gameManager == null)
        {
            gameManager = this;
        }

        if (timeToEnd <= 0)
        {
            timeToEnd = 100;
        }

        InvokeRepeating("Stopper", 2, 1);
    }

    private void Update()
    {
        PauseCheck();
        DebugPickUps(); // Borrar luego
    }

    private void PauseCheck()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (gamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void Stopper()
    {
        timeToEnd--;

        if (timeToEnd <= 0)
        {
            timeToEnd = 0;
            endGame = true;
        }

        DebugUpdateTimerUI();

        if (endGame)
        {
            EndGame();
        }
    }

    public void PauseGame()
    {
        Debug.Log("Game paused");
        Time.timeScale = 0;
        gamePaused = true;
        debugPausePopup.SetActive(true);
        debugCounter.SetActive(false);
    }

    public void ResumeGame()
    {
        Debug.Log("Game resumed");
        Time.timeScale = 1;
        gamePaused = false;
        debugPausePopup.SetActive(false);
        debugCounter.SetActive(true);
    }

    public void EndGame()
    {
        CancelInvoke("Stopper");

        if (win)
        {
            Debug.Log("You win!!Reload?");
        }else
        {
            Debug.Log("You Lose!!Reload?");
        }
    }

    public void AddPoints(int point)
    {
        points += point;
    }

    public void AddTime(int addTime)
    {
        timeToEnd += addTime;
    }

    public void FreezTime(int freez)
    {
        CancelInvoke("Stopper");
        InvokeRepeating("Stopper", freez, 1);
    }

    void DebugUpdateTimerUI()
    {
        int minutes = timeToEnd / 60;
        int seconds = timeToEnd % 60;

        debugCounterText.text = $"{minutes:00}:{seconds:00}";
    }

    void DebugPickUps()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Actual Time: " + timeToEnd);
            Debug.Log("Points: " + points);
            Debug.Log($"Red Key:{redKey}, Green Key:{greenKey}, Gold Key:{goldKey}");
        }
    }
}
