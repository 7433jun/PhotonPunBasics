using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]

public class PhotonPlayer : MonoBehaviourPun
{
	public float animSpeed = 1.5f;              // 애니메이션 재생 속도 설정

	// 이하 캐릭터 컨트롤러용 파라미터
	// 전진 속도
	public float forwardSpeed = 7.0f;
	// 후진 속도
	public float backwardSpeed = 2.0f;
	// 회전 속도
	public float rotateSpeed = 2.0f;
	// 점프 힘
	public float jumpPower = 3.0f;
	// CapsuleCollider에서 설정된 콜라이더의 Height, Center의 초기값을 저장하는 변수
	private Animator anim;                          // 캐릭터에 부착된 애니메이터 참조

	private GameObject cameraObject;    // 메인 카메라 참조
	private Vector3 cameraForward;
	private Vector3 cameraEuler;

	private CinemachineFreeLook cinemachineFreeLook;


	// 초기화
	void Start()
	{
		if (!photonView.IsMine) return;

		// Animator 컴포넌트를 가져옴
		anim = GetComponent<Animator>();
		// 메인 카메라를 가져옴
		cameraObject = GameObject.FindWithTag("MainCamera");


		cinemachineFreeLook = GameObject.Find("FreeLook Camera").GetComponent<CinemachineFreeLook>();

		cinemachineFreeLook.Follow = transform;
		cinemachineFreeLook.LookAt = transform;
	}


	void Update()
	{
		if (!photonView.IsMine) return;

		float h = Input.GetAxis("Horizontal");              // 입력 장치의 수평 축을 h로 정의
		float v = Input.GetAxis("Vertical");                // 입력 장치의 수직 축을 v로 정의
		anim.SetFloat("Speed", v);                          // Animator 측에서 설정한 "Speed" 파라미터에 v를 전달
		anim.SetFloat("Direction", h);                      // Animator 측에서 설정한 "Direction" 파라미터에 h를 전달
		anim.speed = animSpeed;                             // Animator의 모션 재생 속도에 animSpeed를 설정

		if (v > 0.1)
		{
			v *= forwardSpeed;
		}
		else if (v < -0.1)
		{
			v *= backwardSpeed;
		}

		cameraForward = cameraObject.transform.forward;
		transform.localPosition += new Vector3(cameraForward.x, 0, cameraForward.z).normalized * v * Time.deltaTime;

		cameraEuler = cameraObject.transform.rotation.eulerAngles;
		Vector3 characterEuler = transform.rotation.eulerAngles;
		characterEuler.y = cameraEuler.y;
		transform.rotation = Quaternion.Euler(characterEuler);
	}
}