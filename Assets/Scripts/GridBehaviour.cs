using UnityEngine;
using System.Collections;
using System;

public enum blocks{

    GRASS, RELICS


};

public class GridBehaviour : MonoBehaviour {

    public static GridBehaviour instance;

    public GameObject grass;

    public GameObject relics;

    public const int gridX = 32, gridY = 32;

    public blocks[,] grid = new blocks[gridX, gridY];
    

    void Awake()
    {
        instance = this;
        
    }

	// Use this for initialization
	void Start () {
        setGrass();
        setRelics();
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

    public void setRelics()
    {
        grid[18, 14] = blocks.RELICS;
        grid[18, 18] = blocks.RELICS;
        grid[14, 18] = blocks.RELICS;
    }

    public void drawMap()
    {
        for (int i = 0; i < gridX; i++)
        {
            for (int j = 0; j < gridY; j++)
            {
                GameObject aux = null;
                if (grid[i, j] == blocks.GRASS)
                {
                    aux = Instantiate(grass, new Vector2(transform.position.x + i, transform.position.y + j), grass.transform.rotation) as GameObject;
                }
                else if (grid[i, j] == blocks.RELICS)
                {
                    aux = Instantiate(grass, new Vector2(transform.position.x + i, transform.position.y + j), grass.transform.rotation) as GameObject;
                    aux.GetComponent<SpriteRenderer>().color = Color.magenta;
                }
                aux.transform.SetParent(GameObject.Find("Grid").transform);
            }
        }
    }

    public blocks getTypeGrid(int x, int y)
    {
        return grid[x, y];
    }
}
