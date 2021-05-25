using UnityEngine;
using UnityEngine.UI;

public class FaceWithName : MonoBehaviour
{
    public Image image;
    public Text _name;

    public void init(Sprite img, string name){
        image.sprite = img;
        _name.text = name;
    }
}
