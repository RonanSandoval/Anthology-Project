using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public string[] script;

    public GameProgressManager.ProgressFlag dialogueFlag;

    public bool dialogueIsAvailable() {
        return GameProgressManager.Instance.checkProgress(dialogueFlag);
    }
}
