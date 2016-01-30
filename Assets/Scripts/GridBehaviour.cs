using UnityEngine;
using System.Collections;

public enum blocks{

    GRASS


};

public class GridBehaviour : MonoBehaviour {

    public GameObject grass;

    public const int gridX = 33, gridY = 33;

    public blocks[,] grid = new blocks[gridX, gridY];

	// Use this for initialization
	void Start () {
        setGrass();
        drawMap();
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void setGrass()
    {
        for (int i = 0; i < gridX; i++)
        {
            for (int j = 0; j < gridY; j++)
            {
                grid[i, j] = blocks.GRASS;
            }
        }
    }

    public void drawMap()
    {
        for (int i = 0; i < gridX; i++)
        {
            for (int j = 0; j < gridY; j++)
            {
                if (grid[i, j] == blocks.GRASS)
                {
                    GameObject aux = Instantiate(grass, new Vector2(transform.position.x + i, transform.position.y + j), grass.transform.rotation) as GameObject;
                   
                    aux.transform.SetParent(GameObject.Find("Grid").transform);
                }
            }
        }
    }
}
