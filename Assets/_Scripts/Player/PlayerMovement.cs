using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {
    #region Public Variables
    [Header("Controllers/References")]
    [Tooltip("The level's keyboard controller")]
    public KeyboardInterfaceController keyboardInput;

    [Tooltip("This player's animation controller")]
    public Animator anim;
    [Tooltip("This player's sprite")]
    public SpriteRenderer sprite;

    [Header("Movement Variables")]
    [Tooltip("How fast, in unity units per second, the player can move")]
    public float speed = 8f;

    public bool frozen;
    #endregion

    #region Private Variables
    private Vector3 direction;
    private Rigidbody body;
    private bool wasMoving;
    #endregion

    #region MonoBehavior Methods
    private void Awake() {
        direction = Vector3.zero;
        body = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    //void Update() {
    //    // get the direction from the keyboard controller
    //    direction = keyboardInput.Direction;

    //    Move();
    //}

    private void Update() {
        direction = keyboardInput.Direction;

        Move();
    }
    #endregion

    #region Script Specific Methods
    /// <summary>
    /// Moves the player in the given direction at the given speed.
    /// </summary>
    private void Move() {
        Vector3 displacement = Vector3.zero;
        if (!frozen) {
            displacement = direction * speed * Time.deltaTime;
            if (displacement.magnitude >= Mathf.Epsilon)
            {
                body.MovePosition(transform.position + displacement);
            }
            else
            {
                body.velocity = Vector3.zero;
            }
        }

        //Set animations and flip status
        if (wasMoving && displacement.magnitude <= Mathf.Epsilon)
        {//Stopped moving
            anim.Play("PlayerWalkEnd");
        }
        if (!wasMoving && displacement.magnitude > Mathf.Epsilon)
        {//Started moving
            anim.Play("PlayerWalkStart");
        }
        wasMoving = (displacement.magnitude > Mathf.Epsilon);
        sprite.flipX = (Mathf.Abs(displacement.x) > 0) ? (displacement.x < 0) : sprite.flipX;
    }
    #endregion
}
