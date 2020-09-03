using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    public int Columns = 4;
    public int Rows = 4;
    public GameObject Wall;
    public GameObject Floor;

    void Start()
    {
        Vector3 floorSize = Floor.transform.localScale;
        Vector3 wallSize = Wall.transform.localScale;
        string floorName = "floor_";
        string leftWallName = "leftWall_";
        string rightWallName = "rightWall_";
        string downLeftWallName = "downLeftWall_";
        string downRightWallName = "downRightWall_";
        int height = 0;

        for (int row = 0; row < Rows; row++)
        {
            for (int column = 0; column < Columns; column++)
            {
                GameObject floor = Instantiate(Floor, new Vector3(column * floorSize.x, height, -row * floorSize.z), Quaternion.identity);
                floor.name = floorName + row + "_" + column;
                Wall.GetComponent<Transform>().localScale = new Vector3(floorSize.x, wallSize.y, wallSize.z);
                GameObject leftWall = Instantiate(Wall, new Vector3(column * floorSize.x, height + (wallSize.y / 2), -row * floorSize.z + (floorSize.z / 2)), Quaternion.identity);
                leftWall.name = leftWallName + row + "_" + column;
                GameObject leftDownWall = Instantiate(Wall, new Vector3(column * floorSize.x, height + (wallSize.y / 2), -row * floorSize.z - (floorSize.z / 2)), Quaternion.identity);
                leftDownWall.name = downLeftWallName + row + "_" + column;
                Wall.GetComponent<Transform>().localScale = new Vector3(floorSize.z, wallSize.y, wallSize.z);
                GameObject rightWall = Instantiate(Wall, new Vector3(column * floorSize.x + (floorSize.x / 2), height + (wallSize.y / 2), -row * floorSize.z), Quaternion.Euler(0, 90, 0));
                rightWall.name = rightWallName + row + "_" + column;
                GameObject rightDownWall = Instantiate(Wall, new Vector3(column * floorSize.x - (floorSize.x / 2), height + (wallSize.y / 2), -row * floorSize.z), Quaternion.Euler(0, 90, 0));
                rightDownWall.name = downLeftWallName + row + "_" + column;
            }
        }
        Wall.GetComponent<Transform>().localScale = wallSize;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
