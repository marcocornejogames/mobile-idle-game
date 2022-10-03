using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BuildingFeedbackDisplay : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private Image _feedbackImage;
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _subtitle;
    [SerializeField] private TextMeshProUGUI _bodyText;

    public void SetImage(Sprite image)
    {
        _feedbackImage.sprite = image;
    }

    public void SetTitle(string text)
    {
        _title.text = text;
    }

    public void SetSubtitle(string text)
    {
        _subtitle.text = text;
    }

    public void SetBody(string text)
    {
        _bodyText.text = text;
    }

}
