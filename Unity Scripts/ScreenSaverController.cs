using UnityEngine;

public class ScreensaverController : MonoBehaviour
{
    public GameObject screensaverContent;
    public GameObject[] nextContentPages;

    private int currentPageIndex = 0;
    private bool screensaverActive = true;
    private bool inputReceived = false;

    void Start()
    {
        // Show screensaver content
        screensaverContent.SetActive(true);
        Cursor.visible = false;

        // Hide all next content pages
        foreach (var page in nextContentPages)
        {
            page.SetActive(false);
        }
    }

    void Update()
    {
        // Check for any key press or mouse movement to transition
        if (screensaverActive && !inputReceived && (Input.anyKeyDown || Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0))
        {
            inputReceived = true;
            TransitionToNextContent();
            Cursor.visible = true;
        }
    }

    void TransitionToNextContent()
    {
        // Hide screensaver content
        screensaverContent.SetActive(false);

        // Show current page of next content
        nextContentPages[currentPageIndex].SetActive(true);

        // Increment page index for next transition
        currentPageIndex++;

        // Check if all pages are shown, reset if true
        if (currentPageIndex >= nextContentPages.Length)
        {
            currentPageIndex = 0;
        }
    }
}