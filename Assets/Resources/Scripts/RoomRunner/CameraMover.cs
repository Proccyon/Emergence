
//-----UnityImports-----//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-----GameImports-----//
using RoomSpace;

public class CameraMover : MonoBehaviour
{


    public float ScreenRatio;
    public float ScrollSpeed = 0.03f;
    public float ZoomSpeed = 0.7f;
    public string CameraName = "Main Camera";
    public float RoomHeight;
    public float RoomWidth;
    public float BorderSize = 2;

    public float NewZoomPos(float ZoomRatio, float Xm, float OldX)
    {
        return Xm * (1 - ZoomRatio) + ZoomRatio * OldX;
    }

    public void ResetCameraPosition(GameObject Camera)
    {
        var CameraComponent = Camera.GetComponent<Camera>();

        float CameraX = Camera.transform.position.x;
        float CameraY = Camera.transform.position.y;
        float CameraZ = Camera.transform.position.z;
        float Rx = CameraComponent.orthographicSize * ScreenRatio;
        float Ry = CameraComponent.orthographicSize;
        if (CameraX < Rx-BorderSize)
        {
            CameraX = Rx-BorderSize;
        }

        if (CameraX > RoomWidth-Rx+BorderSize)
        {
            CameraX = RoomWidth-Rx+BorderSize;
        }

        if (CameraY < Ry-BorderSize)
        {
            CameraY = Ry-BorderSize;
        }

        if (CameraY > RoomHeight-Ry+BorderSize)
        {
            CameraY = RoomHeight-Ry+BorderSize;
        }
        Camera.transform.position = new Vector3(CameraX, CameraY, CameraZ);
    }

    public void ResetCameraZoom(GameObject camera)
    {
        var CameraComponent = camera.GetComponent<Camera>();

        if (2 * (CameraComponent.orthographicSize * ScreenRatio+BorderSize) > RoomWidth)
        {
            CameraComponent.orthographicSize = RoomWidth / (2 * ScreenRatio);
        }

        if (2 * (CameraComponent.orthographicSize+BorderSize) > RoomHeight)
        {
            CameraComponent.orthographicSize = RoomHeight / 2;
        }
    }

    void Start()
    {
        ScreenRatio = (float)Screen.width / (float)Screen.height;
        RoomHeight = RoomRunner.ActiveRoom.Height;
        RoomWidth = RoomRunner.ActiveRoom.Width;
}

    // Update is called once per frame
    void Update()
    {

        GameObject Camera = GameObject.Find(CameraName);
        var CameraComponent = Camera.GetComponent<Camera>();

        var ZoomAmount = Input.GetAxis("Mouse ScrollWheel");
        Vector3 MousePosition = Vector3.Scale(Input.mousePosition, new Vector3(1 / (float)Screen.width, 1 / (float)Screen.height, 1));        

        if (Input.GetKey("up"))
        {
            Camera.transform.position += new Vector3(0, ScrollSpeed, 0) * CameraComponent.orthographicSize;
        }

        if(Input.GetKey("down"))
        {
            Camera.transform.position += new Vector3(0, -ScrollSpeed, 0) * CameraComponent.orthographicSize;
        }

        if (Input.GetKey("left"))
        {
            Camera.transform.position += new Vector3(-ScrollSpeed, 0, 0) * CameraComponent.orthographicSize;
        }

        if (Input.GetKey("right"))
        {
            Camera.transform.position += new Vector3(ScrollSpeed, 0, 0) * CameraComponent.orthographicSize;
        }

        if (ZoomAmount != 0)
        {
            float OldRx = CameraComponent.orthographicSize * ScreenRatio;
            float OldRy = CameraComponent.orthographicSize;
            float ZoomRatio = (1 - ZoomAmount * ZoomSpeed);

            float MouseX = Camera.transform.position.x + OldRx * (2 * MousePosition.x - 1);
            float MouseY = Camera.transform.position.y + OldRy * (2 * MousePosition.y - 1);
            Camera.transform.position = new Vector3(NewZoomPos(ZoomRatio, MouseX, Camera.transform.position.x), NewZoomPos(ZoomRatio, MouseY, Camera.transform.position.y), Camera.transform.position.z);
            CameraComponent.orthographicSize = OldRy * ZoomRatio;
            
        }
        ResetCameraZoom(Camera);
        ResetCameraPosition(Camera);

    }
}
