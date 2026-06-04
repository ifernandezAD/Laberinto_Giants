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
        Debug.Log($"Time: {timeToEnd} s");

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

    void DebugUpdateTimerUI()
    {
        int minutes = timeToEnd / 60;
        int seconds = timeToEnd % 60;

        debugCounterText.text = $"{minutes:00}:{seconds:00}";
    }
}
