using UnityEngine;

public class BowlingGameManager : MonoBehaviour
{
    private CameraFollowController CameraFollowController;
    [SerializeField] private float pinDestroyDelay = 5f; // زمان حذف بولینگ‌های افتاده
    [SerializeField] private float ballDestroyDelay = 10f; // زمان حذف توپ بدون برخورد
    private Rigidbody BallRigidbody;
    private BallManager BallManager;
    // [SerializeField] private AudioSource cllapSound;
    [SerializeField] private int totalPins=10; // تعداد کل پین‌ها
    
    private int currentStage = 0; // مرحله فعلی
    [SerializeField] private int maxStages = 5; // تعداد کل مراحل   
    [SerializeField] private GameObject gameOverButton; // دکمه گیم‌اور

    private bool ballThrown = false; // آیا توپ پرتاب شده است؟
    private bool pinHit = false; // آیا برخورد با بولینگ رخ داده است؟
    private float throwStartTime=0; // زمان شروع پرتاب

    private void Start()
    {
       
    GetComponentValues();
    gameOverButton.SetActive(false);
    }

    private void Update()
    {
        CheckBallTimeout();
        //CheckAllPinsFallen();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Pin"))
        {
            pinHit = true;
            // cllapSound.Play();

            // حذف بولینگ‌های افتاده و توپ پس از تاخیر
            Invoke(nameof(RemovePinsAndBall), pinDestroyDelay);
        }
    }

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

// private void CheckAllPinsFallen()
// {
//     // بررسی اینکه آیا همه پین‌ها افتاده‌اند
//     GameObject[] pins = GameObject.FindGameObjectsWithTag("Pin");
//     int fallenPins = 0;

//     foreach (GameObject pin in pins)
//     {
//         if (pin.transform.position.y < -12f) // معیار افتادن پین
//         {
//             fallenPins++;
//         }
//     }

//     if (fallenPins == totalPins) // اگر همه پین‌ها افتاده‌اند
//     {
//         //GoToNextStage();
//     }
// }

// private void GoToNextStage()
// {
//     currentStage++;
//     if (currentStage > maxStages) // اگر تمام مراحل تمام شده باشد
//     {
//         Debug.Log("Game Completed!");
//         gameOverButton.SetActive(true); // نمایش دکمه گیم‌اور
//     }
//     else
//     {
//         ResetPinsAndBalls(); // بازنشانی پین‌ها و توپ‌ها
//     }
// }

// private void ResetPinsAndBalls()
// {
//     // بازنشانی تمام پین‌ها
//     FindObjectOfType<PinSpawnController>().GeneratePinGrid();

//     // بازنشانی توپ‌ها
//     FindObjectOfType<BallManager>().ResetBalls();
// }

// public void GameOver()
// {
//     Debug.Log("Game Over!");
//     gameOverButton.SetActive(true); // نمایش دکمه گیم‌اور
// }
}
