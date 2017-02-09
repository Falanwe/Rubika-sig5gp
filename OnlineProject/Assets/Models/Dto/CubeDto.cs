using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Models.Dto
{
    public class CubeDto
    {
        public Vector3Dto Position { get; set; }
        public Vector3Dto Rotation { get; set; }
        public ColorDto Color { get; set; }
    }
}
