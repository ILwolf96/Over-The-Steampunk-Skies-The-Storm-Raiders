using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelManager : MonoBehaviour
{
    [SerializeField] private GameObject wheel;
    [SerializeField] private AnimationCurve wheelCurve;
    public float rotateSpeed = 15f;
    private Vector3 currentPosition;
    private Vector3 nextposition;
    private void Start()
    {
        currentPosition = new Vector3(wheel.transform.rotation.x, wheel.transform.rotation.y, wheel.transform.rotation.z);
    }
    // Update is called once per frame
    private void Update()
    {
        if(Input.touchCount>0)
        {
            if(Input.GetTouch(0).phase== TouchPhase.Began) // Checks if touch has began.
            {
                
                if (Input.GetTouch(0).position.x < Screen.width / 2) // if its on the left side of the screen, rotate the wheel left.
                {
                    nextposition.z=currentPosition.z+15;
                    currentPosition = nextposition;
                    Debug.Log("Left! " + nextposition.z);
                    //wheel.transform.Rotate(new Vector3(wheel.transform.rotation.x, wheel.transform.rotation.y, wheel.transform.rotation.z + 15));
                    wheel.transform.DORotate(nextposition,rotateSpeed).SetEase(wheelCurve);
                }
                else if (Input.GetTouch(0).position.x > Screen.width / 2) // if its on the right side of the screen, rotate the wheel right.
                {
                    Debug.Log("Right!");
                    // wheel.transform.Rotate(new Vector3(wheel.transform.rotation.x, wheel.transform.rotation.y, wheel.transform.rotation.z - 15));
                    nextposition.z = currentPosition.z - 15;
                    currentPosition = nextposition;
                    wheel.transform.DORotate(nextposition, rotateSpeed).SetEase(wheelCurve);
                }
                else
                    Debug.Log("Center!");
            }
        }
    }
}
