using UnityEngine;

public class SlideMenuBar : MonoBehaviour
{
    public RectTransform buttonBar;        // Assign in inspector
    private float hiddenY = 350f;          // Hidden position
    private float shownY = 290f;              // Shown position
    public float animationSpeed = 500f;    // Pixels per second

    private bool isOpen = false;
    private bool isAnimating = false;

    void Update()
    {
        if (isAnimating)
        {
            Vector2 targetPos = new Vector2(buttonBar.anchoredPosition.x, isOpen ? shownY : hiddenY);
            buttonBar.anchoredPosition = Vector2.MoveTowards(buttonBar.anchoredPosition, targetPos, animationSpeed * Time.deltaTime);

            if (Vector2.Distance(buttonBar.anchoredPosition, targetPos) < 0.01f)
            {
                buttonBar.anchoredPosition = targetPos;
                isAnimating = false;
            }
        }
    }

    public void ToggleBar()
    {
        if (!isAnimating)
        {
            isOpen = !isOpen;
            isAnimating = true;
        }
    }
}
