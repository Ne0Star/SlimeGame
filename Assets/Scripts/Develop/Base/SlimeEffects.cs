using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SlimeEffects
{

    public List<Effect> effect;

    [Serializable]
    public class Effect
    {
        public string effectName;
        public string effectInfo;
        public Sprite effectImage;
    }
}
