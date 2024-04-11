using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Scripting.APIUpdating;

public class PlayerController : MonoBehaviour
{
    // SerializeField : private 변수를 에디터 창에서도 보이게 해주는 코드
    // 이는 에디터 상에서는 사용하고 싶지만 다른 클래스에서는 사용하고 싶지 않은 변수를 정의할 때 사용합니다.
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float horsePower = 0;      // 미는 힘으로 자동차의 마력에 해당하는 정보입니다.
    [SerializeField] private float rpm;
    private const float turnSpeed = 45.0f;
    private float horizontalInput;
    private float verticalInput;
    private Rigidbody playerRb;
    [SerializeField] GameObject centerOfMass;           // 중심점 관련 변수입니다.
    [SerializeField] TextMeshProUGUI speedmoterText;    // 현재 속도계 변수
    [SerializeField] TextMeshProUGUI rpmText;           // RPM 변수
    [SerializeField] List<WheelCollider> allWheels;
    [SerializeField] int wheelsOnGround;


    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRb.centerOfMass = centerOfMass.transform.position;
    }

    // Update is called once per frame
    // FixedUpdate 는 그냥 Update 의 경우에는 카메라 움직임에서 발생하는 문제를 해결하기에 유용합니다.
    // 예를 들어 움직임에 따라 카메라가 따라오는 속도에 차이가 생기면 버벅이면서 움직이는 듯한 이질감이 들게 됩니다.
    // 그런데 이 FixedUpdate 를 사용하면 Update 가 호출되기 전에 호출되므로 게임 플레이 시 발생하는 물리를 계산하려고 할 때 작동하게 됩니다.
    void FixedUpdate()
    {
        // This is where once per frame
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // We Move the vehicle forward
        //transform.Translate(0, 0, 1);
        //transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        // 기존의 AddForce으로는 회전 방향에 대한 물리 값이 제대로 적용되지 않습니다.
        // 이는 오브젝트의 회전방향에 맞춰서 물리력을 주는것이 아닌 World 방향 기준으로 적용되는 것이 문제입니다.
        // 따라서 이를 Local 방향에 맞게 줌으로써 해결이 가능한데 여기서 사용한 Relative 의 경우에는 정확히는 로컬이 아니라
        // 요소 자기 자신의 원래 위치(static일 때의 위치)를 기준으로 배치한다는 의미로 즉 자기 자신을 기준으로 회전값을 정하게 됩니다.
        if (IsOnGround())
        {
            playerRb.AddRelativeForce(Vector3.forward * horsePower * verticalInput);

            // We turn the vehicle
            transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * horizontalInput);

            speed = Mathf.RoundToInt(playerRb.velocity.magnitude * 2.237f); // For kph, change 2.237 to 3.6
            speedmoterText.SetText("Speed : " + speed + "mph");

            rpm = (speed % 30) * 40;
            rpmText.SetText("RPM : " + rpm);
        }

    }

    bool IsOnGround()
    {
        wheelsOnGround = 0;
        foreach (WheelCollider wheel in allWheels)
        {
            if (wheel.isGrounded)
            {
                wheelsOnGround++;
            }
        }

        if (wheelsOnGround == 4)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
