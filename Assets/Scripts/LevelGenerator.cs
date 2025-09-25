using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private int rows;
    private int cols;
    public int[,] flippedMap = null;
    public int[,] thirdquadmap = null;
    public int[,] fourthquadmap = null;
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
            for (int col = 0; col < levelMap.GetLength(1); col++)

            {

                Vector3Int cellpos = new Vector3Int(col, -row, 0);
                Vector3 worldPos = grid.CellToWorld(cellpos);


                Procedurallogic(row, col, levelMap, worldPos);

            }
        }



        flippedMap = new int[levelMap.GetLength(0), levelMap.GetLength(1)];

        for(int irow = 0; irow < levelMap.GetLength(0); irow++) {  //initial row

            for (int icol = 0; icol < levelMap.GetLength(1); icol++) { //initial col

                flippedMap[irow, icol] = levelMap[irow, levelMap.GetLength(1) - 1 - icol];
            }

        }


        //horizontally flipped map 2nd Quadrant

        for (int row = 0; row < flippedMap.GetLength(0); row++)
        {
            for (int col = 0; col < flippedMap.GetLength(1); col++)

            {


                Vector3Int cellpos = new Vector3Int(col + ((levelMap.GetLength(1))), -row, 0);
                Vector3 worldPos = grid.CellToWorld(cellpos);


               Procedurallogic(row,col, flippedMap, worldPos);



            }
        }



        thirdquadmap = new int[flippedMap.GetLength(0), flippedMap.GetLength(1)];

        for (int irow = 0; irow < flippedMap.GetLength(0); irow++)
        {  //initial row

            for (int icol = 0; icol < flippedMap.GetLength(1); icol++)
            { //initial col

                thirdquadmap[irow, icol] = flippedMap[flippedMap.GetLength(0) - 1 - irow, icol];
            }

        }


        //Vertically flipped map 3rd Qaudrant

        for (int row = 0; row < thirdquadmap.GetLength(0); row++)
        {
            for (int col = 0; col < thirdquadmap.GetLength(1); col++)

            {


                Vector3Int cellpos = new Vector3Int(col + levelMap.GetLength(1), -row - ((flippedMap.GetLength(0))) + 1, 0);
                Vector3 worldPos = grid.CellToWorld(cellpos);


               

                Procedurallogic(row, col, thirdquadmap, worldPos);



            }
        }


        fourthquadmap = new int[levelMap.GetLength(0), levelMap.GetLength(1)];

        for (int irow = 0; irow < levelMap.GetLength(0); irow++)
        {  //initial row

            for (int icol = 0; icol < levelMap.GetLength(1); icol++)
            { //initial col

                fourthquadmap[irow, icol] = levelMap[levelMap.GetLength(0) - 1 - irow, icol];
            }

        }


        //horizontally flipped map fourth Quadrant

        for (int row = 0; row < flippedMap.GetLength(0); row++)
        {
            for (int col = 0; col < flippedMap.GetLength(1); col++)

            {


                Vector3Int cellpos = new Vector3Int(col, -row - levelMap.GetLength(0) + 1, 0);
                Vector3 worldPos = grid.CellToWorld(cellpos);


                Procedurallogic(row, col, fourthquadmap, worldPos);



            }
        }







    }

    // Update is called once per frame
    void Update()
    {
        
    }




    public void Procedurallogic(int row, int col, int[,] map, Vector3 worldPos)
    {

        int rows = map.GetLength(0);
        int cols = map.GetLength(1);

        //neighbor is floor 5 or 6
        bool up = row > 0 && (map[row - 1, col] == 5 || map[row - 1, col] == 6);
        bool down = row < rows - 1 && (map[row + 1, col] == 5 || map[row + 1, col] == 6);
        bool left = col > 0 && (map[row, col - 1] == 5 || map[row, col - 1] == 6);
        bool right = col < cols - 1 && (map[row, col + 1] == 5 || map[row, col + 1] == 6);

        //neighbour is empty

        bool emptyup = row - 2 >= 0 && (map[row - 1, col] == 0 && (map[row - 2, col] == 2 || map[row - 2, col] == 4));
        bool emptydown = row + 2 < rows && (map[row + 1, col] == 0 && (map[row + 2, col] == 2 || map[row + 2, col] == 4));
        bool emptyleft = col - 2 >= 0 && (map[row, col - 1] == 0 && (map[row, col - 2] == 2 || map[row, col - 2] == 4));
        bool emptyright = col +  2 < cols && (map[row, col + 1] == 0 && (map[row, col + 2] == 2 || map[row, col + 2] == 4));

        // Determine if this empty tile is in a horizontal corridor (left/right) but not vertical
        bool emptyHorizontal = (emptyleft || emptyright) && !(emptyup || emptydown);

        // Determine if this empty tile is in a vertical corridor (up/down) but not horizontal
        bool emptyVertical = (emptyup || emptydown) && !(emptyleft || emptyright);




        //neighbour is  outer wall
        bool wallup =   row > 0 && (map[row - 1, col] == 2);
        bool walldown = row < rows - 1 && (map[row + 1, col] == 2);
        bool wallleft = col > 0 && (map[row, col - 1] == 2);
        bool wallright = col < cols - 1 && (map[row, col + 1] == 2);


        //neighbour is inner wall
        bool iwallup = row > 0 && (map[row - 1, col] == 4);
        bool iwalldown = row < rows - 1 && (map[row + 1, col] == 4);
        bool iwallleft = col > 0 && (map[row, col - 1] == 4);
        bool iwallright = col < cols - 1 && (map[row, col + 1] == 4);


        

        int tile = map[row, col];

        switch (tile)
        {
            case 0: // Empty
                Instantiate(Empty, worldPos, Quaternion.identity);
                break;

            case 1: // OuterCorner
                if (wallup && wallleft && right && down) Instantiate(OuterCorner, worldPos, Quaternion.Euler(0, 0, 180));
                else if (wallup && wallright && down && left) Instantiate(OuterCorner, worldPos, Quaternion.Euler(0, 0, 90));
                else if (walldown && wallleft && up && right) Instantiate(OuterCorner, worldPos, Quaternion.Euler(0, 0, -90));
                else if (walldown && wallright && up && left) Instantiate(OuterCorner, worldPos, Quaternion.Euler(0, 0, 0));
                else if (walldown && wallright) Instantiate(OuterCorner, worldPos, Quaternion.Euler(0, 0, 180));
                else if (wallup && wallright) Instantiate(OuterCorner, worldPos, Quaternion.Euler(0, 0, -90));
                else Instantiate(OuterCorner, worldPos, Quaternion.identity); 
                break;

            case 2: // Outerwall
                if (right) Instantiate(Outerwall, worldPos, Quaternion.Euler(0, 0, 90));
                else if(left) Instantiate(Outerwall, worldPos, Quaternion.Euler(0,0,-90));
                else if (up) Instantiate(Outerwall, worldPos, Quaternion.Euler(0, 0, 180));
                else if (down) Instantiate(Outerwall, worldPos, Quaternion.Euler(0, 0, 0));
                else Instantiate(Outerwall,worldPos,Quaternion.identity);
                    break;

            case 3: // InnerCorner
                if (down && left) Instantiate(InnerCorner, worldPos, Quaternion.Euler(0, 0, 90));
                else if (down && right) Instantiate(InnerCorner, worldPos, Quaternion.Euler(0, 0, 180));
                else if (up && left) Instantiate(InnerCorner, worldPos, Quaternion.Euler(0, 0, 0));
                else if (up && right) Instantiate(InnerCorner, worldPos, Quaternion.Euler(0, 0, -90));
                else Instantiate(InnerCorner, worldPos, Quaternion.identity); 
                break;

            case 4: // Innerwall
                if (up) Instantiate(Innerwall, worldPos, Quaternion.Euler(0, 0, 90));
                else if (down) Instantiate(Innerwall, worldPos, Quaternion.Euler(0, 0, -90));
                else if (right) Instantiate(Innerwall, worldPos, Quaternion.Euler(0, 0, 0));
                else if (left) Instantiate(Innerwall, worldPos, Quaternion.Euler(0, 0, 180));
                else Instantiate(Innerwall, worldPos, Quaternion.identity);
                break;

            case 5: // Floor + Pellet
                GameObject floor = Instantiate(Floor, worldPos, Quaternion.identity);
                GameObject pellet = Instantiate(Pellet, worldPos, Quaternion.identity);
                floor.GetComponent<SpriteRenderer>().sortingOrder = 0;
                pellet.GetComponent<SpriteRenderer>().sortingOrder = 1;
                break;

            case 6: // Floor + PowerPellet
                GameObject floor2 = Instantiate(Floor, worldPos, Quaternion.identity);
                GameObject powerPellet = Instantiate(PowerPellet, worldPos, Quaternion.identity);
                floor2.GetComponent<SpriteRenderer>().sortingOrder = 0;
                powerPellet.GetComponent<SpriteRenderer>().sortingOrder = 1;
                break;

            case 7: // T-junction
                if(iwalldown) Instantiate(Tjunction, worldPos, Quaternion.Euler(0,0,180));
                else if(iwallup) Instantiate(Tjunction,worldPos, Quaternion.identity);
                    break;

            case 8: // GhostExit

                //fix this logic
                if (map[row -2,col] == 3)
                {
                    Instantiate(GhostExit, worldPos, Quaternion.Euler(0, 0, -90));
                }

                else {
                    Instantiate(GhostExit, worldPos, Quaternion.Euler(0, 0, 90)); }
                break;

        }

    }

        }

