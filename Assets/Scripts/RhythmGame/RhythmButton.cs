using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RhythmButton : MonoBehaviour
{
    [Serializable]
    public struct SpritePair
    {
        public MoveDirection direction;
        public Sprite sprite;
    }
    public List<SpritePair> sprites;
    public Sprite defaultSprite;
    private MoveDirection direction = MoveDirection.None;
    private Image image;

    private IEnumerator Sequence ()
    {
        yield return new WaitForSeconds(2.0f);
        Direction = MoveDirection.Left;
        yield return new WaitForSeconds(2.0f);
        Direction = MoveDirection.Right;
        yield return new WaitForSeconds(2.0f);
        Direction = MoveDirection.Up;
        yield return new WaitForSeconds(2.0f);
        Direction = MoveDirection.Down;
        yield return new WaitForSeconds(2.0f);
        Direction = MoveDirection.None;
    }

    private void Start()
    {
        image = GetComponent<Image>();
        //StartCoroutine(Sequence());
    }
    public MoveDirection Direction
    {
        get { return direction; }
        set 
        {
            direction = value;
            Sprite newSprite = defaultSprite;
            foreach (var kvp in sprites)
            {
                if (value == kvp.direction)
                {
                    newSprite = kvp.sprite;
                    break;
                }
            }
            image.sprite = newSprite;
        }
    }
}
