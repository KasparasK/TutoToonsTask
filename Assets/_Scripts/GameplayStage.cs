using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameplayStage : MonoBehaviour , IStage
{
    public GameObject nodePref;
    public GameObject ropePref;

    private List<Vector2> currLevel;
    private List<Node> nodes;

    private Node selected;
    private int lastConnectedID;

    private Transform parent;

    public LevelDataHolder levelDataHolder;
    public StageManager stageManager;

    private Queue<Connection> connectionQueue;
    private bool connectionInProgress;
    private const float ropeWidth = 0.45f;
    private float ropeAnimationDuration = 1f;

    private bool initialized;
    void Update()
    {
        if(initialized)
        {
            if (!connectionInProgress && connectionQueue.Count > 0)
            {
                Connection conn = connectionQueue.Dequeue();
                StartCoroutine(Connect(conn));
            }
        }
    }

    IEnumerator Connect(Connection conn, bool final = false)
    {
        connectionInProgress = true;

        GameObject rope = Instantiate(ropePref, conn.ropePos,
            Quaternion.Euler(new Vector3(0, 0, conn.ropeZAngle)), parent);
        SpriteRenderer renderer = rope.GetComponent<SpriteRenderer>();
        renderer.size = new Vector2(ropeWidth, 0);

        for (float t = 0; t < ropeAnimationDuration; t+=Time.deltaTime)
        {
            float size = Mathf.Lerp(0, conn.ropeLength, t / ropeAnimationDuration);
            renderer.size = new Vector2(ropeWidth, size);

            Vector2 pos = Vector2.Lerp(conn.posA, conn.ropePos, t / ropeAnimationDuration);
            rope.transform.position = pos;

            yield return null;
        }

        conn.a.OnConnected();
        conn.b.OnConnected();

        connectionInProgress = false;
        
        if(!final)
            CheckForFinalRope();
        else
            stageManager.SetStage(2);
    }

    void CheckForFinalRope()
    {
        if (lastConnectedID == nodes.Count && connectionQueue.Count == 0)
        {
            StartCoroutine(Connect(new Connection(nodes[nodes.Count - 1],nodes[0]), true));

        }
    }

    public void Initialize()
    {
        connectionQueue = new Queue<Connection>();
        currLevel = levelDataHolder.GetActiveLevel();

        DeleteNodes();
        SpawnNodes();
        
        lastConnectedID = 1;
        connectionInProgress = false;
        initialized = true;
    }
   
    public void Finish()
    {
        StopAllCoroutines();
        DeleteNodes();
        initialized = false;
    }

    void DeleteNodes()
    {
        if(parent != null)
            Destroy(parent.gameObject);

        parent = new GameObject("Nodes").transform;
        nodes = new List<Node>();
    }
    void SpawnNodes()
    {
        for (int i = 0; i < currLevel.Count; i++)
        {
            GameObject obj = Instantiate(nodePref, currLevel[i], Quaternion.identity, parent);
            Node node = obj.GetComponent<Node>();
            node.Initialize(i+1, OnNodeClick); 
            nodes.Add(node);
        }
    }

    void OnNodeClick(Node node)
    {
        if (selected == null)
            selected = node;
        else if (selected == node)
            return;
        else if (node.num - 1 == selected.num && selected.num == lastConnectedID)
        {
            connectionQueue.Enqueue(new Connection(selected, node));
            lastConnectedID++;
            selected = node;
        }
        else
            selected = null;
            
    }
}
