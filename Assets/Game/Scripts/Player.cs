using System;
using System.Collections;
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

    [SerializeField]
    private int currentAmmo;
    private int maxAmmo = 250;

    private bool isWeaponReloadRunning = false;
    private UiManager _uiManager;

    public bool hasCoin = false;

    [SerializeField]
    private GameObject weapon;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _particleSystem = gameObject.GetComponentInChildren<ParticleSystem>();
        currentAmmo = maxAmmo;
        _uiManager = GameObject.Find("Canvas").GetComponent<UiManager>();
        _uiManager.UpdateAmmo(currentAmmo);
        weapon.SetActive(false);
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

        if (weapon.activeSelf && Input.GetMouseButton(0))
        {
            if (currentAmmo > 0)
            {
                currentAmmo--;
                _uiManager.UpdateAmmo(currentAmmo);
                Shoot();
            }
            else
            {
                StopShoot();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            StopShoot();
        }

        if (Input.GetKeyDown(KeyCode.R) && !isWeaponReloadRunning)
        {   
            StartCoroutine(ReloadWeapon());
        }

    }

    IEnumerator ReloadWeapon()
    {
        isWeaponReloadRunning = true;
        yield return new WaitForSeconds(1.5f);
        currentAmmo = maxAmmo;
        _uiManager.UpdateAmmo(currentAmmo);
        isWeaponReloadRunning = false;
    }

    private void StopShoot()
    {
        _particleSystem.Stop();
        _audioWeapon.Stop();
    }

    private void Shoot()
    {
        if (_particleSystem!=null && !_particleSystem.isPlaying)
        {
            _particleSystem.Play();
        }

        if (_audioWeapon!=null && _audioWeapon.isActiveAndEnabled && !_audioWeapon.isPlaying)
        {
            _audioWeapon.Play();
        }

        Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        RaycastHit hitInfo;

        if (Physics.Raycast(rayOrigin, out hitInfo))
        {
            Destroy(Instantiate(_hitMarkerPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal)), 3f);

            Destructable destructable = hitInfo.transform.GetComponent<Destructable>();
            if (destructable != null)
            {
                destructable.DestroyComponent();
            }
          
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

    public void enableWeapon()
    {
        weapon.SetActive(true);
    }
}
