using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionStage : MonoBehaviour , IStage
{
    public LevelSelectionPanel levelSelectionPanel;

    
    public void Initialize()
    {
        levelSelectionPanel.Initialize();
    }

    public void Finish()
    {
        levelSelectionPanel.Hide();
    }
}
