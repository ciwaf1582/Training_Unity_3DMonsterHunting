using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rigid;
    Animator anim;
    public GameObject quest;

    public int hp;
    public int atk;
    public float speed;

    public float hAxis;
    public float vAxis;

    public bool isAttack = false;
    bool isDefence = false;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            StartCoroutine(Attack());
            if (Physics.Raycast(ray, out hit))
            {
                NPC npc = hit.collider.GetComponent<NPC>();
                if (npc != null)
                {
                    // NPC�� OnClick �޼��� ȣ��
                    npc.OnClick();
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(Defen());
        }

    }
    private void FixedUpdate()
    {
        Move();
    }
    void Move()
    {
        if (isAttack || isDefence)
        {
            return;
        }
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        Vector3 inputVec = new Vector3(hAxis, 0, vAxis).normalized;

        // �Է� ���Ͱ� 0�� �ƴ� ���� �̵�
        if (inputVec != Vector3.zero)
        {
            anim.SetBool("isRun", true);
            Vector3 nextVec = inputVec * speed * Time.fixedDeltaTime;
            rigid.MovePosition(rigid.position + nextVec);

            // ȸ�� �߰� (�̵� ��������)
            Quaternion targetRotation = Quaternion.LookRotation(inputVec);
            rigid.rotation = Quaternion.Slerp(rigid.rotation, targetRotation, Time.fixedDeltaTime * 10f);
        }
        else
        {
            anim.SetBool("isRun", inputVec != Vector3.zero);
        }
    }
    IEnumerator Attack()
    {
        isAttack = true;
        anim.SetBool("isAttack", true);
        yield return new WaitForSeconds(0.8f);
        anim.SetBool("isAttack", false);
        isAttack = false;
        yield return null;
    }
    IEnumerator Defen()
    {
        isDefence = true;
        anim.SetBool("isDefence", true); // ��� �ִϸ��̼� ����
        yield return new WaitForSeconds(0.5f); // ��� ��� ���� �ð�
        anim.SetBool("isDefence", false); // ��� �ִϸ��̼� ����
        isDefence = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("door"))
        {
            quest.gameObject.SetActive(true);
        }
        if (collision.gameObject.CompareTag("In"))
        {
            Vector3 newPos = new Vector3(22, 0, 3);
            transform.position = newPos;
            Debug.Log("�̵� �Ϸ�");
        }
    }
}

