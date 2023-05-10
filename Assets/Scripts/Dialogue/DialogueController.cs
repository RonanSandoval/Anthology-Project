using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueController : MonoBehaviour
{
    public Dialogue selectedDialogue;

    public DialogueBox dialogueBox;

    // Start is called before the first frame update
    void Start()
    {
        dialogueBox = GameObject.Find("Dialogue Box").GetComponent<DialogueBox>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void selectDialogue() {
        for(int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            Dialogue childDialogue = child.GetComponent<Dialogue>();
            if (childDialogue.dialogueIsAvailable()) {
                selectedDialogue = childDialogue;
                return;
            }
        }
    }

    public void startDialogue() {

        GameStateManager.Instance.setCurrentState(GameStateManager.GameState.Talking);
        selectDialogue();
        dialogueBox.startDialogue(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            startDialogue();
        }
    }
}
