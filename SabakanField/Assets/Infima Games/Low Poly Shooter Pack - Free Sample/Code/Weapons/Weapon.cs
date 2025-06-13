// Copyright 2021, Infima Games. All Rights Reserved.

using UnityEngine;

namespace InfimaGames.LowPolyShooterPack
{
    /// <summary>
    /// Weapon. This class handles most of the things that weapons need.
    /// </summary>
    public class Weapon : WeaponBehaviour
    {
        #region FIELDS SERIALIZED

        [Header("Firing")]

        [Tooltip("Is this weapon automatic? If yes, then holding down the firing button will continuously fire.")]
        [SerializeField, Header("セミorフル")]
        private bool automatic;

        [SerializeField, Header("ショットガンor単発")]
        private bool shotGan;


        [Tooltip("How fast the projectiles are."), Header("マズルフラッシュの調整")]
        [SerializeField]
        private float projectileImpulse = 400.0f;

        [Tooltip("Amount of shots this weapon can shoot in a minute. It determines how fast the weapon shoots.")]
        [SerializeField, Header("次の弾が出るまでのインターバル、大きいほど早い")]
        private int roundsPerMinutes = 200;

        [Tooltip("Mask of things recognized when firing.")]
        [SerializeField, Header("発射時に認識されるマスク")]
        private LayerMask mask;

        [Header("Animation")]

        [Tooltip("Transform that represents the weapon's ejection port, meaning the part of the weapon that casings shoot from.")]
        [SerializeField, Header("武器の射出口（薬莢が発射される部分）を表すトランスフォーム")]
        private Transform socketEjection;

        [Header("Resources")]

        [Tooltip("Casing Prefab."), Header("薬莢")]
        [SerializeField]
        private GameObject prefabCasing = null;

        [Tooltip("Projectile Prefab. This is the prefab spawned when the weapon shoots.")]
        [SerializeField, Header("発射体プレハブ これは武器が発射されるときに生成されるプレハブ")]
        private GameObject prefabProjectile = null;

        [Tooltip("The AnimatorController a player character needs to use while wielding this weapon.")]
        [SerializeField, Header("銃アニメーション")]
        public RuntimeAnimatorController controller;

        [Tooltip("Weapon Body Texture."), Header("UI左下に映る銃のスプライト")]
        [SerializeField]
        private Sprite spriteBody;

        [Header("Audio Clips Holster")]

        [Tooltip("Holster Audio Clip.")]
        [SerializeField]
        private AudioClip audioClipHolster;

        [Tooltip("Unholster Audio Clip.")]
        [SerializeField]
        private AudioClip audioClipUnholster;

        [Header("Audio Clips Reloads")]

        [Tooltip("Reload Audio Clip.")]
        [SerializeField]
        private AudioClip audioClipReload;

        [Tooltip("Reload Empty Audio Clip.")]
        [SerializeField]
        private AudioClip audioClipReloadEmpty;

        [Header("Audio Clips Other")]

        [Tooltip("AudioClip played when this weapon is fired without any ammunition.")]
        [SerializeField]
        private AudioClip audioClipFireEmpty;

        private CharacterBehaviour playerCharacter;
        // スコプ系
        [SerializeField]
        private GameObject weponScope = null;
        private GameObject _playerCamera = null;
        private float adsDirayTime = 0.0f;

        #endregion

        #region FIELDS

        /// <summary>
        /// Weapon Animator.
        /// </summary>
        private Animator animator;
        /// <summary>
        /// Attachment Manager.
        /// </summary>
        private WeaponAttachmentManagerBehaviour attachmentManager;

        /// <summary>
        /// Amount of ammunition left.
        /// </summary>
        private int ammunitionCurrent;

        #region Attachment Behaviours

        /// <summary>
        /// Equipped Magazine Reference.
        /// </summary>
        private MagazineBehaviour magazineBehaviour;
        /// <summary>
        /// Equipped Muzzle Reference.
        /// </summary>
        private MuzzleBehaviour muzzleBehaviour;

        #endregion

        /// <summary>
        /// The GameModeService used in this game!
        /// </summary>
        private IGameModeService gameModeService;
        /// <summary>
        /// The main player character behaviour component.
        /// </summary>
        private CharacterBehaviour characterBehaviour;

        /// <summary>
        /// The player character's camera.
        /// </summary>
        private Transform playerCamera;

        #endregion

        #region UNITY

        protected override void Awake()
        {
            //Get Animator.
            animator = GetComponent<Animator>();
            //Get Attachment Manager.
            attachmentManager = GetComponent<WeaponAttachmentManagerBehaviour>();

            // カメラが非アクティブだと取得できないので親から数珠繋ぎで取得
            _playerCamera = GameObject.Find("head").transform.GetChild(0).gameObject;

            //Cache the game mode service. We only need this right here, but we'll cache it in case we ever need it again.
            gameModeService = ServiceLocator.Current.Get<IGameModeService>();
            //Cache the player character.
            characterBehaviour = gameModeService.GetPlayerCharacter();
            //Cache the world camera. We use this in line traces.
            playerCamera = characterBehaviour.GetCameraWorld().transform;

            //Get Player Character.
            playerCharacter = ServiceLocator.Current.Get<IGameModeService>().GetPlayerCharacter();
            if (weponScope != null) weponScope.SetActive(false);
        }
        protected override void Start()
        {
            #region Cache Attachment References

            //Get Magazine.
            magazineBehaviour = attachmentManager.GetEquippedMagazine();
            //Get Muzzle.
            muzzleBehaviour = attachmentManager.GetEquippedMuzzle();

            #endregion

            //Max Out Ammo.
            ammunitionCurrent = magazineBehaviour.GetAmmunitionTotal();
        }

