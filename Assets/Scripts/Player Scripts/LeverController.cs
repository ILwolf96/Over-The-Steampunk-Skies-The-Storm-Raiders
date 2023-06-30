using UnityEngine;
using UnityEngine.EventSystems;

public class LeverController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    // The positions for the five modes of the lever
    [SerializeField] GameObject lever;
    public Transform forwardPosition;
    public Transform midForwardPosition;
    public Transform midPosition;
    public Transform midBackwardPosition;
    public Transform backwardPosition;
    
    // The speed at which the lever moves towards the target position
    public float movementSpeed = 5f;

    // The current mode of the lever
    private LeverMode currentMode = LeverMode.Mid;

    // The target position for the lever
    private Transform targetPosition;

    // Flag to indicate if the lever is being dragged
    private bool isDragging = false;

    // Enum to represent the different modes of the lever
    public enum LeverMode
    {
        Forward,
        MidForward,
        Mid,
        MidBackward,
        Backward
    }

    // Event handler for when the pointer is pressed on the lever
    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
    }

    // Event handler for when the pointer is dragged on the lever
    public void OnDrag(PointerEventData eventData)
    {
        // Calculate the drag distance
        float dragDistance = Vector2.Distance(eventData.position, transform.position);

        // Calculate the direction of the drag
        Vector2 dragDirection = (eventData.position - (Vector2)transform.position).normalized;

        // Update the target position based on the drag direction
        if (dragDistance > 0f)
        {
            if (dragDirection.y > 0f)
            {
                if (dragDirection.x > 0f)
                {
                    targetPosition = midForwardPosition;
                    currentMode = LeverMode.MidForward;
                }
                else
                {
                    targetPosition = forwardPosition;
                    currentMode = LeverMode.Forward;
                }
            }
            else
            {
                if (dragDirection.x > 0f)
                {
                    targetPosition = midBackwardPosition;
                    currentMode = LeverMode.MidBackward;
                }
                else
                {
                    targetPosition = backwardPosition;
                    currentMode = LeverMode.Backward;
                }
            }
        }
    }

    // Event handler for when the pointer is released from the lever
    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
        targetPosition = midPosition;
        currentMode = LeverMode.Mid;
    }

    // Update is called once per frame
    private void Update()
    {
        // If the lever is not being dragged, move it towards the target position
        if (!isDragging)
        {
            lever.transform.position = Vector3.Lerp(lever.transform.position, targetPosition.position, Time.deltaTime * movementSpeed);
        }

        // Perform actions based on the current mode
        switch (currentMode)
        {
            case LeverMode.Forward:
                // Perform action for the forward mode
                break;
            case LeverMode.MidForward:
                // Perform action for the mid-forward mode
                break;
            case LeverMode.Mid:
                // Perform action for the mid mode
                break;
            case LeverMode.MidBackward:
                // Perform action for the mid-backward mode
                break;
            case LeverMode.Backward:
                // Perform action for the backward mode
                break;
        }
    }
}
