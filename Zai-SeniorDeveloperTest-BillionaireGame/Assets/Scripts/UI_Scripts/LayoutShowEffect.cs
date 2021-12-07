using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LayoutShowEffect : MonoBehaviour
{
    [Header("Setting")]
    public float waitTime;
    [Header("Pop out padding parameter")]
    public Pop_parameter pop_out_padding;
    [Header("Pop in padding parameter")]
    public Pop_parameter pop_in_padding;

    public HorizontalLayoutGroup horizontal_layout;

    // Start is called before the first frame update
    void Start()
    {
        horizontal_layout = this.GetComponent<HorizontalLayoutGroup>();

    }

    public IEnumerator PopIn() 
    {
        float elapsedTime = 0;
        var current_padding = new Pop_parameter();

        while (elapsedTime < waitTime)
        {
            current_padding.top = Mathf.Lerp(horizontal_layout.padding.top,pop_in_padding.top,(elapsedTime/waitTime));
            horizontal_layout.padding.top = (int)current_padding.top;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return null;
    }


    [System.Serializable]
    public struct Pop_parameter
    {
        public float left;
        public float right;
        public float top;
        public float bottom;
    }

}
