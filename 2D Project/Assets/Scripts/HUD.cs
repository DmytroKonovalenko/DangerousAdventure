using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ScriptsPlayer
{
    public class HUD : MonoBehaviour
    {


        [SerializeField]
        private InventoryUIButton inventoryItemPrefab;

        [SerializeField]
        private Transform inventoryContainer;

        [SerializeField]
        private Text damageValue;

        [SerializeField]
        private Text healthValue;

        [SerializeField]
        private Text speedValue;

        [SerializeField]
        private Slider healthBar;

        [SerializeField]
        private Text scoreLabel;

        [SerializeField]
        private GameObject inventoryWindow;

        [SerializeField]
        private GameObject levelWonWindow;

        [SerializeField]
        private GameObject levelLoseWindow;

        [SerializeField]
        private GameObject optionsWindow;

        [SerializeField]
        private GameObject inGameMenu;

        static private HUD _instance;

        public static HUD Instance { get { return _instance; } }

        public Slider HealthBar { get => healthBar; set => healthBar = value; }
        public Text DamageValue { get => damageValue; set => damageValue = value; }
        public Text HealthValue { get => healthValue; set => healthValue = value; }
        public Text SpeedValue { get => speedValue; set => speedValue = value; }

        private void HandeOnUpdateHeroParameters(HeroParameters parameters)
        {
            HealthBar.maxValue = parameters.MaxHealth;
            HealthBar.value = parameters.MaxHealth;
            UpdateCharacterValues(parameters.MaxHealth, parameters.Speed, parameters.Damage);
        }

        private void Awake()
        {   
            _instance = this;
            GameController.Instance.AudioManager.PlayMusic(false);
        }

        private void Start()
        {
            LoadInventory();

            GameController.Instance.OnUpdateHeroParameters += HandeOnUpdateHeroParameters;

            GameController.Instance.StartNewLevel();
        }
        private void OnDestroy()
        {
            GameController.Instance.OnUpdateHeroParameters -= HandeOnUpdateHeroParameters;
        }

        public void ShowWindow(GameObject window)
        {
            window.GetComponent<Animator>().SetBool("Open", true);
            GameController.Instance.State = GameState.Pause;
        }

        public void HideWindow(GameObject window)
        {
            window.GetComponent<Animator>().SetBool("Open", false);
            GameController.Instance.State = GameState.Play;
        }
        public void SetScore(string scoreValue)
        {
            scoreLabel.text = scoreValue;
        }
        public void UpdateCharacterValues(float newHealth, float newSpeed, float newDamage) 
        {
            healthValue.text = newHealth.ToString();
            
            speedValue.text = newSpeed.ToString();
            
            damageValue.text = newDamage.ToString();
            
        }

        public InventoryUIButton AddNewInventoryItem(InventoryItem itemData)
        {
            InventoryUIButton newItem = Instantiate(inventoryItemPrefab) as InventoryUIButton;
            newItem.transform.SetParent(inventoryContainer);
            newItem.transform.localScale = new Vector2(1, 1);
            newItem.ItemData = itemData;
            return newItem;
        }

        public void ButtonNext()
        {
            GameController.Instance.LoadNextLevel();
        }

        public void ButtonRestart()
        {
            GameController.Instance.RestartLevel();
        }

        public void ButtonMainMenu()
        {
            GameController.Instance.LoadMainMenu();
        }

        public void ShowLevelWonWindow()
        {
            ShowWindow(levelWonWindow);
        }

        public void ShowLevelLoseWindow()
        {
            ShowWindow(levelLoseWindow);
        }

        public void SetSoundVolume(Slider slider)
        {
            GameController.Instance.AudioManager.SfxVolume = slider.value;
        }
        public void SetMusicVolume(Slider slider)
        {
            GameController.Instance.AudioManager.MusicVolume = slider.value;
        }

        public void LoadInventory()
        {
            InventoryUsedCallback callback = new InventoryUsedCallback(GameController.Instance.InventoryItemUsed);

            for (int i = 0; i < GameController.Instance.Inventory.Count; i++)
            {
                InventoryUIButton newItem = HUD.Instance.AddNewInventoryItem(GameController.Instance.Inventory[i]);
                newItem.Callback = callback;
            }
        }
    }

    
}
