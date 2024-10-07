using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    public Player player;
    Rigidbody rigid;
    Collider coll;
    Animator anim;
    MeshRenderer[] meshes; // 색상을 변경할 MeshRenderer 배열
    Color hitColor = Color.red; // 피격 시 색상
    Rigidbody target;

    public int monsterID;
    public int atk;
    public int hp;
    public float speed;

    bool isAttack = false;
    bool isDefence = false;
    bool isDie = false;
    private void Awake()
    {
        meshes = GetComponentsInChildren<MeshRenderer>();
        //hitColor = GetComponent<Color>();
        rigid = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (target != null)
        {
            Walking();
        }
        
    }
    void Walking()
    {

        if (isDie)
        {
            return;
        }
        else if (!isAttack)
        {
            target = player.GetComponent<Rigidbody>(); // 또는 원하는 타겟으로 설정
            Vector3 dirVec = target.position - transform.position; // enemy 방향

            if (dirVec != Vector3.zero) // 목표가 존재할 때
            {
                Quaternion targetRotation = Quaternion.LookRotation(dirVec);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f); // 부드러운 회전

                anim.SetBool("isWalk", true); // 걷기 애니메이션 실행

                // 이동
                Vector3 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
                rigid.MovePosition(rigid.position + nextVec); // 현 위치 + 나아가야 할 거리
            }
            else
            {
                anim.SetBool("isWalk", false); // 목표에 도착했을 경우 걷기 애니메이션 중지
            }
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(golemAttack());
        }
        else if (collision.gameObject.CompareTag("Sword"))
        {
            //StartCoroutine(Defence());
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerArea"))
        {
            // target을 설정하여 Walking() 메서드가 작동하도록 함
            target = player.GetComponent<Rigidbody>(); // 또는 원하는 타겟으로 설정
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // 플레이어 영역에서 나갔을 때
        if (other.CompareTag("PlayerArea"))
        {
            target = null; // target을 초기화하여 Walking()이 멈추게 합니다.
        }
    }
    IEnumerator golemAttack()
    {
        int attackNum = Random.Range(0, 2);
        isAttack = true;
        switch (attackNum)
        {
            case 0:
                anim.SetBool("isAttack1", true);
                break;
            case 1:
                anim.SetBool("isAttack2", true);
                break;
        }
        Debug.Log($"monsterID번의 몬스터가 공격하였습니다!");
        yield return new WaitForSeconds(2f);
        anim.SetBool("isAttack1", false);
        anim.SetBool("isAttack2", false);

        isAttack = false;
    }
    IEnumerator Defence()
    {
        hp -= player.atk;
        foreach (MeshRenderer mesh in meshes)
        {
            mesh.material.color = hitColor; // 피격 시 색상으로 변경
        }
        // 지정된 시간 대기
        yield return new WaitForSeconds(0.5f);

        // 원래 색상으로 복원
        foreach (MeshRenderer mesh in meshes)
        {
            mesh.material.color = Color.white; // 원래 색상으로 복원
        }
    }
}
