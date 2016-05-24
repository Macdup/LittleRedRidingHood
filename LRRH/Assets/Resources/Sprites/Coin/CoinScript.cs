using UnityEngine;
using System.Collections;


public class CoinScript : MonoBehaviour {

    // public member
    [HideInInspector]
    public CoinAnim CoinAnim;
    public GameObject LootText;

    //private member
    private Rigidbody2D Rigidbody2D;


    void Start()
    {
        CoinAnim = this.GetComponentInChildren<CoinAnim>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
		Rigidbody2D.AddForce(new Vector2(Random.Range(-150,150),Random.Range(50,350)),ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PopLootText(Vector3 lootPos) {
        GameObject instance = (GameObject)Instantiate(LootText, lootPos, transform.rotation);
    }


}
