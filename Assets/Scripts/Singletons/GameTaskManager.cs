using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameTaskManager : MonoBehaviour
{
    public static GameTaskManager Instance { get; private set; }

    public UnityEvent onTaskUpdate;
    public UnityEvent onTaskComplete;
    public UnityEvent onNewTask;

    public enum TaskName {
        None,
        NoUpdate,
        Bunnies,
        DenialTalk,
        Rocks,
        AngerTalk,
        Paper,
        BargainingTalk,
        Fear,
        DepressionTalk,
        Finale
        }

    public class Task {
        string description;
        int goal;
        public int progress;
        GameTaskManager.TaskName completionTask;
        GameProgressManager.ProgressFlag completionProgress;

        public Task(string description, int goal, TaskName completionTask, GameProgressManager.ProgressFlag completionProgress) {
            this.description = description;
            this.goal = goal;
            this.progress = 0;
            this.completionTask = completionTask;
            this.completionProgress = completionProgress; 
        }

        public void updateTaskProgress(int taskChange) {
            progress += taskChange; 
        }

        public bool isComplete() {
            return progress >= goal;
        }

        public GameTaskManager.TaskName getCompletionTask() {
            return completionTask;
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

    Dictionary<TaskName, Task> taskList;
    [SerializeField] TaskName currentTask;

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
            currentTask = TaskName.None;
            defineTasks();
        } 
    }

    void defineTasks() {
        taskList = new Dictionary<TaskName, Task>();
        taskList.Add( TaskName.Bunnies,
            new Task(
                "Catch 5 Dream Bunnies",
                5, // how many steps
                TaskName.DenialTalk, // next task?
                GameProgressManager.ProgressFlag.CaughtDreamBunnies
            )
        );
        taskList.Add( TaskName.DenialTalk,
            new Task(
                "Talk to Sleepy Cypress",
                0, // how many steps
                TaskName.None, // next task?
                GameProgressManager.ProgressFlag.FinishedDenial
            )
        );
        taskList.Add( TaskName.Rocks,
            new Task(
                "Find a Gift in the Crystals",
                15, // how many steps
                TaskName.AngerTalk, // next task?
                GameProgressManager.ProgressFlag.BrokeRocks
            )
        );
        taskList.Add( TaskName.AngerTalk,
            new Task(
                "Talk to Sour Cypress",
                0, // how many steps
                TaskName.None, // next task?
                GameProgressManager.ProgressFlag.FinishedAnger
            )
        );
        taskList.Add( TaskName.Paper,
            new Task(
                "Obtain 30 Pieces of Paper",
                30, // how many steps
                TaskName.BargainingTalk, // next task?
                GameProgressManager.ProgressFlag.ObtainedPapers
            )
        );
        taskList.Add( TaskName.BargainingTalk,
            new Task(
                "Talk to Starry-Eyed Cypress",
                0, // how many steps
                TaskName.None, // next task?
                GameProgressManager.ProgressFlag.FinishedBargaining
            )
        );
        taskList.Add( TaskName.Fear,
            new Task(
                "Find the Fear",
                1, // how many steps
                TaskName.DepressionTalk, // next task?
                GameProgressManager.ProgressFlag.ObtainedPapers
            )
        );
        taskList.Add( TaskName.DepressionTalk,
            new Task(
                "Talk to Sad Cypress",
                0, // how many steps
                TaskName.None, // next task?
                GameProgressManager.ProgressFlag.FinishedDepression
            )
        );
        taskList.Add( TaskName.Finale,
            new Task(
                "Begin the Ceremony",
                0, // how many steps
                TaskName.None, // next task?
                GameProgressManager.ProgressFlag.None
            )
        );
    }

    public void updateTask(TaskName taskName, int taskChange) {
        Debug.Log("task updated: " + taskName + " , " + taskChange);
        taskList[taskName].updateTaskProgress(taskChange);
        currentTask = taskName;

        // updates for it a task is completed
        if (taskList[taskName].isComplete()) {
            onTaskComplete.Invoke();
            currentTask = taskList[taskName].getCompletionTask();
            GameProgressManager.Instance.addProgress(taskList[taskName].getCompletionProgress());
        } else {
            onTaskUpdate.Invoke();
        }
    }

    public void setCurrentTask(TaskName currentTask) {
        this.currentTask = currentTask;
    }

    public Task getCurrentTask() {
        return taskList[currentTask];
    }

    public TaskName getCurrentTaskName() {
        return currentTask;
    }

    public bool checkTaskExists() {
        return currentTask != TaskName.None;
    }
}
