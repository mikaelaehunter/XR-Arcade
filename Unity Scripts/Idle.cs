using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Idle : MonoBehaviour
{
    // Create all the GameObjects and variables associated with the IdleScreenSaver
    [SerializeField] private string buffer = "a";
    [SerializeField] private float maxTimeDif = 5f; //five seconds of idle time before idleScreenSaver appears
    [SerializeField] private float timeDif; //counts down from 5
    [SerializeField] private GameObject screenSaver;
    [SerializeField] private GameObject screenUI;
    [SerializeField] private GameObject aboutPage;
    [SerializeField] private GameObject buttonPage1;
    [SerializeField] private GameObject buttonPage2;
    private Vector3 lastMouseCoordinate = Vector3.zero;
    private bool wasScreenSaverActive = false; //f;ag to check if screen saver was active
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
    public Button buttonPage1BackButton;
    public Button buttonPage1DownButton;

    //buttonPage2 buttons
    public Button game7Button;
    public Button game8Button;
    public Button game9Button;
    public Button game10Button;
    public Button game11Button;
    public Button game12Button;
    public Button buttonPage2BackButton;

    public GameObject[] gamePages; // Assign the game detail pages GameObjects here
    private Button[] buttons1;
    private Button[] buttons2;
    private Color originalColor;
    private Color highlightedColor = new Color(1.0f, 0.75f, 0.8f); // Light pink color
    private Button currentlyHighlightedButton;
    private GameObject currentPage;
    private Button lastHighlightedButton; // Store the last highlighted button

    // Start is called before the first frame update
    void Start()
    {
        //code relating to the idle screen saver
        timeDif = maxTimeDif;

        //code relating to the button highlighting
        buttons1 = buttonPage1.GetComponentsInChildren<Button>();
        buttons2 = buttonPage2.GetComponentsInChildren<Button>();

        // Store the original color of the buttons (assuming all buttons have the same color)
        if (buttons1.Length > 0)
        {
            originalColor = buttons1[0].GetComponent<Image>().color;
        }

        if (buttons2.Length > 0)
        {
           originalColor = buttons2[0].GetComponent<Image>().color; 
        }

        aboutPage.SetActive(true); //aboutPage is active initially
        buttonPage1.SetActive(false); //buttonPage1 is inactive initially
        buttonPage2.SetActive(false); //buttonPage2 is inactive initially

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
        if (timeDif <= 0) //empty buffer = idle screen saver activated
        {
            buffer = "";
        }

        if (Input.anyKey || (mouseDelta.x > 0 || mouseDelta.y > 0)) //moving mouse = idle screen saver inactive
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
            HandleAboutPageNavigation();
        }
        else if (currentPage == buttonPage1)
        {
            HandleButtonPage1Navigation();
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                OpenGameDetails();
            }
        }
        else if (currentPage == buttonPage2)
        {
            HandleButtonPage2Navigation();
        }
        else
        {
            HandleGamePageNavigation();
        }
    }


    private void HandleAboutPageNavigation()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow)) //if we press the right arrow key
        {
            HighlightButton(aboutPageNextButton); //highlight the aboutPage arrow
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) //if we press the left arrow key
        {
            UnhighlightButton(aboutPageNextButton); //unhighlight the aboutPage arrow
        }
        else if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) &&
                 currentlyHighlightedButton == aboutPageNextButton) //if wer press enter or return when the aboutPage arrow is highlighted
        {
            SwitchToButtonPage1(); //move to buttonPage1
        }
    }

    private void HandleGamePageNavigation() //how we will handle each game menu
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) //if we press the left arrow key
        {
            HighlightButton(currentPage.GetComponentsInChildren<Button>()[0]); // Assuming back button is first
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) //if we press the right arrow
        {
            UnhighlightButton(currentPage.GetComponentsInChildren<Button>()[0]); //unhighlight arrow
        }
        else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) //if we press return or enter
        {
            if (currentlyHighlightedButton.name.Contains("BackButton")) //and left arrow is highlighted
            {
                SwitchToButtonPage1(); //move to button page 1
            }
        }
    }

    private void SwitchToButtonPage1() // Switch from current page to buttonPage
    {
        currentPage.SetActive(false); // The current page that was not button page is inactive
        buttonPage1.SetActive(true); // Button page is active

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

        currentPage = buttonPage1;
    }

    private void SwitchToButtonPage2()
    {
        // Store the currently highlighted button before switching pages
        lastHighlightedButton = currentlyHighlightedButton;

        // Switch from buttonPage1 to buttonPage2
        buttonPage1.SetActive(false);
        buttonPage2.SetActive(true);
        currentPage = buttonPage2;

        // Highlight the first button on the new page (e.g., game7Button)
        HighlightButton(buttonPage2.GetComponentsInChildren<Button>()[0]);
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
        Debug.Log("Highlighted Button: " + buttonToHighlight.name);
    }

    private void UnhighlightButton(Button buttonToUnhighlight) //function to unhighlight arrow buttons
    {
        if (buttonToUnhighlight == currentlyHighlightedButton) //if you want to unhighlight a button
        {
            buttonToUnhighlight.GetComponent<Image>().color = originalColor;
            currentlyHighlightedButton = null;
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    private void HandleButtonPage1Navigation() //navigate the button pages
    {
        if (currentlyHighlightedButton == game1Button) //if game1Button is highlighted
        {
            if (Input.GetKeyDown(KeyCode.DownArrow)) //and the down arrow is pressed
            {
                HighlightButton(game4Button); //highlight game4Button
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow)) //if the right arrow is pressed
            {
                HighlightButton(game2Button); //highlight game2Button
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow)) //if the left arrow is pressed
            {
                HighlightButton(buttonPage1BackButton); //highlight arrow button
            }
        }
        else if (currentlyHighlightedButton == game2Button) //if game2Button is highlighted
        {
            if (Input.GetKeyDown(KeyCode.RightArrow)) //and the right arrow is pressed
            {
                HighlightButton(game3Button); //highlight game3Button
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow)) //if the left arrow is pressed
            {
                HighlightButton(game1Button); //highlight the game1Button
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow)) //if the down arrow is pressed
            {
                HighlightButton(game5Button); //highlight game1Button
            }
        }
        else if (currentlyHighlightedButton == game3Button) //if the game3Button is highlighted
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) //and the left arrow is pressed
            {
                HighlightButton(game2Button); //highlight game2Button
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow)) //if the right arrow is pressed
            {
                HighlightButton(game4Button); //highlight game4Button
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow)) //if the down arrow is pressed
            {
                HighlightButton(game6Button); //highlight the game6Button
            }
        }
        else if (currentlyHighlightedButton == game4Button) //if game4Button is highlighted
        {
            if (Input.GetKeyDown(KeyCode.UpArrow)) //and the up arrow is pressed
            {
                HighlightButton(game1Button); //highlight the game1Button
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow)) //if the right arrow is pressed
            {
                HighlightButton(game5Button); //highlight the game5Button
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow)) //if the left arrow is pressed
            {
                HighlightButton(buttonPage1BackButton); //highlight the arrow button
            }
        }
        else if (currentlyHighlightedButton == game5Button) //if game5Button is highlighted
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) //and the left arrow is pressed
            {
                HighlightButton(game4Button); //highlight the game4Button
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow)) //if the right arrow is pressed
            {
                HighlightButton(game6Button); //highlight the game6Button
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow)) //if the up arrow is pressed
            {
                HighlightButton(game2Button); //highlight the game2Button
            }
        }
        else if (currentlyHighlightedButton == game6Button) //if game6Button is highlighted
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) //and the left arrow is pressed
            {
                HighlightButton(game5Button); //highlight the game5Button
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow)) //if the up arrow is pressed
            {
                HighlightButton(game3Button); //highlight the game3Button
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow)) //if the right arrow is pressed
            {
                HighlightButton(buttonPage1DownButton); //highlight the down arrow
            }
        }
        else if (currentlyHighlightedButton == buttonPage1BackButton) //if the buttonPage1BackButton is highlighted
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) //and return or enter are pressed
            {
                SwitchToAboutPage(); //go back to the aboutPage
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow)) //if the right arrow is pressed
            {
                 HighlightButton(game1Button); //highlight the game1Button
            }
        }
        else if (currentlyHighlightedButton == buttonPage1DownButton) //if the buttonPage1DownArrow is highlighted
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) //and the left arrow is pressed
            {
                HighlightButton(game6Button); //highlight the game6Button
            }
            else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) //if enter or return are pressed
            {
                SwitchToButtonPage2(); //switch to second button page
            }
        }
    }

    private void HandleButtonPage2Navigation()
    {
        if (currentlyHighlightedButton == game7Button) // When game7Button is highlighted
        {
            if (Input.GetKeyDown(KeyCode.RightArrow)) // Right arrow pressed
            {
                HighlightButton(game8Button); // Highlight game8Button
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow)) // Down arrow pressed
            {
                HighlightButton(game10Button); // Highlight game10Button
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow)) // Left arrow pressed
            {
                HighlightButton(buttonPage2BackButton); // Highlight back button
            }
        }
        else if (currentlyHighlightedButton == game8Button) // When game8Button is highlighted
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) // Left arrow pressed
            {
                HighlightButton(game7Button); // Highlight game7Button
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow)) // Down arrow pressed
            {
                HighlightButton(game11Button); // Highlight game11Button
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow)) // Down arrow pressed
            {
                HighlightButton(game9Button); // Highlight game11Button
            }
        }
        else if (currentlyHighlightedButton == game9Button) // When game9Button is highlighted
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) // Left arrow pressed
            {
                HighlightButton(game8Button); // Highlight game8Button
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow)) // Down arrow pressed
            {
                HighlightButton(game12Button); // Highlight game12Button
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow)) // right arrow pressed
            {
                HighlightButton(game10Button); // Highlight game10Button
            }
        }
        else if (currentlyHighlightedButton == game10Button) // When game10Button is highlighted
        {
            if (Input.GetKeyDown(KeyCode.RightArrow)) // Right arrow pressed
            {
                HighlightButton(game11Button); // Highlight game11Button
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow)) // up arrow pressed
            {
                HighlightButton(game7Button); // Highlight game7Button
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow)) // Left arrow pressed
            {
                HighlightButton(buttonPage2BackButton); // Highlight back button
            }
        }
        else if (currentlyHighlightedButton == game11Button) // When game11Button is highlighted
        {
            if (Input.GetKeyDown(KeyCode.RightArrow)) // Right arrow pressed
            {
                HighlightButton(game12Button); // Highlight game12Button
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow)) // up arrow pressed
            {
                HighlightButton(game8Button); // Highlight game8Button
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow)) // Left arrow pressed
            {
                HighlightButton(game10Button); // Highlight back button
            }
        }
        else if (currentlyHighlightedButton == game12Button) // When game12Button is highlighted
        {
            if (Input.GetKeyDown(KeyCode.UpArrow)) // up arrow pressed
            {
                HighlightButton(game9Button); // Highlight game9Button
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow)) // Left arrow pressed
            {
                HighlightButton(game11Button); // Highlight game11Button
            }
        }
        else if (currentlyHighlightedButton == buttonPage2BackButton) // Back button highlighted
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) // Enter key pressed
            {
                SwitchToButtonPage1(); // Navigate back to ButtonPage1
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                HighlightButton(game7Button);
            }
        }
    }

    private void OpenGameDetails()
    {
        if (currentlyHighlightedButton == game1Button) //if game1Button is selected
        {
            SwitchToGamePage(0); //switch to game1Menu
        }
        else if (currentlyHighlightedButton == game2Button) //if game2Button is selected
        {
            SwitchToGamePage(1); //switch to game2Menu
        }
        else if (currentlyHighlightedButton == game3Button) //if game3Button is selected
        {
            SwitchToGamePage(2); //switch to game3Menu
        }
        else if (currentlyHighlightedButton == game4Button) //if game4Button is selected
        {
            SwitchToGamePage(3); //switch to game4Menu
        }
        else if (currentlyHighlightedButton == game5Button) //if game5Button is selected
        {
            SwitchToGamePage(4); //switch to game5Menu
        }
        else if (currentlyHighlightedButton == game6Button) //if game6Button is selected
        {
            SwitchToGamePage(5); //switch to game6Menu
        }
    }

    private void SwitchToGamePage(int pageIndex)
    {
         // Store the currently highlighted button before switching pages
        lastHighlightedButton = currentlyHighlightedButton;

        // Switch from buttonPage to respective game detail page
        buttonPage1.SetActive(false);

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