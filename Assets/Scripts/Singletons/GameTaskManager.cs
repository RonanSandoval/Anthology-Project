using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameTaskManager : MonoBehaviour
{
    public static GameTaskManager Instance { get; private set; }

    public UnityEvent onTaskUpdate;
    public UnityEvent onTaskComplete;

    public class Task {
        string description;
        int goal;
        public int progress;
        int completionTaskIndex;
        GameProgressManager.ProgressFlag completionProgress;

        public Task(string description, int goal, int completionTaskIndex, GameProgressManager.ProgressFlag completionProgress) {
            this.description = description;
            this.goal = goal;
            this.progress = 0;
            this.completionTaskIndex = completionTaskIndex;
            this.completionProgress = completionProgress; 
        }

        public void updateTaskProgress(int taskChange) {
            progress += taskChange; 
        }

        public bool isComplete() {
            return progress >= goal;
        }

        public int getCompletionTaskIndex() {
            return completionTaskIndex;
        }

        public GameProgressManager.ProgressFlag getCompletionProgress() {
            return completionProgress;
        }

        public string getDescription() {
            return description;
        }

        public string getProgressText() {
            if (goal == 0) {
                return "";
            } else {
                return progress + " / " + goal; 
            }
        }

    }

    List<Task> taskList;
    [SerializeField] int currentTaskIndex; // -1 means no current task

    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
        
        if (Instance != null && Instance != this) 
        { 
            Destroy(gameObject); 
        } 
        else 
        { 
            DontDestroyOnLoad(gameObject);
            Instance = this;
            currentTaskIndex = 0;
            defineTasks();
        } 
    }

    void defineTasks() {
        taskList = new List<Task>();
        taskList.Add(
            new Task(
                "Catch 5 Dream Bunnies",
                5, // how many steps
                1, // next task?
                GameProgressManager.ProgressFlag.CaughtDreamBunnies
            )
        );
        taskList.Add(
            new Task(
                "Talk to Sleepy MC",
                0, // how many steps
                2, // next task?
                GameProgressManager.ProgressFlag.FinishedDenial
            )
        );
    }

    public void updateTask(int taskIndex, int taskChange) {
        Debug.Log("task updated: " + taskIndex + " , " + taskChange);
        taskList[taskIndex].updateTaskProgress(taskChange);

        // updates for it a task is completed
        if (taskList[taskIndex].isComplete()) {
            onTaskComplete.Invoke();
            currentTaskIndex = taskList[taskIndex].getCompletionTaskIndex();
            GameProgressManager.Instance.addProgress(taskList[taskIndex].getCompletionProgress());
        } else {
            onTaskUpdate.Invoke();
        }
    }

    public void setCurrentTask(int currentTask) {
        currentTaskIndex = currentTask;
    }

    public Task getCurrentTask() {
        return taskList[currentTaskIndex];
    }
}
