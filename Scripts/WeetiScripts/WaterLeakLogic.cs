using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLeakLogic : MonoBehaviour
{
    [SerializeField] GameObject WaterLeakObject;

    [SerializeField] GameObject ActiveAlarm;
    [SerializeField] GameObject DeactiveAlarm;

    [SerializeField] GameObject WaterDroplet;
    private Transform WaterDropletLocation;
    public List<Transform> waterDropletLocations;

    public Leak childWaterLeak;
    public List<Leak> leakSPots;


    public bool isActive;

    private void Start()
    {
        //ActivateWaterDrop();
    }

    public void ActivateWaterDrop()
    {
        if (!isActive)
        {
            StartCoroutine(DropTimer());
        }
    }

    IEnumerator DropTimer()
    {
        isActive = true;
        // Aktivoidaan alarm
        DeactiveAlarm.SetActive(false);
        ActiveAlarm.SetActive(true);
        yield return new WaitForSeconds(4);

        // Aloitetaan vuoto
        WaterDropletLocation = waterDropletLocations[Random.Range(0, waterDropletLocations.Count)];
        WaterLeakObject.transform.position = WaterDropletLocation.position;
        WaterLeakObject.transform.rotation = WaterDropletLocation.rotation;
        WaterLeakObject.SetActive(true);
        childWaterLeak.health = 0;
        yield return new WaitForSeconds(8);

        // Pudotetaan droplet
        if (isActive)
        {
            GameObject droplet = Instantiate(WaterDroplet, WaterDropletLocation.position, Quaternion.identity);

            Droplet dropletScript = droplet.GetComponent<Droplet>();

            dropletScript.Initialize(-WaterDropletLocation.up, 5f);

            yield return new WaitForSeconds(3);
        }
        
        
        // Pys‰ytet‰‰n leak ja water
        WaterLeakObject.SetActive(false);

        yield return new WaitForSeconds(2);
        DeactiveAlarm.SetActive(true);
        ActiveAlarm.SetActive(false);
        isActive = false;

        //GameManager.instance.waveCooldown = 20;
        //GameManager.instance.waveActive = false;
        
    }

    public void DeactivateLeak()
    {
        StopCoroutine(DropTimer());
        // Pys‰ytet‰‰n leak ja water
        WaterLeakObject.SetActive(false);
        isActive = false;


        //Invoke(nameof(DeactivateAlarm), 2f);
    }

    public void DeactivateAlarm()
    {
        DeactiveAlarm.SetActive(true);
        ActiveAlarm.SetActive(false);
        isActive = false;

        //GameManager.instance.waveCooldown = 20;
        //GameManager.instance.waveActive = false;
    }
}
