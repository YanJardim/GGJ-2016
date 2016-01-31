using UnityEngine;
using System.Collections;

public enum blocks{

    GRASS,
    RELIC,
    ALTAR
};

public class GridBehaviour : MonoBehaviour {

    public static GridBehaviour instance;

    public GameObject grass;

    public const int gridX = 33, gridY = 33;

    public blocks[,] grid = new blocks[gridX, gridY];
    public blocks[,] relics = new blocks[gridX, gridY];

    void Awake()
    {
        instance = this;

    }

    void setAltar()
    {
        grid[14, 14] = blocks.ALTAR;
        grid[18, 18] = blocks.ALTAR;
        grid[18, 14] = blocks.ALTAR;
    }

	// Use this for initialization
	void Start () {
        setGrass();
        setRelics();
        setAltar();
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
        for (int i = 0; i < GameManager.instance.relicObjects.Count; i++ )
        {
            int x = Random.Range(0, gridX);
            int y = Random.Range(0, gridY);

            print("X: " + x + " Y: " + y);
            relics[x, y] = blocks.RELIC;

        }

        int relicIndex = 0;
        for (int i = 0; i < gridX; i++)
        {
            for (int j = 0; j < gridY; j++)
            {
                if (relics[i, j] == blocks.RELIC)
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
                } else if (grid[i, j] == blocks.ALTAR)
                {
                    //GameObject aux = Instantiate(grass, new Vector2(transform.position.x + i, transform.position.y + j), grass.transform.rotation) as GameObject;
                    //GameObject aux = Instantiate(grass, new Vector3(transform.position.x + i, 0, transform.position.y + j), grass.transform.rotation) as GameObject;
                    GameObject aux = Instantiate(grass, new Vector2(transform.position.x + i, transform.position.y + j), grass.transform.rotation) as GameObject;
                    aux.transform.SetParent(GameObject.Find("Grid").transform);
                    aux.GetComponent<Renderer>().material.color = Color.magenta;
                }




            }
        }
        //GameObject.Find("Grid").transform.position = new Vector3(-16, 0, -16);
    }

    public blocks getGridItem(int x, int y)
    {
        return grid[x, y];
    }
}
