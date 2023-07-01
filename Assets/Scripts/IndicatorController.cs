using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorController : MonoBehaviour
{
    [SerializeField] private GameObject mLever;
    [SerializeField] private GameObject mfLever;
    [SerializeField] private GameObject fLever;
    [SerializeField] private GameObject mbLever;
    [SerializeField] private GameObject bLever;
    [SerializeField] private GameObject switchingIndicator;

    [SerializeField] private GameObject mIndicator;
    [SerializeField] private GameObject mfIndicator;
    [SerializeField] private GameObject fIndicator;
    [SerializeField] private GameObject mbIndicator;
    [SerializeField] private GameObject bIndicator;

    private GameObject activeIndicator;

    void Start()
    {
        UpdateIndicator();
    }

    void Update()
    {
        UpdateIndicator();
    }

    private void UpdateIndicator()
    {
        if (mLever.activeSelf)
        {
            ActivateIndicator(mIndicator);
        }
        else if (mfLever.activeSelf)
        {
            ActivateIndicator(mfIndicator);
        }
        else if (fLever.activeSelf)
        {
            ActivateIndicator(fIndicator);
        }
        else if (mbLever.activeSelf)
        {
            ActivateIndicator(mbIndicator);
        }
        else if (bLever.activeSelf)
        {
            ActivateIndicator(bIndicator);
        }
        else
        {
            ActivateIndicator(switchingIndicator);
        }
    }

    private void ActivateIndicator(GameObject indicator)
    {
        if (activeIndicator != null)
        {
            activeIndicator.SetActive(false);
        }
        activeIndicator = indicator;
        activeIndicator.SetActive(true);
    }
}
