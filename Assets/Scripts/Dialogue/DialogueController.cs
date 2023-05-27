using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueController : Interactable
{
    public Dialogue selectedDialogue;

    public DialogueBox dialogueBox;

    [SerializeField] bool noFlip;

    SpriteRenderer sr;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        dialogueBox = GameObject.Find("Dialogue Box").GetComponent<DialogueBox>();
        sr = GetComponent<SpriteRenderer>();

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

    protected override void onInteract() {
        if (!noFlip) {
            sr.flipX = GameObject.FindGameObjectWithTag("Player").transform.position.x < transform.position.x;
        }

        GameStateManager.Instance.setCurrentState(GameStateManager.GameState.Talking);
        selectDialogue();
        dialogueBox.startDialogue(this);
    }

}
