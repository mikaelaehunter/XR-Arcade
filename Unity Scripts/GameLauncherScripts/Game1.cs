using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class Game1 : MonoBehaviour {

    private Process process;
    private Transform messageTransform;
    private Text messageText;
    private Button_UI topDownShooterButton;

    private void Awake() {
        Application.runInBackground = false;
        Application.targetFrameRate = 100;

        topDownShooterButton = transform.Find("topDownShooterBtn").GetComponent<Button_UI>();
        topDownShooterButton.ClickFunc = () => {
            // Launch Game!
            string path = Application.dataPath + "/../Builds/Students/alex/Rosenberger_Alexandra_Ass01/Rosenberger_Alexandra_Ass01.exe";
            process = Process.Start(path);
            ShowProcessLaunchedMessage("Launching Forest of Music...");
        };

        messageTransform = transform.Find("message");
        messageText = messageTransform.Find("Text").GetComponent<Text>();
        HideProcessLaunchedMessage();
    }

    private void Update() {

        if (process != null && process.HasExited) {
            // Process exited!
            process = null;
            HideProcessLaunchedMessage();
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            // Close the application
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.Return)) {
            // Simulate button click when Enter key is pressed
            topDownShooterButton.ClickFunc();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            // Simulate button hover when Right Arrow key is pressed
            topDownShooterButton.ManualOnPointerEnter();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            // Simulate mouse hover exit when Left Arrow key is pressed
            topDownShooterButton.ManualOnPointerExit();
        }
    }

    private void ShowProcessLaunchedMessage(string message) {
        messageTransform.gameObject.SetActive(true);
        messageText.text = message;
    }

    private void HideProcessLaunchedMessage() {
        messageTransform.gameObject.SetActive(false);
    }
}