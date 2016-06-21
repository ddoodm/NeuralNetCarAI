using UnityEngine;
using System.Collections;

public class Lightning : MonoBehaviour {

    private LineRenderer lineRenderer;
    private float maxZ = 4f;
    private int noSegments = 6;
    private Color colour = Color.white;
    private float posRange = 0.2f;
    private float radius = 0.1f;
    private Vector2 midpoint;

    //private GameObject opponent;
    private GameObject player;
    private GameObject enemy;
    public Transform opponent;


    
	void Start () {
        player = GameObject.FindWithTag("Player");
        enemy = GameObject.FindWithTag("Enemy");
        if (enemy != null && player != null)
        {
            if (Vector3.Distance(player.transform.position, this.transform.position) < 5)
            {
                opponent = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Transform>();
            }
            else
            {
                opponent = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            }
        }
        
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetVertexCount(noSegments);

        for(int i = 1; i < noSegments - 1; i++)
        {
            float z = ((float)i) * (maxZ) / (float)(noSegments - 1);

            lineRenderer.SetPosition(i, new Vector3(opponent.position.x + Random.Range(-posRange, posRange), opponent.position.y + Random.Range(-posRange, posRange), opponent.position.z));
        }


        lineRenderer.useWorldSpace = true;
        lineRenderer.SetPosition(0, this.transform.position);
        lineRenderer.SetPosition(noSegments - 1, opponent.position);
    }


    /*
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.SetVertexCount(noSegments);

        midpoint = new Vector2(Random.Range(-radius, radius), Random.Range(-radius, radius));

        for (int i = 1; i < noSegments - 1; i++)
        {
            float z = ((float)i) * (maxZ) / (float)(noSegments - 1);

            float x = -midpoint.x * z * z / 16f + z * midpoint.x / 2f;

            float y = -midpoint.y * z * z / 16f + z * midpoint.y / 2f;


            lineRenderer.SetPosition(i, new Vector3(x + Random.Range(-posRange, posRange), y + Random.Range(-posRange, posRange), z));
        }


        lineRenderer.SetPosition(0, new Vector3(0.0f, 0.0f, 0.0f));
        lineRenderer.SetPosition(noSegments - 1, new Vector3(0.0f, 0.0f, 8.0f));


    }
    */






        void Update () {
        colour.a -= 10f * Time.deltaTime;

        lineRenderer.SetColors(colour, colour);

        if(colour.a <= 0f)
        {
            Destroy(this.gameObject);
        }
	}
}
