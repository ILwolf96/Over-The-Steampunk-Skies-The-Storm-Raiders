using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineManager : MonoBehaviour
{
    [Header("Ship")]
    [SerializeField] private GameObject warship; //Warship object.
    [Header("Engine Parts")]
    [SerializeField] private GameObject mLever;
    [SerializeField] private GameObject mfLever;
    [SerializeField] private GameObject fLever;
    [SerializeField] private GameObject mbLever;
    [SerializeField] private GameObject bLever;
    [SerializeField] private GameObject border;
    private Vector2 originalPosition; //tracks original position.
    private Vector2 direction; //Tracks direction.
    private int boatGear = 0;
    public float firstGear = 1;
    public float secondGear = 2;
    /*Notes:
     1: Detect via swipes for with phase "moved". - DONE
     2: Sort the button using borders the same way the wheel is done. - DONE
     3: Look into how to scale things up and down for different devices. - ???
     4: Can phase ended be used with the button? if so it saves us time and sanity. probably use if(!phase.ended) for the phase ended for the button.
        Answer: 
     */
    // Start is called before the first frame update
    void Start()
    {
        mfLever.SetActive(false);
        mbLever.SetActive(false);
        bLever.SetActive(false);
        fLever.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch(boatGear)
        {
            case 1: warship.transform.position += warship.transform.up*firstGear*Time.deltaTime;
                break;
            case 2: warship.transform.position += warship.transform.up * secondGear * Time.deltaTime;
                break;
            case -1: warship.transform.position -= warship.transform.up * firstGear * Time.deltaTime;
                break;
            case -2:
                warship.transform.position -= warship.transform.up * secondGear * Time.deltaTime;
                break;
        }
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.position.x>border.transform.position.x)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        //records initial position
                        originalPosition = touch.position;
                        //Debug.Log("Started Tracking");
                        break;
                    case TouchPhase.Moved: 
                        //tracks current direction to know where to shift the gear to.
                        direction = touch.position - originalPosition;
                        //Debug.Log("Tracking...");
                        break;
                    case TouchPhase.Ended:
                        //looks at what to do after the touch stops.
                        switch (direction.y)
                        {
                            case 0:
                                //Debug.Log("Nothing Happened");
                                break;
                            case < 0:
                                //Debug.Log("Slowing Down/Going Backwards!");
                                boatGear--;
                                changeShift();
                                break;
                            case > 0:
                                //Debug.Log("Speeding Up!");
                                boatGear++;
                                changeShift();
                                break;
                        }
                        break;
                }
            }
        }
    }
    private void changeShift() //This Method handles the change in the gear shifts for the boat...Boats have no gears.
    {
        //Sets all levers to false so it could be changed to the correct one.
        if(boatGear<-2) //Fix for everything disappearing.
            boatGear = -2;
        if(boatGear>2)
            boatGear= 2;
        mfLever.SetActive(false);
        mbLever.SetActive(false);
        bLever.SetActive(false);
        fLever.SetActive(false);
        mLever.SetActive(false);
        switch (boatGear) //switch swaps turns on the correct lever. 0 is for neutral. 1+2 are for forward, -1 + -2 are for backward movement.
        {
            case 0:
                mLever.SetActive(true);
                break;
            case 1:
                mfLever.SetActive(true);
                break;
            case 2:
                fLever.SetActive(true);
                break;
            case -1:
                mbLever.SetActive(true);
                break;
            case -2:
                bLever.SetActive(true);
                break;
        }
    }
}
