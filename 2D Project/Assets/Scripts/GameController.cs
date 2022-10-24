
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
public enum GameState {Play, Pause}
public delegate void InventoryUsedCallback(InventoryUIButton item);
public delegate void UpdateHeroParametersHanlder(HeroParameters parameters);
namespace ScriptsPlayer
{
    public class GameController : MonoBehaviour
    {
        public event UpdateHeroParametersHanlder OnUpdateHeroParameters;    

        [SerializeField]
        private HeroParameters hero;

        [SerializeField]
        private Audio audioManager;

        [SerializeField]
        private int dragonKillExperience;

        //[SerializeField]
        //private float maxHealth;

        private Knight knight;

        private int score;

        private int dragonHitScore = 10;

        private int dragonKillScore = 50;

        [SerializeField]
        private List<InventoryItem> inventory;

        private GameState state;

        static private GameController _instance;

        public static GameController Instance 
        {
            get
            {
                if (_instance == null)
                {
                    GameObject gameController = Instantiate(Resources.Load("Prefabs/GameController")) as GameObject;
                    _instance = gameController.GetComponent<GameController>();
                }

                return _instance;
            }

        }

        private int Score 
        { 
            get 
            { 
                return score; 
            } 
            set 
            {
                if (value != score)
                {
                    score = value;
                    HUD.Instance.SetScore(score.ToString());
                }
            } 
        }

       // public float MaxHealth { get => maxHealth; set => maxHealth = value; }
        public GameState State
        {
            get
            {
                return state;
            }
            set
            {
                if (value == GameState.Play)
                {
                    Time.timeScale = 1.0f;
                }
                else
                {
                    Time.timeScale = 0.0f;
                }
                state = value;
            }
        }

        public Knight Knight { get => knight; set => knight = value; }
        public Audio AudioManager { get => audioManager; set => audioManager = value; }
        public List<InventoryItem> Inventory { get => inventory; set => inventory = value; }
        public HeroParameters Hero { get => hero; set => hero = value; }

        private void Awake()
        {
            InitializeAudioManager();

            if (_instance == null)
            {
                _instance = this;
            }
            else 
            {
                if (_instance != this)
                {
                    Destroy(gameObject);
                }
            }
            DontDestroyOnLoad(gameObject);
            State = GameState.Play;
            Inventory = new List<InventoryItem>();
            
           
        }
        public void StartNewLevel()
        {
            HUD.Instance.SetScore(Score.ToString());

            if (OnUpdateHeroParameters != null) 
            {
                OnUpdateHeroParameters(hero);
            }

            State = GameState.Play;
            // if (knight)
            //{
            // HUD.Instance.UpdateCharacterValues(MaxHealth, knight.Speed, knight.Damage);
            // HUD.Instance.HealthBar.maxValue = maxHealth;
            // HUD.Instance.HealthBar.value = maxHealth;
           // }
        }

        public void LevelUp()
        {
            if (OnUpdateHeroParameters != null)
            {
                OnUpdateHeroParameters(hero);
            }
        }

        public void Hit(IDestrucable victim)
        {
            
            AudioManager.PlaySoundRandomPitch("DM-CGS-46");

                if (victim.GetType() == typeof(Dragon))
            {
                Score += dragonHitScore;
               // if (victim.Health > 0)
               // {
               //     Score += dragonHitScore;
               // }
                //else 
                //{
                //    Score += dragonKillScore;
                //}
                //Debug.Log("DragonHit.Curent score" + Score);
            }

            if (victim.GetType() == typeof(Knight))
            {
                HUD.Instance.HealthBar.value = victim.Health;
            }
        }

        public void Killed(IDestrucable victim)
        {
            if (victim.GetType() == typeof(Dragon))
            {
                Score += dragonKillScore;
                hero.Experience += dragonKillExperience;
                Destroy((victim as MonoBehaviour).gameObject);
            }
            if (victim.GetType() == typeof(Knight))
            {
                GameOver();
            }
        }

        public void AddNewInventoryItem(InventoryItem itemData)
        {
            AudioManager.PlaySound("DM-CGS-45");
            InventoryUIButton newUiButton = HUD.Instance.AddNewInventoryItem(itemData);
            InventoryUsedCallback callback = new InventoryUsedCallback(InventoryItemUsed);
            newUiButton.Callback = callback;
            Inventory.Add(itemData);
           
        }

        public void InventoryItemUsed(InventoryUIButton item) 
        {
            switch (item.ItemData.CrystallType) 
            {
                case CrystallType.Blue:
                    // knight.Speed += item.ItemData.Quantity / 10f; 
                    hero.Speed += item.ItemData.Quantity / 10f;
                    break;

                case CrystallType.Red:
                    //knight.Damage += item.ItemData.Quantity / 10f;
                    hero.Damage += item.ItemData.Quantity / 10f;
                    break;

                case CrystallType.Green:
                    //MaxHealth += item.ItemData.Quantity / 10f;
                    // knight.Health = MaxHealth;
                    // HUD.Instance.HealthBar.maxValue = MaxHealth;
                    //  HUD.Instance.HealthBar.value = MaxHealth;
                    hero.MaxHealth += item.ItemData.Quantity / 10f; 
                    break;

                default:
                    Debug.LogError("Wrong Crystall Type!");
                    break;
            }
            Inventory.Remove(item.ItemData);
            AudioManager.PlaySound("DM-CGS-32");
            Destroy(item.gameObject);
            //HUD.Instance.UpdateCharacterValues(MaxHealth, knight.Speed, knight.Damage);
            if (OnUpdateHeroParameters != null)
            {
                OnUpdateHeroParameters(hero);
            }
        }

        public void PrincessFound()
        {
            AudioManager.PlaySound("DM-CGS-18");
            HUD.Instance.ShowLevelWonWindow();
        }

        public void LoadNextLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }

        public void GameOver()
        {
            HUD.Instance.ShowLevelLoseWindow();
        }

        private void InitializeAudioManager()
        {
            audioManager.SourceSFX = gameObject.AddComponent<AudioSource>();
            audioManager.SourceMusic = gameObject.AddComponent<AudioSource>();
            audioManager.SourceRandomPitchSFX = gameObject.AddComponent<AudioSource>();
            gameObject.AddComponent<AudioListener>();
        }

        
    }
    
}
