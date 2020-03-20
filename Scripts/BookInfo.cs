using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookInfo : MonoBehaviour
{
    // Start is called before the first frame update

    public string bookTitle;
    
    [TextArea(3,10)]
    public string bookText;
}
