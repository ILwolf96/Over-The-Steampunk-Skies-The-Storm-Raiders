using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelManager : MonoBehaviour
{
    /*
        Stuff to do:
     1: Modify the code so the wheel will only move if its being touched on the left or right parts of it. - DONE
     2: Probably converge this and future stuff into a single Game Manager, so currently its going to be split into different classes.
     Yes, I know that this method of using comments isn't great( as in the /* one), but it helps me a little.
    */
    [Header("Ship")]
    [SerializeField] private GameObject warship; //Warship object.
    private Vector3 shipPosition;
    [Header("Wheel")]
    [SerializeField] private GameObject wheel; //This is a separate Script that is outside of the game object itself, Sort of a Psuedo Game Manager except for the helm of the ship.
    [SerializeField] private AnimationCurve wheelCurve; // Animation Curves!
    [SerializeField] private GameObject leftBorder;
    [SerializeField] private GameObject rightBorder;
    public float rotateSpeed = 15f; //As the namesake says...Its not that its actually duration.
    public float degrees = 15; // Makes the rotation customizable.
    private Vector3 currentPosition; // Saves current position for the tweening.
    private Vector3 nextposition; //Target Vector3 for the tween to use.
    private void Start()
    {
        currentPosition = new Vector3(wheel.transform.rotation.x, wheel.transform.rotation.y, wheel.transform.rotation.z); //Saves the initial wheel position to work from there, it means the inititial rotation of the wheel can be changed in Unity and it won't break everything!
        shipPosition= wheel.transform.rotation.eulerAngles;
    }
    // Update is called once per frame
    private void Update()
    {
        
        if(Input.touchCount>0)
        {
            if(Input.GetTouch(0).phase== TouchPhase.Began) // Checks if touch has started.
            {
                
                if ((Input.GetTouch(0).position.x < wheel.transform.position.x)&&Input.GetTouch(0).position.x>leftBorder.transform.position.x) // if its on the left side of the screen, rotate the wheel left.
                {
                    nextposition.z=currentPosition.z+degrees; // changes the Vector to the new one with the new Z coordinates.
                    currentPosition = nextposition; //...Kinda Pointless to be honest, it made sense when I wrote it Okay? - Sahar
                    shipPosition.z += degrees; // Its the two above lines compressed into one, its for the ship though
                    Debug.Log("Left! " + nextposition.z);
                    //wheel.transform.Rotate(new Vector3(wheel.transform.rotation.x, wheel.transform.rotation.y, wheel.transform.rotation.z + 15));
                    wheel.transform.DORotate(nextposition,rotateSpeed).SetEase(wheelCurve);
                    warship.transform.DORotate(shipPosition,rotateSpeed).SetEase(wheelCurve);
                }
                else if ((Input.GetTouch(0).position.x > wheel.transform.position.x)&& Input.GetTouch(0).position.x < rightBorder.transform.position.x) // if its on the right side of the screen, rotate the wheel right, literally just a mirrored version of the first if.
                {
                    nextposition.z = currentPosition.z - degrees;
                    currentPosition = nextposition;
                    shipPosition.z -= degrees;
                    Debug.Log("Right! " +nextposition.z);
                    // wheel.transform.Rotate(new Vector3(wheel.transform.rotation.x, wheel.transform.rotation.y, wheel.transform.rotation.z - 15));
                    wheel.transform.DORotate(nextposition, rotateSpeed).SetEase(wheelCurve);
                    warship.transform.DORotate(shipPosition, rotateSpeed).SetEase(wheelCurve);
                }
               // else
               //     Debug.Log("Center!"); // I was taught to prepare for every scenario, Is here because of testing.
            }
        }
    }
}
