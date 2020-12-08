using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayStage : MonoBehaviour , IStage
{
    public GameObject nodePref;
    private List<Node> nodes;
    private Node selected;

    public LevelDataHolder levelDataHolder;

    private List<Vector2> currLevel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize()
    {
        currLevel = levelDataHolder.GetActiveLevel();
        print(currLevel.Count + " asdasdf");
        SpawnNodes();
    }

    public void Finish()
    {

    }

    void SpawnNodes()
    {

        for (int i = 0; i < currLevel.Count; i++)
        {
            Instantiate(nodePref, currLevel[i], Quaternion.identity);
        }
    }
}
