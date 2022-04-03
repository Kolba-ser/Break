
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : Singleton<InputController>
{

    private Input inputControlls;

    private float moveInput;
    private Vector3 moveKeyboardInput;
    private Vector2 mousePosition;

    public float MoveInput => moveInput;
    public Vector3 MoveKeyboardInput => moveKeyboardInput;
    public Vector2 MousePosition => mousePosition;

    private void Awake()
    {
        inputControlls = new Input();

        inputControlls.Player.Move.performed += _ => moveInput = _.ReadValue<float>();
        inputControlls.Player.Move.canceled += _ => moveInput = _.ReadValue<float>();

        inputControlls.Player.MoveKeyboard.performed += _ => moveKeyboardInput = _.ReadValue<Vector3>();
        inputControlls.Player.MoveKeyboard.canceled += _ => moveKeyboardInput = _.ReadValue<Vector3>();
    }

    /// <summary>
    /// Подписывает два callback'a на два разных действия
    /// </summary>
    /// <param name="callbackOnPerformed"></param>
    /// <param name="callbackOnCanceled"></param>    
    public void OnFire(Action callbackOnPerformed, Action callbackOnCanceled)
    {
        inputControlls.Player.Fire.performed += _ => callbackOnPerformed();
        inputControlls.Player.Fire.canceled += _ => callbackOnCanceled();
    }

    /// <summary>
    /// Подписывает один callback на одно действие в зависимости от флага
    /// </summary>
    /// <param name="callback"></param>
    public void OnFire(Action callback, bool isCanceled = false)
    {
        if (!isCanceled)
        {
            inputControlls.Player.Fire.performed += _ => callback();
            return;
        }

        inputControlls.Player.Fire.canceled += _ => callback();
    } 

    /// <summary>
    /// Подписвает callback на два действия
    /// </summary>
    /// <param name="callback"></param>
    public void OnFire(Action callback)
    {
        inputControlls.Player.Fire.performed += _ => callback();
        inputControlls.Player.Fire.canceled += _ => callback();
    }

    public void OnJump(Action callback)
    {
        inputControlls.Player.Jump.performed += _ => callback();
    }

    private void OnEnable()
    {
        inputControlls.Enable();
    }
    private void OnDisable()
    {
        inputControlls.Disable();
    }

    public void Update()
    {
        mousePosition = Mouse.current.position.ReadValue();
    }
}

