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
    MeshRenderer[] meshes; // ������ ������ MeshRenderer �迭
    Color hitColor = Color.red; // �ǰ� �� ����
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
            target = player.GetComponent<Rigidbody>(); // �Ǵ� ���ϴ� Ÿ������ ����
            Vector3 dirVec = target.position - transform.position; // enemy ����

            if (dirVec != Vector3.zero) // ��ǥ�� ������ ��
            {
                Quaternion targetRotation = Quaternion.LookRotation(dirVec);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f); // �ε巯�� ȸ��

                anim.SetBool("isWalk", true); // �ȱ� �ִϸ��̼� ����

                // �̵�
                Vector3 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
                rigid.MovePosition(rigid.position + nextVec); // �� ��ġ + ���ư��� �� �Ÿ�
            }
            else
            {
                anim.SetBool("isWalk", false); // ��ǥ�� �������� ��� �ȱ� �ִϸ��̼� ����
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
            // target�� �����Ͽ� Walking() �޼��尡 �۵��ϵ��� ��
            target = player.GetComponent<Rigidbody>(); // �Ǵ� ���ϴ� Ÿ������ ����
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // �÷��̾� �������� ������ ��
        if (other.CompareTag("PlayerArea"))
        {
            target = null; // target�� �ʱ�ȭ�Ͽ� Walking()�� ���߰� �մϴ�.
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
        Debug.Log($"monsterID���� ���Ͱ� �����Ͽ����ϴ�!");
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
            mesh.material.color = hitColor; // �ǰ� �� �������� ����
        }
        // ������ �ð� ���
        yield return new WaitForSeconds(0.5f);

        // ���� �������� ����
        foreach (MeshRenderer mesh in meshes)
        {
            mesh.material.color = Color.white; // ���� �������� ����
        }
    }
}
