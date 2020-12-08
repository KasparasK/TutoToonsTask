using System.Collections.Generic;
using Sirenix.OdinInspector;

public class StageManager : SerializedMonoBehaviour
{
    public IStage currentStage;
    public static int currentStageIndex;
    public void SetStage(int stageIndex)
    {
        currentStage?.Finish();
        currentStageIndex = stageIndex;
        currentStage = stages[currentStageIndex];
        currentStage.Initialize();
    }

    void Start()
    {
        currentStage.Initialize();
    }

    public List<IStage> stages;
}