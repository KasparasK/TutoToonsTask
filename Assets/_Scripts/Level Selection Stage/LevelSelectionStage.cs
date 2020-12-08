using UnityEngine;

public class LevelSelectionStage : MonoBehaviour , IStage
{
    public LevelSelectionPanel levelSelectionPanel;
    public void Initialize()
    {
        levelSelectionPanel.Show();
    }

    public void Finish()
    {
        levelSelectionPanel.Hide();
    }
}
