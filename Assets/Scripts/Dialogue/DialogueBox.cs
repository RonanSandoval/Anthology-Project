using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DialogueBox : MonoBehaviour
{
    public Text dialogueText;
    public Text speakerText;
    private Image textBox;

    public float scrollSpeed;

    public UnityEvent onDialogueStart;
    public UnityEvent onDialogueEnd;

    void Start()
    {
        textBox = GetComponent<Image>();
        clearText();

        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        onDialogueStart.AddListener(player.pauseMovement);
        onDialogueEnd.AddListener(player.resumeMovement);
    }

    public void startDialogue(DialogueController dc) {
        textBox.enabled = true;   
        onDialogueStart.Invoke();
        StartCoroutine(scrollDialogue(dc.selectedDialogue));
    }

    private IEnumerator scrollDialogue(Dialogue dialogue) {
        // prevents dialogue from automatically fast-forwarding
        bool buttonReleased = true;

        string[] script = dialogue.script;
        string[] speaker = dialogue.speaker;

        for (int i = 0; i < script.Length; i++) {
            speakerText.text = speaker[i];
            for (int j = 0; j < script[i].Length; j++) {
                yield return new WaitForSeconds(scrollSpeed);
                dialogueText.text = script[i].Substring(0, j);
                // fast-forward text
                if ((Input.GetKey("space") ||  Input.GetKey(KeyCode.E)) && buttonReleased) {
                    break;
                }
                if (Input.GetKeyUp("space") ||  Input.GetKeyUp(KeyCode.E)) {
                    buttonReleased = true;
                }
            }

            dialogueText.text = script[i];

            while (!(Input.GetKeyDown("space") ||  Input.GetKeyDown(KeyCode.E))) {
                yield return null;
            }

            buttonReleased = false;
        }

        // on completion of dialogue
        GameProgressManager.Instance.addProgress(dialogue.flagAfterCompletion);
        GameStateManager.Instance.setCurrentState(GameStateManager.GameState.Exploring);
        GameTaskManager.Instance.setCurrentTask(dialogue.taskIndexAfterCompletion);
        clearText();
        onDialogueEnd.Invoke();
    }

    private void clearText() {
        textBox.enabled = false;
        dialogueText.text = "";
        speakerText.text = "";
    }
}
