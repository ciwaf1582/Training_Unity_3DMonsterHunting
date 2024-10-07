using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFallow : MonoBehaviour
{
    public GameObject target; // 카메라가 따라다닐 타겟

    // offset : 타겟으로부터의 카메라 위치
    public float offsetX = 0.0f;   // 카메라 x좌표
    public float offsetY = 10.0f;  // 카메라 y좌표
    public float offsetZ = -10.0f; // 카메라 z좌표

    public float cameraSpeed; // 카메라 속도
    Vector3 targetPos;        // 타겟 위치

    private void FixedUpdate()
    {
        // 타겟의 x, y, z좌표에 카메라의 좌표를 더하여 카메라의 위치 결정
        targetPos = new Vector3(target.transform.position.x + offsetX,
                                target.transform.position.y + offsetY,
                                target.transform.position.z + offsetZ);

        // 카메라의 움직임을 부드럽게 하는 함수(Lerp)
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * cameraSpeed);

        //if (Input.GetKeyDown(KeyCode.Space))
        //    Debug.Log(KeyCode.Space);
        //    RotateCamera();
    }
    private void RotateCamera()
    {
        Vector3 playerForward = target.transform.forward; // 플레이어 시선 방향
        Quaternion targetRoration = Quaternion.LookRotation(playerForward);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRoration, Time.deltaTime * cameraSpeed);
    }
}
