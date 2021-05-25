﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //UI 관련 라이브러리
using UnityEngine.SceneManagement; //씬 관리 관련 라이브러리
public class Gamemanager : MonoBehaviour
{
    public GameObject GameoverText; // 게임오버 시 활성화할 텍스트 게임 오브젝트
    public Text TimeText; // 생존 시간을 표시할 텍스트 컴포넌트
    public Text RecordText; // 최고 기록을 표시할 텍스트 컴포넌트

    public GameObject level; // 불렛 등 레벨 수정할 변수
    public GameObject bullerSpawnerPrefab;
    public GameObject itemPrefab;
    int prevItemCheck;

    private Vector3[] bulletSpawners = new Vector3[4];
    int spawnCounter = 0;

    private float surviveTime; // 생존시간
    private bool isGameover; // 게임오버 상태
    void Start()
    {
        // 생존 시간과 게임오버 상태 초기화
        surviveTime = 0;
        isGameover = false;

        bulletSpawners[0].x = -8f;
        bulletSpawners[0].y = 1f;
        bulletSpawners[0].z = 8f;

        bulletSpawners[1].x = 8f;
        bulletSpawners[1].y = 1f;
        bulletSpawners[1].z = 8f;

        bulletSpawners[2].x = 8f;
        bulletSpawners[2].y = 1f;
        bulletSpawners[2].z = -8f;

        bulletSpawners[3].x = -8f;
        bulletSpawners[3].y = 1f;
        bulletSpawners[3].z = -8f;
    }

    
    void Update()
    {
        // 게임오버가 아닌 동안
        if(!isGameover)
        {
            // 생존 시간 갱신
            surviveTime += Time.deltaTime;
            // 갱신한 생존 시간을 timetext 텍스트 컴포넌트를 이용해 표시
            TimeText.text = "Time: " + (int)surviveTime;

            if(surviveTime % 5f <= 0.01f && prevItemCheck == 4)
            {
                Vector3 randpos = new Vector3(Random.Range(-8f, 8f), 0f, Random.Range(-8f, 8f));

                GameObject item = Instantiate(itemPrefab, randpos, Quaternion.identity);
                item.transform.parent = level.transform;
                item.transform.localPosition = randpos;
            }
            prevItemCheck = (int)(surviveTime % 5f);
            if (surviveTime < 5f && spawnCounter == 0)
            {
                GameObject bulletSpawner = Instantiate(bullerSpawnerPrefab, bulletSpawners[spawnCounter], Quaternion.identity);
                bulletSpawner.transform.parent = level.transform;
                bulletSpawner.transform.localPosition = bulletSpawners[spawnCounter];
                level.GetComponent<Rotator>().rotationSpeed += 15f;
                spawnCounter++;
            }
            else if (surviveTime >= 5f && surviveTime < 10f && spawnCounter ==1)
            {
                GameObject bulletSpawner = Instantiate(bullerSpawnerPrefab, bulletSpawners[spawnCounter], Quaternion.identity);
                bulletSpawner.transform.parent = level.transform;
                bulletSpawner.transform.localPosition = bulletSpawners[spawnCounter];
                level.GetComponent<Rotator>().rotationSpeed += 15f;
                spawnCounter++;
            }
            else if (surviveTime >= 10f && surviveTime < 15f && spawnCounter == 2)
            {
                GameObject bulletSpawner = Instantiate(bullerSpawnerPrefab, bulletSpawners[spawnCounter], Quaternion.identity);
                bulletSpawner.transform.parent = level.transform;
                bulletSpawner.transform.localPosition = bulletSpawners[spawnCounter];
                level.GetComponent<Rotator>().rotationSpeed += 15f;
                spawnCounter++;
            }
            else if (surviveTime >= 15f && surviveTime < 20f && spawnCounter == 3)
            {
                GameObject bulletSpawner = Instantiate(bullerSpawnerPrefab, bulletSpawners[spawnCounter], Quaternion.identity);
                bulletSpawner.transform.parent = level.transform;
                bulletSpawner.transform.localPosition = bulletSpawners[spawnCounter];
                level.GetComponent<Rotator>().rotationSpeed += 15f;
                spawnCounter++;
            }
            else if (surviveTime >= 20f && surviveTime < 25f && spawnCounter == 4)
            {
                GameObject bulletSpawner = Instantiate(bullerSpawnerPrefab, bulletSpawners[spawnCounter], Quaternion.identity);
                bulletSpawner.transform.parent = level.transform;
                bulletSpawner.transform.localPosition = bulletSpawners[spawnCounter];
                level.GetComponent<Rotator>().rotationSpeed += 15f;
                spawnCounter++;
            }
        }
        else
        {
            // 게임오버 상태에서 R키를 누른 경우
            if(Input.GetKeyDown(KeyCode.R))
            {   
                // SampleScene 신을 로드
                SceneManager.LoadScene("SampleScene");
            }
        }
    }
    
    // 현재 게임을 게임오버 상태로 변경하는 메서드
    public void Endgame()
    {
        // 현재상태를 게임오버 상태로 전환
        isGameover = true;
        // 게임오버 텍스트 게임 오브젝트를 활성화
        GameoverText.SetActive(true);

        // BestTime 키로 저장된 이전까지의 최고 기록 가져오기
        float BestTime = PlayerPrefs.GetFloat("BestTime");
        // 이전까지의 최고 기록보다 현재 생존 시간이 더 크다면
        if(surviveTime > BestTime)
        {
            BestTime = surviveTime;

            PlayerPrefs.SetFloat("BestTime", BestTime);
        }
        //최고 기록을 RecordText 텍스트 컴포넌트를 이용해 표시
        RecordText.text = "Best Time: " + (int)BestTime;
    }
}
