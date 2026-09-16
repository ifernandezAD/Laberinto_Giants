using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    AudioSource audioSource;
    public AudioClip resumeClip;
    public AudioClip pauseClip;
    public AudioClip winClip;
    public AudioClip loseClip;

    public TextMeshProUGUI timeText;
    public TextMeshProUGUI goldKeyText;
    public TextMeshProUGUI redKeyText;
    public TextMeshProUGUI greenKeyText;
    public TextMeshProUGUI crystalText;
    public Image snowFlake;
    public GameObject infoPanel;
    public TextMeshProUGUI pauseEnd;
    public TextMeshProUGUI reloadInfo;
    public TextMeshProUGUI useInfo;


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

        snowFlake.enabled = false;
        timeText.text = timeToEnd.ToString();
        infoPanel.SetActive(false);
        pauseEnd.text = "Pause";
        reloadInfo.text = "";

        audioSource = GetComponent<AudioSource>();
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
        timeText.text = timeToEnd.ToString();
        snowFlake.enabled = false;

        if (timeToEnd <= 0)
        {
            timeToEnd = 0;
            endGame = true;
        }

        if (endGame)
        {
            EndGame();
        }
    }

    public void PauseGame()
    {
        PlayClip(pauseClip);
        infoPanel.SetActive(true);
        Time.timeScale = 0;
        gamePaused = true;
    }

    public void ResumeGame()
    {
        infoPanel.SetActive(false);
        Time.timeScale = 1;
        gamePaused = false;
    }

    public void EndGame()
    {
        CancelInvoke("Stopper");
        infoPanel.SetActive(true);

        if (win)
        {
            pauseEnd.text = "You Win!!!";
            reloadInfo.text = "Reload? Y/N";
        }
        else
        {
            pauseEnd.text = "You Lose!!!";
            reloadInfo.text = "Reload? Y/N";
        }
    }

    public void PlayClip(AudioClip playClip)
    {
        audioSource.clip = playClip;
        audioSource.Play();
    }

    #region PickUps

    public void AddPoints(int point)
    {
        points += point;
        crystalText.text = points.ToString();
    }

    public void AddTime(int addTime)
    {
        timeToEnd += addTime;
        timeText.text = timeToEnd.ToString();
    }

    public void FreezTime(int freez)
    {
        CancelInvoke("Stopper");
        snowFlake.enabled = true;
        InvokeRepeating("Stopper", freez, 1);
    }


    public void AddKey(KeyColor color)
    {
        if (color == KeyColor.Gold)
        {
            goldKey++;
            goldKeyText.text = goldKey.ToString();
        }
        else if (color == KeyColor.Green)
        {
            greenKey++;
            greenKeyText.text = greenKey.ToString();
        }
        else if (color == KeyColor.Red)
        {
            redKey++;
            redKeyText.text = redKey.ToString();
        }
    }

    #endregion

    
}
