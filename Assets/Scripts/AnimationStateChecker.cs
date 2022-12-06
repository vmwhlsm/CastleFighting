using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using KinematicCharacterController.Examples;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(ExampleCharacterController))]
[RequireComponent(typeof(Transform))]
public class AnimationStateChecker : MonoBehaviour
{
    private Animator _animator;
    private ExampleCharacterController _characterController;
    private UnityEngine.Vector3 _lastUpdatePosition;
    private bool _stopped;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<ExampleCharacterController>();
        _lastUpdatePosition = transform.position;
    }

    void Update()
    {
        var angleBetweenVectors = Vector3.Angle(_characterController._moveInputVector.normalized,
            _characterController._lookInputVector.normalized);
        var angleBetweenRight = Vector3.Angle(transform.right, _characterController._moveInputVector.normalized);

        var state = _animator.GetCurrentAnimatorClipInfo(0);

        if(_characterController.JumpRequested)
            UpdateAnimation(state[0], "Jump", "Jump");
        else if (_characterController._moveInputVector.x + _characterController._moveInputVector.z == 0)
            UpdateAnimation(state[0], "Idle", "Stopped");
        else if (angleBetweenVectors < 45)
            UpdateAnimation(state[0], "RunForward", "Front");
        else if (angleBetweenVectors > 135)
            UpdateAnimation(state[0], "RunBackward", "Back");
        else if (angleBetweenRight < 90)
            UpdateAnimation(state[0], "RunLeft", "Right");
        else
            UpdateAnimation(state[0], "RunRight", "Left");
        /*{
            if(Math.Abs(_characterController._moveInputVector.z + _characterController._lookInputVector.z) + Math.Abs(_characterController._moveInputVector.x + _characterController._lookInputVector.x) <= 1.3)
                Debug.Log("back");
            else if(Math.Abs(_characterController._moveInputVector.z + _characterController._lookInputVector.z) + Math.Abs(_characterController._moveInputVector.x + _characterController._lookInputVector.x) >= 1.8)
                Debug.Log("front");
        }*/

        _animator.SetBool("Fall", _characterController.OnAir);
    }

    private void UpdateAnimation(AnimatorClipInfo state, string clipName, string triggerName)
    {
        if (state.clip.name != clipName)
            _animator.SetTrigger(triggerName);
    }
}