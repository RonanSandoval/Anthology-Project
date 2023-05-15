using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public string[] script;
    public string[] speaker;

    public GameProgressManager.ProgressFlag dialogueFlag;
    public GameProgressManager.ProgressFlag flagAfterCompletion;
    public int taskIndexAfterCompletion;

    public bool dialogueIsAvailable() {
        return GameProgressManager.Instance.checkProgress(dialogueFlag);
    }
}
