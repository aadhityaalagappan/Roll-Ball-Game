using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager manager;

    [SerializeField] private GameObject targetPrefab;
    [SerializeField] private float gameDuration = 80f;
    [SerializeField] private float insertTargetInterval = 3f;

    public int score;
    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private TextMeshProUGUI timerText;

    [SerializeField] private GameObject endGamePanel;

    [SerializeField] private TextMeshProUGUI endGameScoreText;

    [SerializeField] private Button playAgain;

    [SerializeField] private Button quitGame;

    public bool gameActive = true;

    private float timeRemaining;

     /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        manager = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        endGamePanel.SetActive(false);
        timeRemaining = gameDuration;
        StartCoroutine(InsertTargets());
        StartCoroutine(UpdateTimer());
        playAgain.onClick.AddListener(PlayAgain);
        quitGame.onClick.AddListener(QuitGame);
    }



    public IEnumerator InsertTargets() {
        while(gameActive) {
            Instantiate(targetPrefab, new Vector3(Random.Range(-6f, 6f), Random.Range(-2f, 2f), 0), Quaternion.identity);
            yield return new WaitForSeconds(insertTargetInterval);
        }
    }
    IEnumerator UpdateTimer() {
        while(gameActive) {
            timeRemaining -= Time.deltaTime;
            timerText.text = "Time Remaining: " + timeRemaining.ToString("F0");
            if(timeRemaining <= 0) {
                gameActive = false;
                ShowEndGamePanel();
            }
            yield return null;
        }
    }

    private void ShowEndGamePanel() {
        endGamePanel.SetActive(true);
        endGameScoreText.text = "Score: " + score;
        gameActive = false;
        MusicManager.Instance.StopMusic();
        
    }

    private void PlayAgain() {
        score = 0;
        scoreText.text = "Score: " + score;
        endGameScoreText.text = "";
        gameActive = true;
        endGamePanel.SetActive(false);
        timeRemaining = gameDuration;
        StartCoroutine(InsertTargets());
        StartCoroutine(UpdateTimer());
        MusicManager.Instance.RestartMusic();

    }

    private void QuitGame() {
        Application.Quit();
    }

    public void ScorePoint() {
        score++;
        scoreText.text = "Score: " + score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
