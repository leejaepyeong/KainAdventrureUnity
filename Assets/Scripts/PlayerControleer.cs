using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControleer : MonoBehaviour
{
    [SerializeField]    //인스펙터창에서  수정 가능하게(private유지)
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;

    float applySpeed; // 적용 속도


    //스틱기준
    public Transform Stick;         // stick

    // ??????
    private Vector3 StickFirstPos;  // 처음 위치 스틱
    private Vector3 JoyVec;         // 벡터
    private float Radius;           // 반지름
    private bool MoveFlag;          // 움직임


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
    Vector3 offset = new Vector3(-0.197f,2.215f,-3.116f);


    // 컴포넌트 가져오기
    private Rigidbody playerRigid;
    private CapsuleCollider capsulCollider;
    public Animator anim;
    public Camera[] theCamera;
    public Weapon equipWeapon; 

    // Start is called before the first frame update
    void Start()
    {
        capsulCollider = GetComponent<CapsuleCollider>();
        playerRigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        applySpeed = walkSpeed;
        equipWeapon = weapons[0].GetComponent<Weapon>();


        // 3시점용
        Radius = Stick.GetComponent<RectTransform>().sizeDelta.y * 0.5f;
        StickFirstPos = Stick.transform.position;

        // 부모 트랜스폼 값
        float Can = Stick.transform.parent.GetComponent<RectTransform>().localScale.x;
        Radius *= Can;  // 반지름을 부모 크기에 맞

        MoveFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Move();
        Run();
        if(GameManager.instance.is1stCam)
        {
            tryJump();
            tryAttack();
        }
        
        tryEquipChange();

        if(GameManager.instance.is1stCam)
        {
            CameraRotation();
            CharacterRotation();
        }
        

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

        if(GameManager.instance.is1stCam) // GameManager.instance.is1stCam
        {

            float _moveDirX = Input.GetAxisRaw("Horizontal");   //좌우 값
            float _moveDirZ = Input.GetAxisRaw("Vertical");     //앞뒤 값

            if (_moveDirX != 0 || _moveDirZ != 0)
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
        }
        else
        {
            if (MoveFlag)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * applySpeed);
                isMove = true;
            }
            else
            {
                isMove = false;
            }
        }

        theCamera[1].transform.position = transform.position + offset;

        if (!isRun)
            anim.SetBool("Walk", isMove);
      
    }


    public void Drag(BaseEventData _Data)
    {
        if(!GameManager.instance.is1stCam)
        {
            MoveFlag = true;
            PointerEventData Data = _Data as PointerEventData;
            Vector3 Pos = Data.position;


            JoyVec = (Pos - StickFirstPos).normalized;


            float Dis = Vector3.Distance(Pos, StickFirstPos);


            if (Dis < Radius)
                Stick.position = StickFirstPos + JoyVec * Dis;

            else
                Stick.position = StickFirstPos + JoyVec * Radius;

            transform.eulerAngles = new Vector3(0, Mathf.Atan2(JoyVec.x, JoyVec.y) * Mathf.Rad2Deg, 0);
        }
    }

    // 드래그 종료 > 원위치
    public void DragEnd()
    {
        if(!GameManager.instance.is1stCam)
        {
            Stick.position = StickFirstPos; // 처음 위ㅊ
            JoyVec = Vector3.zero;
            MoveFlag = false;
        }
       
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

    void tryJump()
    {
        if (isJump)
        {
            Jump();
        }
    }

    // 점프
    public void Jump()
    {
        if(isGround)
        {
            playerRigid.velocity = transform.up * jumpForce;
            isGround = false;
            anim.SetBool("Jump", true);
        }
     
    }

    void tryAttack()
    {
        if (isAttack)
        {
            Attack();
        }
    }

    // 공격
    public void Attack()
    {
        if(isGround)
        {
            equipWeapon.Use();
        }
        
        
    }

    void tryEquipChange()
    {
        if (isChange && !isJump && !equipWeapon.isAttack)
        {
            EquipWeaponChange();
        }
    }

    // 무기 교체
    public void EquipWeaponChange()
    {
            GameManager.instance.weaponImage[weaponIndex].gameObject.SetActive(false);

            if (equipWeapon != null)
                equipWeapon.gameObject.SetActive(false);    // 일단 전부 비활성화

            weaponIndex++;  // 다음 무기 인덱스
            if (weaponIndex >= 4) weaponIndex = 0;  // 넘어가면 0으로 초기화

            equipWeapon = weapons[weaponIndex].GetComponent<Weapon>();
            equipWeapon.gameObject.SetActive(true);
            GameManager.instance.weaponImage[weaponIndex].gameObject.SetActive(true);

            //anim.SetTrigger("doSwap");

            isSwap = true;

            Invoke("SwapOut", 0.4f);
        
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

        theCamera[0].transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);

    }




    void Dead()
    {
        anim.SetTrigger("Die");
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