        protected override void Update()
        {
            // スナイパーなどスコープがあるものはads時スコープでのぞくようにする
            ScopeMode();
        }

        #endregion

        #region GETTERS

        public override Animator GetAnimator() => animator;

        public override Sprite GetSpriteBody() => spriteBody;

        public override AudioClip GetAudioClipHolster() => audioClipHolster;
        public override AudioClip GetAudioClipUnholster() => audioClipUnholster;

        public override AudioClip GetAudioClipReload() => audioClipReload;
        public override AudioClip GetAudioClipReloadEmpty() => audioClipReloadEmpty;

        public override AudioClip GetAudioClipFireEmpty() => audioClipFireEmpty;

        public override AudioClip GetAudioClipFire() => muzzleBehaviour.GetAudioClipFire();

        public override int GetAmmunitionCurrent() => ammunitionCurrent;

        public override int GetAmmunitionTotal() => magazineBehaviour.GetAmmunitionTotal();

        public override bool IsAutomatic() => automatic;
        public override float GetRateOfFire() => roundsPerMinutes;

        public override bool IsFull() => ammunitionCurrent == magazineBehaviour.GetAmmunitionTotal();
        public override bool HasAmmunition() => ammunitionCurrent > 0;

        public override RuntimeAnimatorController GetAnimatorController() => controller;
        public override WeaponAttachmentManagerBehaviour GetAttachmentManager() => attachmentManager;

        #endregion

        #region METHODS

        public override void Reload()
        {
            //Play Reload Animation.
            animator.Play(HasAmmunition() ? "Reload" : "Reload Empty", 0, 0.0f);
        }
        public override void Fire(float spreadMultiplier = 1.0f)
        {
            //We need a muzzle in order to fire this weapon!
            if (muzzleBehaviour == null)
                return;

            //Make sure that we have a camera cached, otherwise we don't really have the ability to perform traces.
            if (playerCamera == null)
                return;

            //Get Muzzle Socket. This is the point we fire from.
            Transform muzzleSocket = muzzleBehaviour.GetSocket();

            //Play the firing animation.
            const string stateName = "Fire";
            animator.Play(stateName, 0, 0.0f);
            //Reduce ammunition! We just shot, so we need to get rid of one!
            // 弾薬を減らす！さっき撃ったばかりだから、1発減らさないと！
            ammunitionCurrent = Mathf.Clamp(ammunitionCurrent - 1, 0, magazineBehaviour.GetAmmunitionTotal());

            //Play all muzzle effects.
            muzzleBehaviour.Effect();

            //Determine the rotation that we want to shoot our projectile in.
            // 弾丸を発射する角度を決める。
            // この時歩き撃ちをしていたならば弾をぶらす

            Vector3 angle = Vector3.zero;

            // 走り撃ち(Shift)
            if (playerCharacter.IsRunning())
            {
                float randomX = Random.Range(-25.0f, 25.0f);
                float randomY = Random.Range(-25.0f, 25.0f);
                angle = playerCamera.transform.eulerAngles + new Vector3(randomX, randomY, 0);
            }
            // 歩き撃ち
            else if (playerCharacter.IsWalking())
            {
                float randomX = Random.Range(-15.0f, 15.0f);
                float randomY = Random.Range(-15.0f, 15.0f);
                angle = playerCamera.transform.eulerAngles + new Vector3(randomX, randomY, 0);
            }
            else
            {
                angle = playerCamera.transform.eulerAngles;
            }

            //Spawn projectile from the projectile spawn point.
            // 発射体スポーンポイントから発射体をスポーンする。
            // 薬莢のこと
            GameObject projectile = Instantiate(prefabProjectile);
            projectile.transform.position=muzzleSocket.position;
            projectile.transform.eulerAngles = angle;

            prefabProjectile.transform.eulerAngles = playerCamera.forward;

            //Add velocity to the projectile.
            // 弾丸に速度を加える。
            projectile.GetComponent<Rigidbody>().velocity = projectile.transform.forward* projectileImpulse;
            if (!shotGan) return;

            float yAngle = Mathf.Atan2(projectile.transform.forward.x, projectile.transform.forward.z);

            for(int i = 0; i < 6; i++) 
            {


                projectile = Instantiate(prefabProjectile);
                projectile.transform.position = muzzleSocket.position;
                projectile.transform.eulerAngles = angle;

                Vector3 randomAngle = new Vector3(Random.Range(-50, 50), Random.Range(-50, 50), Random.Range(-50, 50))*0.005f;

                //Add velocity to the projectile.
                // 弾丸に速度を加える。
                projectile.GetComponent<Rigidbody>().velocity = (projectile.transform.forward+ randomAngle) * projectileImpulse;




            }


        }

        private void ScopeMode()
        {
            if (weponScope == null)
            {
                return;
            }
            bool holding = PlayerManager.GetIsHoldingAds();
            if (adsDirayTime >= 0.3f)
            {
                _playerCamera.SetActive(!holding);
                weponScope.SetActive(holding);
                PlayerManager.SetIsScope(true);
            }
            if (holding)
            {
                adsDirayTime += Time.deltaTime;
            }
            else
            {
                PlayerManager.SetIsScope(false);
                adsDirayTime = 0.0f; 
            }
        }

        public override void FillAmmunition(int amount)
        {
            //Update the value by a certain amount.
            ammunitionCurrent = amount != 0 ? Mathf.Clamp(ammunitionCurrent + amount,
                0, GetAmmunitionTotal()) : magazineBehaviour.GetAmmunitionTotal();
        }

        public override void EjectCasing()
        {
            //Spawn casing prefab at spawn point.
            if (prefabCasing != null && socketEjection != null)
                Instantiate(prefabCasing, socketEjection.position, socketEjection.rotation);
        }

        #endregion
    }
}