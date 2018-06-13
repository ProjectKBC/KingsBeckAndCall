using UnityEngine;

namespace old_0609
{
    public enum PlayerID
    {
        Player1,
        Player2,
    }

    public class PlayerActor : Actor
    {
        #region Member
        private PlayerID playerID;
        private PlayerScriptableObject scriptable;
        private SpriteRenderer sr_ = null;
        #endregion

        #region Main Function
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="_playerID">player1 or player2</param>
        /// <param name="_scriptable">PlayerScriptableObject</param>
        /// <param name="_name">GameObjectの名前</param>
        /// <param name="_localPosition">localPosition</param>
        public PlayerActor(PlayerID _playerID, PlayerScriptableObject _scriptable, string _name, Vector3 _localPosition)
               : base(_name, _localPosition)
        {
            this.playerID = _playerID;
            this.scriptable = _scriptable;
            this.sr_ = go_.AddComponent<SpriteRenderer>();
            this.sr_.sprite = scriptable.sprite;
        }

        /// <summary>
        /// PlayerManager.Update()で呼び出される
        /// </summary>
        public override void OnUpdate()
        {
            Move();
            Shot();
        }
        #endregion

        #region Private Function
        /// <summary>
        /// 移動処理
        /// </summary>
        private void Move()
        {
            switch (playerID)
            {
                case PlayerID.Player1:
                    if (Input.GetKey(KeyCode.UpArrow))
                    {
                        trans_.position += Vector3.up * scriptable.speed * Time.deltaTime * 100;
                    }
                    if (Input.GetKey(KeyCode.DownArrow))
                    {
                        trans_.position += Vector3.down * scriptable.speed * Time.deltaTime * 100;
                    }
                    if (Input.GetKey(KeyCode.RightArrow))
                    {
                        trans_.position += Vector3.right * scriptable.speed * Time.deltaTime * 100;
                    }
                    if (Input.GetKey(KeyCode.LeftArrow))
                    {
                        trans_.position += Vector3.left * scriptable.speed * Time.deltaTime * 100;
                    }
                    break;

                case PlayerID.Player2:
                    /*
                    if (Input.GetKey(KeyCode.UpArrow))
                    {
                        trans_.position += Vector3.up    * scriptable.speed * Time.deltaTime * 100;
                    }
                    if (Input.GetKey(KeyCode.DownArrow))
                    {
                        trans_.position += Vector3.down  * scriptable.speed * Time.deltaTime * 100;
                    }
                    if (Input.GetKey(KeyCode.RightArrow))
                    {
                        trans_.position += Vector3.right * scriptable.speed * Time.deltaTime * 100;
                    }
                    if (Input.GetKey(KeyCode.LeftArrow))
                    {
                        trans_.position += Vector3.left  * scriptable.speed * Time.deltaTime * 100;
                    }
                    */
                    break;
            }
        }

        private void Shot()
        {
            switch (playerID)
            {
                case PlayerID.Player1:
                    if (Input.GetKey(KeyCode.Z))
                    {
                        Debug.Log(go_.name + ":shot!");
                    }
                    break;

                case PlayerID.Player2:
                    /*
                    if (Input.GetKey(KeyCode.Z))
                    {
                        Debug.Log(go_.name + ":shot!");
                    }
                    */
                    break;
            }
        }
        #endregion
    }
}