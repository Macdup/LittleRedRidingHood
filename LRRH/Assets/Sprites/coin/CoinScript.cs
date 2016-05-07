using UnityEngine;
using System.Collections;


public class CoinScript : MonoBehaviour {

    // public member
    [HideInInspector]
    public CoinAnim CoinAnim;

    void Start()
    {
        CoinAnim = this.GetComponentInChildren<CoinAnim>();
    }

    // Update is called once per frame
    void Update()
    {

    }


}
