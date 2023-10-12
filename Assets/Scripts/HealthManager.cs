using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image healthBar;
    public float healthAmount = 100f;
    public float decreaseRate = 0.001f;//10.1f; // per second 마다 줄어들게 만들 값 

    void Update()
    {
        DecreaseHealthOverTime();
    }

    private void DecreaseHealthOverTime()
    {
        if (healthAmount <= 0)
        {
            GameManager.Instance.GameOver();
        }
        else
        {
            healthAmount -= decreaseRate * Time.deltaTime;

            healthBar.fillAmount = healthAmount / 100f;
        }
    }

    public void HealthDamage(float damage)
    {
        healthAmount -= damage;
        healthBar.fillAmount = healthAmount / 100f;

        if (healthAmount <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }

    public void Heal(float healAmount)
    {
        healthAmount += healAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);

        healthBar.fillAmount = healthAmount / 100f;
    }
}
