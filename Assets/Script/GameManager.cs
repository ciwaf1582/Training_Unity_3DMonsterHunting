using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player player;
    public GameObject door;

    Collider coll;

    private void Awake()
    {
        instance = this;
        coll = door.GetComponent<Collider>();
    }
    private void Update()
    {
        
    }
    public void onClickIn()
    {
        StartCoroutine(MoveDoor());
    }
    private IEnumerator MoveDoor()
    {
        coll.enabled = false;
        while (door.transform.position.x > -2f)
        {
            // ���� �� ��ġ ��������
            Vector3 vec = door.transform.position;

            // �̵��� ���� ��� (���� ��������)
            vec -= transform.right * 0.5f * Time.deltaTime;

            // �� ��ġ ������Ʈ
            door.transform.position = vec;

            // ������ ���̿� ��� ���
            yield return null; // ���� �����ӱ��� ���
        }
    }
}
