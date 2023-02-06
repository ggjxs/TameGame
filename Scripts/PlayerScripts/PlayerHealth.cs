using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    private GameManager GameM;
    private PlayerController PlCon;

    public float PlayerHp{ get; private set; }

    [SerializeField] private float Interval;
    [SerializeField] private float Hp,knockBackPower;//Player��Hp,�m�b�N�o�b�N�����
    [SerializeField] private GameObject GameOverPanel;//�t�F�[�h�p�l���̎擾

    private Rigidbody2D Rbody;
    private string EnemyName = "Enemy";//�G�L������tag�̖��O
    

    //�_�ł̊Ԋu
    [SerializeField] private float flashInterval;
    //�_�ł�����Ƃ��̃��[�v�J�E���g
    [SerializeField] private int loopCount;
    //�_�ł����邽�߂�SpriteRenderer
    private SpriteRenderer sp;
    private Slider Hpslider;
    private Image PanelImage;
    private float Alpha;

    //�v���C���[�̏�ԗp�񋓌^�i�m�[�}���A�_���[�W�A���G��3��ށj

    private enum PlayerState
    {
        NOMAL,
        DAMAGED,
        MUTEKI
    }
    //STATE�^�̕ϐ�
    PlayerState state;

    IEnumerator _hit()
    {
        sp.color = Color.black;
        for (int i = 0; i < loopCount; i++)
        {

            yield return new WaitForSeconds(flashInterval);
            sp.enabled = false;
            yield return new WaitForSeconds(flashInterval);
            sp.enabled = true;
            if (i > 20)
            {
                state = PlayerState.MUTEKI;
                sp.color = Color.green;
            }
        }
        state = PlayerState.NOMAL;
        sp.color = Color.white;
    }
    private IEnumerator GameOver()
    {
        for (int i = 0; i < loopCount; i++)
        {
            Debug.Log("a");
            Alpha += 0.05f;
            yield return new WaitForSeconds(Interval);
            PanelImage.color = new Color(0, 0, 0, Alpha);
        }
        
    }
    private void Awake()
    {
        PlCon = GetComponent<PlayerController>();
        GameM = GameObject.Find("GameManager").GetComponent<GameManager>();
        //SpriteRenderer�i�[
        sp = GetComponent<SpriteRenderer>();
        Hpslider = GameObject.Find("PlayerSlider").GetComponent<Slider>();
        Rbody = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        PanelImage = GameOverPanel.GetComponent<Image>();
        PlayerHp = Hp;
        Hpslider.maxValue = PlayerHp;
    }

    void Update()
    {
        Hpslider.value = PlayerHp;

        //�X�e�[�g���_���[�W�Ȃ烊�^�[��
        if (state == PlayerState.DAMAGED)
        {
            return;
        }
    }
    void FixedUpdate()
    {
        if (PlayerHp <= 0)
        {
            StartCoroutine(GameOver());
            gameObject.SetActive(false);
        }
    }
    private void knockBack()
    {
        var AttackHori = PlCon.AttackHori;


        if (AttackHori == 0)
            Rbody.AddForce(new Vector2(-knockBackPower, Rbody.velocity.y + 1), ForceMode2D.Impulse);
        else if (AttackHori > 0)
            Rbody.AddForce(new Vector2(-knockBackPower, Rbody.velocity.y + 1), ForceMode2D.Impulse);
        else if (AttackHori < 0)
            Rbody.AddForce(new Vector2(knockBackPower, Rbody.velocity.y + 1), ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(EnemyName))    
        {
            //���G��ԂȂ珈�����s��Ȃ�
            if (state == PlayerState.MUTEKI) 
            {
                return;
            }

            if (state == PlayerState.NOMAL)
            {
                knockBack();
                PlayerHp -= GameM.PlayerAttckD;
                state = PlayerState.DAMAGED;
                //�R���[�`�����J�n
                StartCoroutine(_hit());

            }


        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(EnemyName))
        {
            //���G��ԂȂ珈�����s��Ȃ�
            if (state == PlayerState.MUTEKI)
            {
                return;
            }

            if (state == PlayerState.NOMAL)
            {
                PlayerHp -= GameM.PlayerAttckD;
                state = PlayerState.DAMAGED;
                //�R���[�`�����J�n
                StartCoroutine(_hit());

            }
        }
    }
}
