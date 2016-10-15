using UnityEngine;
using System.Collections;

public class BallSpawner : MonoBehaviour {

    public GameObject Ball;
    public float SpawnDelay;

    private float _timeFromLastSpawn=0.0f;
    private float _deltaTime = 0.0f;

    void Start () {
        SpawnBall();
	}
	
	// Update is called once per frame
	void Update () {
        _timeFromLastSpawn += Time.deltaTime;
        if(_timeFromLastSpawn >= SpawnDelay)
        {
            SpawnBall();
            _timeFromLastSpawn = 0.0f;
        }
    }

    void SpawnBall()
    {
        GameObject ball = Object.Instantiate(Ball);
        ball.transform.parent = this.transform;
        ball.transform.position = this.transform.position;
    }
}
