using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineManager : MonoBehaviour
{
    [SerializeField] private GameObject mLever;
    [SerializeField] private GameObject mfLever;
    [SerializeField] private GameObject fLever;
    [SerializeField] private GameObject mbLever;
    [SerializeField] private GameObject bLever;
    [SerializeField] private GameObject border;
    private Vector2 originalPosition; //tracks original position.
    private Vector2 direction; //Tracks direction.
    private int boatGear = 0;
    /*Notes:
     1: Detect via swipes for with phase "moved".
     2: Sort the button using borders the same way the wheel is done.
     3: Look into how to scale things up and down for different devices.
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
                        Debug.Log("Started Tracking");
                        break;
                    case TouchPhase.Moved:
                        direction = touch.position - originalPosition;
                        Debug.Log("Tracking...");
                        break;
                    case TouchPhase.Ended:
                        switch (direction.y)
                        {
                            case 0:
                                Debug.Log("Nothing Happened");
                                break;
                            case < 0:
                                Debug.Log("Slowing Down/Going Backwards!");
                                boatGear--;
                                changeShift();
                                break;
                            case > 0:
                                Debug.Log("Speeding Up!");
                                boatGear++;
                                changeShift();
                                break;
                        }
                        break;
                }
            }
        }
    }
    private void changeShift()
    {
        mfLever.SetActive(false);
        mbLever.SetActive(false);
        bLever.SetActive(false);
        fLever.SetActive(false);
        mLever.SetActive(false);
        switch (boatGear)
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
