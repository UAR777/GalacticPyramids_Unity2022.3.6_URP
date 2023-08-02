/*
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update

    /*
    public PlayerControls control;
    void Awake()
    {
        control = new PlayerControls();
        control.UI.MouseMovement.performed += _ => MovementInput();
        control.UI.Selection.performed += SelectionInput;
    }

    void MovementInput()
    {
        Vector2 mousePositon = control.UI.MouseMovement.ReadValue<Vector2>();
        mousePositon = Camera.main.WorldToScreenPoint(mousePositon);
    }

    void OnEnable()
    {
        control.UI.Enable();
    }

    void OnDisable()
    {
        control.UI.Disable();
    }

    void Start()
    {
        string pyramidName;
        
        GameObject.Find(pyramidName).GetComponent<Movement>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
*/