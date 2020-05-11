using UnityEngine;
using UnityEngine.UI;

public class BallControll : MonoBehaviour

{

    public Rigidbody PlayerBall;
    public Slider StrokeSlider;
    public Image FillImage;
    public Color MaxStrokeForceColor = Color.red;
    public Color MinStrokeForceColor = Color.green;

    public float StrokeAngle { get; protected set; }
    public float MinStrokeForce = 1f;
    public float MaxStrokeForce = 15f;
    public float MaxChargeTime = 1f;
    public int strokeCount { get; protected set; }

    private float CurrentStrokeForce;
    private float ChargeSpeed;
    private int fillDirection = 1;
    private Canvas strokeCanvas;

    private enum BallStateEnum { AIMING, POWERING, SHOOT, ROLLING };
    private BallStateEnum BallState;

    private void OnEnable()
    {
        CurrentStrokeForce = MinStrokeForce;
        StrokeSlider.value = MinStrokeForce;
    }

    


    // Start is called before the first frame update
    void Start()
    {
        BallState = BallStateEnum.AIMING;

        strokeCanvas = GetComponentInChildren<Canvas>();

        ChargeSpeed = (MaxStrokeForce - MinStrokeForce) / MaxChargeTime;
        CurrentStrokeForce = MinStrokeForce;
        StrokeSlider.value = MinStrokeForce;
        strokeCount = 0;
    }

    private void Update()
    {
        if(BallState == BallStateEnum.AIMING)
        {
            StrokeAngle += Input.GetAxis("Horizontal") * 100f * Time.deltaTime;
            
            if(Input.GetButtonUp("Fire")) {

                CurrentStrokeForce = MinStrokeForce;
                BallState = BallStateEnum.POWERING;
                return;
            }
        }

        if (BallState == BallStateEnum.POWERING)
        {
            CurrentStrokeForce += (ChargeSpeed * fillDirection) * Time.deltaTime;
            SetStrokeUI();

            if (CurrentStrokeForce > MaxStrokeForce)
            {
                CurrentStrokeForce = MaxStrokeForce;
                fillDirection = -1;
            }
            else if (CurrentStrokeForce < MinStrokeForce)
            {
                CurrentStrokeForce = MinStrokeForce;
                fillDirection = 1;
            }
            
            if (Input.GetButtonUp("Fire"))
            {
                BallState = BallStateEnum.SHOOT;
            }
        }
    }


    void FixedUpdate()
    {
        if(BallState == BallStateEnum.ROLLING)
        {
            CheckRolling();
            return;
        } 
        else if(BallState != BallStateEnum.SHOOT)
        {
            return;
        }

        Stroke();

    }

    private void Stroke()
    {
        Vector3 forceVec = new Vector3(0, 0, CurrentStrokeForce);

        PlayerBall.AddForce(Quaternion.Euler(0, StrokeAngle, 0) * forceVec, ForceMode.Impulse);
        BallState = BallStateEnum.ROLLING;
        CurrentStrokeForce = MinStrokeForce;
        StrokeSlider.value = MinStrokeForce;

        strokeCanvas.enabled = false;

        strokeCount++;
    }

    private void SetStrokeUI()
    {
        StrokeSlider.value = CurrentStrokeForce;
        FillImage.color = Color.Lerp(MinStrokeForceColor, MaxStrokeForceColor, CurrentStrokeForce / MaxStrokeForce);
    }

    private void CheckRolling()
    {
        if(PlayerBall.IsSleeping())
        {
            BallState = BallStateEnum.AIMING;
            strokeCanvas.enabled = true;
        }
    }

    public void ResetStrokeCounter()
    {
        strokeCount = 0;
    }

}
