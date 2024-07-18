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
    public Button buttonPageBackButton;
    public GameObject[] gamePages; // Assign the game detail pages GameObjects here
    private Button[] buttons1;
    private Button[] buttons2;
    private Color originalColor;
    private Color highlightedColor = new Color(1.0f, 0.75f, 0.8f); // Light pink color
    private Button currentlyHighlightedButton;
    private GameObject currentPage;
    private Button lastHighlightedButton; // Store the last highlighted button

    private int columns = 3; // Number of columns in the button grid

    // Start is called before the first frame update
    void Start()
    {
        //code relating to the idle screen saver
        timeDif = maxTimeDif;

        //code relating to the button highlighting
        buttons1 = buttonPage.GetComponentsInChildren<Button>();// Initialize button array

        // Store the original color of the buttons (assuming all buttons have the same color)
        if (buttons1.Length > 0)
        {
            originalColor = buttons1[0].GetComponent<Image>().color;

        }

        aboutPage.SetActive(true); //aboutPage is active initially
        buttonPage.SetActive(false); //buttonPage is inactive initially

        // Initially hide all game detail pages
        foreach (GameObject page in gamePages)
        {
            page.SetActive(false);
        }

        currentPage = aboutPage; // Start with aboutPage
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseDelta = Input.mousePosition - lastMouseCoordinate;

        timeDif -= Time.deltaTime;
        if (timeDif <= 0)
        {
            buffer = "";
        }

        if (Input.anyKey || (mouseDelta.x > 0 || mouseDelta.y > 0))
        {
            AddToBuffer("a");
        }

        lastMouseCoordinate = Input.mousePosition;

        CheckBuffer();

        HandlePageNavigation();
    }

    void AddToBuffer(string c)
    {
        timeDif = maxTimeDif;
        buffer += c;
    }

    void CheckBuffer()
    {
        if (buffer.Length < 1) // If the buffer is empty
        {
            ActivateScreenSaver();
        }
        else // If the buffer is not empty/is filling with "a"
        {
            DeactivateScreenSaver();
        }
    }

    void ActivateScreenSaver() // When the buffer is empty
    {
        screenSaver.SetActive(true); // Idle screen saver is active
        screenUI.SetActive(false); // Canvas is inactive
        wasScreenSaverActive = true; // Flag idle screen saver was active
        Debug.Log("The buffer is empty.");
    }

    void DeactivateScreenSaver() // When the buffer is full of "a"
    {
        if (screenSaver.activeSelf)
        {
            screenSaver.SetActive(false); // Screen saver is inactive
            screenUI.SetActive(true); // Canvas is active
            if (wasScreenSaverActive) // If idle screen saver was active
            {
                SwitchToAboutPage(); // Reset to aboutPage only if the screensaver was active
                isFirstNavigationAfterScreenSaver = true; // Set the flag to true
            }
            wasScreenSaverActive = false; // Reset flag so idle screen saver is inactive
        }
    }

    void HandlePageNavigation()
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
    if (currentlyHighlightedButton == game1Button)
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            HighlightButton(game4Button);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            HighlightButton(game2Button);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            HighlightButton(buttonPageBackButton);
        }
    }
    else if (currentlyHighlightedButton == game2Button)
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            HighlightButton(game3Button);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            HighlightButton(game1Button);
        }
    }
    else if (currentlyHighlightedButton == game3Button)
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            HighlightButton(game2Button);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            HighlightButton(game4Button);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            HighlightButton(game6Button);
        }
    }
    else if (currentlyHighlightedButton == game4Button)
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            HighlightButton(game1Button);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            HighlightButton(game5Button);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            HighlightButton(buttonPageBackButton);
        }
    }
    else if (currentlyHighlightedButton == game5Button)
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            HighlightButton(game4Button);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            HighlightButton(game6Button);
        }
    }
    else if (currentlyHighlightedButton == game6Button)
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            HighlightButton(game5Button);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            HighlightButton(game3Button);
        }
    }
    else if (currentlyHighlightedButton == buttonPageBackButton)
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SwitchToAboutPage();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            HighlightButton(game1Button);


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