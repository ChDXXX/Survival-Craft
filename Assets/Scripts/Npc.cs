using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum NPCType
{
    Fire,
    Heal
}

public class Npc : MonoBehaviour
{
    public NPCType npcType;
    public string[] dialouge;

    [SerializeField]
    private GameObject dialoguePanel;
    [SerializeField]
    private Text dialogueText;
    [SerializeField]
    private Button continueButton;
    [SerializeField]
    private float speakSpeed;
    private int index;
    private bool isPlayerClose;

    [SerializeField]
    private GameObject allLight;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            isPlayerClose = true;
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerClose = false;
            ResetText();
        }
    }

    private void Start()
    {
        continueButton.onClick.AddListener(() => NextLine());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isPlayerClose)
        {
            if (dialoguePanel.activeInHierarchy)
            {
                ResetText();
            }
            else
            {
                GameManager.Instance.canShooting = false; 
                dialoguePanel.SetActive(true);
                StartCoroutine(Speaking());
            }
        }
        if (dialogueText.text == dialouge[index])
        {
            continueButton.gameObject.SetActive(true);
        }

        if (npcType == NPCType.Fire && Input.GetKeyDown(KeyCode.F))
        { 
            if (allLight.activeSelf == false && GameManager.Instance.GetScroe() >= 20)
            {
                GameManager.Instance.SetScore(-20);
                allLight.SetActive(true);
            }
        }

        if (npcType == NPCType.Heal && Input.GetKeyDown(KeyCode.H))
        {
            if (GameManager.Instance.GetScroe() >= 30)
            {
                GameManager.Instance.SetScore(-30);
                GameManager.Instance.PlayerHeal(100);
            }
        }
    }

    public virtual void ResetText()
    {
        GameManager.Instance.canShooting = true;
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    protected virtual IEnumerator Speaking()
    {
        foreach (char letter in dialouge[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(speakSpeed);
        }
    }

    public virtual void NextLine()
    {
        continueButton.gameObject.SetActive(false);

        if (index < dialouge.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Speaking());
        }
        else
        {
            ResetText();
        }
    }
}
