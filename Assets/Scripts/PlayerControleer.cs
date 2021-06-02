using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControleer : MonoBehaviour
{
    [SerializeField]    //인스펙터창에서  수정 가능하게(private유지)
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;

    float applySpeed; // 적용 속도

    // 점프 힘
    [SerializeField]
    private float jumpForce;

    // 상태확인
    bool isMove = false;
    bool isRun = false;
    bool isJump = false;
    bool isAttack = false;
    bool isGround = true;
    bool isChange = false;
    bool isSwap;

    //무기

    public GameObject[] weapons;
    //public bool[] hasWeapons;
    private int weaponIndex = 0;


    //민감도
    [SerializeField]
    private float lookSensitivity;  // 카메라 감도

    [SerializeField]
    private float cameraRotationLimit;  // 카메라 각도 
    private float currentCameraRotationX = 0;   // 현재 보는 각도


    // 컴포넌트 가져오기
    private Rigidbody playerRigid;
    private CapsuleCollider capsulCollider;
    public Animator anim;
    public Camera theCamera;
    public Weapon equipWeapon; 

    // Start is called before the first frame update
    void Start()
    {
        capsulCollider = GetComponent<CapsuleCollider>();
        playerRigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        applySpeed = walkSpeed;
        equipWeapon = weapons[0].GetComponent<Weapon>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Move();
        Run();
        Jump();
        Attack();
        EquipWeaponChange();
        CameraRotation();
        CharacterRotation();

    }

    void GetInput()
    {
        isJump = Input.GetButtonDown("Jump");
        isAttack = Input.GetButtonDown("Fire1");
        isRun = Input.GetKey(KeyCode.LeftShift);
        isChange = Input.GetKeyDown(KeyCode.E);
    }

    void Move()
    {
        float _moveDirX = Input.GetAxisRaw("Horizontal");   //좌우 값
        float _moveDirZ = Input.GetAxisRaw("Vertical");     //앞뒤 값

        if(_moveDirX != 0 || _moveDirZ != 0)
        {
            isMove = true;

            Vector3 _moveHorizontal = transform.right * _moveDirX;  // 좌우 이동
            Vector3 _moveVertical = transform.forward * _moveDirZ;  // 앞뒤 이동

            Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * applySpeed;
            // 크기 1의 방향 설정 * 스피드 =  속도

            playerRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
            // 오브젝트 이동

        }
        else
        {
            isMove = false;
        }

        if (!isRun)
            anim.SetBool("Walk", isMove);
      

    }

    //달리기
   void Run()
    {
        anim.SetBool("Run", isRun && isMove);

        if (isRun)
        {
           
            applySpeed = runSpeed;
        }
        else
        {
           
            applySpeed = walkSpeed;
        }

        

    }

    // 점프
    void Jump()
    {
        if(isJump && isGround)
        {
            playerRigid.velocity = transform.up * jumpForce;
            isGround = false;
            anim.SetBool("Jump", true);
        }
    }

    // 공격
    void Attack()
    {
        
        if(isAttack && isGround)
        {
            equipWeapon.Use();
        }
    }

    // 무기 교체
    void EquipWeaponChange()
    {
        if (isChange && !isJump && !equipWeapon.isAttack)
        {
            if (equipWeapon != null)
                equipWeapon.gameObject.SetActive(false);    // 일단 전부 비활성화

            weaponIndex++;  // 다음 무기 인덱스
            if (weaponIndex >= 4) weaponIndex = 0;  // 넘어가면 0으로 초기화

            equipWeapon = weapons[weaponIndex].GetComponent<Weapon>();
            equipWeapon.gameObject.SetActive(true);

            //anim.SetTrigger("doSwap");

            isSwap = true;

            Invoke("SwapOut", 0.4f);
        }
    }
    void SwapOut()
    {
        isSwap = false;
    }

    void CharacterRotation()
    {
        //좌우 캐릭터 회전
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;

        playerRigid.MoveRotation(playerRigid.rotation * Quaternion.Euler(_characterRotationY));
        // 오일러 회전값을  현상태 회전값에 곱
    }

    void CameraRotation()
    {
        // 상하 카메라 회전

        float _xRotation = Input.GetAxisRaw("Mouse Y"); // 마우스 각도 회전
        float _cameraRotationX = _xRotation * lookSensitivity;  //감도에 따른 카메라 회전값


        currentCameraRotationX -= _cameraRotationX; //현 상태에서 회전값 더하기
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);
        // 최소  최대  카메라 회전값 설정

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);

    }

    


    // 충돌 여부
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGround = true;
            anim.SetBool("Jump", false);
        }
    }


    
}
