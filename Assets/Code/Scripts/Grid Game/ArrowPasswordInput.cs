using UnityEngine;
using UnityEngine.Events;
using static ArrowPassword;

public class ArrowPasswordInput : MonoBehaviour
{
    private GridNodeDirectional node;
    private ArrowPassword password;
    public UnityEvent onCorrect;
    public UnityEvent onIncorrect;
    [SerializeField] GameObject[] passImages;

    private int index = 0;

    // Initializes password sequence 
    public void StartSequence(ArrowPassword pw, GridNodeDirectional baseNode)
    {
        node = baseNode;
        password = pw;
        index = 0;
        gameObject.SetActive(true);
        passImages[index].gameObject.SetActive(true);
    }

    void Update()
    {
        // Password Input

        // For interaction, if node doesn't contain a password return
        if (password == null) return;

        // Password Inputs (uses Enum)
        if (Input.GetKeyDown(KeyCode.W)) 
            CheckInput(ArrowDir.Up);
        if (Input.GetKeyDown(KeyCode.S)) 
            CheckInput(ArrowDir.Down);
        if (Input.GetKeyDown(KeyCode.A)) 
            CheckInput(ArrowDir.Left);
        if (Input.GetKeyDown(KeyCode.D)) 
            CheckInput(ArrowDir.Right);

        // Deactivates password gameobject
        if (Input.GetKeyDown(KeyCode.Escape))
            gameObject.SetActive(false);

    }

    // Password Logic
    private void CheckInput(ArrowDir dir)
    {
        // If input = password element at index, progress
        if (dir == password.sequence[index])
        {
            index++;
            passImages[index].gameObject.SetActive(true);
            passImages[index -1].gameObject.SetActive(false);


            // If index reaches end of password, set correct
            if (index >= password.sequence.Length)
            {
                Debug.Log("correct");
                for (int i = 0; i < passImages.Length; i++)
                {
                    passImages[i].gameObject.SetActive(false);
                }
                onCorrect.Invoke();


                gameObject.SetActive(false);
                node.onActivate.Invoke();
                password = null;
            }
        }

        // If input != password element at index, reset progress/index
        else
        {
            Debug.Log("incorrect");
            index = 0;
            for (int i = 1; i < passImages.Length; i++)
            {
                passImages[i].gameObject.SetActive(false);
            }
            passImages[index].gameObject.SetActive(true);
            

            onIncorrect.Invoke();
        }
    }
}
