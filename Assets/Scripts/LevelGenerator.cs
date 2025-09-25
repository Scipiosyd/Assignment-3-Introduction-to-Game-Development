using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

 
    public GameObject Empty;
    public GameObject OuterCorner;
    public GameObject InnerCorner;
    public GameObject Tjunction;
    public GameObject Outerwall;
    public GameObject Innerwall;
    public GameObject GhostExit;
    public GameObject Floor;
    public GameObject Pellet;
    public GameObject PowerPellet;
    private Grid grid;
    public int[,] levelMap =
    {
    {1,2,2,2,2,2,2,2,2,2,2,2,2,7},
    {2,5,5,5,5,5,5,5,5,5,5,5,5,4},
    {2,5,3,4,4,3,5,3,4,4,4,3,5,4},
    {2,6,4,0,0,4,5,4,0,0,0,4,5,4},
    {2,5,3,4,4,3,5,3,4,4,4,3,5,3},
    {2,5,5,5,5,5,5,5,5,5,5,5,5,5},
    {2,5,3,4,4,3,5,3,3,5,3,4,4,4},
    {2,5,3,4,4,3,5,4,4,5,3,4,4,3},
    {2,5,5,5,5,5,5,4,4,5,5,5,5,4},
    {1,2,2,2,2,1,5,4,3,4,4,3,0,4},
    {0,0,0,0,0,2,5,4,3,4,4,3,0,3},
    {0,0,0,0,0,2,5,4,4,0,0,0,0,0},
    {0,0,0,0,0,2,5,4,4,0,3,4,4,8},
    {2,2,2,2,2,1,5,3,3,0,4,0,0,0},
    {0,0,0,0,0,0,5,0,0,0,4,0,0,0},
    };


    private void Awake()
    {
        grid = GetComponent<Grid>(); 
    }


    // Start is called before the first frame update
    void Start()
    {
        GameObject Tilemap = GameObject.Find("Tilemap");
        GameObject Tilemap1 = GameObject.Find("Tilemap (1)");
        GameObject Tilemap2 = GameObject.Find("Tilemap (2)");
        GameObject Tilemap3 = GameObject.Find("Tilemap (3)");
        Destroy(Tilemap);
        Destroy(Tilemap1);
        Destroy(Tilemap2);
        Destroy(Tilemap3);

       // Instantiate(OuterCorner, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 180));


        for (int row = 0; row < levelMap.GetLength(0); row++)
        {
            for(int col = 0; col < levelMap.GetLength(1); col++)
            
            {

                Vector3Int cellpos = new Vector3Int(col, -row, 0);
                Vector3 worldPos = grid.CellToWorld(cellpos);


                if (levelMap[row,col] == 0) { 
                    Instantiate(Empty, worldPos, Quaternion.Euler(0, 0,0));
                }

                if (levelMap[row, col] == 1) {

                    if (levelMap[row + 1, col] == 2 && levelMap[row, col + 1] == 2)
                    {

                        Instantiate(OuterCorner, worldPos, Quaternion.Euler(0, 0, 180));

                    }

                }


                if (levelMap[row, col] == 2) { Instantiate(Outerwall, worldPos, Quaternion.Euler(0, 0, 0)); }
                if (levelMap[row, col] == 3) { Instantiate(InnerCorner, worldPos, Quaternion.Euler(0, 0, 0)); }
                if (levelMap[row, col] == 4)
                {

                    if (row > 0 && (levelMap[row - 1, col] == 5 || levelMap[row-1, col] == 6))
                    {
                        Instantiate(Innerwall, worldPos, Quaternion.Euler(0, 0, 90));
                    }

                    else if (row < levelMap.GetLength(0) - 1 && (levelMap[row + 1, col] == 5 || levelMap[row + 1, col] == 6))
                    {

                        Instantiate(Innerwall, worldPos, Quaternion.Euler(0, 0, -90));

                    }

                    else if (col < levelMap.GetLength(1) - 1 && (levelMap[row, col + 1] == 5 || levelMap[row, col + 1] == 6))
                    {
                        Instantiate(Innerwall, worldPos, Quaternion.Euler(0, 0, 0));
                    }

                    else if (col > 0 && (levelMap[row, col - 1] == 5 || levelMap[row,col-1] == 6))
                    {
                        Instantiate(Innerwall, worldPos, Quaternion.Euler(0, 0, 180));
                    }



                }
                
                if (levelMap[row, col] == 5) { 
                    GameObject floorobj = Instantiate(Floor, worldPos, Quaternion.identity);
                    GameObject pelletobj = Instantiate(Pellet,worldPos, Quaternion.identity);

                    floorobj.GetComponent<SpriteRenderer>().sortingOrder = 0;
                    pelletobj.GetComponent<SpriteRenderer>().sortingOrder = 1;
                }
                if (levelMap[row, col] == 6) {
                    GameObject floorobj = Instantiate(Floor, worldPos, Quaternion.identity);
                    GameObject powerpelletobj = Instantiate(PowerPellet, worldPos, Quaternion.identity);

                    floorobj.GetComponent<SpriteRenderer>().sortingOrder = 0;
                    powerpelletobj.GetComponent<SpriteRenderer>().sortingOrder = 1;
                }
                if (levelMap[row, col] == 7) { Instantiate(Tjunction, worldPos, Quaternion.Euler(0, 0, 0)); }
                if (levelMap[row, col] == 8) { Instantiate(GhostExit, worldPos, Quaternion.Euler(0, 0, 0)); }

              
             }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

