using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueController : MonoBehaviour
{
    public Dialogue selectedDialogue;

    public DialogueBox dialogueBox;

    private GameObject keyIndicator;

    // Start is called before the first frame update
    void Start()
    {
        dialogueBox = GameObject.Find("Dialogue Box").GetComponent<DialogueBox>();
        keyIndicator = transform.Find("Key Indicator").gameObject;
        keyIndicator.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void selectDialogue() {
        for(int i = 0; i < transform.Find("Dialogue").childCount; i++)
        {
            GameObject child = transform.Find("Dialogue").GetChild(i).gameObject;
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
            keyIndicator.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            keyIndicator.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E)
            && GameStateManager.Instance.checkState(GameStateManager.GameState.Exploring)) {
            startDialogue();
        }
    }

}
