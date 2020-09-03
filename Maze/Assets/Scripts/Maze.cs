using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    public int Columns = 4;
    public int Rows = 4;
    public GameObject Wall;
    public GameObject Floor;

    private MazeCell[,] grid;
    private int currentRow = 0;
    private int currentColumn = 0;

    void Start()
    {
        createGrid();
        createPath();
    }
    void createGrid()
    {
        grid = new MazeCell[Rows, Columns];
        Vector3 floorSize = Floor.transform.localScale;
        Vector3 wallSize = Wall.transform.localScale;
        string floorName = "floor_";
        string upWallName = "upWall_";
        string downWallName = "downWall_";
        string leftWallName = "leftWall_";
        string rightWallName = "rightWall_";
        int height = 0;

        for (int row = 0; row < Rows; row++)
        {
            for (int column = 0; column < Columns; column++)
            {
                GameObject floor = Instantiate(Floor, new Vector3(column * floorSize.x, height, -row * floorSize.z), Quaternion.identity);
                floor.name = floorName + row + "_" + column;

                Wall.GetComponent<Transform>().localScale = new Vector3(floorSize.x, wallSize.y, wallSize.z);
                GameObject upWall = Instantiate(Wall, new Vector3(column * floorSize.x, height + (wallSize.y / 2), -row * floorSize.z + (floorSize.z / 2)), Quaternion.identity);
                upWall.name = upWallName + row + "_" + column;
                GameObject leftWall = Instantiate(Wall, new Vector3(column * floorSize.x, height + (wallSize.y / 2), -row * floorSize.z - (floorSize.z / 2)), Quaternion.identity);
                leftWall.name = leftWallName + row + "_" + column;
                Wall.GetComponent<Transform>().localScale = new Vector3(floorSize.z, wallSize.y, wallSize.z);
                GameObject downWall = Instantiate(Wall, new Vector3(column * floorSize.x + (floorSize.x / 2), height + (wallSize.y / 2), -row * floorSize.z), Quaternion.Euler(0, 90, 0));
                downWall.name = downWallName + row + "_" + column;
                GameObject rightWall = Instantiate(Wall, new Vector3(column * floorSize.x - (floorSize.x / 2), height + (wallSize.y / 2), -row * floorSize.z), Quaternion.Euler(0, 90, 0));
                rightWall.name = rightWallName + row + "_" + column;

                grid[row, column] = new MazeCell();
                grid[row, column].upWall = upWall;
                grid[row, column].downWall = downWall;
                grid[row, column].leftWall = leftWall;
                grid[row, column].rightWall = rightWall;

                floor.transform.parent = transform;
                upWall.transform.parent = transform;
                leftWall.transform.parent = transform;
                downWall.transform.parent = transform;
                rightWall.transform.parent = transform;
            }
        }
        Wall.GetComponent<Transform>().localScale = wallSize;
    }
    bool end()
    {
        if (isCellVisited(currentRow - 1, currentColumn))
        {
            return true;
        }
        if (isCellVisited(currentRow + 1, currentColumn))
        {
            return true;
        }
        if (isCellVisited(currentRow, currentColumn + 1))
        {
            return true;
        }
        if (isCellVisited(currentRow, currentColumn - 1))
        {
            return true;
        }
        return false;
    }
    bool isCellVisited(int r, int c)
    {
        if (r >= 0 && c >= 0 && r < Rows && c < Columns
            && grid[r, c].Visited)
        {
            return true;
        }
        return false;
    }
    void createPath()
    {
        grid[currentRow, currentColumn].Visited = true;
        while (end())
        {
            int direction = Random.Range(0, 4);
            //create path
            if (direction == 0)
            {
                Debug.Log(1);
                if (isCellVisited(currentRow - 1, currentColumn))
                {
                    if (grid[currentRow, currentColumn].upWall)
                    {
                        Destroy(grid[currentRow, currentColumn].upWall);
                    }
                    currentRow--;
                    grid[currentRow, currentColumn].Visited = true;
                    if (grid[currentRow, currentColumn].downWall)
                    {
                        Destroy(grid[currentRow, currentColumn].downWall);
                    }
                }
            }
            else if (direction == 1)
            {
                Debug.Log(2);
                if (isCellVisited(currentRow + 1, currentColumn))
                {
                    if (grid[currentRow, currentColumn].downWall)
                    {
                        Destroy(grid[currentRow, currentColumn].downWall);
                    }
                    currentRow++;
                    grid[currentRow, currentColumn].Visited = true;
                    if (grid[currentRow, currentColumn].upWall)
                    {
                        Destroy(grid[currentRow, currentColumn].upWall);
                    }
                }
            }
            else if (direction == 2)
            {
                Debug.Log(3);
                if (isCellVisited(currentRow, currentColumn - 1))
                {
                    if (grid[currentRow, currentColumn].leftWall)
                    {
                        Destroy(grid[currentRow, currentColumn].leftWall);
                    }
                    currentColumn--;
                    grid[currentRow, currentColumn].Visited = true;
                    if (grid[currentRow, currentColumn].rightWall)
                    {
                        Destroy(grid[currentRow, currentColumn].rightWall);
                    }
                }
            }
            else if (direction == 3)
            {
                Debug.Log(4);
                if (isCellVisited(currentRow, currentColumn + 1))
                {
                    if (grid[currentRow, currentColumn].rightWall)
                    {
                        Destroy(grid[currentRow, currentColumn].rightWall);
                    }
                    currentColumn++;
                    grid[currentRow, currentColumn].Visited = true;
                    if (grid[currentRow, currentColumn].leftWall)
                    {
                        Destroy(grid[currentRow, currentColumn].leftWall);
                    }
                }
            }
        }
    }
}
