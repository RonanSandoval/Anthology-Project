using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public Dialogue selectedDialogue;

    // Start is called before the first frame update
    void Start()
    {
        
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

        Debug.Log(selectedDialogue.script[0]);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            startDialogue();
        }
    }
}
