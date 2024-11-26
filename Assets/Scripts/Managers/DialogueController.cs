using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using System;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private GameObject dialogPanel; // Dialogue box UI
    [SerializeField] private TextMeshProUGUI dialogueText;      // Dialogue text
    [SerializeField] private GameObject gamepadHint; // UI for gamepad button (e.g., "Press A")
    [SerializeField] private GameObject keyboardHint; // UI for keyboard button (e.g., "Press Space")
    [SerializeField] private float typingSpeed = 0.05f; // Speed of typing effect

    private string[] dialogueLines = {
        "Welcome to the Gravity Snap!",
        "Move: A-D or Left/Right Arrows or Gamepad Joystick.",
        "Jump: Space or Gamepad A Button.",
        "Flip Gravity: G or Gamepad B Button.",
        "Let's start the adventure!"
    };

    private int currentLineIndex = 0;
    private bool isTyping = false;

    private void Start()
    {
        dialogPanel.SetActive(true);
        ShowLine();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Detect both keyboard and gamepad input
        {
            NextLine();
        }
    }

    private void ShowLine()
    {
        if (currentLineIndex < dialogueLines.Length)
        {
            StartCoroutine(TypeLine(dialogueLines[currentLineIndex]));
            UpdateInputHints();
        }
        else
        {
            EndDialogue();
        }
    }

    private IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }
    private void NextLine()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.text = dialogueLines[currentLineIndex];
            isTyping = false;
        }
        else
        {
            currentLineIndex++;
            ShowLine();
        }
    }

    private void EndDialogue()
    {
        dialogPanel.SetActive(false);
    }

    private void UpdateInputHints()
    {
        bool isGamepadConnected = Input.GetJoystickNames().Length > 0;
        gamepadHint.SetActive(isGamepadConnected);
        keyboardHint.SetActive(!isGamepadConnected);
    }
}
