using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unknown : MonoBehaviour
{
    [SerializeField] private GameObject image;
    private Manager gameController;

    public void OnMouseDown()
    {
        if (image.activeSelf && gameController.canOpen)
        {
            image.SetActive(false);
            gameController.imageOpened(this);
        }
    }

    private void Start()
    {
        gameController = GameObject.Find("GameManager").GetComponent<Manager>();
    }


    private int _spriteId;

    public int spriteId
    {
        get { return _spriteId; }
    }

    public void ChangeSprite(int id, Sprite image)
    {
        _spriteId = id;
        GetComponent<SpriteRenderer>().sprite = image;
    }

    public void Close()
    {
        image.SetActive(true);
    }

}
