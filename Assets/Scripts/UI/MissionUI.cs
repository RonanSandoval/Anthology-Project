using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionUI : MonoBehaviour
{
    [SerializeField] Text missionTitle;
    [SerializeField] Text missionProgress;

    Image image;
    RectTransform rt;

    Color regularColor;

    // Start is called before the first frame update
    void Start()
    {
        GameTaskManager.Instance.onTaskUpdate.AddListener(taskUpdate);
        GameTaskManager.Instance.onTaskComplete.AddListener(taskComplete);
        GameTaskManager.Instance.onNewTask.AddListener(taskBegin);

        image = GetComponent<Image>();
        regularColor = image.color;
        rt = GetComponent<RectTransform>();

        rt.anchoredPosition3D = new Vector3(-250f, -17f, 0f);

        if (GameTaskManager.Instance.checkTaskExists()) {
            taskBegin();
        }
    }

    public void taskUpdate() {
        updateMissionText();
        StartCoroutine(updateCoroutine());
    }

    IEnumerator updateCoroutine() {
        image.color = new Color(1f,1f, 1f, 1f);

        while (image.color.b - regularColor.b > 0.01) {
            yield return null;
            image.color = Color.Lerp(image.color, regularColor, Time.deltaTime * 2f);
        }

        image.color = regularColor;
    }

    public void taskComplete() {
        updateMissionText();
        StartCoroutine(completeCoroutine());
    }

    IEnumerator completeCoroutine() {
        image.color = new Color(1f,1f, 1f, 1f);

        while (image.color.b - 0.10f > 0.01) {
            yield return null;
            image.color = Color.Lerp(image.color, new Color(0.50f, 0.40f, 0.10f, 0.9f), Time.deltaTime * 2f);
        }

        image.color = new Color(0.50f, 0.40f, 0.10f, 0.9f);

        while (rt.anchoredPosition3D.x > -249f) {
            rt.anchoredPosition3D = Vector3.Lerp(rt.anchoredPosition3D, new Vector3(-250f, -17f, 0f), Time.deltaTime * 10f);
            yield return null;
        }

        image.color = regularColor;

        if (GameTaskManager.Instance.checkTaskExists()) {
            taskBegin();
        }
    }

    public void taskBegin() {
        StartCoroutine(beginCoroutine());
    }

    IEnumerator beginCoroutine() {
        updateMissionText();

        rt.anchoredPosition = new Vector3(-250f, -17f, 0f);

        while (rt.anchoredPosition3D.x < -0.1f) {
            rt.anchoredPosition3D = Vector3.Lerp(rt.anchoredPosition3D, new Vector3(0f, -17f, 0f), Time.deltaTime * 10f);
            yield return null;
        }

        rt.anchoredPosition = new Vector3(0f, -17f, 0f);
    }

    void updateMissionText() {
        GameTaskManager.Task currentTask = GameTaskManager.Instance.getCurrentTask();
        missionTitle.text = currentTask.getDescription();
        missionProgress.text = currentTask.getProgressText();
    }
}
