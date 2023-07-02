using UnityEngine;

public class StormBorder : MonoBehaviour
{
    [SerializeField]
    private float maxDistance = 10f;

    [SerializeField]
    private float stormDamageDistance = 5f;

    private Transform centerPoint;

    private void Start()
    {
        centerPoint = GameObject.FindGameObjectWithTag("CenterPoint").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (centerPoint == null)
        {
            Debug.LogError("CenterPoint object not found. Make sure you have a CenterPoint object with the 'CenterPoint' tag in the scene.");
            return;
        }

        // Calculate the distance between the player (airship) and the center point
        float distance = Vector3.Distance(transform.position, centerPoint.position);

        // Check if the distance exceeds the maximum distance
        if (distance > maxDistance)
        {
            // Calculate the direction from the player to the center point
            Vector3 direction = (centerPoint.position - transform.position).normalized;

            // Move the player back within the maximum distance
            transform.position = centerPoint.position - direction * maxDistance;
        }

        // Check if the distance is greater than or equal to the storm damage distance
        if (distance >= stormDamageDistance)
        {
            Debug.Log("Storm Deals Damage");
            // Here you can add your code for handling the damage to the player (airship)
        }
    }
}
