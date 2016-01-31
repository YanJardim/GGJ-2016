using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum blocks{

    GRASS,
    RELIC,
    THREE
};

public class GridBehaviour : MonoBehaviour {

    public static GridBehaviour instance;

    public GameObject grass;

    public const int gridX = 33, gridY = 33;

    public blocks[,] grid = new blocks[gridX, gridY];
    public blocks[,] gridGameObjects = new blocks[gridX, gridY];




    void Awake()
    {
        instance = this;
    }

	// Use this for initialization
	void Start () {
        setGrass();
        setThrees(30);
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

    public bool checkThreeArea(int x, int y, int tileDistance)
    {
            for(int i = 0; i < gridX; i ++){
                for(int j = 0; j < gridY; j ++){
                    if(gridGameObjects[i,j] == blocks.THREE){
                        if (i > x - tileDistance && i < x + tileDistance && j > y - tileDistance && j < y + tileDistance)
                        {
                            return false;
                        }
                    }
                }
            }
    
        return true;
    }

    public void setThrees(int amount)
    {
        for (int loops = 0; loops < amount; loops++)
        {
            
            int x = Random.Range(0, gridX);
            int y = Random.Range(0, gridY);

            if(checkThreeArea(x, y, 3))
                gridGameObjects[x, y] = blocks.THREE;
        }
                    

        for (int i = 0; i < gridX; i++)
        {
            for (int j = 0; j < gridY; j++)
            {
                if (gridGameObjects[i, j] == blocks.THREE )
                {
                    int t = Random.Range(0, GameManager.instance.threes.Count);
                    GameObject aux = Instantiate(GameManager.instance.threes[t], new Vector2(transform.position.x + i, transform.position.y + j), GameManager.instance.threes[t].transform.rotation) as GameObject;

                    aux.transform.SetParent(GameObject.Find("Grid").transform);

                }
            }
        }
    }

    public void setRelics()
    {
        for (int i = 0; i < GameManager.instance.relicObjects.Count; i++ )
        {
            int x = Random.Range(0, gridX);
            int y = Random.Range(0, gridY);

            print("X: " + x + " Y: " + y);
            gridGameObjects[x, y] = blocks.RELIC;

        }

        int relicIndex = 0;
        for (int i = 0; i < gridX; i++)
        {
            for (int j = 0; j < gridY; j++)
            {
                if (gridGameObjects[i, j] == blocks.RELIC)
                {
                    GameObject aux = Instantiate(GameManager.instance.relicObjects[relicIndex], new Vector2(transform.position.x + i, transform.position.y + j), GameManager.instance.relicObjects[relicIndex].transform.rotation) as GameObject;
                    
                    aux.transform.SetParent(GameObject.Find("GameManager").transform);

                    relicIndex++;
                }
            }
        }
        //GameManager.instance.relicObjects
    }

    public void drawMap()
    {
        
        for (int i = 0; i < gridX; i++)
        {
            for (int j = 0; j < gridY; j++)
            {
                if (grid[i, j] == blocks.GRASS)
                {
                    //GameObject aux = Instantiate(grass, new Vector2(transform.position.x + i, transform.position.y + j), grass.transform.rotation) as GameObject;
                    //GameObject aux = Instantiate(grass, new Vector3(transform.position.x + i, 0, transform.position.y + j), grass.transform.rotation) as GameObject;
                    GameObject aux = Instantiate(grass, new Vector2(transform.position.x + i, transform.position.y + j), grass.transform.rotation) as GameObject;
                    aux.transform.SetParent(GameObject.Find("Grid").transform);
                }

                


            }
        }
        //GameObject.Find("Grid").transform.position = new Vector3(-16, 0, -16);
    }
}
