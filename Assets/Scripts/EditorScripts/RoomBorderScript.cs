//Usage: Creates black borders around the room in editor mode.
//The position of the borders depend on the RoomHeight and RoomWidth variables.
//The script automatically replaces old borders when RoomHeigt or RoomWidth is updated.
//Attach this to the room control object.

//Unity imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//Atribute
[ExecuteInEditMode]

public class RoomBorderScript : MonoBehaviour
{

    //Checks if an object with name "ObjectName" is in the scene
    //Can be done more elegant but works for now.
    public bool IsOnScene(string ObjectName)
    {
        try
        {
            GameObject Test = GameObject.Find(ObjectName);
            string TestName = Test.name; //This will fail object is not found.
            return true;
        }
        catch
        {
            return false;
        }

    }

    //Checks if object with name "Objectname" is at position (TargetX, TargetY)
    //Destroys object if it is not.
    public bool IsRightPosition(string ObjectName, double TargetX, double TargetY)
    {
        if (IsOnScene(ObjectName)) //Checks if object is in the scene
        {
            GameObject Object = GameObject.Find(ObjectName);
            double x = Object.transform.position.x;
            double y = Object.transform.position.y;

            //Calculate distance of object to target position
            double Distance = System.Math.Sqrt(System.Math.Pow(TargetX - x, 2) + System.Math.Pow(TargetY - y, 2));

            //Object is considered to be at target position if it is within a distance of 0.1.
            if (Distance >= 0.1)
            {
                DestroyImmediate(Object); //Destroy the old object
                return false;
            }
            else
            {
                return true;
            }
        }

        return false; //Returns false if object is not in the scene at all
    }

    //Creates a Cube at position (x,y) with scale (XLength,YLength) and name BorderName
    public void CreateBorder(double x, double y, int XLength, int YLength, string BorderName)
    {
        //Checks if there already is a border at (x,y). Stop if there is.
        if (!IsRightPosition(BorderName, x, y))
        {
            GameObject Border = GameObject.CreatePrimitive(PrimitiveType.Cube); //Create Object
            Border.name = BorderName; //Set name
            Border.transform.position = new Vector3((float)x, (float)y, 0); //Set position
            Border.transform.localScale = new Vector3((float)XLength, (float)YLength, 1); //Set length
            Border.AddComponent<RemoveRoomBorder>(); //Adds a script to the border that automatically removes it when game starts

        }
    }

    void Update()
    {

        //Doesn't execute while game is active.
        if (!EditorApplication.isPlaying)
        {

            int RoomHeight = gameObject.GetComponent<RoomPropertiesScript>().RoomHeight;
            int RoomWidth = gameObject.GetComponent<RoomPropertiesScript>().RoomWidth;

            //Creates borders at the correct positions.
            CreateBorder(-0.5, (double)RoomHeight / 2, 1, RoomHeight + 2, "LeftRoomBorder");
            CreateBorder(RoomWidth + 0.5, (double)RoomHeight / 2, 1, RoomHeight + 2, "RightRoomBorder");
            CreateBorder((double)RoomWidth / 2, RoomHeight + 0.5, RoomWidth + 2, 1, "TopRoomBorder");
            CreateBorder((double)RoomWidth / 2, -0.5, RoomWidth + 2, 1, "BottomRoomBorder");

        }
        else
        {
            Destroy(this);//Destroys this script when game starts.
        }
    }
}
