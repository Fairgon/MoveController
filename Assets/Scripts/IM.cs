using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Содержит кнопки управления персонажем.
/// </summary>
namespace InputManager
{
    public enum KeyType
    {
        Forward,
        Backward,
        Left,
        Right,
        Jump,
        Sprint,
        Walk,
    }

    public static class Key
    {
        // Кнопки движения.
        public static KeyCode Forward { get; set; }
        public static KeyCode Backward { get; set; }
        public static KeyCode Right { get; set; }
        public static KeyCode Left { get; set; }

        // Кнопки основных действий.
        public static KeyCode Jump { get; set; }
        public static KeyCode Sprint { get; set; }
        public static KeyCode Walk { get; set; }
        
        // Назначет кнопки по дефолту либо берет те которые выставил игрок. 
        public static void LoadKeys()
        {
            Forward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ForwardKey", "W"));
            Backward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("BackwardKey", "S"));
            Right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("RightKey", "D"));
            Left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("LeftKey", "A"));

            Jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("JumpKey", "Space"));
            Sprint = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("SprintKey", "LeftShift"));
            Walk = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("WalkKey", "LeftControl"));
        }

        public static void SetDefaultKeys()
        {
            PlayerPrefs.SetString("ForwardKey", "W");
            PlayerPrefs.SetString("BackwardKey", "S");
            PlayerPrefs.SetString("RightKey", "D");
            PlayerPrefs.SetString("LeftKey", "A");

            PlayerPrefs.SetString("JumpKey", "Space");
            PlayerPrefs.SetString("SprintKey", "LeftShift");
            PlayerPrefs.SetString("WalkKey", "LeftControl");
        }

        public static void SaveKeys()
        {
            PlayerPrefs.SetString("ForwardKey", Forward.ToString());
            PlayerPrefs.SetString("BackwardKey", Backward.ToString());
            PlayerPrefs.SetString("RightKey", Right.ToString());
            PlayerPrefs.SetString("LeftKey", Left.ToString());

            PlayerPrefs.SetString("JumpKey", Jump.ToString());
            PlayerPrefs.SetString("SprintKey", Sprint.ToString());
            PlayerPrefs.SetString("WalkKey", Walk.ToString());
        }

        public static void SetKey(KeyType type, KeyCode value)
        {
            switch(type)
            {
                case KeyType.Forward:
                    Forward = value;
                    break;
                case KeyType.Backward:
                    Backward = value;
                    break;
                case KeyType.Right:
                    Right = value;
                    break;
                case KeyType.Left:
                    Left = value;
                    break;
                case KeyType.Sprint:
                    Sprint = value;
                    break;
                case KeyType.Jump:
                    Jump = value;
                    break;
                case KeyType.Walk:
                    Walk = value;
                    break;
            }
        }

        public static KeyCode GetKey(KeyType type)
        {
            switch (type)
            {
                case KeyType.Forward:
                    return Forward;
                case KeyType.Backward:
                    return Backward;
                case KeyType.Right:
                    return Right;
                case KeyType.Left:
                    return Left;
                case KeyType.Sprint:
                    return Sprint;
                case KeyType.Jump:
                    return Jump;
                case KeyType.Walk:
                    return Walk;
            }

            return KeyCode.None;
        }
    }

    public class IM : MonoBehaviour
    {
        private static float axisH = 0.0f;
        private static float axisV = 0.0f;

        private void Awake()
        {
            Key.LoadKeys();
        }

        public static bool Hold(KeyCode key)
        {
            return Input.GetKey(key);
        }

        public static bool Press(KeyCode key)
        {
            return Input.GetKeyDown(key);
        }

        public static bool Sprint
        {
            get
            {
                return Input.GetKey(Key.Sprint);
            }
        }

        public static bool Walk
        {
            get
            {
                return Input.GetKey(Key.Walk);
            }
        }

        public static bool Jump
        {
            get
            {
                return Input.GetKeyDown(Key.Jump);
            }
        }
        
        public static float AxisH
        {
            get
            {
                if (Input.GetKey(Key.Right))
                    return Helper.FloatLerp(ref axisH, 1, 0.05f);
                else if (Input.GetKey(Key.Left))
                    return Helper.FloatLerp(ref axisH, -1, 0.05f);
                else
                    return Helper.FloatLerp(ref axisH, 0, 0.05f);
            }
        }

        public static float AxisV
        {
            get
            {
                if (Input.GetKey(Key.Forward))
                    return Helper.FloatLerp(ref axisV, 1, 0.05f);
                else if (Input.GetKey(Key.Backward))
                    return Helper.FloatLerp(ref axisV, -1, 0.05f);
                else
                    return Helper.FloatLerp(ref axisV, 0, 0.05f);
            }
        }
    }
}
