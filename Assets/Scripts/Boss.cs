using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public Transform target;
    public Image healthBar;
    public float healthAmount = 100f;
    [SerializeField]
    private int bossDamage;
    [SerializeField]
    private NavMeshAgent nmAgent;
    [SerializeField]
    private ParticleSystem attackEffect;
    [SerializeField]
    private ParticleSystem damageEffect;
    [SerializeField]
    private ParticleSystem deathEffect;
    private int score = 100;
    private AudioSource audioSource;
    private Animator animator;
    private bool isDeath;

    private void Start()
    {
        animator = GetComponent<Animator>();
        nmAgent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        nmAgent.SetDestination(target.position);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (isDeath != true && col.gameObject.CompareTag("Bullet"))
        {
            damageEffect.Play();
            animator.SetTrigger("TakeDamage");
            HealthDamage(bossDamage);
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
        healthBar.transform.parent.gameObject.SetActive(false);
        audioSource.Play();
        deathEffect.Play();
        animator.SetTrigger("Die");

        yield return new WaitForSeconds(audioSource.clip.length + 1.1f);
        GameManager.Instance.ReduceEnemyCount();
        Destroy(gameObject);
    }
}