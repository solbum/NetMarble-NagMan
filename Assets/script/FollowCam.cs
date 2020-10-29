using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{ 
	public GameObject player,
				backGround;

	public Vector2 velocity;
	public Vector2 minPos, maxPos;

	public bool bound;
	public float smoothTimeX, smoothTimeY;

	// 캐릭터 초기화

	// 캐릭터의 위에 따라 카메라가 이동하도록 하는 메서드
	void FixedUpdate()
	{
		float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
		// Mathf.SmoothDamp는 천천히 값을 증가시키는 메서드이다.

		float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);
		// 카메라로 이동


		transform.position = new Vector3(posX, posY, transform.position.z);
	}
}