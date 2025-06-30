using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class HQText : MonoBehaviour
{
    public GameObject dialogue1;
    public TextMeshProUGUI dialogueText;
    public GameObject dialogue2;
    public TextMeshProUGUI dialogueText2;
    public GameObject dialogue3;
    public TextMeshProUGUI dialogueText3;
    public GameObject dialogue4;
    public TextMeshProUGUI dialogueText4;
    public GameObject dialogue5;
    public TextMeshProUGUI dialogueText5;
    public GameObject dialogue6;
    public TextMeshProUGUI dialogueText6;
    public GameObject dialogue7;
    public TextMeshProUGUI dialogueText7;
    public GameObject dialogue8;
    public TextMeshProUGUI dialogueText8;
    public GameObject dialogue9;
    public TextMeshProUGUI dialogueText9;
    public GameObject dialogue10;
    public TextMeshProUGUI dialogueText10;
    public GameObject dialogue11;
    public TextMeshProUGUI dialogueText11;
    public GameObject dialogue12;
    public TextMeshProUGUI dialogueText12;
    public GameObject dialogue13;
    public TextMeshProUGUI dialogueText13;

    public Image hqA;
    public Image hqB;
    private string[] sentences;
    public GameObject hQ2;
    private Sprite[] images;

    private int index = 0;
    [HideInInspector] public float typingSpeed = 0.05f;

    private bool isTyping = false;
    private string currentSentence;
    private Coroutine typingCoroutine;

    public AudioClip typingSound;

    private AudioSource audioSource;

    void Start()
    {
        hQ2.SetActive(false);
        GlobalVariables.waveCounter = 0;
        audioSource = GetComponent<AudioSource>();
        images = new Sprite[]{
             Resources.Load<Sprite>("HQs/RoM_tHM Intro1.png"),//0
             Resources.Load<Sprite>("HQs/RoM_tHM Intro2.png"),//1
             Resources.Load<Sprite>("HQs/RoM_tHM Intro3.png"),//2
             Resources.Load<Sprite>("HQs/RoM_tHM Intro4"),//3
             Resources.Load<Sprite>("HQs/RoM_tHM Intro5"),//4
             Resources.Load<Sprite>("HQs/RoM_tHM Intro6.png"),//5
             Resources.Load<Sprite>("HQs/RoM_tHM Intro7.png"),//6
             Resources.Load<Sprite>("HQs/RoM_tHM Intro8.png"),//7
             Resources.Load<Sprite>("HQs/RoM_tHM Intro9.png")//8
            };
    }
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.X))
        {
            SceneManager.LoadScene("phase1");
        }
        {
            
        }
        if (GlobalVariables.waveCounter == 0)
        {
            hqA.sprite = images[0];
        }else if(GlobalVariables.waveCounter == 1)
        {
            hqA.sprite = images[1];
        }else if (GlobalVariables.waveCounter == 3)
        {
            hqA.sprite = images[2];
        }else if(GlobalVariables.waveCounter == 4){
            hqA.sprite = images[3];
        }else if(GlobalVariables.waveCounter == 5)
        {
            hqA.sprite = images[4];
        }
        if ((GlobalVariables.waveCounter == 7))
        {
            hQ2.SetActive(true);
            hqB.sprite = images[5];
        }
        else if (GlobalVariables.waveCounter == 9)
        {
            hqB.sprite= images[6];
        }else if (GlobalVariables.waveCounter == 10)
        {
            hqB.sprite = images[7];
        }else if (GlobalVariables.waveCounter == 11)
        {
            hqB.sprite = images[8];
        }
    }
    void Update()
    {
        if (GlobalVariables.waveCounter == 0 && sentences == null)
        {
            sentences = new string[] {
            "SUNRISE CITY"
        };

            StartDialogue();
        }
        else if (GlobalVariables.waveCounter == 1 && sentences == null)
        {
            sentences = new string[] {
            "A CITY ONCE KNOWN FOR ITS BEAUTIFUL SIGHTS"
        };

            StartDialogue();
        }
        else if (GlobalVariables.waveCounter == 2 && sentences == null)
        {
            sentences = new string[] {
            "BELOVED BY TOURISTS WORLDWIDE..."
        };

            StartDialogue();
        }
        else if (GlobalVariables.waveCounter == 3 && sentences == null)
        {
            sentences = new string[] {
            "WELL..."
        };

            StartDialogue();
        }
        else if (GlobalVariables.waveCounter == 4 && sentences == null)
        {
            sentences = new string[] {
            "UNTIL ONE DAY..."
        };

            StartDialogue();
        }
        else if (GlobalVariables.waveCounter == 5 && sentences == null)
        {
            sentences = new string[] {
            "THE PHANTOM OMERTA MAFIA CONSOLIDATED POWER TO TAKE OVER SUNRISE"
        };

            StartDialogue();
        }
        else if (GlobalVariables.waveCounter == 6 && sentences == null)
        {
            sentences = new string[] {
            "AND SPLIT IT INTO THREE DISTRICTS"
        };

            StartDialogue();
        }
        else if (GlobalVariables.waveCounter == 7 && sentences == null)
        {
            sentences = new string[] {
            "IN AN ATTEMPT TO FREE THE CITY FROM THE MAFIA'S INFLUENCE"
        };

            StartDialogue();
        }
        else if (GlobalVariables.waveCounter == 8 && sentences == null)
        {
            sentences = new string[] {
            "THE SKY BREAKERS SPECIAL FORCE WAS CREATED"
        };

            StartDialogue();
        }
        else if (GlobalVariables.waveCounter == 9 && sentences == null)
        {
            sentences = new string[] {
            "YOUR PREDECESSOR DID ALL SHE COULD TO PUT AN END TO THEIR EVIL"
        };

            StartDialogue();
        }
        else if (GlobalVariables.waveCounter == 10 && sentences == null)
        {
            sentences = new string[] {
            "BUT WAS ULTIMATELY UNSUCCESSFUL"
        };

            StartDialogue();
        }
        else if (GlobalVariables.waveCounter == 11 && sentences == null)
        {
            sentences = new string[] {
            "NOW..."
        };

            StartDialogue();
        }
        else if (GlobalVariables.waveCounter == 12 && sentences == null)
        {
            sentences = new string[] {
            "IT'S UP TO YOU, MAVERICK"
        };

            StartDialogue();
        }
        else if (GlobalVariables.waveCounter == 13 && sentences == null)
        {
            sentences = new string[] {
            "."
        };

            StartDialogue();
        }

        if (dialogue1.activeInHierarchy &&
            (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.L)))
        {
            if (isTyping)
            {
                StopCoroutine(typingCoroutine);
                isTyping = false;
                if (GlobalVariables.waveCounter == 0)
                {
                    dialogueText.text = currentSentence;
                }
                else if (GlobalVariables.waveCounter == 1)
                {
                    dialogueText2.text = currentSentence;
                }
                else if (GlobalVariables.waveCounter == 2)
                {
                    dialogueText3.text = currentSentence;
                }
                else if (GlobalVariables.waveCounter == 3)
                {
                    dialogueText4.text = currentSentence;
                }
                else if (GlobalVariables.waveCounter == 4)
                {
                    dialogueText5.text = currentSentence;
                }
                else if (GlobalVariables.waveCounter == 5)
                {
                    dialogueText6.text = currentSentence;
                }
                else if (GlobalVariables.waveCounter == 6)
                {
                    dialogueText7.text = currentSentence;
                }
                else if (GlobalVariables.waveCounter == 7)
                {
                    dialogueText8.text = currentSentence;
                }
                else if (GlobalVariables.waveCounter == 8)
                {
                    dialogueText9.text = currentSentence;
                }
                else if (GlobalVariables.waveCounter == 9)
                {
                    dialogueText10.text = currentSentence;
                }
                else if (GlobalVariables.waveCounter == 10)
                {
                    dialogueText11.text = currentSentence;
                }
                else if (GlobalVariables.waveCounter == 11)
                {
                    dialogueText12.text = currentSentence;
                }
                else if (GlobalVariables.waveCounter == 12)
                {
                    dialogueText13.text = currentSentence;
                }
            }
            else
            {
                index++;
                if (index < sentences.Length)
                {
                    currentSentence = sentences[index];
                    typingCoroutine = StartCoroutine(TypeSentence(currentSentence));
                }
                else
                {
                    EndDialogue();
                }
            }
        }
    }

    public void StartDialogue()
    {
        index = 0;
        currentSentence = sentences[index];
        typingCoroutine = StartCoroutine(TypeSentence(currentSentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;

        foreach (char letter in sentence.ToCharArray())
        {
            if (GlobalVariables.waveCounter == 0)
            {
                dialogueText.text += letter;
            }else if(GlobalVariables.waveCounter == 1)
            {
                dialogueText2.text += letter;
            }
            else if (GlobalVariables.waveCounter == 2)
            {
                dialogueText3.text += letter;
            }
            else if (GlobalVariables.waveCounter == 3)
            {
                dialogueText4.text += letter;
            }
            else if (GlobalVariables.waveCounter == 4)
            {
                dialogueText5.text += letter;
            }
            else if (GlobalVariables.waveCounter == 5)
            {
                dialogueText6.text += letter;
            }
            else if (GlobalVariables.waveCounter == 6)
            {
                dialogueText7.text += letter;
            }
            else if (GlobalVariables.waveCounter == 7)
            {
                dialogueText8.text += letter;
            }
            else if (GlobalVariables.waveCounter == 8)
            {
                dialogueText9.text += letter;
            }
            else if (GlobalVariables.waveCounter == 9)
            {
                dialogueText10.text += letter;
            }
            else if (GlobalVariables.waveCounter == 10)
            {
                dialogueText11.text += letter;
            }
            else if (GlobalVariables.waveCounter == 11)
            {
                dialogueText12.text += letter;
            }
            else if (GlobalVariables.waveCounter == 12)
            {
                dialogueText13.text += letter;
            }
            audioSource.Play();
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    void EndDialogue()
    {
        sentences = null;
        if (GlobalVariables.waveCounter == 12)
        {
            SceneManager.LoadScene("Phase1");
        }
        else
        {

            GlobalVariables.waveCounter += 1;
        }
    }
}