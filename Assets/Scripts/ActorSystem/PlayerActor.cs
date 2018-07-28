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
        private PlayerID playerID;
        private PlayerScriptableObject scriptable;
        private SpriteRenderer sr_ = null;
        private RiaCollider mRiaCollider;

        // _playerID      player1 or player2
        // _scriptable    PlayerScriptableObject
        // _name          GameObjectの名前
        // _localPosition localPosition
        public PlayerActor(PlayerID _playerID, PlayerScriptableObject _scriptable, string _name, Vector3 _localPosition)
               : base(_name, _localPosition)
        {
            this.playerID = _playerID;
            this.scriptable = _scriptable;
            this.sr_ = go_.AddComponent<SpriteRenderer>();
            this.sr_.sprite = scriptable.sprite;

            this.mRiaCollider = new RiaCollider(this.go_, this.trans_, this.scriptable);
        }

        protected override void OnCreate()
        {
            
        }

        protected override void OnRun()
        {
            //this.mRiaCollider.Run();
            Move();
            Shot();
        }
        
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
    }
}