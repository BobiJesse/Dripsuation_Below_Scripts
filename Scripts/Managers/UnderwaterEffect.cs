using UnityEngine;
using UnityEngine.Rendering;

public class UnderwaterEffect : MonoBehaviour
{
    [Header("Depth Parameters")]
    [SerializeField] Transform mainCamera;
    [SerializeField] Transform swimCap;
    [SerializeField] Transform swimReset;
    public float swimResetTime;
    public Transform waterLevel;
    public LayerMask waterLayer;

    [Header("Post Processing Volume")]
    [SerializeField] Volume postProcessingVolume;

    [Header("Post Processing Profiles")]
    [SerializeField] VolumeProfile aboveWaterProfile;
    [SerializeField] VolumeProfile underWaterProfile;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        swimResetTime -= Time.deltaTime;
        
        if (mainCamera.transform.position.y - 0.16f < waterLevel.transform.position.y)
        {
            ToggleEffect(true);
            if (PlayerMovement.instance.oxygenLevel >= 0)
            {
                PlayerMovement.instance.oxygenLevel -= Time.deltaTime * 0.2f;
            }
        }
        else
        {
            ToggleEffect(false);
            if (PlayerMovement.instance.oxygenLevel <= 1)
            {
                PlayerMovement.instance.oxygenLevel += Time.deltaTime * 0.2f;
            }
        }

        if (swimCap.transform.position.y < waterLevel.transform.position.y && swimResetTime < 0)
        {
            PlayerMovement.instance.canSwim = true;
        }

        else if (swimCap.transform.position.y > waterLevel.transform.position.y)
        {
            PlayerMovement.instance.canSwim = false;
            PlayerMovement.instance.swimmingUp = false;
            swimResetTime = 0.3f;
        }


        /*
        if (Physics.Raycast(mainCamera.transform.position, Vector3.up, 2f))
        {
            Debug.Log("Underwater");
            ToggleEffect(true);
        }

        else
        {
            ToggleEffect(false);
        }
        */
    }

    private void ToggleEffect(bool active)
    {
        if (active)
        {
            postProcessingVolume.profile = underWaterProfile;
            RenderSettings.fog = true;
        }

        else
        {
            postProcessingVolume.profile = aboveWaterProfile;
            RenderSettings.fog = false;
        }
    }
}
