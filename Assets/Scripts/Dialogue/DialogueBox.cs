using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Text.RegularExpressions;

public class DialogueBox : MonoBehaviour
{
    public Text dialogueText;
    public Text speakerText;
    private Image textBox;
    [SerializeField] private Image spinner;

    public float scrollSpeed;

    public UnityEvent onDialogueStart;
    public UnityEvent onDialogueEnd;

    SoundController sc;

    bool skipped;

    void Start()
    {
        textBox = GetComponent<Image>();
        spinner.enabled = false;
        clearText();

        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        sc = GetComponent<SoundController>();
        onDialogueStart.AddListener(player.pauseMovement);
        onDialogueEnd.AddListener(player.resumeMovement);
    }

    public void startDialogue(DialogueController dc) {
        textBox.enabled = true;   
        onDialogueStart.Invoke();
        sc.playSound(0);
        StartCoroutine(scrollDialogue(dc.selectedDialogue));
    }

    void Update() {
        if (Input.GetKeyDown("space") ||  Input.GetKeyDown(KeyCode.E)) {
            skipped = true;
        }
    }

    private IEnumerator scrollDialogue(Dialogue dialogue) {
        // prevents dialogue from automatically fast-forwarding
        bool buttonReleased = false;

        string[] script = dialogue.script;
        string[] speaker = dialogue.speaker;

        for (int i = 0; i < script.Length; i++) {
            speakerText.text = speaker[i];
            buttonReleased = false;
            skipped = false;
            GameObject.Find("Main Camera").GetComponent<CameraController>().setOnPartner(!speaker[i].Equals("Cypress")); 
            for (int j = 0; j < script[i].Length; j++) {
                dialogueText.text = processText(script[i].Substring(0, j));
                if (dialogueText.text.EndsWith(".") || dialogueText.text.EndsWith("?") || dialogueText.text.EndsWith("!")) {
                    yield return new WaitForSeconds(scrollSpeed * 10);
                }
                yield return new WaitForSeconds(scrollSpeed);
                // fast-forward text
                if (skipped && buttonReleased) {
                    buttonReleased = false;
                    break;
                }

                if (Input.GetKeyUp("space") ||  Input.GetKeyUp(KeyCode.E)) {
                    buttonReleased = true;
                }
            }

            dialogueText.text = processText(script[i]);
            spinner.enabled = true;

            yield return null;

            while (Input.GetKey("space") ||  Input.GetKey(KeyCode.E)) {
                yield return null;
                Debug.Log("waiting");
            }

            while (!(Input.GetKeyDown("space") ||  Input.GetKeyDown(KeyCode.E))) {
                yield return null;
            }

            sc.playSoundWorldly(1);
            buttonReleased = false;
            spinner.enabled = false;
        }

        // on completion of dialogue
        GameProgressManager.Instance.addProgress(dialogue.flagAfterCompletion);
        GameStateManager.Instance.setCurrentState(GameStateManager.GameState.Exploring);
        if (dialogue.taskCompleted != GameTaskManager.TaskName.None) {
            GameTaskManager.Instance.updateTask(dialogue.taskCompleted, 1);
        }
        else if (dialogue.newTaskAfterCompletion != GameTaskManager.TaskName.NoUpdate && GameTaskManager.Instance.getCurrentTaskName() != dialogue.newTaskAfterCompletion) {
            GameTaskManager.Instance.setCurrentTask(dialogue.newTaskAfterCompletion);
            GameTaskManager.Instance.onNewTask.Invoke();
        }
        clearText();
        onDialogueEnd.Invoke();
    }

    private void clearText() {
        textBox.enabled = false;
        dialogueText.text = "";
        speakerText.text = "";
    }

    private string processText(string input) {
        Regex yellowText = new Regex("#");  
        if (yellowText.Matches(input).Count % 2 != 0) {
            input = input + "#";
        }
        while(yellowText.Matches(input).Count > 0) {
            input = yellowText.Replace(input, "<color=yellow>", 1);
            input = yellowText.Replace(input, "</color>", 1);
        }

        Regex cyanText = new Regex(@"\*");  
        if (cyanText.Matches(input).Count % 2 != 0) {
            input = input + "*";
        }
        while(cyanText.Matches(input).Count > 0) {
            input = cyanText.Replace(input, "<color=cyan>", 1);
            input = cyanText.Replace(input, "</color>", 1);
        }

        Regex greyText = new Regex("&");  
        if (greyText.Matches(input).Count % 2 != 0) {
            input = input + "&";
        }
        while(greyText.Matches(input).Count > 0) {
            input = greyText.Replace(input, "<color=silver>", 1);
            input = greyText.Replace(input, "</color>", 1);
        }

        return input;
    }
}
