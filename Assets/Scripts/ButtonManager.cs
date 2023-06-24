using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject lBorder;
    [SerializeField] private GameObject rBorder;
    [SerializeField] private GameObject tBorder;
    [SerializeField] private GameObject bBorder;
    // Update is called once per frame
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (((Input.GetTouch(0).position.x < rBorder.transform.position.x)&&(Input.GetTouch(0).position.x > lBorder.transform.position.x))&& 
                ((Input.GetTouch(0).position.y < tBorder.transform.position.y)&&(Input.GetTouch(0).position.y > bBorder.transform.position.y))) //checks if the press happens between the 4 borders.
                {
                    Debug.Log("Boop!");
                }
            }
        }
    }
}
