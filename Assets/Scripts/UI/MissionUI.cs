using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionUI : MonoBehaviour
{
    [SerializeField] Text missionTitle;
    [SerializeField] Text missionProgress;

    Image image;

    // Start is called before the first frame update
    void Start()
    {
        GameTaskManager.Instance.onTaskUpdate.AddListener(taskUpdate);
        GameTaskManager.Instance.onTaskComplete.AddListener(taskComplete);

        image = GetComponent<Image>();
    }

    public void taskUpdate() {
        GameTaskManager.Task currentTask = GameTaskManager.Instance.getCurrentTask();
        missionTitle.text = currentTask.getDescription();
        missionProgress.text = currentTask.getProgressText();
        StartCoroutine(updateCoroutine());
    }

    IEnumerator updateCoroutine() {
        image.color = new Color(1f,1f, 1f, 1f);

        while (image.color.b - 0.5f > 0.01) {
            yield return null;
            image.color = Color.Lerp(image.color, new Color(0f,0f, 0.5f, 0.6f), Time.deltaTime * 3f);
        }

        image.color = new Color(0f,0f, 0.5f, 0.6f);
    }

    public void taskComplete() {
        GameTaskManager.Task currentTask = GameTaskManager.Instance.getCurrentTask();
        missionTitle.text = currentTask.getDescription();
        missionProgress.text = currentTask.getProgressText();
    }

    public void taskStart() {

    }
}
