using UnityEngine;

public class Dropletcatcher : MonoBehaviour
{
    public static Dropletcatcher instance;

    public int maxHoldAmount;
    public int currentHoldAmount;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Droplet"))
        {
            Debug.Log("Droplet catched");
            Destroy(other.gameObject);
            if (currentHoldAmount < maxHoldAmount)
            {
                //GameManager.instance.AddScore();
                currentHoldAmount++;
                UIManager.instance.bucketAmount.text = currentHoldAmount + " / 3";
            }
        }
    }
}
