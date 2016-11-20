using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public GameObject target;
	public float yOffset = 0;
    public float xOffset = 0;
    public float DistanceMax = 170;

    private float shakeDuration = 0;
    private float shakeIntensity = 0;
    private float _lockZ = 0;
	private float _targetDirection;
	private float _targetLastDirection;
	private float _targetLastPosition;
	private float _targetPositionDelta;
	private float _targetDistanceAfterDirChange;

    // Use this for initialization
    void Start () {
        _lockZ = transform.position.z;
		_targetDirection = target.transform.localScale.x;
		_targetLastDirection = target.transform.localScale.x;
		_targetLastPosition = target.transform.position.x;
		_targetDistanceAfterDirChange = 0;
		Vector3 pos = this.transform.position;
		pos.x = target.transform.position.x + (xOffset * target.transform.localScale.x);
		pos.y = target.transform.position.y + yOffset;
		this.transform.position = pos;
    }
	
	// Update is called once per frame
	void FixedUpdate() {
		if(target != null)
        {
			_targetPositionDelta = target.transform.position.x - _targetLastPosition;
			_targetLastPosition = target.transform.position.x;
			//détecter un changement
			_targetDirection = target.transform.localScale.x;
			if (_targetDirection != _targetLastDirection) {
				_targetLastDirection = _targetDirection;
				_targetDistanceAfterDirChange = 0;
			} else {
				_targetDistanceAfterDirChange += _targetPositionDelta;
			}

			Vector3 pos = this.transform.position;

			pos.x = target.transform.position.x + (xOffset * target.transform.localScale.x);
			pos.y = target.transform.position.y + yOffset;
			this.transform.position =  Vector3.Lerp(this.transform.position,pos,0.05f);
				
            // make sure the object is always visible

			/*Vector2 dir = new Vector2(target.transform.position.x, target.transform.position.y) - new Vector2(this.transform.position.x, this.transform.position.y);
            float dist = dir.magnitude;
            if (dir.magnitude > DistanceMax)
            {
                dir.Normalize();
                Vector2 adjust = dir * (dist - DistanceMax);
                pos = this.transform.position;
                pos.x += adjust.x;
                pos.y += adjust.y;
                this.transform.position = pos;
            }*/

        }

        shake();

        // Lock Z
        Vector3 posZLock = this.transform.position;
        posZLock.z = _lockZ;
        transform.position = posZLock;
    }

    public void shake()
    {
        if (shakeDuration > 0)
        {
            Vector3 shakePos = Random.insideUnitCircle * shakeIntensity * shakeDuration;
            shakePos.z = -1f;
            transform.GetComponent<RectTransform>().position += shakePos;
            shakeDuration -= Time.deltaTime * 1;
        }
    }

    public void setShake(float _shakeDuration, float _shakeIntensity) {
        shakeDuration = _shakeDuration;
        shakeIntensity = _shakeIntensity;
    }
}
