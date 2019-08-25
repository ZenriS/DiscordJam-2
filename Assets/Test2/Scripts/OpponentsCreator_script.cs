using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentsCreator_script : MonoBehaviour
{
    public float AimTimeMin;
    public float AimTimeMax;
    public float ReactionTimeMin;
    public float ReactionTimeMax;
    public float SkillMin;
    public float SkillMax;
    public GameObject OpponentPrefab;
    private Transform _opponentHolder;
    public EnemyActions_script EnemyActions;
    public GameController_script GameController;
    private List<GameObject> _opponents;
    public Color[] HeadColors;
    public Color[] ClothColors;
    public Sprite[] Heads;
    public Sprite[] Torso;
    public Sprite[] Legs;
    public Sprite[] Arms;

    void Awake()
    {
        _opponentHolder = transform.GetChild(2);
        _opponents = new List<GameObject>();
        //CreateOppoents(2);
    }

    public void CreateOppoents()
    {
        CleanUp();
        int a = Random.Range(2, 4);
        for (int i = 0; i < a; i++)
        {
            float aim = Random.Range(AimTimeMin, AimTimeMax);
            float react = Random.Range(ReactionTimeMin, ReactionTimeMax);
            float skill = Random.Range(SkillMin, SkillMax);
            GameObject b = Instantiate(OpponentPrefab, _opponentHolder);
            OpponentsStates_scipt os = b.GetComponent<OpponentsStates_scipt>();
            Color headColor = HeadColors[Random.Range(0, HeadColors.Length)];
            Color torsoColor = ClothColors[Random.Range(0, ClothColors.Length)];
            Color legsColor = ClothColors[Random.Range(0, ClothColors.Length)];
            Sprite headSprite = Heads[Random.Range(0, Heads.Length)];
            Sprite torsoSprite = Torso[Random.Range(0, Torso.Length)];
            Sprite legSprite = Legs[Random.Range(0, Legs.Length)];
            Sprite arms = Arms[Random.Range(0, Arms.Length)];
            int bullets = Random.Range(1, 6);
            os.Config(aim,AimTimeMax,react,ReactionTimeMax,skill,SkillMax,headColor,torsoColor,legsColor,headSprite,torsoSprite,legSprite,arms, bullets);
            _opponents.Add(b);
        }
    }

    void CleanUp()
    {
        foreach (GameObject go in _opponents)
        {
            Destroy(go);
        }
        _opponents.Clear();
    }

}
