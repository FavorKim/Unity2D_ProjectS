using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    #region Variable

    [Space(20)]
    [SerializeField] protected LayerMask spinLayer;

    #region MonoBehaviour
    protected GameObject enemy;
    public GameObject Enemy { get; set; }

    [SerializeField] protected CinemachineVirtualCamera cam;

    [Space(10)]
    [Header("Anchor")]
    [SerializeField] protected Anchor anchor;
    [SerializeField] protected Transform ancPos;

    [Space(10)]
    [Header("Line")]
    [SerializeField] protected LineRenderer aimLine;
    [SerializeField] protected LineRenderer hookLine;

    [Space(10)]
    [Header("Manger")]
    [SerializeField] BossManager bM;
    [SerializeField] HPManager hp;

    [Space(10)]
    [Header("Arm")]
    [SerializeField] protected Animator arm;
    [SerializeField] protected SpriteRenderer armSr;

    protected SpriteRenderer sR;
    protected Animator anim;
    protected Rigidbody2D rb;
    protected CapsuleCollider2D col;



    [Space(20)]
    [SerializeField] protected float moveForce = 45.0f;
    [SerializeField] protected float jumpForce = 900.0f;
    [SerializeField] public float MaxSpeed = 9.0f;
    [SerializeField] public float targetMaxSpeed = 9.0f;
    [SerializeField] protected float climbSpeed = 10.0f;
    [SerializeField] protected float skyClimbSpeed = 10.0f;
    [SerializeField] protected float targetGravity = 4.0f;
    [SerializeField] protected float rewindDist = 0.5f;
    [SerializeField] protected float rewindSpeed = 1.5f;
    [SerializeField] protected float dashForce = 1.5f;
    [SerializeField] protected float excuteMove = 0.3f;
    [Header("About Dash")]
    [SerializeField] protected float swingForce = 40.0f;
    [SerializeField] protected float swingCooldown = 3.0f;
    [SerializeField] protected float damagedDash = 5.0f;
    protected float swingDashCool { get; set; }

    [Space(20)]
    [Header("About SpinAttack")]
    [SerializeField] float spinSpeed = 10.0f;

    public float GetTargetGrav() { return targetGravity; }

    protected Vector2 moveVal;
    protected Vector2 climbVal;

    public int JumpCount { get; set; }


    public void ResetDashCool() { swingDashCool = swingCooldown; }
    public bool CanDash() { return swingDashCool == swingCooldown; }
    public bool IsAttacking { get; set; }
    public bool IsSpinning { get; set; }
    public SpriteRenderer GetSR() { return sR; }
    private PlayerState state;
    public PlayerState GetState() { return state; }

    protected int MaxHpCount = 4;
    protected int CurHpCount = 4;
    public int GetCurHp { get { return CurHpCount; } }

    float curSize;
    public bool isFreeze { get; set; }

    bool isInvincible = false;
    #endregion

    #region Vector2
    protected Vector2 moveVal;
    protected Vector2 climbVal;
    #endregion
    #region float
    protected float moveForce = 45.0f;
    protected float jumpForce = 1300.0f;
    [HideInInspector] public float MaxSpeed = 9.0f;
    [HideInInspector] public float targetMaxSpeed = 9.0f;
    protected float climbSpeed = 10.0f;
    protected float skyClimbSpeed = 10.0f;
    protected float targetGravity = 4.0f;
    protected float rewindDist = 1f;
    protected float rewindSpeed = 0.5f;
    protected float dashForce = 1.5f;
    protected float excuteMove = 4f;
    protected float swingForce = 45.0f;
    protected float swingCooldown = 4.0f;
    protected float damagedDash = 5.0f;
    protected float swingDashCool { get; set; }
    float spinSpeed = 30.0f;
    float curSize;
    #endregion
    #region int
    public int JumpCount { get; set; }
    protected int MaxHpCount = 4;
    protected int CurHpCount = 4;
    #endregion
    #region bool
    public bool IsAttacking { get; set; }
    public bool IsSpinning { get; set; }
    public bool isFreeze { get; set; }
    bool isInvincible = false;
    #endregion
    #region getter
    public SpriteRenderer GetSR() { return sR; }
    public PlayerState GetState() { return state; }
    public Animator GetArm() { return arm; }
    public Animator GetAnimator() { return anim; }
    public float GetTargetGrav() { return targetGravity; }
    public int GetCurHp { get { return CurHpCount; } }
    #endregion
    #endregion
    
    #region Methods
    #region State
    private PlayerState state;
    public void StateStaying(PlayerState state) { this.state = state; }
    public void SetState(PlayerState state)
    {
        this.state.Exit();
        this.state = state;
        state.Enter();
    }
    #endregion

    void SetAnim()
    {
        if (rb.velocity.y < -20f)
            anim.SetBool("isFall", true);
        else
            anim.SetBool("isFall", false);

        if (rb.velocity.y > 0.1f)
            anim.SetBool("isJump", true);
        else
            anim.SetBool("isJump", false);

        if (moveVal.x != 0)
        {
            anim.SetBool("isRun", true);
            arm.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRun", false);
            arm.SetBool("isRunning", false);
        }

        if (moveVal.x < 0)
        {
            if (GetState() == BossAttackState.Instance || GetState() == QTEState.Instance) return;
            armSr.flipX = true;
            sR.flipX = true;
        }
        if (moveVal.x > 0)
        {
            if (GetState() == BossAttackState.Instance || GetState() == QTEState.Instance) return;
            armSr.flipX = false;
            sR.flipX = false;
        }




    }
    public void Recover()
    {
        CurHpCount++;
    }

    public void ResetDashCool() { swingDashCool = swingCooldown; }
    public bool CanDash() { return swingDashCool == swingCooldown; }

    #endregion
    
    #region LifeCycle
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        state = new PlayerState(this);
        col = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        sR = GetComponent<SpriteRenderer>();
        IsAttacking = false;
        IsSpinning = false;
        curSize = cam.m_Lens.OrthographicSize;


        StartCoroutine(DashCool());

        UnityEngine.Debug.Log(GameManager.Instance.difficulty);
    }
    private void FixedUpdate()
    {
        state.Move();
    }
    private void Update()
    {
        SetAnim();
    }
    #endregion

    #region PlayerInput
    void OnMove(InputValue val)
    {
        moveVal = val.Get<Vector2>();
        if (state == NormalState.Instance && !val.isPressed)
            rb.velocity = new Vector2(0, rb.velocity.y);
    }
    void OnJump(InputValue val)
    {
        if (val.isPressed)
            state.Jump();
    }
    void OnSkill(InputValue val)
    {
        if (val.isPressed)
            state.Skill();
    }
    void OnClimb(InputValue val)
    {
        climbVal = val.Get<Vector2>().normalized;
    }
    #endregion

    #region Event
    public void OnDamaged()
    {
        if (CurHpCount == 1 || GameManager.Instance.difficulty == "legend")
        {
            OnDead();
            return;
        }
        StartCoroutine(InvincibleTime());
        SetState(AirState.Instance);
        rb.velocity = Vector2.zero;
        if (GameManager.Instance.difficulty != "easy")
        {
            CurHpCount--;
            hp.OnDamaged();
        }
        Vector2 dir = new Vector2(-1, 1);
        rb.AddForce(dir * damagedDash * Time.deltaTime, ForceMode2D.Impulse);
    }
    public void OnDead()
    {
        GameManager.Instance.deadScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Dead");
    }
    public void OnBossATKEnd()
    {
        SetState(NormalState.Instance);
    }
    #endregion

    #region Coroutine
    private IEnumerator DashCool()
    {
        while (true)
        {
            yield return new WaitForSeconds(swingCooldown);
            swingDashCool = swingCooldown;
        }
    }
    private IEnumerator InvincibleTime()
    {
        isInvincible = true;
        yield return new WaitForSeconds(2f);
        isInvincible = false;
        StopCoroutine(InvincibleTime());
    }
    #endregion

    #region PlayerState
    public class PlayerState : IPlayerState
    {
        #region vars
        protected static PlayerController player;
        protected float moveForce => player.moveForce;
        protected float jumpForce => player.jumpForce;
        protected float swingForce => player.swingForce;
        protected float dashCoolDown => player.swingCooldown;
        protected float curDashCool => player.swingDashCool;
        protected float climbSpeed => player.climbSpeed;
        protected float rewindDist => player.rewindDist;
        protected float targetGravity => player.targetGravity;
        protected Vector2 moveVal => player.moveVal;
        protected Vector2 climbVal => player.climbVal;
        protected LineRenderer aimLine => player.aimLine;
        protected LineRenderer hookLine => player.hookLine;
        protected Transform ancPos => player.ancPos;
        protected Rigidbody2D rb => player.rb;
        protected LayerMask spinLayer => player.spinLayer;
        protected float spinSpeed => player.spinSpeed;
        protected CapsuleCollider2D col => player.col;
        protected Anchor anchor => player.anchor;
        protected float dashForce => player.dashForce;
        protected Animator anim => player.anim;
        protected bool isSkilled = false;
        protected float excuteMove => player.excuteMove;
        protected float rewindSpeed => player.rewindSpeed;
        protected SpriteRenderer armSr => player.armSr;
        protected CinemachineVirtualCamera cam => player.cam;
        protected BossManager bM => player.bM;
        #endregion

        public PlayerState(PlayerController player)
        {
            PlayerState.player = player;
        }

        public virtual void Move()
        {
            if (player.isFreeze == true) return;
            rb.AddForce(moveVal * moveForce * Time.deltaTime, ForceMode2D.Impulse);

            if (rb.velocity.x > player.MaxSpeed)
                rb.velocity = new Vector2(player.MaxSpeed, rb.velocity.y);
            if (rb.velocity.x < player.MaxSpeed * -1)
                rb.velocity = new Vector2(player.MaxSpeed * -1, rb.velocity.y);
        }
        public virtual void Jump()
        {
            if (player.isFreeze == true) return;

            if (player.JumpCount < 1)
            {
                rb.AddForce(new Vector2(rb.velocity.x, jumpForce * Time.deltaTime), ForceMode2D.Impulse);
                VFXManager.Instance.PlayVFX("VFX_Jump");
                player.JumpCount++;
            }
        }
        public virtual void Skill()
        {
            if (player.isFreeze == true) return;
        }

        public virtual void Enter() { }
        public virtual void Exit() { }

        public void CheckAttach(RaycastHit2D hit)
        {
            if (hit.transform.gameObject == null) return;
            if (player.GetState() == QTEState.Instance) return;

            anim.SetBool("isAttach", true);
            if (hit.collider.CompareTag("Monster") || hit.collider.CompareTag("FlyingMonster"))
            {
                player.enemy = hit.collider.gameObject;
                player.SetState(MonAttachState.Instance);
            }
            else if (hit.collider.CompareTag("Boss"))
            {
                player.enemy = hit.collider.gameObject;
                player.SetState(BossAttackState.Instance);
            }
            else
                player.SetState(AttachState.Instance);
        }
    }

    public class BossAttackState : PlayerState
    {
        public BossAttackState(PlayerController player) : base(player) { }
        private static BossAttackState instance;
        public static BossAttackState Instance
        {
            get
            {
                if (instance == null)
                    instance = new BossAttackState(player);
                return instance;
            }
        }
        KangSeonController KS;
        bool isQTE = false;

        void BossAttack()
        {
            KS = player.enemy.GetComponent<KangSeonController>();
            KS.GetLaser().SetLaserOff();
            KS.GetLaser().GetLaser().SetActive(false);
            cam.m_Lens.OrthographicSize = 5f;

            if (bM.GetWave() % 3 == 0 && bM.GetWave() != 0)
            {
                isQTE = true;
                player.transform.position = new Vector2(KS.gameObject.transform.position.x, KS.gameObject.transform.position.y);
                player.SetState(QTEState.Instance);
                KS.EnterQTE();
                isQTE = false;
                return;
            }

            player.GetAnimator().SetBool("Boss", true);



            if (bM.GetWave() > 0)
            {
                player.GetSR().flipX = true;
                player.transform.position = new Vector2(KS.gameObject.transform.position.x + 2f, KS.gameObject.transform.position.y);
                player.GetAnimator().Play("SNB_KS_Attack");
                KS.GetAnimator().Play("KangSeon_Attacked");
            }
            else
            {
                //player.transform = new Vector2()
                player.GetSR().flipX = false;

                player.transform.position = new Vector2(KS.gameObject.transform.position.x + 2f, KS.gameObject.transform.position.y);

                player.GetAnimator().Play("SNB_KS_Excute");
                KS.GetAnimator().Play("KangSeon_Excute");

            }
        }

        public override void Enter()
        {
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0f;
            col.isTrigger = true;
            armSr.enabled = false;
            hookLine.enabled = false;
            aimLine.enabled = false;
            BossAttack();
        }

        public override void Exit()
        {
            if (isQTE) return;

            col.isTrigger = false;
            rb.gravityScale = targetGravity;
            cam.m_Lens.OrthographicSize = player.curSize;
            anim.SetBool("Boss", false);
            armSr.enabled = true;
            hookLine.enabled = false;
            aimLine.enabled = true;
        }

        public override void Jump() { }
        public override void Move() { }
        public override void Skill() { }

    }
    public class QTEState : PlayerState
    {
        public QTEState(PlayerController player) : base(player) { }
        private static QTEState instance;
        public static QTEState Instance
        {
            get
            {
                if (instance == null)
                    instance = new QTEState(player);
                return instance;
            }
        }
        public override void Enter()
        {
            player.anim.Play("SNB_Clash_Start");
            player.GetSR().flipX = true;
            
            bM.EnterQTE();
        }
        public override void Exit()
        {
            col.isTrigger = false;
            rb.gravityScale = targetGravity;
            cam.m_Lens.OrthographicSize = player.curSize;
            anim.Play("Idle");
            //anim.SetTrigger("ClashEnd");
            armSr.enabled = true;
            armSr.flipX = false;
            player.GetSR().flipX = false;
            hookLine.enabled = false;
            aimLine.enabled = true;
        }


        public override void Move() { }
        public override void Jump() { }
        public override void Skill() { }
    }
    
    public class AttachState : PlayerState
    {
        public AttachState(PlayerController player) : base(player) { }

        private static AttachState instance;
        public static AttachState Instance
        {
            get
            {
                if (instance == null)
                    instance = new AttachState(player);
                return instance;
            }
        }
        public override void Move()
        {
            Vector2 newPos = ancPos.position - player.transform.position;
            float rotZ = Mathf.Atan2(newPos.y, newPos.x) * Mathf.Rad2Deg;

            player.transform.rotation = Quaternion.Euler(0, 0, rotZ - 90f);

            rb.AddForce(player.transform.TransformDirection(moveVal) * moveForce * 0.4f * Time.deltaTime, ForceMode2D.Impulse);

        }
        public override void Skill()
        {
            if (player.CanDash())
            {
                rb.gravityScale = 0f;
                isSkilled = true;
                player.swingDashCool = 0.0f;
                Vector2 dir;
                if (Input.GetKey(KeyCode.A)) dir = player.transform.TransformDirection(Vector2.left);
                else if (Input.GetKey(KeyCode.D)) dir = player.transform.TransformDirection(Vector2.right);
                else dir = Vector2.zero;
                Vector2 reverse = (ancPos.position - player.transform.position).normalized * -9.8f;
                Vector2 finalDir = dir + reverse;
                rb.velocity = Vector2.zero;
                rb.AddForce(finalDir * swingForce, ForceMode2D.Impulse);
                rb.gravityScale = targetGravity;

            }
        }
        public override void Jump()
        {
            float dist = Vector2.Distance(ancPos.position, player.transform.position) - rewindDist;
            player.transform.position = Vector2.MoveTowards(player.transform.position, ancPos.position, dist);
            //VFXManager.Instance.PlayVFX()
            anchor.GetJoint().autoConfigureDistance = true;
            //player.SetState(ClimbState.Instance);
        }
        public override void Enter()
        {
            player.MaxSpeed += 3.0f;
            player.GetArm().SetBool("isAttach", true);
            anim.SetTrigger("WireShoot");
            anchor.GetJoint().enabled = true;
            hookLine.enabled = true;
            aimLine.enabled = false;
        }
        public override void Exit()
        {

            if (isSkilled)
            {
                anim.Play("SNB_Rolling", 0, 1f);
            }
            anim.SetBool("isAttach", false);
            player.GetArm().SetBool("isAttach", false);
            anchor.GetJoint().enabled = false;
            hookLine.enabled = false;
            aimLine.enabled = true;
            isSkilled = false;
            player.transform.rotation = Quaternion.identity;
        }


    }
    public class MonAttachState : PlayerState
    {
        public MonAttachState(PlayerController player) : base(player) { }
        private static MonAttachState instance;
        public static MonAttachState Instance
        {
            get
            {
                if (instance == null)
                    instance = new MonAttachState(player);
                return instance;
            }
        }
        MonsterShoot mon;



        public override void Enter()
        {
            mon = player.enemy.gameObject.GetComponent<MonsterShoot>();

            if (mon.CompareTag("FlyingMonster"))
                rb.gravityScale = 0.1f;

            anim.SetTrigger("ExcuteEnter");
            player.GetArm().SetTrigger("Excute");
            player.GetArm().SetBool("isExcute", true);
            anim.SetBool("isAttach", true);
            armSr.enabled = false;

            player.transform.position = mon.transform.position;
            mon.SetAttach(true);

            hookLine.enabled = false;
            aimLine.enabled = false;

            anchor.GetJoint().enabled = false;

            rb.velocity = Vector2.zero;
        }
        public override void Exit()
        {
            anim.Play("SNB_Rolling", 0, 0.4f);
            VFXManager.Instance.PlayVFX(player.transform.position, "VFX_ExcuteEnd");
            //anim.SetTrigger("Exit");

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mouseDir = (mousePos - (Vector2)player.transform.position);
            rb.gravityScale = targetGravity;
            rb.AddForce(mouseDir * dashForce, ForceMode2D.Impulse);


            anim.SetBool("isAttach", false);
            player.GetArm().SetBool("isExcute", false);


            hookLine.enabled = false;
            aimLine.enabled = true;
            armSr.enabled = true;

            mon.transform.parent = null;
            mon.gameObject.SetActive(false);
            player.enemy = null;
        }

        public override void Move()
        {
            if (player.enemy.CompareTag("Boss")) return;

            if (mon.CompareTag("FlyingMonster"))
                player.transform.Translate(climbVal * Time.deltaTime * excuteMove);
            else if (mon.CompareTag("Monster"))
                player.transform.Translate(moveVal * Time.deltaTime * excuteMove * 3f);

        }


    }

    public class NormalState : PlayerState
    {
        public NormalState(PlayerController player) : base(player) { }

        private static NormalState instance;
        public static NormalState Instance
        {
            get
            {
                if (instance == null)
                    instance = new NormalState(player);
                return instance;
            }
        }
        public override void Move()
        {
            base.Move();

        }
        public override void Jump() { base.Jump(); }
        public override void Skill() { base.Skill(); }
        public override void Enter()
        {
            player.MaxSpeed = player.targetMaxSpeed;
        }
    }
    public class ClimbState : PlayerState
    {
        public ClimbState(PlayerController player) : base(player) { }

        private static ClimbState instance;
        public static ClimbState Instance
        {
            get
            {
                if (instance == null)
                    instance = new ClimbState(player);


                return instance;
            }
        }
        public override void Move()
        {
            if (climbVal.y > 0)
            {
                anim.SetBool("ClimbUp", true);
                anim.SetBool("ClimbDown", false);
                //anim.SetBool("SkyLine", false);

            }
            else if (climbVal.y < 0)
            {
                anim.SetBool("ClimbUp", false);
                anim.SetBool("ClimbDown", true);
                //anim.SetBool("SkyLine", false);
                VFXManager.Instance.PlayVFX("VFX_WallSlide");
            }
            else if (climbVal.x != 0)
            {
                anim.SetBool("ClimbUp", false);
                anim.SetBool("ClimbDown", false);
                //anim.SetBool("SkyLine", true);
                player.GetArm().SetBool("isSky", true);
            }
            else
            {
                anim.SetBool("ClimbUp", false);
                anim.SetBool("ClimbDown", false);
                player.GetArm().SetBool("isSky", false);
                VFXManager.Instance.ExitVFX();
            }

            rb.velocity = climbVal * climbSpeed;
        }
        public override void Jump()
        {
            rb.gravityScale = targetGravity;
            base.Jump();
            VFXManager.Instance.PlayVFX("VFX_WallJump");
            player.SetState(AirState.Instance);
        }
        //public override void Skill() { base.Skill(); }
        public override void Enter()
        {
            player.MaxSpeed = player.targetMaxSpeed;
            anim.Play("SNB_ClimbStart");
            anim.SetBool("Climbing", true);

            player.GetArm().SetTrigger("Climb");
            player.GetArm().SetBool("isClimbing", true);
            rb.gravityScale = 0.0f;
        }
        public override void Exit()
        {
            rb.AddForce(Vector2.up * 8, ForceMode2D.Impulse);
            rb.gravityScale = targetGravity;
            anim.SetBool("Climbing", false);
            anim.SetBool("isAttach", false);
            player.GetArm().SetBool("isClimbing", false);
            //anchor.GetJoint().autoConfigureDistance = false;

            //anim.SetTrigger("Exit");
        }
    }

    public class AirState : PlayerState
    {
        public AirState(PlayerController player) : base(player) { }
        private static AirState instance;
        public static AirState Instance
        {
            get
            {
                if (instance == null)
                    instance = new AirState(player);
                return instance;
            }
        }
        public override void Move() { base.Move(); armSr.enabled = true; }
        public override void Jump() { base.Jump(); }
        public override void Skill()
        {
            if (Input.GetKey(KeyCode.LeftShift))
                player.SetState(SpinState.Instance);
        }
        public override void Enter()
        {
            armSr.enabled = true;
        }

    }
    public class SpinState : PlayerState
    {
        public SpinState(PlayerController player) : base(player) { }
        private static SpinState instance;
        public static SpinState Instance
        {
            get
            {
                if (instance == null)
                    instance = new SpinState(player);
                return instance;
            }
        }

        float chargeTime = 1.0f;
        Transform target;
        float time;


        public override void Move()
        {
            Spin();
        }
        public override void Jump() { base.Jump(); player.SetState(AirState.Instance); }

        public override void Enter()
        {
            target = GetNearest();

            anim.Play("SNB_Spinning");
            Spin();

            armSr.enabled = false;
        }
        public override void Exit()
        {
            rb.gravityScale = targetGravity;
            player.StopCoroutine(ITrace());
            col.isTrigger = false;
            player.IsSpinning = false;

        }

        void Spin()
        {
            rb.gravityScale = 0.0f;
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0.0f;

            if (target == null) 
            {
                anim.Play("SNB_Fall");
                player.SetState(AirState.Instance);
                return;
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                time += Time.deltaTime;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                if (time < chargeTime)
                {
                    anim.Play("SNB_Fall");
                    player.SetState(AirState.Instance);
                    return;
                }
                else
                {
                    col.isTrigger = true;
                    anim.SetTrigger("SpinEnd");
                    player.IsSpinning = true;
                    player.StartCoroutine(ITrace());
                }
                time = 0.0f;


            }

        }


        IEnumerator ITrace()
        {
            Vector2 dir;
            while (player.IsSpinning)
            {

                yield return null;
                dir = target.position - player.transform.position;
                player.transform.Translate(dir.normalized * spinSpeed * Time.deltaTime);

            }
        }
        private Transform GetNearest()
        {
            Transform nearest = null;
            Collider2D[] cols = Physics2D.OverlapCircleAll(player.transform.position, 20.0f, spinLayer);
            float nearestDist = 200.0f;

            foreach (Collider2D col in cols)
            {
                float dist = Vector2.Distance(col.transform.position, player.transform.position);
                if (dist < nearestDist)
                {
                    nearestDist = dist;
                    nearest = col.transform;
                }
            }
            return nearest;
        }

    }
    #endregion

    #region Collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsSpinning)
        {
            if ((collision.CompareTag("Monster") || collision.CompareTag("FlyingMonster") || collision.CompareTag("HeavyMonster")))
            {
                enemy = null;
                collision.gameObject.SetActive(false);
                SetState(AirState.Instance);
            }
            anim.SetTrigger("Exit");
            VFXManager.Instance.PlayVFX(collision.transform.position, "VFX_ExcuteEnd");
        }

        if (!isInvincible)
        {
            if (collision.CompareTag("Bullet") && !IsSpinning)
            {
                anim.Play("SNB_Damaged");
            }
            if (collision.CompareTag("DamageTile"))
                anim.Play("SNB_Damaged");
        }

        if (collision.CompareTag("Finish"))
            isFreeze = true;

        if (collision.CompareTag("DeadZone"))
        {
            MySceneManager.Instance.ChangeScene(SceneManager.GetActiveScene().name, 1f);
        }

    }
    #endregion
}

public interface IPlayerState
{
    public void Move();
    public void Jump();
    public void Skill();
}
