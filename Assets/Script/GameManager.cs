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
            // 현재 문 위치 가져오기
            Vector3 vec = door.transform.position;

            // 이동할 방향 계산 (왼쪽 방향으로)
            vec -= transform.right * 0.5f * Time.deltaTime;

            // 문 위치 업데이트
            door.transform.position = vec;

            // 프레임 사이에 잠시 대기
            yield return null; // 다음 프레임까지 대기
        }
    }
}
