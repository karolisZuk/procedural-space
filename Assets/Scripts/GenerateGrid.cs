using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateGrid : MonoBehaviour
{
    [Range(2, 100)] public int length = 5;
    public GameObject cube;
    public GameObject player;
    public int detailScale = 8;
    public int noiseHeight = 3;
    private Vector3 startPos = Vector3.zero;
    private Dictionary<Vector2, GameObject> cubePos = new Dictionary<Vector2, GameObject>();

    private int XPlayerMove => (int)(player.transform.position.x - startPos.x);
    private int ZPlayerMove => (int)(player.transform.position.z - startPos.z);

    private int XPlayerLocation => (int)Mathf.Floor(player.transform.position.x);
    private int ZPlayerLocation => (int)Mathf.Floor(player.transform.position.z);

    // Start is called before the first frame update
    void Start()
    {
        GenerateTerrain();
    }

    private void Update()
    {
        if (Mathf.Abs(XPlayerMove) >= 1 || Mathf.Abs(ZPlayerMove) >= 1)
        {
            GenerateTerrain();
            DeleteUnusedTerrain();
        }
    }

    private void DeleteUnusedTerrain()
    {
        // TODO
    }

    private void GenerateTerrain()
    {
        for (int x = -length; x < length; x++)
        {
            for (int z = -length; z < length; z++)
            {
                Vector3 pos = new Vector3(
                    x + XPlayerLocation,
                    yNoise(x + XPlayerLocation, z + ZPlayerLocation) * noiseHeight,
                    z + ZPlayerLocation
                );

                if (!cubePos.ContainsKey(new Vector2(pos.x, pos.z)))
                {
                    GameObject cubeInstance = Instantiate(cube, pos, Quaternion.identity, transform);

                    cubePos.Add(new Vector2(pos.x, pos.z), cubeInstance);
                }
            }
        }
    }

    private float yNoise(int x, int z)
    {
        float xNoise = (x + transform.position.x) / detailScale;
        float zNoise = (z + transform.position.y) / detailScale;
        return Mathf.PerlinNoise(xNoise, zNoise);
    }

}
