using System.Collections;
using TMPro;
using UnityEngine;

public class MachineScript : MonoBehaviour
{
    [SerializeField]
    float num1;
    [SerializeField]
    float num2;
    [SerializeField] 
    float sign;
    [SerializeField]
    float solution;
    [SerializeField]
    TMP_Text TextScreen;
    [SerializeField]
    GameControllerScript GC;
    [SerializeField]
    public float ProblemsToSolve;
    [SerializeField]
    float ProblemsSolved;
    [SerializeField]
    bool ImposibleUhOh;
    [SerializeField]
    float FakeNum1;
    [SerializeField]
    float FakeNum2;
    [SerializeField]
    string FinalAnswer;
    [SerializeField]
    public SpriteRenderer MachineState;
    [SerializeField]
    public Sprite CorrectState;
    [SerializeField]
    public Sprite WrongState;
    [SerializeField]
    bool Answered;
    [SerializeField]
    public CapsuleCollider Trigger;
    [SerializeField]
    public Transform PlayerPosition;
    [SerializeField]
    public string[] ComplementHints = new string[]
    {
        "You did great!"
    };
    [SerializeField]
    public string[] GuiltHints = new string[]
{
        "You better leave"
};
    [SerializeField]
    public AudioSource TheAud;
    [SerializeField]
    public AudioClip CorrectAnswer;
    [SerializeField]
    public AudioClip WrongAnswer;
    void Start()
    {
        if(GC.mode != "hard" | GC.mode != "endless")
        {
            gameObject.SetActive(false);
        }
        GenerateProblem();
    }

    private void Update()
    {
        FakeNum1 = Random.Range(0, 99);
        FakeNum2 = Random.Range(0, 99);
        if (ImposibleUhOh)
        {
            if (sign == 0)
            {
                TextScreen.text = string.Concat(new object[]
                {
                num1,
                " + ",
                num2,
                " = ?"
                });
            }
            else if (sign == 1)
            {
                TextScreen.text = string.Concat(new object[]
                {
                num1,
                " - ",
                num2,
                " = ?"
                });
            }
            else if (sign == 2)
            {
                TextScreen.text = string.Concat(new object[]
                {
                num1,
                " X ",
                num2,
                " = ?"
                });
            }
            num1 = FakeNum1;
            num2 = FakeNum2;
        }
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit Hit;
            Ray eye = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(eye, out Hit) && Hit.collider == Trigger && Vector3.Distance(PlayerPosition.position, transform.position) <= 15)
            {
                if(!Answered)
                {
                    AnswerProblem();
                }
            }
        }
    }

    void GenerateProblem()
    {
        if(ProblemsSolved != ProblemsToSolve)
        {
            if (GC.mode == "hard" && (GC.notebooks != 2f && ProblemsSolved <= 1) || GC.mode == "endless" && (GC.notebooks != 2f && ProblemsSolved <= 1))
            {
                sign = Mathf.RoundToInt(Random.Range(0f, 2f));
                if (sign == 0)
                {
                    num1 = Random.Range(0, 99);
                    num2 = Random.Range(0, 99);
                    solution = num1 + num2;
                    TextScreen.text = string.Concat(new object[]
                    {
                num1,
                " + ",
                num2,
                " = ?"
                    });
                }
                else if (sign == 1)
                {
                    num1 = Random.Range(0, 99);
                    num2 = Random.Range(0, 99);
                    solution = num1 - num2;
                    TextScreen.text = string.Concat(new object[]
                    {
                num1,
                " - ",
                num2,
                " = ?"
                    });
                }
                else if (sign == 2)
                {
                    num1 = Random.Range(0, 99);
                    num2 = Random.Range(0, 99);
                    solution = num1 * num2;
                    TextScreen.text = string.Concat(new object[]
                    {
                num1,
                " X ",
                num2,
                " = ?"
                    });
                }
            }
            else
            {
                ImposibleUhOh = true;
                sign = Mathf.RoundToInt(Random.Range(0f, 2f));
            }
        }
        else
        {
            StartCoroutine(ShowRightorWrong());
        }
    }

    void AnswerProblem()
    {
        Answered = true;
        ProblemsSolved++;
        if (FinalAnswer == solution.ToString() && !ImposibleUhOh)
        {
            TheAud.PlayOneShot(CorrectAnswer, 1f);
          if(ProblemsSolved == ProblemsToSolve)
            {
                MachineState.sprite = CorrectState;
                StartCoroutine(ShowRightorWrong());
            }
            else
            {
                Answered = false;
            }
        }
        else
        {
            TheAud.PlayOneShot(WrongAnswer, 1f);
            ImposibleUhOh = false;
            if (ProblemsSolved == ProblemsToSolve)
            {
                MachineState.sprite = WrongState;
                StartCoroutine(ShowRightorWrong());
            }
            else
            {
                Answered = false;
            }
        }
    }

    IEnumerator ShowRightorWrong()
    {
        if (ImposibleUhOh)
        {
            solution = 404;
        }
        if (sign == 0)
        {
            TextScreen.text = string.Concat(new object[]
            {
                num1,
                " + ",
                num2,
                " = ",
                solution
            });
        }
        else if (sign == 1)
        {
            TextScreen.text = string.Concat(new object[]
            {
                num1,
                " - ",
                num2,
                " = ",
                solution
            });
        }
        else if (sign == 2)
        {
            TextScreen.text = string.Concat(new object[]
            {
                num1,
                " * ",
                num2,
                " = ",
                solution
            });
        }
        yield return new WaitForSeconds(2f);

        if (FinalAnswer == solution.ToString() && !ImposibleUhOh)
        {
            TextScreen.text = string.Concat(new object[]
        {
                    ComplementHints[Random.Range(0 , ComplementHints.Length)]
        });
        }
        else
        {

            TextScreen.text = string.Concat(new object[]
        {
                    GuiltHints[Random.Range(0 , GuiltHints.Length)]
        });
        }

        yield break;
    }
}
