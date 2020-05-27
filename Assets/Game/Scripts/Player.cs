using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float _speed=3.5f;
    private float _gravity = -9.81f;

    private CharacterController _controller;
    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
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
