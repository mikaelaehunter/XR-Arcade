using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class Game2 : MonoBehaviour {

    private Process process;
    private Transform messageTransform;
    private Text messageText;
    private Button_UI minesweeperButton;

    private void Awake() {
        Application.runInBackground = false;
        Application.targetFrameRate = 100;

        minesweeperButton = transform.Find("minesweeperBtn").GetComponent<Button_UI>();
        minesweeperButton.ClickFunc = () => {
            // Launch Game!
            string path = Application.dataPath + "/../Builds/Students/anna/AnnaRose_Garvey_HeadRoomVR/HeadRoomExe/My project (1).exe";
            process = Process.Start(path);
            ShowProcessLaunchedMessage("Launching Head Room...");
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
            minesweeperButton.ClickFunc();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            // Simulate button hover when Right Arrow key is pressed
            minesweeperButton.ManualOnPointerEnter();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            // Simulate mouse hover exit when Left Arrow key is pressed
            minesweeperButton.ManualOnPointerExit();
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


