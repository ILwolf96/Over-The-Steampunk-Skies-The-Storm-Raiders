using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelManager : MonoBehaviour
{
    [SerializeField] private GameObject wheel;
    public float rotateSpeed;

    // Update is called once per frame
    private void Update()
    {
        if(Input.touchCount>0)
        {
            if(Input.GetTouch(0).phase== TouchPhase.Began) // Checks if touch has began.
            {
                if (Input.GetTouch(0).position.x < Screen.width / 2) // if its on the left side of the screen, rotate the wheel left.
                {
                    Debug.Log("Left!");
                    wheel.transform.Rotate(new Vector3(wheel.transform.rotation.x, wheel.transform.rotation.y, wheel.transform.rotation.z + 15));
                }
                else if (Input.GetTouch(0).position.x > Screen.width / 2) // if its on the right side of the screen, rotate the wheel right.
                {
                    Debug.Log("Right!");
                    wheel.transform.Rotate(new Vector3(wheel.transform.rotation.x, wheel.transform.rotation.y, wheel.transform.rotation.z - 15));
                }
                else
                    Debug.Log("Center!");
            }
            //if(Input.GetTouch(0).position.x)
        }
    }
}
