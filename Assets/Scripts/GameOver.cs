using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    private Button startButton;

    [SerializeField]
    private Text bestScoreText;

    private void Start()
    { 
        startButton.onClick.AddListener(() => SceneManager.LoadScene(1));
        bestScoreText.text = "Best Score: " + PlayerPrefs.GetInt("BestScore").ToString();
    }
}
