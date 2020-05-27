using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float _speed=3.5f;
    private float _gravity = -9.81f;

    [SerializeField]
    private GameObject _hitMarkerPrefab;

    private ParticleSystem _particleSystem;

    private CharacterController _controller;

    [SerializeField]
    private AudioSource _audioWeapon;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _particleSystem = gameObject.GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetMouseButton(0))
        {
            if (!_particleSystem.isPlaying)
            {
                _particleSystem.Play();
            }

            if (!_audioWeapon.isPlaying)
            {
                _audioWeapon.Play();
            }

            Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            RaycastHit hitInfo;

            if(Physics.Raycast(rayOrigin, out hitInfo))
            {
               Destroy(Instantiate(_hitMarkerPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal)), 3f);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            _particleSystem.Stop();
            _audioWeapon.Stop();
        }

    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
        Vector3 velocity = direction * _speed;
        
        //transform to worldSpace
        velocity = transform.transform.TransformDirection(velocity);

        //gravity always in worlSpace coordenates
        velocity.y = _gravity;
        _controller.Move(velocity * Time.deltaTime);
    }
}
