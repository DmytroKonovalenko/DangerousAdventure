using ScriptsPlayer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HeroParameters
{
    #region Private_Variables

    private int nextExperienceLevel = 100;

    private int previousExperienceLevel = 0;

    private int level = 1;

    private float maxHealth = 100;

    private float damage = 20;

    private float speed = 50;

    [SerializeField]
    private float experience = 0;

    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public float Damage { get => damage; set => damage = value; }
    public float Speed { get => speed; set => speed = value; }
    public float Experience 
    { get
        {
            return experience;
        }
        set
        {
            experience = value;
            CheckExperienceLevel();
        }
    }

    #endregion

    private void CheckExperienceLevel()
    {
        if (experience > nextExperienceLevel)
        {
            level++;
            int addition = previousExperienceLevel;
            previousExperienceLevel = nextExperienceLevel;
            nextExperienceLevel += addition;

            switch (Random.Range(0, 3))
            {
                case 0: maxHealth++; break;
                case 1: damage++; break;
                case 2: speed++; break;
            }
            GameController.Instance.LevelUp();
          
        }
    }

   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
