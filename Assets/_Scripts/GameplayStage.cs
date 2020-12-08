using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Connection
{
    public Vector2 posA;
    public Vector2 posB;

    public Vector2 ropePos;
    public float ropeZAngle;
    public float ropeLength;

    public Connection(Node a, Node b)
    {
        posA = a.transform.position;
        posB = b.transform.position;

        ropePos = (posB + posA)/2;
        ropeZAngle = 0;
        ropeLength = Vector2.Distance(posA,posB);
        ropeZAngle = AngleInDeg(posA, posB);
    }

   float AngleInRad(Vector3 vec1, Vector3 vec2)
    {
        return Mathf.Atan2(vec2.y - vec1.y, vec2.x - vec1.x);
    }

    //This returns the angle in degrees
     float AngleInDeg(Vector3 vec1, Vector3 vec2)
    {
        return AngleInRad(vec1, vec2) * 180 / Mathf.PI + 90;
    }
}
public class GameplayStage : MonoBehaviour , IStage
{
    public GameObject nodePref;
    public GameObject ropePref;

    private List<Node> nodes;
    private Node selected;
    private int lastConnectedID;

    private Transform parent;

    public LevelDataHolder levelDataHolder;

    private List<Vector2> currLevel;
    private Queue<Connection> connectionQueue = new Queue<Connection>();

    private bool connectionInProgress;

    private const float ropeWidth = 0.45f;

    private float ropeAnimationDuration = 2f;

    void Update()
    {
        if (!connectionInProgress && connectionQueue.Count > 0)
        {
            Connection conn = connectionQueue.Dequeue();
           StartCoroutine( Connect(conn));
        }
    }

    IEnumerator Connect(Connection conn)
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

        connectionInProgress = false;
        CheckForFinalRope();
    }

    void CheckForFinalRope()
    {
        if (lastConnectedID == nodes.Count && connectionQueue.Count == 0)
        {
            StartCoroutine(Connect(new Connection(nodes[nodes.Count - 1],nodes[0])));

        }
    }

    public void Initialize()
    {
        currLevel = levelDataHolder.GetActiveLevel();
        DeleteNodes();
        SpawnNodes();
        lastConnectedID = 1;
        connectionInProgress = false;
    }
   
    public void Finish()
    {

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
            selected = null;
        }
        else
            selected = null;
            
    }
}
