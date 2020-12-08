using UnityEngine;

public struct Connection
{
    public Node a;
    public Node b;

    public Vector2 posA;
    public Vector2 posB;

    public Vector2 ropePos;
    public float ropeZAngle;
    public float ropeLength;

    public Connection(Node a, Node b)
    {
        this.a = a;
        this.b = b;


        posA = a.transform.position;
        posB = b.transform.position;

        ropePos = (posB + posA) / 2;
        ropeZAngle = 0;
        ropeLength = Vector2.Distance(posA, posB);
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