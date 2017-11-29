using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class DefineIntention : System.Attribute {

    // Valeur de l'attribut
    public string value;

    // Constructeur de l'attribut
    public DefineIntention(string value) {
        this.value = value;
    }

}
