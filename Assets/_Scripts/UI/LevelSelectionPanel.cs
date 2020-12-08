using UnityEngine;

public class LevelSelectionPanel : MonoBehaviour
{
    public GameObject gridParentPref;
    public GameObject tilePref;

    public LevelDataHolder levelDataHolder;
    public StageManager stageManager;

    public Transform gridParent;
    public void Show()
    {
        int levelCount = levelDataHolder.LevelsRoot.levels.Count;

        Populate(levelCount);
        gameObject.SetActive(true);

    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    void Populate(int count)
    {
        ResetLevelSelectionGrid();

        for (int i = 0; i < count; i++)
        {
            GameObject tile = Instantiate(tilePref, gridParent);
            tile.GetComponent<Tile>().Initialize(OnTileClick, i);
        }
    }

    void ResetLevelSelectionGrid()
    {
        if(gridParent != null)
            Destroy(gridParent.gameObject);

        gridParent = Instantiate(gridParentPref, transform).transform;
    }

    void OnTileClick(int selectedID)
    {
        levelDataHolder.activeLevelID = selectedID;
        stageManager.SetStage(1);
    }


}
