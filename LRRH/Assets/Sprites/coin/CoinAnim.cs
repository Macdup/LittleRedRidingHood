using UnityEngine;
using System.Collections;

public class CoinAnim : MonoBehaviour {

    // public member
    // protected member
    // private member
    // variable
    private Animator _anim;



    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void destroy()
    {
        GameObject.Destroy(transform.parent.gameObject);
    }
}
