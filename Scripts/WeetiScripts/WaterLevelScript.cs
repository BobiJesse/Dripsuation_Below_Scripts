using UnityEngine;

public class WaterLevelScript : MonoBehaviour
{
    public static WaterLevelScript instance;

    int WaterHeight;


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

    /*
    private void OnTriggerEnter(Collider Collision)
    {
        if (Collision.tag == "Droplet")
        {
            Destroy(Collision.gameObject);
            Debug.Log("Droplet hit the floor, Raising water.");

            WaterHeight++;
            RaiseWaterHeight();
        }
    }
    */

    // Nostetaan vettä ylöspäin
    public void RaiseWaterHeight()
    {
        transform.position += new Vector3(0, 0.5f, 0);

        if (WaterHeight == 10)
        {
            Debug.Log("GAME OVER");
            WaterHeight = 0;
        }
    }
}
