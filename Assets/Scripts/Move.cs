﻿using UnityEngine;
using System.Collections;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using Completed;

using Assets.Scripts;

public class Move : MonoBehaviour
{
    public enum MoveDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    public Vector2 end;
    private Rigidbody2D rb;
    public float speed=1.0f;
    private GameManager gameManager;
    private Astar amplitud;
    private Vector2 last;
    float deltaTime= 0.0f;
    public IMind MindController { get; set; }

    public bool keyControl = false;
    private float cooldown = 1.0f;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();


        MindController = new RandomMind();
    }

    public void MoveLeft()
    {
        if (end.x == 0)
            return;
        end += Vector2.left;
        Debug.Log("Left\n");
    }

    public void MoveUp()
    {
        if (end.y == gameManager.Map.rows)
            return;
        end += Vector2.up;
        Debug.Log("Up\n");
    }

    public void MoveDown()
    {
        if (end.y == 0)
            return;
        end += Vector2.down;
        Debug.Log("Down\n");
    }

    public void MoveRight()
    {
        if (end.y == gameManager.Map.cols)
            return;
        end += Vector2.right;
        Debug.Log("Right\n");
    }

    public void KeyControl()
    {
        cooldown -= Time.deltaTime;

        if (cooldown < 0)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                MoveDown();
                cooldown = 1.0f;
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                MoveUp();
                cooldown = 1.0f;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                MoveLeft();
                cooldown = 1.0f;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                MoveRight();
                cooldown = 1.0f;
            }
        }
    }

    public bool AtDestination()
    {
        if (Vector2.Distance(last, end) > float.Epsilon)
        {
            var dist = Vector2.Distance(transform.position, end);
            return dist < 0.1;
        }
        else
        {
                return true;
            }
    }

    // Update is called once per frame
    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        if (gameManager == null)
        {
            var gO = GameObject.Find("GameManager") as GameObject;
            gameManager = gO.GetComponent<GameManager>();
        }
        if(amplitud == null)
        {
            amplitud = new Astar(new Vector2(0, 0), gameManager.Map,new Vector2(13,7));
            MindController = amplitud;
        }
        if (!AtDestination())
        {
            MoveNeed = false;
            Vector2 pos = Vector2.Lerp(transform.position, end, Time.deltaTime*speed);
            rb.MovePosition(pos);
            
        }
        else
        {
            transform.position = end;
            MoveNeed = true;
            if (keyControl)
            {
                KeyControl();
            }
            else
            {
                if (MindController != null)
                {
                    if (MoveNeed)
                    {
                        last = new Vector2(end.x, end.y);
                        MoveDirection dir = MindController.GetNextMove(end, gameManager.Map);
                        if (dir == MoveDirection.Left)
                        {
                            MoveLeft();
                        }
                        if (dir == MoveDirection.Up)
                        {
                            MoveUp();
                        }
                        if (dir == MoveDirection.Down)
                        {
                            MoveDown();
                        }
                        if (dir == MoveDirection.Right)
                        {
                            MoveRight();
                        }
                    }
                    
                }
                    
            }
        }
    }
    void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 100;
        style.normal.textColor = new Color(0.0f, 1.0f, 0.0f, 1.0f);
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        GUI.Label(rect, text, style);
    }
    public bool MoveNeed { get; set; }
}