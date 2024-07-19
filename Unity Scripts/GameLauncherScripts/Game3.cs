using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class Game3 : MonoBehaviour {

    private Process process;
    private Transform messageTransform;
    private Text messageText;
    private Button_UI washYourHandsButton;

    private void Awake() {
        Application.runInBackground = false;
        Application.targetFrameRate = 100;

        washYourHandsButton = transform.Find("washYourHandsBtn").GetComponent<Button_UI>();
        washYourHandsButton.ClickFunc = () => {
            // Launch Game!
            string path = Application.dataPath + "/../Builds/Students/david/David_Askari_Ass01EXE/RoadDownClimateChange.exe";
            process = Process.Start(path);
            ShowProcessLaunchedMessage("Launching Road Down Climate Change...");
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
            washYourHandsButton.ClickFunc();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            // Simulate button hover when Right Arrow key is pressed
            washYourHandsButton.ManualOnPointerEnter();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            // Simulate mouse hover exit when Left Arrow key is pressed
            washYourHandsButton.ManualOnPointerExit();
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


