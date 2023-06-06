using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance;
        public GameObject camara;
        Rigidbody rb;
        Touch touch;
        [Range(20, 60)] public int speedModifier;
        public int forwardSpeed;
        public GameObject restartBtn;
        public static GameObject Player;


        private void Awake() => Instance = this;

        private void Start()
        {
            Player = transform.gameObject;
            rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (Variables.FirstTouch == 1 && !Variables.RoundOver)
            {
                transform.position += new Vector3(0, 0, forwardSpeed * Time.deltaTime);
                camara.transform.position += new Vector3(0, 0, forwardSpeed * Time.deltaTime);
                Bounds.Instance.vectorForward.position += new Vector3(0, 0, forwardSpeed * Time.deltaTime);
                Bounds.Instance.vectorBack.position += new Vector3(0, 0, forwardSpeed * Time.deltaTime);
            }

            if (Input.touchCount > 0 && !Variables.RoundOver)
            {
                touch = Input.GetTouch(0);

                switch (touch.phase)
                {
                    case TouchPhase.Moved:
                        if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                        {
                            rb.velocity = new Vector3(touch.deltaPosition.x * speedModifier * Time.deltaTime,
                                transform.position.y,
                                touch.deltaPosition.y * speedModifier * Time.deltaTime);
                        }

                        break;
                    case TouchPhase.Began:
                        if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                        {
                            Variables.FirstTouch = 1;
                            UIManager.Instance.UIStart();
                        }

                        break;
                    case TouchPhase.Ended:
                        rb.velocity = Vector3.zero;
                        break;
                }
            }

            if (Variables.RoundOver)
            {
                camara.transform.position += new Vector3(0, 0, forwardSpeed * Time.deltaTime);
            }
        }


        private void OnCollisionEnter(Collision hit)
        {
            if (hit.gameObject.CompareTag("Obstacles"))
            {
                CameraShake.Instance.CameraShakesCall();
                UIManager.Instance.WhiteEffectCall();
                transform.GetChild(0).gameObject.SetActive(false);

                for (int i = 1; i < 33; i++)
                {
                    transform.GetChild(i).gameObject.AddComponent<SphereCollider>();
                    transform.GetChild(i).gameObject.AddComponent<Rigidbody>().drag = 1;
                }

                StartCoroutine(Restart());
            }
        }

        private IEnumerator Restart()
        {
            Variables.RoundOver = true;
            yield return new WaitForSeconds(0.4f);
            Time.timeScale = 0.4f;
            restartBtn.SetActive(true);
            rb.velocity = Vector3.zero;
        }
    }
}