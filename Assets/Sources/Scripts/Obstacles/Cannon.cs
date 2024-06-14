using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] AudioClip shootSound;
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject fireEffect;
    [SerializeField] GameObject dieEffect;
    [SerializeField] Transform firePosition;
    [SerializeField] float fireRate = 2f;

    private float nextFire = 0.0f;
    [SerializeField] float volume = .5f;

    bool isSleeping = true;

    private void Start()
    {
        System.Random rnd = new System.Random();
        nextFire = rnd.Next(0, (int)nextFire);
        double db = rnd.NextDouble();
        nextFire += (float)db;

        FinishLine.FinishLineReached += BeginStop;
        LevelEventsHandler.LevelStarted += BeginStop;
    }

    void BeginStop()
    {
        isSleeping = isSleeping ? false : true;
    }

    void Update()
    {
        if (!isSleeping)
        {
            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        GameObject clone = Instantiate(projectile, firePosition.position, firePosition.rotation);
        clone.GetComponent<BaseObstacle>().dieEffect = dieEffect;
        GameObject fire = Instantiate(fireEffect, firePosition.position, firePosition.rotation);
        clone.GetComponent<BaseObstacle>();

        if (shootSound != null)
            SoundFXManager.instance.PlaySoundFXClip(shootSound, transform, volume);
        else
            Debug.LogError("No shootSound assigned: Cannon");
    }

    private void OnDestroy()
    {
        FinishLine.FinishLineReached -= BeginStop;
        LevelEventsHandler.LevelStarted -= BeginStop;
    }
}
