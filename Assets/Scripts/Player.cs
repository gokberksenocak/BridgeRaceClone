using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    private int boxCount, secondCount;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject CollectPoint;
    [SerializeField] private Material _material;
    [SerializeField] private NavMeshAgent npc1;
    [SerializeField] private NavMeshAgent npc2;
    [SerializeField] private GameObject[] Greens;
    [SerializeField] private GameObject[] Bridges;
    [SerializeField] private GameObject[] LastGreens;
    void Start()
    {
        boxCount = 0;
        secondCount = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Green"))
        {
            boxCount++;
            other.gameObject.SetActive(false);
            CollectPoint.transform.GetChild(boxCount - 1).gameObject.SetActive(true);
        }
        if (other.gameObject.CompareTag("SecondPart"))
        {
            for (int i = 0; i < LastGreens.Length; i++)
            {
                LastGreens[i].SetActive(true);
            }
        }
        if (other.gameObject.CompareTag("Green2"))
        {
            secondCount++;
            other.gameObject.SetActive(false);
            CollectPoint.transform.GetChild(secondCount - 1).gameObject.SetActive(true);
        }
        if (other.gameObject.CompareTag("End"))
        {
            _animator.SetBool("winning", true);
            npc1.GetComponent<NavMeshAgent>().enabled = false;
            npc2.GetComponent<NavMeshAgent>().enabled = false;
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("BridgeBox") && other.gameObject.GetComponent<MeshRenderer>().sharedMaterial != _material)
        {
            RespawnBox();
            if (boxCount>0)
            {
                other.gameObject.GetComponent<MeshRenderer>().enabled = true;
                other.gameObject.GetComponent<MeshRenderer>().sharedMaterial = _material;
                boxCount--;
                CollectPoint.transform.GetChild(boxCount).gameObject.SetActive(false);
            }
            else
            {
                other.gameObject.GetComponent<BoxCollider>().isTrigger = true;
                Char_Move.free = false;
            }
        }
        if (other.gameObject.CompareTag("LastBridgeBox") && other.gameObject.GetComponent<MeshRenderer>().sharedMaterial != _material)
        {
            RespawnBox2();
            if (secondCount>0)
            {
                other.gameObject.GetComponent<MeshRenderer>().enabled = true;
                other.gameObject.GetComponent<MeshRenderer>().sharedMaterial = _material;
                secondCount--;
                CollectPoint.transform.GetChild(secondCount).gameObject.SetActive(false);
            }
            else
            {
                other.gameObject.GetComponent<BoxCollider>().isTrigger = true;
                Char_Move.free = false;
            }
        }
    }
    private void RespawnBox()
    {
        for (int i = 0; i < Greens.Length; i++)
        {
            Greens[i].SetActive(true);
        }
    }
    private void RespawnBox2()
    {
        for (int i = 0; i < LastGreens.Length; i++)
        {
            LastGreens[i].SetActive(true);
        }
    }
}