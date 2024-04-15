using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    // Variables related to player character movement
    public InputAction MoveAction;
    public InputAction BookAction;
    public InputAction FAction;
    public InputAction BAction;
    Rigidbody2D rigidbody2d;
    Vector2 move;
    public float speed = 3.0f;
    Animator animator;
    Vector2 moveDirection = new Vector2(1, 0);


    // Variables related to the health system
    public int maxMana = 5;
    int currentMana;


    // Start is called before the first frame update
    void Start()
    {
        MoveAction.Enable();
        BookAction.Enable();
        BAction.Enable();
        FAction.Enable();
        BookAction.performed += TogBook;
        FAction.performed += Forward;
        BAction.performed += Backward;
        rigidbody2d = GetComponent<Rigidbody2D>();
        currentMana = maxMana;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        move = MoveAction.ReadValue<Vector2>();

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            moveDirection.Set(move.x, move.y);
            moveDirection.Normalize();
        }
        animator.SetFloat("speed", move.magnitude);
    }


    // FixedUpdate has the same call rate as the physics system 
    void FixedUpdate()
    {
        Vector2 position = (Vector2)rigidbody2d.position + move * speed * Time.deltaTime;
        rigidbody2d.MovePosition(position);
        RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, moveDirection, 0.1f, LayerMask.GetMask("NPC"));

        if (hit.collider != null)
        {
            NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
            if (character != null)
            {
                UIHandler.instance.DisplayDialogue(character.dialogText);
            }

        }
    }


    void TogBook(InputAction.CallbackContext context)
    {
        UIHandler.instance.ChangeBook();
    }

    void Forward(InputAction.CallbackContext context)
    {
        UIHandler.instance.BookForward();
    }

    void Backward(InputAction.CallbackContext context)
    {
        UIHandler.instance.BookBackward();
    }

}