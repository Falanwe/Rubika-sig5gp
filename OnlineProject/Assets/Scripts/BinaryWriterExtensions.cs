using Assets.Models.Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace System.IO
{
    public static class BinaryExtensions
    {
        public static void Write(this BinaryWriter writer, Vector3Dto vector)
        {
            writer.Write(vector.X);
            writer.Write(vector.Y);
            writer.Write(vector.Z);
        }

        public static void Write(this BinaryWriter writer, ColorDto color)
        {
            writer.Write(color.Red);
            writer.Write(color.Green);
            writer.Write(color.Blue);
            writer.Write(color.Alpha);
        }

        public static void Write(this BinaryWriter writer, CubeDto cube)
        {
            writer.Write(cube.Position);
            writer.Write(cube.Rotation);
            writer.Write(cube.Color);
        }

        public static CubeDto ReadCube(this BinaryReader reader)
        {
            var position = reader.ReadVector3();
            var rotation = reader.ReadVector3();
            var color = reader.ReadColor();

            return new CubeDto { Position = position, Rotation = rotation, Color = color };
        }

        public static Vector3Dto ReadVector3(this BinaryReader reader)
        {
            var x = reader.ReadSingle();
            var y = reader.ReadSingle();
            var z = reader.ReadSingle();

            return new Vector3Dto { X = x, Y = y, Z = z };
        }

        public static ColorDto ReadColor(this BinaryReader reader)
        {
            var r = reader.ReadByte();
            var g = reader.ReadByte();
            var b = reader.ReadByte();
            var a = reader.ReadByte();

            return new ColorDto { Red = r, Green = g, Blue = b, Alpha = a };
        }
    }
}
