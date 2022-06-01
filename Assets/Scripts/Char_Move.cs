using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char_Move : MonoBehaviour
{
    private Camera cam;
    private Animator animator;
    public static bool free;
    [SerializeField] private float turnspeed, speed;
    void Start()
    {
        free = true;
        cam = Camera.main;
        animator = GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        if (Input.GetMouseButton(0)&&free)
        {
            Movement();
        }
        else
        {
            if (animator.GetBool("running"))
            {
                animator.SetBool("running", false);
            }
        }
        if (free==false)
        {
            Gravity();
        }
    }
    private void Movement()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = cam.transform.localPosition.z;
        Ray ray = cam.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit,Mathf.Infinity))
        {
            Vector3 hitVec = hit.point;
            hitVec.y = transform.position.y;
            transform.position = Vector3.MoveTowards(transform.position, hitVec, Time.deltaTime * speed);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(hitVec - transform.position), turnspeed * Time.deltaTime);
            if (!animator.GetBool("running"))
            {
                animator.SetBool("running", true);
            }
        }
    }
    void Gravity()
    {
        transform.Translate(new Vector3(0, -0.5f, 0));
    }
}