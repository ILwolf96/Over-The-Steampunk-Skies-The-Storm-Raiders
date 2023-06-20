using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelManager : MonoBehaviour
{
    [SerializeField] private GameObject wheel; //This is a separate Script that is outside of the game object itself, Sort of a Psuedo Game Manager except for the helm of the ship.
    [SerializeField] private AnimationCurve wheelCurve; // Animation Curves!
    public float rotateSpeed = 15f; //As the namesake says...Its not that its actually duration.
    public float degrees = 15; // Makes the rotation customizable.
    private Vector3 currentPosition; // Saves current position for the tweening.
    private Vector3 nextposition; //Target Vector3 for the tween to use.
    private void Start()
    {
        currentPosition = new Vector3(wheel.transform.rotation.x, wheel.transform.rotation.y, wheel.transform.rotation.z); //Saves the initial wheel position to work from there, it means the inititial rotation of the wheel can be changed in Unity and it won't break everything!
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
                    nextposition.z=currentPosition.z+degrees; // changes the Vector to the new one with the new Z coordinates.
                    currentPosition = nextposition; //...Kinda Pointless to be honest, it made sense when I wrote it Okay? - Sahar
                    Debug.Log("Left! " + nextposition.z);
                    //wheel.transform.Rotate(new Vector3(wheel.transform.rotation.x, wheel.transform.rotation.y, wheel.transform.rotation.z + 15));
                    wheel.transform.DORotate(nextposition,rotateSpeed).SetEase(wheelCurve);
                }
                else if (Input.GetTouch(0).position.x > Screen.width / 2) // if its on the right side of the screen, rotate the wheel right, literally just a mirrored version of the first if.
                {
                    Debug.Log("Right!");
                    // wheel.transform.Rotate(new Vector3(wheel.transform.rotation.x, wheel.transform.rotation.y, wheel.transform.rotation.z - 15));
                    nextposition.z = currentPosition.z - degrees;
                    currentPosition = nextposition;
                    wheel.transform.DORotate(nextposition, rotateSpeed).SetEase(wheelCurve);
                }
                else
                    Debug.Log("Center!");
            }
        }
    }
}
