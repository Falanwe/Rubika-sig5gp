using UnityEngine;

namespace Assets.Models.Dto
{
    public class ColorDto
    {
        public byte Red { get; set; }
        public byte Green { get; set; }
        public byte Blue { get; set; }
        public byte Alpha { get; set; }

        public static implicit operator ColorDto(Color color)
        {
            return new ColorDto { Red = (byte)(255 * color.r), Green = (byte)(255 * color.g), Blue = (byte)(255 * color.b), Alpha = (byte)(255 * color.a) };
        }

        public static implicit operator Color(ColorDto color)
        {
            return new Color(color.Red / 255f, color.Green / 255f, color.Blue / 255f, color.Alpha / 255f);
        }
    }
}