using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerController : MonoBehaviour
{
    // SerializeField : private 변수를 에디터 창에서도 보이게 해주는 코드
    // 이는 에디터 상에서는 사용하고 싶지만 다른 클래스에서는 사용하고 싶지 않은 변수를 정의할 때 사용합니다.
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float turnSpeed = 25.0f;
    private float horizontalInput;
    private float forwardInput;

    // Update is called once per frame
    // FixedUpdate 는 그냥 Update 의 경우에는 카메라 움직임에서 발생하는 문제를 해결하기에 유용합니다.
    // 예를 들어 움직임에 따라 카메라가 따라오는 속도에 차이가 생기면 버벅이면서 움직이는 듯한 이질감이 들게 됩니다.
    // 그런데 이 FixedUpdate 를 사용하면 Update 가 호출되기 전에 호출되므로 게임 플레이 시 발생하는 물리를 계산하려고 할 때 작동하게 됩니다.
    void FixedUpdate()
    {
        // This is where once per frame
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        // We Move the vehicle forward
        //transform.Translate(0, 0, 1);
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);

        // We turn the vehicle
        transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * horizontalInput);
    }
}
