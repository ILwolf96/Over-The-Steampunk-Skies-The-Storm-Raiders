using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    private Gyroscope gyro;
    private Quaternion rotation;
    bool GyroActive;

    public void EnableGyro()
    {
        if (GyroActive)
        return;

        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
        }
        GyroActive = gyro.enabled;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GyroActive)
        {
            rotation = gyro.attitude;
            transform.rotation = rotation;

        }
    }
}
