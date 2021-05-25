using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody PlayerRigidbody;//이동에 사용활 리지드바디 컴포넌트
    public float speed = 8f;//이동속력

    public int hp = 100;
    public HPBar hpbar;
    // Start is called before the first frame update
    void Start()
    {
        PlayerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // 수평축과 수직축의 입력값을 감지하여 저장
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        // 실제 이동 속도를 입력값과 이동 속력을 사용해 결정
        float xSpeed = xInput * speed;
        float zSpped = zInput * speed;

        // Vector3 속도를 (xSpeed, 0, zSpeed)로 생성
        Vector3 newVelocity = new Vector3(xSpeed, 0f, zSpped);
        // 리지드바디의 속도에 newVelcocity 할당
        PlayerRigidbody.velocity = newVelocity;
    }

    void Die()
    {
        // 자신의 게임 오브젝트를 비활성화
        gameObject.SetActive(false);

        //씬에 존재하는 Gamamanager 타입의 오브젝트를 찾아서 가져오기
        Gamemanager gameManager = FindObjectOfType<Gamemanager>();
        //가져온 Gamemanager 오브젝트의 EndGame() 메서트 실행
        gameManager.Endgame();
    }

    public void GetHeal(int heal)
    {
        hp += heal;
        if(hp > 100)
        {
            hp = 100;
        }
        hpbar.SetHP(hp);
    }

    public void GetDamage(int damage)
    {
        hp -= damage;
        hpbar.SetHP(hp);
        if(hp <= 0)
        {
            Die();
        }
    }
}
