using UnityEngine;

public class Leak : MonoBehaviour
{
    public float health;
    public WaterLeakLogic parentWaterLeak;
    public ParticleSystem weldingParticle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health > 100)
        {
            parentWaterLeak.DeactivateLeak();
            GameManager.instance.AddScore(1);
            health = 10;
        }
    }
}
