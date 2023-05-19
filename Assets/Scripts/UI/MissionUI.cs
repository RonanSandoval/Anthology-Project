using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionUI : MonoBehaviour
{
    [SerializeField] Text missionTitle;
    [SerializeField] Text missionProgress;

    // Start is called before the first frame update
    void Start()
    {
        GameTaskManager.Instance.onTaskUpdate.AddListener(taskUpdate);
        GameTaskManager.Instance.onTaskComplete.AddListener(taskComplete);
    }

    public void taskUpdate() {
        GameTaskManager.Task currentTask = GameTaskManager.Instance.getCurrentTask();
        missionTitle.text = currentTask.getDescription();
        missionProgress.text = currentTask.getProgressText();
    }

    public void taskComplete() {
        GameTaskManager.Task currentTask = GameTaskManager.Instance.getCurrentTask();
        missionTitle.text = currentTask.getDescription();
        missionProgress.text = currentTask.getProgressText();
    }

    public void taskStart() {
        
    }
}
