using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public enum DifficultyLevel
    {
        Easy,
        Normal,
        Hard
    }
    public Image healthBar;
    public float healthAmount = 100f;
    public DifficultyLevel difficultyLevel;
    public Transform target;

    [SerializeField]
    private int playerDamage = 1;
    [SerializeField]
    private int enemyDamage = 10;
    [SerializeField]
    private int score = 10;
    [SerializeField]
    private ParticleSystem deathEffect;
    private NavMeshAgent nmAgent;
    private AudioSource audioSource;
    private bool isDeath = false;


    private void Start()
    {
        nmAgent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();

        AdjustNavMeshAgentProperties();
    }

    private void Update()
    {
        nmAgent.SetDestination(target.position);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        { 
            GameManager.Instance.PlayerDamage(playerDamage);
        }
        if (isDeath != true && col.gameObject.CompareTag("Bullet"))
        {
            HealthDamage(enemyDamage);
        }
    }
    private void AdjustNavMeshAgentProperties()
    {
        switch (difficultyLevel)
        {
            case DifficultyLevel.Easy:
                nmAgent.speed = 0.5f;
                nmAgent.acceleration = 2f;
                nmAgent.angularSpeed = 60f;
                break;

            case DifficultyLevel.Normal:
                nmAgent.speed = 0.8f;
                nmAgent.acceleration = 6f;
                nmAgent.angularSpeed = 90f;
                break;

            case DifficultyLevel.Hard:
                nmAgent.speed = 1f;
                nmAgent.acceleration = 8f;
                nmAgent.angularSpeed = 120f;
                break;
        }
    }

    public void HealthDamage(float damage)
    {
        healthAmount -= damage;
        healthBar.fillAmount = healthAmount / 100f;

        if (healthAmount <= 0)
        {
            isDeath = true;
            GameManager.Instance.SetScore(score);
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        GameManager.Instance.ReduceEnemyCount();
        audioSource.Play();
        deathEffect.Play();
        yield return new WaitForSeconds(audioSource.clip.length + 0.1f);

        Destroy(gameObject);
    }
}
