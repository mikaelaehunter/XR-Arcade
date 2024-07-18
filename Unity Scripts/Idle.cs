using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Idle : MonoBehaviour
{
    // Create all the GameObjects and variables associated with the IdleScreenSaver
    [SerializeField] private string buffer = "a";
    [SerializeField] private float maxTimeDif = 5f;
    [SerializeField] private float timeDif; // Counts down from 5
    [SerializeField] private GameObject screenSaver;
    [SerializeField] private GameObject screenUI;
    [SerializeField] private GameObject aboutPage;
    [SerializeField] private GameObject buttonPage;
    private Vector3 lastMouseCoordinate = Vector3.zero;
    private bool wasScreenSaverActive = false;
    private bool isFirstNavigationAfterScreenSaver = false; // Flag to track first navigation after screen saver

    // Create all the GameObjects and variables associated with ButtonHighlighter
    public Button firstButton;
    public Button game1Button;
    public Button game2Button;
    public Button game3Button;
    public Button game4Button;
    public Button game5Button;
    public Button game6Button;
    public Button aboutPageNextButton;
{
    if (currentPage == aboutPage)
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            HighlightButton(aboutPageNextButton); // Highlight the right arrow button
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            UnhighlightButton(aboutPageNextButton); // Unhighlight the right arrow button
        }
        else if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) &&
                 currentlyHighlightedButton == aboutPageNextButton)
        {
            SwitchToButtonPage(); // Move to buttonPage if rightArrowButton is highlighted
        }
    }
    else if (currentPage == buttonPage)
    {
        HandleButtonNavigation();

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            OpenGameDetails();
        }
    }
    else
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SwitchToButtonPage();
        }
    }
}

    private void SwitchToButtonPage() // Switch from current page to buttonPage
    {
        currentPage.SetActive(false); // The current page that was not button page is inactive
        buttonPage.SetActive(true); // Button page is active
   
        // Highlight the appropriate button on the buttonPage
        if (isFirstNavigationAfterScreenSaver)
        {
            HighlightButton(game1Button); // Highlight the game1Button
            isFirstNavigationAfterScreenSaver = false; // Reset the flag
        }
        else if (lastHighlightedButton != null)
        {
            HighlightButton(lastHighlightedButton);
        }
        else
        {
            HighlightButton(firstButton); // Default to highlighting the first button
        }

        currentPage = buttonPage;
    }

    private void HighlightButton(Button buttonToHighlight)
    {
        if (buttonToHighlight != currentlyHighlightedButton)
        {
            // Unhighlight the currently highlighted button
            if (currentlyHighlightedButton != null)
            {
                currentlyHighlightedButton.GetComponent<Image>().color = originalColor;
            }

            // Highlight the new button
            buttonToHighlight.GetComponent<Image>().color = highlightedColor;
            currentlyHighlightedButton = buttonToHighlight;

            // Set the new button as the selected button in the EventSystem
            EventSystem.current.SetSelectedGameObject(buttonToHighlight.gameObject);
        }
    }

    private void UnhighlightButton(Button buttonToUnhighlight)
{
    if (buttonToUnhighlight == currentlyHighlightedButton)
    {
        buttonToUnhighlight.GetComponent<Image>().color = originalColor;
        currentlyHighlightedButton = null;
        EventSystem.current.SetSelectedGameObject(null);
    }
}

    private void HandleButtonNavigation()
    {
        int currentIndex = System.Array.IndexOf(buttons, currentlyHighlightedButton);

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentlyHighlightedButton == game1Button || currentlyHighlightedButton == game4Button)
            {
                SwitchToAboutPage();
            }
            else if (currentIndex > 0)
            {
                HighlightButton(buttons[currentIndex - 1]);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentIndex < buttons.Length - 1)
            {
                HighlightButton(buttons[currentIndex + 1]);
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentIndex >= columns)
            {
                HighlightButton(buttons[currentIndex - columns]);
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentIndex + columns < buttons.Length)
            {
                HighlightButton(buttons[currentIndex + columns]);
            }
        }
    }

    private void OpenGameDetails()
    {
        if (currentlyHighlightedButton == game1Button)
        {
            SwitchToGamePage(0); // Game 1 details page
        }
        else if (currentlyHighlightedButton == game2Button)
        {
            SwitchToGamePage(1); // Game 2 details page
        }
        else if (currentlyHighlightedButton == game3Button)
        {
            SwitchToGamePage(2); // Game 3 details page
        }
        else if (currentlyHighlightedButton == game4Button)
        {
            SwitchToGamePage(3); // Game 4 details page
        }
        else if (currentlyHighlightedButton == game5Button)
        {
            SwitchToGamePage(4); // Game 5 details page
        }
        else if (currentlyHighlightedButton == game6Button)
        {
            SwitchToGamePage(5); // Game 6 details page
        }
    }

    private void SwitchToGamePage(int pageIndex)
    {
        // Store the currently highlighted button before switching pages
        lastHighlightedButton = currentlyHighlightedButton;

        // Switch from buttonPage to respective game detail page
        buttonPage.SetActive(false);
        gamePages[pageIndex].SetActive(true);
        currentPage = gamePages[pageIndex];
    }

    private void SwitchToAboutPage()
    {
        // Store the currently highlighted button before switching pages
        lastHighlightedButton = currentlyHighlightedButton;

        // Switch from current page to aboutPage
        currentPage.SetActive(false);
        aboutPage.SetActive(true);
        currentPage = aboutPage;
    }
}