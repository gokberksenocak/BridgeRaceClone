using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Cinemachine;

public class NPC_Move : MonoBehaviour
{
    private NavMeshAgent npc;
    private int i, k, box2Count, second2Count;
    [SerializeField] private GameObject[] blues;
    [SerializeField] private GameObject[] LastBlues;
    [SerializeField] private Transform[] SecondTarget2;
    [SerializeField] private GameObject CollectPoint2;
    [SerializeField] private Animator _animator_;
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private Transform FinishLine;
    [SerializeField] private Material material;
    [SerializeField] private GameObject _panel3;
    [SerializeField] private Material _material;
    void Start()
    {
        box2Count = 0;
        second2Count = 0;
        i = 0;
        npc = GetComponent<NavMeshAgent>();
        npc.SetDestination(blues[0].transform.position);
        npc.speed = UnityEngine.Random.Range(20f, 30f);
        npc.acceleration = UnityEngine.Random.Range(50f, 60f);
        npc.angularSpeed = UnityEngine.Random.Range(145f, 155f);
        _animator_.SetBool("running", true);
        k = Random.Range(0, 3);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Blue"))
        {
            other.gameObject.SetActive(false);
            i++;
            box2Count++;
            CollectPoint2.transform.GetChild(box2Count - 1).gameObject.SetActive(true);
            if (i<blues.Length)
            {
                npc.SetDestination(blues[i].transform.position);
            }
            else
            {
                npc.SetDestination(SecondTarget2[k].position);
            }
        }
        if (other.gameObject.CompareTag("SecondPart"))
        {
            i = 0;
            for (int j = 0; j < LastBlues.Length; j++)
            {
                LastBlues[j].SetActive(true);
            }
            npc.SetDestination(LastBlues[i].transform.position);
        }
        if (other.gameObject.CompareTag("Blue2"))
        {
            other.gameObject.SetActive(false);
            i++;
            second2Count++;
            CollectPoint2.transform.GetChild(second2Count - 1).gameObject.SetActive(true);
            if (i < LastBlues.Length)
            {
                npc.SetDestination(LastBlues[i].transform.position);
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
            box2Count--;
            CollectPoint2.transform.GetChild(box2Count).gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("LastBridgeBox") && other.gameObject.GetComponent<MeshRenderer>().sharedMaterial != _material)
        {
            other.gameObject.GetComponent<MeshRenderer>().enabled = true;
            other.gameObject.GetComponent<MeshRenderer>().sharedMaterial = material;
            second2Count--;
            CollectPoint2.transform.GetChild(second2Count).gameObject.SetActive(false);
        }
    }
}