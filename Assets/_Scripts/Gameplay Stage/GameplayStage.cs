using UnityEngine;

public class GameplayStage : MonoBehaviour , IStage
{
    public NodeManager nodeManager;
    public void Initialize()
    {
        nodeManager.Initialize();
    }
   
    public void Finish()
    {
        nodeManager.Finish();
    }
}
