using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ScrollText : MonoBehaviour
{
    public ScrollRect scrollRect;  // Assign this in Inspector
    public float scrollSpeed = 0.1f; // Tweak for faster/slower scroll

    public RectTransform startPos;
    public RectTransform endPos;
    public RectTransform bar;

    public PlayerInput _playerInput;

    void Update()
    {
        float verticalInput = _playerInput.actions["Navigate"].ReadValue<Vector2>().y; // -1 to 1

        // Only scroll if there’s input
        if (Mathf.Abs(verticalInput) > 0.01f)
        {
            float newPosition = scrollRect.verticalNormalizedPosition + verticalInput * scrollSpeed * Time.deltaTime;
            newPosition = Mathf.Clamp01(newPosition); // Keep between 0 and 1
            scrollRect.verticalNormalizedPosition = newPosition;
        }

        // Get the scroll value (1 = top, 0 = bottom)
        float t = scrollRect.verticalNormalizedPosition;

        // Calculate target position between start and end
        Vector2 targetPos = Vector2.Lerp(endPos.anchoredPosition, startPos.anchoredPosition, t); // Note: Lerp from bottom to top

        // Smoothly move the image toward target position
        bar.anchoredPosition = Vector2.Lerp(bar.anchoredPosition, targetPos, Time.deltaTime * 10f);
    }
}
