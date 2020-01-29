//Usage: When this script is added to an object it will no longer be able to move freely and will be constained to the editor grid.
//The script still works even when the game is running(might need to turn this off).
//This script is specifically for walls, it locks them to the side of tiles.


//Unity imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GenericMethods;

[ExecuteInEditMode]

public class WallGridScript : MonoBehaviour
{

    int GridSize = 100; //The size of the grid in pixels
    int PixelsPerUnit = 100; //if PixelsPerUnit = 100 and transform.position = (1,0,0) then object is 100 pixels from origin.
    public Vector2Int WallRotation;
    public bool RotateWall = false;

    //Rounds down the position of the object to half integers
    public float RoundComponentHalf(int GridSize, int PixelsPerUnit, float Position)
    {
        return Mathf.Round(Position * PixelsPerUnit / GridSize + 0.5f) * GridSize / PixelsPerUnit - 0.5f;
    }

    //Rounds down the position of the object to Whole integers
    public float RoundComponentWhole (int GridSize, int PixelsPerUnit, float Position)
    {
        return Mathf.Round(Position * PixelsPerUnit / GridSize) * GridSize / PixelsPerUnit;
    }

    void Update()
    {

        Vector3 CurrentPosition = transform.position;

        //Position of wall when being held with the cursor
        float OldX = CurrentPosition.x;
        float OldY = CurrentPosition.y;
        float OldZ = CurrentPosition.z;

        //x,y if wall is set to the nearest vertical position
        float VerticalX = RoundComponentWhole(GridSize, PixelsPerUnit, CurrentPosition.x);
        float VerticalY = RoundComponentHalf(GridSize, PixelsPerUnit, CurrentPosition.y);

        //x,y if wall is set to the nearest horizontal position
        float HorizontalX = RoundComponentHalf(GridSize, PixelsPerUnit, CurrentPosition.x);
        float HorizontalY = RoundComponentWhole(GridSize, PixelsPerUnit, CurrentPosition.y);

        //Distances from cursor to vertical and horizontal position
        float VerticalDistance = Methods.Distance(OldX, OldY, VerticalX, VerticalY);
        float HorizontalDistance = Methods.Distance(OldX, OldY, HorizontalX, HorizontalY);

        //Move the wall to the position that is closest of the two
        if (VerticalDistance <= HorizontalDistance)
        {
            //-1.5 is set so the wall appears above tiles, blocks, etc
            transform.position = new Vector3(VerticalX, VerticalY, -1.5f);

            //RotateWall determines which direction the wall is aiming at, its set in the editor
            if(RotateWall)
            {
                transform.eulerAngles = new Vector3(0, 0, 180);
                WallRotation = Vector2Int.left;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                WallRotation = Vector2Int.right;
            }
            
        }
        else
        {
            transform.position = new Vector3(HorizontalX, HorizontalY, -1.5f);
            if(RotateWall)
            {
                transform.eulerAngles = new Vector3(0, 0, 270);
                WallRotation = Vector2Int.down;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 90);
                WallRotation = Vector2Int.up;
            }
            
        }
        
    }
}
