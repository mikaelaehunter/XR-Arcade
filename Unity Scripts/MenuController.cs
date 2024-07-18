using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject screenSaver;
    public GameObject aboutMenu;
    public GameObject buttonMenu1;
    public GameObject buttonMenu2;
    public GameObject game1Menu;
    public GameObject game2Menu;
    public GameObject game3Menu;
    public GameObject game4Menu;
    public GameObject game5Menu;
    public GameObject game6Menu;

    void Start()
    {
        // Ensure only the screenSaver is active at the start
        screenSaver.SetActive(true);
        aboutMenu.SetActive(false);
        buttonMenu1.SetActive(false);
        buttonMenu2.SetActive(false);
        game1Menu.SetActive(false);
        game2Menu.SetActive(false);
        game3Menu.SetActive(false);
        game4Menu.SetActive(false);
        game5Menu.SetActive(false);
        game6Menu.SetActive(false);
    }

    void Update()
    {
        // Check if the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitApplication();
        }
    }

    public void ShowScreenSaver()
    {
        screenSaver.SetActive(true);
        aboutMenu.SetActive(false);
        buttonMenu1.SetActive(false);
        buttonMenu2.SetActive(false);
        game1Menu.SetActive(false);
        game2Menu.SetActive(false);
        game3Menu.SetActive(false);
        game4Menu.SetActive(false);
        game5Menu.SetActive(false);
        game6Menu.SetActive(false);
    }

    public void ShowAboutMenu()
    {
        screenSaver.SetActive(false);
        aboutMenu.SetActive(true);
        buttonMenu1.SetActive(false);
        buttonMenu2.SetActive(false);
        game1Menu.SetActive(false);
        game2Menu.SetActive(false);
        game3Menu.SetActive(false);
        game4Menu.SetActive(false);
        game5Menu.SetActive(false);
        game6Menu.SetActive(false);
    }

    public void ShowButtonMenu1()
    {
        screenSaver.SetActive(false);
        aboutMenu.SetActive(false);
        buttonMenu1.SetActive(true);
        buttonMenu2.SetActive(false);
        game1Menu.SetActive(false);
        game2Menu.SetActive(false);
        game3Menu.SetActive(false);
        game4Menu.SetActive(false);
        game5Menu.SetActive(false);
        game6Menu.SetActive(false);
    }

    public void ShowButtonMenu2()
    {
        screenSaver.SetActive(false);
        aboutMenu.SetActive(false);
        buttonMenu1.SetActive(false);
        buttonMenu2.SetActive(true);
        game1Menu.SetActive(false);
        game2Menu.SetActive(false);
        game3Menu.SetActive(false);
        game4Menu.SetActive(false);
        game5Menu.SetActive(false);
        game6Menu.SetActive(false);
    }

    public void ShowGame1Menu()
    {
        screenSaver.SetActive(false);
        aboutMenu.SetActive(false);
        buttonMenu1.SetActive(false);
        buttonMenu2.SetActive(false);
        game1Menu.SetActive(true);
        game2Menu.SetActive(false);
        game3Menu.SetActive(false);
        game4Menu.SetActive(false);
        game5Menu.SetActive(false);
        game6Menu.SetActive(false);
    }

    // Similarly, create ShowGame2Menu(), ShowGame3Menu(), ..., ShowGame6Menu()

    public void ShowGame2Menu()
    {
        screenSaver.SetActive(false);
        aboutMenu.SetActive(false);
        buttonMenu1.SetActive(false);
        buttonMenu2.SetActive(false);
        game1Menu.SetActive(false);
        game2Menu.SetActive(true);
        game3Menu.SetActive(false);
        game4Menu.SetActive(false);
        game5Menu.SetActive(false);
        game6Menu.SetActive(false);
    }

    public void ShowGame3Menu()
    {
        screenSaver.SetActive(false);
        aboutMenu.SetActive(false);
        buttonMenu1.SetActive(false);
        buttonMenu2.SetActive(false);
        game1Menu.SetActive(false);
        game2Menu.SetActive(false);
        game3Menu.SetActive(true);
        game4Menu.SetActive(false);
        game5Menu.SetActive(false);
        game6Menu.SetActive(false);
    }

    public void ShowGame4Menu()
    {
        screenSaver.SetActive(false);
        aboutMenu.SetActive(false);
        buttonMenu1.SetActive(false);
        buttonMenu2.SetActive(false);
        game1Menu.SetActive(false);
        game2Menu.SetActive(false);
        game3Menu.SetActive(false);
        game4Menu.SetActive(true);
        game5Menu.SetActive(false);
        game6Menu.SetActive(false);
    }

    public void ShowGame5Menu()
    {
        screenSaver.SetActive(false);
        aboutMenu.SetActive(false);
        buttonMenu1.SetActive(false);
        buttonMenu2.SetActive(false);
        game1Menu.SetActive(false);
        game2Menu.SetActive(false);
        game3Menu.SetActive(false);
        game4Menu.SetActive(false);
        game5Menu.SetActive(true);
        game6Menu.SetActive(false);
    }

    public void ShowGame6Menu()
    {
        screenSaver.SetActive(false);
        aboutMenu.SetActive(false);
        buttonMenu1.SetActive(false);
        buttonMenu2.SetActive(false);
        game1Menu.SetActive(false);
        game2Menu.SetActive(false);
        game3Menu.SetActive(false);
        game4Menu.SetActive(false);
        game5Menu.SetActive(false);
        game6Menu.SetActive(true);
    }

    public void ExitApplication()
    {
        // Quit the application
        Application.Quit();

        // If running in the Unity editor, stop playing
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}