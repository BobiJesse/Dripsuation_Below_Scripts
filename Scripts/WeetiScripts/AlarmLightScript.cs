using UnityEngine;

public class AlarmLightScript : MonoBehaviour
{
    // Speed of rotation in degrees per second
    public Vector3 rotationSpeed = new Vector3(0, 50, 0);

    void Update()
    {
        // Multiplied by Time.deltaTime for frame-rate independence
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
