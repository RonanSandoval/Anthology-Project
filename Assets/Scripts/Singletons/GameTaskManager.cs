using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTaskManager : MonoBehaviour
{
    public static GameTaskManager Instance { get; private set; }

    struct Task {
        string description;
        int goal;
        int progress;
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

    }

    List<Task> taskList;
    int currentTaskIndex; // -1 means no current task

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
            defineTasks();
        } 
    }

    void defineTasks() {
        taskList = new List<Task>();
        taskList.Add(
            new Task(
                "Catch 5 Dream Bunnies",
                5,
                -1,
                GameProgressManager.ProgressFlag.None
            )
        );
    }

    public void updateTask(int taskIndex, int taskChange) {
        Debug.Log("task updated: " + taskIndex + " , " + taskChange);
        taskList[taskIndex].updateTaskProgress(taskChange);

        // updates for it a task is completed
        if (taskList[taskIndex].isComplete()) {
            currentTaskIndex = taskList[taskIndex].getCompletionTaskIndex();
             GameProgressManager.Instance.addProgress(taskList[taskIndex].getCompletionProgress());
        }
    }
}
