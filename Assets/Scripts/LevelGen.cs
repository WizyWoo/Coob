using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGen : MonoBehaviour
{

    [SerializeField]
    private GameObject wall, coin, jumpPad;
    private List<GameObject> walls, coins, jumpPads;
    [SerializeField]
    private Transform player, ground;
    [SerializeField]
    private int spawnDist, pauseTime;
    private int dist, pauseCount;

    private void Start()
    {

        walls = new List<GameObject>();
        coins = new List<GameObject>();
        jumpPads = new List<GameObject>();

    }

    private void FixedUpdate()
    {

        int z = (int)player.position.z;

        if(z > dist)
        {

            if(z % 2 == 0)
                ground.position = new Vector3(0, 0, z + 50);

            for(int i = 0; i < walls.Count; i++)
                if(walls[i].transform.position.z < z - 10 || walls[i].transform.position.y < -10)
                {
                    Destroy(walls[i]);
                    walls.RemoveAt(i);
                }
            for(int i = 0; i < coins.Count; i++)
            {
                if(coins[i] == null)
                {
                    coins.RemoveAt(i);
                    continue;
                }
                if(coins[i].transform.position.z < z - 10)
                {
                    Destroy(coins[i]);
                    coins.RemoveAt(i);
                }
            }
            for(int i = 0; i < jumpPads.Count; i++)
                if(jumpPads[i].transform.position.z < z - 10)
                {
                    Destroy(jumpPads[i]);
                    jumpPads.RemoveAt(i);
                }

            dist = z;

            UIManager.Instance.DistanceUpdate(dist);

            if(pauseCount > 0)
            {

                pauseCount--;
                return;

            }

            if(Random.Range(0, 80) == 0)
            {

                int _height = Random.Range(2, 7), _depth = Random.Range(3, 4);

                for(int i = 0; i < 4; i++)
                {

                    coins.Add(GameObject.Instantiate(coin, new Vector3(0, 1, dist + spawnDist - 6 + i), Quaternion.identity));

                }

                for(int x = -2; x < 3; x++)
                {

                    for(int y = 1; y < _height; y++)
                    {

                        for(int zp = 1; zp < _depth; zp++)
                        {

                            walls.Add(GameObject.Instantiate(wall, new Vector3(x, y, dist + spawnDist + zp), Quaternion.identity));

                        }

                    }

                }

                if(_height > 3)
                {
                    JumpPad _pad = GameObject.Instantiate(jumpPad, new Vector3(0, 0.6f, dist + spawnDist - 4), Quaternion.identity).GetComponent<JumpPad>();
                    _pad.jumpPower = 8 + (_height - 3);
                    _pad.boostStrength = 100;
                    jumpPads.Add(_pad.gameObject);
                }

                pauseCount = pauseTime + 6;
                return;

            }

            if(Random.Range(0, 30) == 0)
            {

                int _height = Random.Range(2, 7);

                for(int x = -2; x < 3; x++)
                {

                    for(int y = 1; y < _height; y++)
                    {

                        walls.Add(GameObject.Instantiate(wall, new Vector3(x, y, dist + spawnDist), Quaternion.identity));

                    }

                }

                if(_height > 3)
                {
                    JumpPad _pad = GameObject.Instantiate(jumpPad, new Vector3(0, 0.6f, dist + spawnDist - 4), Quaternion.identity).GetComponent<JumpPad>();
                    _pad.jumpPower = 8 + (_height - 3);
                    _pad.boostStrength = 40;
                    jumpPads.Add(_pad.gameObject);
                }

                pauseCount = pauseTime;
                return;

            }

            if(Random.Range(0, 6) == 0)
            {

                int _height = Random.Range(1, 4);
                int _xStart = Random.Range(-2, 3), _xEnd = Random.Range(_xStart, 3);

                for(int x = _xStart; x < _xEnd; x++)
                {

                    for(int y = 1; y < _height; y++)
                    {

                        walls.Add(GameObject.Instantiate(wall, new Vector3(x, y, dist + spawnDist), Quaternion.identity));

                    }

                }

                pauseCount = pauseTime;
                return;
                
            }
            
            if(Random.Range(0, 6) == 0)
            {

                coins.Add(GameObject.Instantiate(coin, new Vector3(Random.Range(-2, 3), 1, dist + spawnDist), Quaternion.identity));

                pauseCount = pauseTime;
                return;

            }

            if(Random.Range(0, 20) == 0)
            {

                JumpPad _pad = GameObject.Instantiate(jumpPad, new Vector3(0, 0.6f, dist + spawnDist - 4), Quaternion.identity).GetComponent<JumpPad>();
                _pad.jumpPower = 10;
                _pad.boostStrength = 10;
                jumpPads.Add(_pad.gameObject);

                pauseCount = pauseTime + 6;
                return;

            }

        }

    }

}
