﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public interface Weapon
{
    void Use();
    void EndUse();
    GameObject GetGameObject();
}
