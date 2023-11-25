using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class CameraSystem : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cmvc;
    [SerializeField] Terrain world;

    float moveSpd = 15.0f;
    float rotateSpd = 100.0f;
    private bool dragPanMoveActive;
    private Vector2 lastMousePosition;

    private float targetFOV = 60.0f;
    [SerializeField] float minFOV = 10f;
    [SerializeField] float maxFOV = 60f;

    [SerializeField] PlayerControl playerControl;

    private void Update()
    {
        TransformGate();

        if (playerControl.selectedChar != null)
        {
            transform.position = new Vector3(playerControl.selectedChar.transform.position.x,
                                                playerControl.selectedChar.transform.position.y + 0.75f, 
                                                playerControl.selectedChar.transform.position.z);
        }
        else
        {
            HandleCameraMovement();
        }
        HandleCameraZoom();
        HandleCameraRotation();
    }

    private void HandleCameraMovement()
    {
        Vector3 inputDir = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.W)) inputDir.z = +1.0f;
        if (Input.GetKey(KeyCode.S)) inputDir.z = -1.0f;
        if (Input.GetKey(KeyCode.A)) inputDir.x = -1.0f;
        if (Input.GetKey(KeyCode.D)) inputDir.x = +1.0f;
        if (Input.GetKey(KeyCode.LeftShift)) inputDir.y = -1.0f;
        if (Input.GetKey(KeyCode.Space)) inputDir.y = +1.0f;

        #region ClampingDirY
        float clampedDirY = Mathf.Clamp(inputDir.y, -5, 20);
        #endregion

        #region DragPan
        //int edgeScrollSize = 20;

        //if (Input.mousePosition.x < edgeScrollSize)
        //{
        //    inputDir.x = -1.0f;
        //}
        //if (Input.mousePosition.y < edgeScrollSize)
        //{
        //    inputDir.z = -1.0f;
        //}
        //if (Input.mousePosition.x > Screen.width - edgeScrollSize)
        //{
        //    inputDir.x = +1.0f;
        //}
        //if (Input.mousePosition.y > Screen.height - edgeScrollSize)
        //{
        //    inputDir.z = +1.0f;
        //}
        #endregion

        #region mouseClickMovement
        //if (Input.GetMouseButtonDown(0))
        //{
        //    dragPanMoveActive = true;
        //    lastMousePosition = Input.mousePosition;
        //}
        //if (Input.GetMouseButtonUp(0))
        //{
        //    dragPanMoveActive = false;
        //}
        #endregion

        #region DragPanActivate
        //if (dragPanMoveActive)
        //{
        //    Vector2 mouseMovementDelta = (Vector2)Input.mousePosition - lastMousePosition;

        //    inputDir.x = mouseMovementDelta.x;
        //    inputDir.z = mouseMovementDelta.y;
        //}
        #endregion

        Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x + transform.up * clampedDirY;
        transform.position += moveDir * moveSpd * Time.deltaTime;
    }

    private void TransformGate()
    {
        Vector3 currentPos = transform.position;
        Vector3 worldSize = world.terrainData.size;

        float clampedX = Mathf.Clamp(currentPos.x, -worldSize.x / 2.0f, worldSize.x / 2.0f);
        float clampedZ = Mathf.Clamp(currentPos.z, -worldSize.z / 2.0f, worldSize.z / 2.0f);

        transform.position = new Vector3(clampedX, currentPos.y, clampedZ);
    }

    private void HandleCameraRotation()
    {
        float rotateDir = 0f;
        if (Input.GetKey(KeyCode.Q)) rotateDir = +1.0f;
        if (Input.GetKey(KeyCode.E)) rotateDir = -1.0f;

        transform.eulerAngles += new Vector3(0, rotateDir * rotateSpd * Time.deltaTime, 0);
    }

    private void HandleCameraZoom()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            targetFOV -= 5;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            targetFOV += 5;
        }

        targetFOV = Mathf.Clamp(targetFOV, minFOV, maxFOV);

        float zoomSpd = 10f;
        cmvc.m_Lens.FieldOfView = Mathf.Lerp(cmvc.m_Lens.FieldOfView, targetFOV, Time.deltaTime * zoomSpd);
    }
}
