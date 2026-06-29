using UnityEngine;

public class Droplet : MonoBehaviour
{

    public float lifetime;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lifetime > 0)
        {
            lifetime -= Time.deltaTime;
        }

        if (lifetime < 0)
        {
            GameManager.instance.AddWaterLevel();
            WaterLevelScript.instance.RaiseWaterHeight();
            Destroy(gameObject);
        }
    }

    public void Initialize(Vector3 direction, float speed)
    {
        rb.linearVelocity = direction * speed;
    }
}
