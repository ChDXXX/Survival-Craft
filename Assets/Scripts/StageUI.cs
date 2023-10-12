using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{
    [SerializeField]
    private Text stageText;
    [SerializeField]
    private AudioClip winClip;
    [SerializeField]
    private AudioClip fightClip;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public IEnumerator SetStageText()
    {
        stageText.text = "Stage " + GameManager.Instance.currentStage;

        yield return new WaitForSeconds(2);
        audioSource.clip = fightClip;
        audioSource.Play();
        stageText.text = "Fight !!";

        yield return new WaitForSeconds(fightClip.length + 0.1f);

        gameObject.SetActive(false);
    }

    public IEnumerator Win()
    {
        audioSource.clip = winClip;
        audioSource.Play();
        stageText.text = "Win !!";

        yield return new WaitForSeconds(winClip.length + 0.1f);
        gameObject.SetActive(false);
        SceneManager.LoadScene(0);
    }
}
