using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Dialogue4Manager : MonoBehaviour
{
    public GameObject spawner;
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public Image dialogueImage;

    public TextMeshProUGUI dialogueName;
    private string[] names;
    private string[] sentences;
    private Sprite[] images;
    private Color[] colors;
    private Color lightBlue = new Color(0, 255, 255, 255);

    private int index = 0;
    [HideInInspector] public float typingSpeed = 0.05f;

    private bool isTyping = false;
    private bool dialogueHappened = true;
    private string currentSentence;
    private Coroutine typingCoroutine;

    public AudioClip typingSound;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        dialoguePanel.SetActive(false);
    }

    void Update()
    {
        if (GlobalVariables.waveCounter == 0 && sentences == null && dialogueHappened)
        {
            dialogueHappened = false;
            sentences = new string[] {
            "YOU DID QUITE THE SPECTACLE BACK THERE, HUMAN", //1
            "IT WAS AWFULLY PLEASANT TO WATCH THOSE STRUGGLES OF YOURS", //2
            "AND YET YOU'VE GOT THE BETTER OFF THE SITUATION...", //3
            "TO TELL YOU THE TRUTH, YOU MANAGING TO GET THIS FAR WASN'T ON MY PLANS", //4
            "YOUR POWER TRULY EXCEEDED MY EXPECTATIONS", //5
            "I'VE DEFEATED THOSE JERKS YOU CALL LOYAL GRUNTS", //6
            "NOW IT'S YOUR TURN", //7
            "HA! IT SEEMS YOU ARE MISINTERPRETING YOUR CURRENT SITUATION", //8
            "YOUR AIRSHIP IS DAMAGED, AFTER THAT TOUGH BATTLE", //9
            "YOU CAN BARELY MOVE, LET ALONE RUN", //10
            "IN MY HUMBLE OPINION, YOU COULD SIMPLY SURRENDER NOW", //11
            "IT WOULD BE MUCH SUITABLE THAN HEADING ONTO ANOTHER CONFLICT", //12
            "MAYBE... YOU'RE RIGHT...",//13
            "I *AM* FEELING KINDA OF TIRED, NOT GONNA LIE",//14
            "MAV! DON'T LET HIM ENTER ON YOUR MIND",//15
            "JUST LIKE YOU HAVE *TELEKINESIS*...",//16
            "HE HOLDS THE *POWER TO CONTROL OTHER PEOPLE'S MINDS*",//17
            "YOU'RE QUITE FAMILIAR WITH THIS POWER OF MINE, AREN'T YOU SAMIRA?",//18
            "AFTER ALL, IT WAS THE CRUCIAL ASPECT TO CAUSE YOUR DOWNFALL",//19
            "WHAT A FILTHY ABILITY",//20
            "GUESS IT SUITS YOU RIGHT",//21
            "MAYBE YOU'RE CORRECT, BUT REGARDLESS OF THAT...",//22
            "THE MOMENT I TAKE OVER YOUR MIND ONCE...",//23 
            "YOUR JOURNEY WILL COME TO A FATEFUL AND YET MAGNIFICENT END!",//24
            "STAY FOCUSED, MAVERICK. DON'T LET YOUR GUARD DOWN FOR A SINGLE SECOND!",//25
            "YOU CAN COUNT ON ME. I AM NOT FALLING FOR A CHEAP TRICK, SAM",//26
            "AFTER ALL...",//27
            "IT'S DO OR DIE"//28
        };

            images = new Sprite[] {
            Resources.Load<Sprite>("sprites/Otello cigar default"), //1
            Resources.Load<Sprite>("sprites/Otello cigar cheeky"), //2
            Resources.Load<Sprite>("sprites/Otello cigar reluctant"), //3
            Resources.Load<Sprite>("sprites/Otello cigar serious"), //4
            Resources.Load<Sprite>("sprites/Otello cheeky"), //5
            Resources.Load<Sprite>("sprites/Maverick angry"), //6
            Resources.Load<Sprite>("sprites/Maverick angry talkin'"), //7
            Resources.Load<Sprite>("sprites/Otello convinced"), //8
            Resources.Load<Sprite>("sprites/Otello thinking"), //9
            Resources.Load<Sprite>("sprites/Otello default"), //10
            Resources.Load<Sprite>("sprites/Otello MENACING"), //11
            Resources.Load<Sprite>("sprites/Otello cigar excited"), //12
            Resources.Load<Sprite>("sprites/Maverick worried"), //13
            Resources.Load<Sprite>("sprites/Maverick worried talkin'"), //14
            Resources.Load<Sprite>("sprites/Samira angry"), //15
            Resources.Load<Sprite>("sprites/Samira pissed-off"), //16
            Resources.Load<Sprite>("sprites/Samira reluctant"), //17
            Resources.Load<Sprite>("sprites/Otello cigar convinced"), //18
            Resources.Load<Sprite>("sprites/Otello excited"), //19
            Resources.Load<Sprite>("sprites/Maverick furious"), //20
            Resources.Load<Sprite>("sprites/Maverick smug"), //21
            Resources.Load<Sprite>("sprites/Otello thinking"), //22
            Resources.Load<Sprite>("sprites/Otello serious"), //23
            Resources.Load<Sprite>("sprites/Otello MENACING"), //24
            Resources.Load<Sprite>("sprites/Samira reluctant"), //25
            Resources.Load<Sprite>("sprites/Maverick determined"), //26
            Resources.Load<Sprite>("sprites/Maverick worried talkin'"), //27
            Resources.Load<Sprite>("sprites/Maverick furious talkin'") //28
        };
            names = new string[]{
            "OTELLO", //1
            "OTELLO", //2
            "OTELLO", //3
            "OTELLO", //4
            "OTELLO", //5
            "MAVERICK", //6
            "MAVERICK", //7
            "OTELLO", //8
            "OTELLO", //9
            "OTELLO", //10
            "OTELLO", //11
            "OTELLO", //12
            "MAVERICK",//13
            "MAVERICK",//14
            "SAMIRA",//15
            "SAMIRA",//16
            "SAMIRA",//17
            "OTELLO",//18
            "OTELLO",//19
            "MAVERICK",//20
            "MAVERICK",//21
            "OTELLO",//22
            "OTELLO",//23
            "OTELLO",//24
            "SAMIRA",//25
            "MAVERICK",//26
            "MAVERICK",//27
            "MAVERICK"//28
            };
            colors = new Color[]{
                Color.red,//1
                Color.red,//2
                Color.red,//3
                Color.red,//4
                Color.red,//5
                lightBlue,//6
                lightBlue,//7
                Color.red,//8
                Color.red,//9
                Color.red,//10
                Color.red,//11
                Color.red,//12
                lightBlue,//13
                lightBlue,//14
                lightBlue,//15
                lightBlue,//16
                lightBlue,//17
                Color.red,//18
                Color.red,//19
                lightBlue,//20
                lightBlue,//21
                Color.red,//22
                Color.red,//23
                Color.red,//24
                lightBlue,//25
                lightBlue,//26
                lightBlue,//27
                lightBlue//28
            };

            StartDialogue();
        }
        if (GlobalVariables.bossCounter == 50 && sentences == null)
        {
            dialogueHappened = false;
            sentences = new string[] {
            "PWEH... THAT WAS A HARD ONE, BUT WE MANAGED TO WIN IN THE END", //1
            "IT SEEMS I WAS THE ONE MISUNDERSTANDING THE SITUATION...", //2
            "VERY WELL, YOU WON FAIR AND SQUARE", //3
            "I SURRENDER, PHANTOM OMERTA IS NOW OVER", //4
            "FINALLY! YOU MADE IT, YOU DORK... OUR JOURNEY IS OVER", //5
            "I WAS EXPECTING YOU TO START COMPLAINING LIKE A SORE LOSER", //6
            "THE OTHERS WERE LIKE THAT", //7
            "IT'S POINTLESS, MY MAFIA WAS COMPLETELY DEFEATED", //8
            "ALRIGHT, ALRIGHT", //9
            "IF YOU TRY SOMETHING AGAIN IN THE FUTURE, GET READY TO HAVE YOUR ASS KICKED" //10
        };

            images = new Sprite[] {
            Resources.Load<Sprite>("sprites/Maverick surprised"), //1
            Resources.Load<Sprite>("sprites/Otello reluctant"), //2
            Resources.Load<Sprite>("sprites/Otello serious"), //3
            Resources.Load<Sprite>("sprites/Otello thinking"), //4
            Resources.Load<Sprite>("sprites/Samira cheeky"), //5
            Resources.Load<Sprite>("sprites/Maverick smug talkin'"), //6
            Resources.Load<Sprite>("sprites/Maverick smug"), //7
            Resources.Load<Sprite>("sprites/Otello default"), //8
            Resources.Load<Sprite>("sprites/Maverick cheeky"), //9
            Resources.Load<Sprite>("sprites/Maverick determined talkin'") //10
        };
            names = new string[]{
            "MAVERICK", //1
            "OTELLO", //2
            "OTELLO", //3
            "OTELLO", //4
            "SAMIRA", //5
            "MAVERICK", //6
            "MAVERICK", //7
            "OTELLO", //8
            "MAVERICK", //9
            "MAVERICK" //10
            };
            colors = new Color[]{
            lightBlue, //1
            Color.red, //2
            Color.red, //3
            Color.red, //4
            lightBlue, //5
            lightBlue, //6
            lightBlue, //7
            Color.red, //8
            lightBlue, //9
            lightBlue //10
            };

            StartDialogue();
        }

        if (dialoguePanel.activeInHierarchy &&
            (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.L) || Input.GetKey(KeyCode.C)))
        {
            if (isTyping)
            {
                StopCoroutine(typingCoroutine);
                isTyping = false;
                dialogueText.text = currentSentence;
            }
            else
            {
                index++;
                if (index < sentences.Length)
                {
                    currentSentence = sentences[index];
                    dialogueImage.sprite = images[index];
                    dialogueName.text = names[index];
                    dialogueName.color = colors[index];
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
        dialoguePanel.SetActive(true);
        currentSentence = sentences[index];
        dialogueImage.sprite = images[index];
        dialogueName.text = names[index];
        dialogueName.color = colors[index];
        typingCoroutine = StartCoroutine(TypeSentence(currentSentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            audioSource.Play();
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
        sentences = null;
        if(GlobalVariables.bossCounter==0)
        {
            spawner.SetActive(true);
        }
        else if (GlobalVariables.bossCounter == 50)
        {
            GlobalVariables.enemiesAlive = 0;
            SceneManager.LoadScene("Victory");
        }
    }
}
