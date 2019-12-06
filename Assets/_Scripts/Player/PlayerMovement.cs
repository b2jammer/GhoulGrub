﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    public float speed = 5f;

    public bool frozen;
    #endregion

    #region Private Variables
    private Vector3 direction;
    private Rigidbody body;
    private bool wasMoving;
    private Vector3 oldPosition;


    private const float MINIMUM_TARGET_DISTANCE = 0.1f;
    private const float EPSILON = 0.01f;
    #endregion

    #region MonoBehavior Methods
    private void Awake() {
        direction = Vector3.zero;
        body = GetComponent<Rigidbody>();
        oldPosition = transform.position;
    }

    // Start is called before the first frame update
    void Start() {

    }

    private void FixedUpdate() {
        direction = keyboardInput.Direction;

        Move();
    }
    #endregion

    private Vector3 GetDirectionToTarget(Vector3 targetPoint) {
        var dir = targetPoint - transform.position;
        return dir.normalized;
    }

    #region Script Specific Methods
    /// <summary>
    /// Moves the player in the given direction at the given speed.
    /// </summary>
    private void Move() {
        Vector3 displacement = Vector3.zero;
        if (!frozen) {
            displacement = direction * speed * Time.deltaTime;
            if (displacement.magnitude >= Mathf.Epsilon) {
                body.MovePosition(transform.position + displacement);
            }
            else {
                body.velocity = Vector3.zero;
            }
        }

        //Set animations and flip status
        //if (Mathf.Abs(direction.x) >= Mathf.Abs(direction.z)) {
        if (wasMoving && displacement.magnitude <= Mathf.Epsilon) {//Stopped moving
            anim.Play("PlayerWalkEnd");
        }
        if (!wasMoving && displacement.magnitude > Mathf.Epsilon) {//Started moving
            anim.Play("PlayerWalkStart");
        }
        //}
        //else {
        //    if (direction.z <= 0) {
        //        if (wasMoving && displacement.magnitude <= Mathf.Epsilon) {//Stopped moving
        //            anim.Play("PlayerWalkForwardEnd");
        //        }
        //        if (!wasMoving && displacement.magnitude > Mathf.Epsilon) {//Started moving
        //            anim.Play("PlayerWalkForwardStart");
        //        }
        //    }
        //    else {
        //        if (wasMoving && displacement.magnitude <= Mathf.Epsilon) {//Stopped moving
        //            anim.Play("PlayerWalkBackwardEnd");
        //        }
        //        if (!wasMoving && displacement.magnitude > Mathf.Epsilon) {//Started moving
        //            anim.Play("PlayerWalkBackwardStart");
        //        }
        //    }
        //}



        wasMoving = (displacement.magnitude > Mathf.Epsilon);
        sprite.flipX = (Mathf.Abs(displacement.x) > 0) ? (displacement.x < 0) : sprite.flipX;
    }
    #endregion
}
