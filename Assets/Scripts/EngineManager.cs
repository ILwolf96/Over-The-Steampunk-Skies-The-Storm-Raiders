using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineManager : MonoBehaviour
{
    [SerializeField] GameObject mLever;
    [SerializeField] GameObject mfLever;
    [SerializeField] GameObject fLever;
    [SerializeField] GameObject mbLever;
    [SerializeField] GameObject bLever;
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
        
    }
}
