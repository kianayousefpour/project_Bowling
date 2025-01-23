using UnityEngine;

public class BowlingGameManager : MonoBehaviour
{
    private CameraFollowController CameraFollowController;
    [SerializeField] private float pinDestroyDelay = 5f; // زمان حذف بولینگ‌های افتاده
    [SerializeField] private float ballDestroyDelay = 10f; // زمان حذف توپ بدون برخورد
    private Rigidbody BallRigidbody;
    private BallManager BallManager;
    [SerializeField] private AudioSource cllapSound;

    private bool ballThrown = false; // آیا توپ پرتاب شده است؟
    private bool pinHit = false; // آیا برخورد با بولینگ رخ داده است؟
    private float throwStartTime=0; // زمان شروع پرتاب

    private void Start()
    {
        GetComponentValues();
    }

    private void Update()
    {
        CheckBallTimeout();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Pin"))
        {
            pinHit = true;
            cllapSound.Play();

            // حذف بولینگ‌های افتاده و توپ پس از تاخیر
            Invoke(nameof(RemovePinsAndBall), pinDestroyDelay);
           // BallManager.SetActiveBall();
           // BallRigidbody.isKinematic = true;
            //transform.SetParent(other.gameObject.transform);
        }
    }

    // private void OnCollisionExit(Collision other)
    // {
    //     if (other.gameObject.CompareTag("Pin"))
    //     {
    //         BallManager.SetActiveBall();
    //         BallRigidbody.isKinematic = true;
    //         transform.SetParent(other.gameObject.transform);
    //     }
    // }

    public void ThrowBall()
    {
        ballThrown = true;
        throwStartTime = Time.time; // ثبت زمان پرتاب
    }

    private void CheckBallTimeout()
    {
        if (ballThrown && !pinHit && Time.time - throwStartTime > ballDestroyDelay)
        {
            Destroy(gameObject); // حذف توپ بعد از 15 ثانیه در صورت عدم برخورد
            BallManager.SetActiveBall();
        }
    }

    private void RemovePinsAndBall()
    {
        // حذف تمام بولینگ‌های افتاده
        GameObject[] pins = GameObject.FindGameObjectsWithTag("Pin");
        foreach (GameObject pin in pins)
        {
            // بررسی افتادن بولینگ‌ها (یعنی اگر محور Y آنها پایین‌تر از مقدار خاصی است)
            if (pin.transform.position.y < -12f)
            {
                Destroy(pin); // حذف بولینگ
            }
        }

        // حذف توپ
        RemoveBall();
        BallManager.SetActiveBall();
    }

    private void RemoveBall()
    {
        Destroy(gameObject); // حذف توپ
    }

    private void GetComponentValues()
    {
        BallRigidbody = GetComponent<Rigidbody>();
        BallManager = GameObject.FindObjectOfType<BallManager>();
    }
}
