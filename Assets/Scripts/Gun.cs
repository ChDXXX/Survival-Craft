using UnityEngine;

public class Gun : MonoBehaviour
{
 
    [SerializeField]
    private float force = 20.0f;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private ParticleSystem shootingEffect;
    [SerializeField]
    private Transform shottingPos;
    private float bulletSpeed = 800;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void Shooting()
    { 
        GameObject bullet = Instantiate(bulletPrefab, shottingPos.position,
                                        shottingPos.rotation);

        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

        bulletRigidbody.AddForce(shottingPos.forward * bulletSpeed);

        shootingEffect.Play();
        audioSource.Play();

        Destroy(bullet, 6f);
    } 
}
