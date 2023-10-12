using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int currentStage = 1;
    public List<GameObject> stages;
    [SerializeField]
    private GameObject allLight;
    [SerializeField]
    private AudioSource bgmAudioSource;
    private List<int> stageEnemyCount = new List<int>();
    public bool canShooting = true;

    [Header("UI")]
    [SerializeField]
    private StageUI nextStageUI;
    [SerializeField]
    private HealthManager healthManager;
    [SerializeField]
    private Text scoreText;
    private int score = 0;
    private int lastStage = 3;
    [SerializeField]
    private Text bestScoreText;
    public int BestScore
    {
        get
        {
            return bestScore;
        }
    }
    private int bestScore = 0;

    [SerializeField]
    private Button homeButton;

    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (instance == null)
                    Debug.Log("instance is null");
            }
            return instance;
        }
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("BestScore"))
        {
            bestScore = PlayerPrefs.GetInt("BestScore");
        }

        homeButton.onClick.AddListener(() => SceneManager.LoadScene(0));
        InitStageEnemyCount();
    }

    private void InitStageEnemyCount()
    {
        foreach (GameObject stage in stages)
        {
            int childCount = stage.transform.childCount;
            stageEnemyCount.Add(childCount);
        }
    }

    private void Update()
    {
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore);
        }
        bestScoreText.text = "Best Score: " + bestScore.ToString();
    }

    [ContextMenu("ResetBestScore")]

    public void ResetBestScore()
    {
        PlayerPrefs.DeleteAll();
    }

    public void ReduceEnemyCount()
    {
        int currentStageIndex = currentStage - 1;

        if (stageEnemyCount[currentStageIndex] > 0)
        {
            Debug.Log("애너미 한명죽음");
            stageEnemyCount[currentStageIndex]--;
        }

        if (stageEnemyCount[currentStageIndex] == 0)
        {
            Debug.Log("애너미 다 죽음!!!!");
            StartCoroutine(NextStage());
        }
    }

    private IEnumerator NextStage()
    {
        stages[currentStage - 1].gameObject.SetActive(false);
        currentStage++;
        if (currentStage > lastStage)
        {
            bgmAudioSource.Stop();
            nextStageUI.gameObject.SetActive(true);
            StartCoroutine(nextStageUI.Win());
        }
        else
        {
            allLight.SetActive(false);
            nextStageUI.gameObject.SetActive(true);

            yield return StartCoroutine(nextStageUI.SetStageText());

            stages[currentStage - 1].gameObject.SetActive(true);
        }
    }
    public int GetScroe()
    {
        return score;
    }

    public void SetScore(int scoreValue)
    {
        score += scoreValue;
        scoreText.text = "Score : " + score.ToString();
    }

    public void GameOver()
    {
        SceneManager.LoadScene(2);
    }

    public void PlayerHeal(float heal)
    {
        healthManager.Heal(heal);
    }

    public void PlayerDamage(float damage)
    {
        healthManager.HealthDamage(damage);
    }
}
