using UnityEngine;

public class TorpedoLoader : MonoBehaviour
{

    public int maxHoldAmount;
    public int currentHoldAmount;

    [SerializeField] TorpedoLaunchScript torpedolauncher;
    [SerializeField] WaterMeterScript WaterMeter;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("player detected");
            if (torpedolauncher != null)
            {
                Debug.Log("torpedolauncher found");
                if (!torpedolauncher.launchActive)
                {
                    Debug.Log("launch not active");
                    //ämpärin tyhjennys
                    currentHoldAmount += Dropletcatcher.instance.currentHoldAmount;
                    Dropletcatcher.instance.currentHoldAmount = 0;
                    UIManager.instance.bucketAmount.text = Dropletcatcher.instance.currentHoldAmount + " / 3";

                    // Mittaria varten
                    WaterMeter.ChangeWaterLevel(currentHoldAmount);

                    if (currentHoldAmount >= maxHoldAmount)
                    {
                        Debug.Log("torpedo launch started");
                        torpedolauncher.ActivateLaunch();
                        currentHoldAmount = 0;
                    }
                }
                else
                {
                    Debug.LogWarning("launch already active");
                }
            }
        }
    }
}
