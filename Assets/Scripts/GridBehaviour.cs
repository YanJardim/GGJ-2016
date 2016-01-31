using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum blocks{

    GRASS,
    RELIC,
    THREE,
    ALTARBLOCK,
    ALTAR
};

public class GridBehaviour : MonoBehaviour {

    public static GridBehaviour instance;

    public GameObject grass;

    public const int gridX = 33, gridY = 33;

    public blocks[,] grid = new blocks[gridX, gridY];
    public blocks[,] gridGameObjects = new blocks[gridX, gridY];

    public Vector4 altar;


    void Awake()
    {
        instance = this;
    }

	// Use this for initialization
	void Start () {
        setGrass();
        setAltar();
        setRelics();
        setThrees(30);
        

        drawMap();
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void setAltar()
    {
        for (int i = 12; i < 20; i++)
        {
            for (int j = 12; j < 20; j++)
            {
                if(i != 16 && j != 16)
                    gridGameObjects[i, j] = blocks.ALTARBLOCK;
                else gridGameObjects[i, j] = blocks.ALTAR;
            }
        }

        GameObject aux = Instantiate(GameManager.instance.altar, new Vector2(0, 0), GameManager.instance.altar.transform.rotation) as GameObject;


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
                        if (i > x - tileDistance && i < x + tileDistance && j > y - tileDistance && j < y + tileDistance )
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

            if(checkThreeArea(x, y, 3) && gridGameObjects[x, y] != blocks.RELIC
                && gridGameObjects[x, y] != blocks.ALTARBLOCK
                && gridGameObjects[x, y] != blocks.ALTAR)

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
    //12 até 20 - altar
    public void setRelics()
    {
        for (int i = 0; i < GameManager.instance.relicObjects.Count; i++ )
        {
            int x = Random.Range(0, gridX);
            int y = Random.Range(0, gridY);

            
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
