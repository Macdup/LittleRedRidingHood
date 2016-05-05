using UnityEngine;
using System.Collections;

public class boar : MonoBehaviour {
    Animator _anim;
    int shotHash = Animator.StringToHash("shot");

    public float speed;
    public GameObject pointA;
    public GameObject pointB;
    
    private Vector3 A;
    private Vector3 B;
    private Vector3 target;
    private bool isFacingRight;

    // Use this for initialization
    void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        A = pointA.transform.position;
        B = pointB.transform.position;
        target = A;
        transform.localScale = (new Vector3(1, 1, 1));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (transform.position == Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime))
            changeDirection();
    }

    void hit()
    {

    }

    void changeDirection() {
        if (target == A)
           {
              target = B;
              transform.localScale = (new Vector3(-1,1,1));
           }
        else
            {
                target = A;
                transform.localScale = (new Vector3(1, 1, 1));
            }
    }


    void death()
    {

    }

}
