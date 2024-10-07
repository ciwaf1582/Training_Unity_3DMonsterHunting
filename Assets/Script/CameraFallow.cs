using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFallow : MonoBehaviour
{
    public GameObject target; // ī�޶� ����ٴ� Ÿ��

    // offset : Ÿ�����κ����� ī�޶� ��ġ
    public float offsetX = 0.0f;   // ī�޶� x��ǥ
    public float offsetY = 10.0f;  // ī�޶� y��ǥ
    public float offsetZ = -10.0f; // ī�޶� z��ǥ

    public float cameraSpeed; // ī�޶� �ӵ�
    Vector3 targetPos;        // Ÿ�� ��ġ

    private void FixedUpdate()
    {
        // Ÿ���� x, y, z��ǥ�� ī�޶��� ��ǥ�� ���Ͽ� ī�޶��� ��ġ ����
        targetPos = new Vector3(target.transform.position.x + offsetX,
                                target.transform.position.y + offsetY,
                                target.transform.position.z + offsetZ);

        // ī�޶��� �������� �ε巴�� �ϴ� �Լ�(Lerp)
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * cameraSpeed);

        //if (Input.GetKeyDown(KeyCode.Space))
        //    Debug.Log(KeyCode.Space);
        //    RotateCamera();
    }
    private void RotateCamera()
    {
        Vector3 playerForward = target.transform.forward; // �÷��̾� �ü� ����
        Quaternion targetRoration = Quaternion.LookRotation(playerForward);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRoration, Time.deltaTime * cameraSpeed);
    }
}
