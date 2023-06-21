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
