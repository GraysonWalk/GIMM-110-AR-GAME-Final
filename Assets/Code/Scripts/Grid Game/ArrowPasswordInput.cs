using UnityEngine;
using UnityEngine.Events;
using static ArrowPassword;

public class ArrowPasswordInput : MonoBehaviour
{
    private GridNodeDirectional node;
    private ArrowPassword password;
    public UnityEvent onCorrect;
    public UnityEvent onIncorrect;

    private int index = 0;

    public void StartSequence(ArrowPassword pw, GridNodeDirectional baseNode)
    {
        node = baseNode;
        password = pw;
        index = 0;
        gameObject.SetActive(true);
    }

    void Update()
    {
        if (password == null) return;

        if (Input.GetKeyDown(KeyCode.W)) 
            CheckInput(ArrowDir.Up);
        if (Input.GetKeyDown(KeyCode.S)) 
            CheckInput(ArrowDir.Down);
        if (Input.GetKeyDown(KeyCode.A)) 
            CheckInput(ArrowDir.Left);
        if (Input.GetKeyDown(KeyCode.D)) 
            CheckInput(ArrowDir.Right);
        if (Input.GetKeyDown(KeyCode.Escape))
            gameObject.SetActive(false);

    }
    private void CheckInput(ArrowDir dir)
    {
        if (dir == password.sequence[index])
        {
            index++;

            if (index >= password.sequence.Length)
            {
                Debug.Log("correct");
                onCorrect.Invoke();

                gameObject.SetActive(false);
                node.onActivate.Invoke();
                password = null;
            }
        }
        else
        {
            Debug.Log("incorrect");
            index = 0;
            onIncorrect.Invoke();
        }
    }
}
