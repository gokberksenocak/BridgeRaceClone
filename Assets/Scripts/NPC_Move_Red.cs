using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Cinemachine;

public class NPC_Move_Red : MonoBehaviour
{
    private NavMeshAgent npc;
    private int i, t, box3Count, second3Count;
    [SerializeField] private GameObject[] reds;
    [SerializeField] private GameObject[] LastReds;
    [SerializeField] private GameObject CollectPoint3;
    [SerializeField] private Animator _animator_;
    [SerializeField] private Transform FinishLine;
    [SerializeField] private Material material;
    [SerializeField] private GameObject _panel3;
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private Material _material;
    [SerializeField] private Transform[] SecondTarget3;
    void Start()
    {
        box3Count = 0;
        second3Count = 0;
        i = 0;
        npc = GetComponent<NavMeshAgent>();
        npc.SetDestination(reds[0].transform.position);
        npc.speed = UnityEngine.Random.Range(20f, 30f);
        npc.acceleration = UnityEngine.Random.Range(50f,60f);
        npc.angularSpeed = UnityEngine.Random.Range(145f, 155f);
        _animator_.SetBool("running", true);
        t = Random.Range(0, 3);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Red"))
        {
            other.gameObject.SetActive(false);
            i++;
            box3Count++;
            CollectPoint3.transform.GetChild(box3Count - 1).gameObject.SetActive(true);
            if (i < reds.Length)
            {
                npc.SetDestination(reds[i].transform.position);
            }
            else
            {
                npc.SetDestination(SecondTarget3[t].position);
            }
        }
        if (other.gameObject.CompareTag("SecondPart"))
        {
            i = 0;
            for (int j = 0; j < LastReds.Length; j++)
            {
                LastReds[j].SetActive(true);
            }
            npc.SetDestination(LastReds[i].transform.position);
        }
        if (other.gameObject.CompareTag("Red2"))
        {
            other.gameObject.SetActive(false);
            i++;
            second3Count++;
            CollectPoint3.transform.GetChild(second3Count - 1).gameObject.SetActive(true);
            if (i < LastReds.Length)
            {
                npc.SetDestination(LastReds[i].transform.position);
            }
            else
            {
                npc.SetDestination(FinishLine.position);
            }
        }
        if (other.gameObject.CompareTag("End"))
        {
            _camera.Priority = 5;
            _panel3.SetActive(true);
            Time.timeScale = 0;
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("BridgeBox") && other.gameObject.GetComponent<MeshRenderer>().sharedMaterial != _material)//
        {
            other.gameObject.GetComponent<MeshRenderer>().enabled = true;
            other.gameObject.GetComponent<MeshRenderer>().sharedMaterial = material;
            box3Count--;
            CollectPoint3.transform.GetChild(box3Count).gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("LastBridgeBox") && other.gameObject.GetComponent<MeshRenderer>().sharedMaterial != _material)
        {
            other.gameObject.GetComponent<MeshRenderer>().enabled = true;
            other.gameObject.GetComponent<MeshRenderer>().sharedMaterial = material;
            second3Count--;
            CollectPoint3.transform.GetChild(second3Count).gameObject.SetActive(false);
        }
    }
}