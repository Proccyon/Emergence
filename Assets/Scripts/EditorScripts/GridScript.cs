//Usage: When this script is added to an object it will no longer be able to move freely and will be constained to the editor grid.
//The script still works even when the game is running(might need to turn this off).


//Unity imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class GridScript : MonoBehaviour
{


    int GridSize = 100; //The size of the grid in pixels
    int PixelsPerUnit = 100; //if PixelsPerUnit = 100 and transform.position = (1,0,0) then object is 100 pixels from origin.

    //Rounds down the position of the object so that it stays within a grid square.
    public float RoundComponent(int GridSize, int PixelsPerUnit, float Position)
    {
        return Mathf.Round(Position * PixelsPerUnit / GridSize + 0.5f) * GridSize / PixelsPerUnit - 0.5f;
    }

    void Update()
    {
        Vector3 CurrentPosition = transform.position;
        float NewX = RoundComponent(GridSize, PixelsPerUnit, CurrentPosition.x);
        float NewY = RoundComponent(GridSize, PixelsPerUnit, CurrentPosition.y);
        float NewZ = RoundComponent(GridSize, PixelsPerUnit, CurrentPosition.z);
        transform.position = new Vector3(NewX, NewY, NewZ);
    }
}
