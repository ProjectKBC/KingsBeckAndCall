using UnityEngine;

namespace Ria
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
        private PlayerScriptableObject scriptObj_;
        private SpriteRenderer sr_ = null;
        #endregion

        #region Main Function
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="_playerID">player1 or player2</param>
        /// <param name="_sto">PlayerScriptableObject</param>
        /// <param name="_name">GameObjectの名前</param>
        /// <param name="_localPosition">localPosition</param>
        public PlayerActor(PlayerID _playerID, PlayerScriptableObject _sto, string _name, Vector3 _localPosition)
               : base(_name, _localPosition)
        {
            playerID = _playerID;
            scriptObj_ = _sto;
            sr_ = go_.AddComponent<SpriteRenderer>();
            sr_.sprite = scriptObj_.sprite;
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
                        trans_.position += Vector3.up * scriptObj_.speed * Time.deltaTime * 100;
                    }
                    if (Input.GetKey(KeyCode.DownArrow))
                    {
                        trans_.position += Vector3.down * scriptObj_.speed * Time.deltaTime * 100;
                    }
                    if (Input.GetKey(KeyCode.RightArrow))
                    {
                        trans_.position += Vector3.right * scriptObj_.speed * Time.deltaTime * 100;
                    }
                    if (Input.GetKey(KeyCode.LeftArrow))
                    {
                        trans_.position += Vector3.left * scriptObj_.speed * Time.deltaTime * 100;
                    }
                    break;

                case PlayerID.Player2:
                    /*
                    if (Input.GetKey(KeyCode.UpArrow))
                    {
                        trans_.position += Vector3.up    * scriptObj_.speed * Time.deltaTime * 100;
                    }
                    if (Input.GetKey(KeyCode.DownArrow))
                    {
                        trans_.position += Vector3.down  * scriptObj_.speed * Time.deltaTime * 100;
                    }
                    if (Input.GetKey(KeyCode.RightArrow))
                    {
                        trans_.position += Vector3.right * scriptObj_.speed * Time.deltaTime * 100;
                    }
                    if (Input.GetKey(KeyCode.LeftArrow))
                    {
                        trans_.position += Vector3.left  * scriptObj_.speed * Time.deltaTime * 100;
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