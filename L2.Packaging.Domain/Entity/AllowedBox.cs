using L2.Packaging.Domain.Models;

namespace L2.Packaging.Domain.Entity;

public class AllowedBox
{
    public string Name { get; private set; }
    public decimal Height { get; private set; }
    public decimal Width { get; private set; }
    public decimal Length { get; private set; }

    public AllowedBox(string name, decimal height, decimal width, decimal length)
    {
        Name = name;
        Height = height;
        Width = width;
        Length = length;
    }

    public decimal GetVolume()
    {
        return Height * Width * Length;
    }

    public bool FitsInTheBox(decimal height, decimal width, decimal length)
    {
        return (height <= Height && width <= Width && length <= Length) ||
             (height <= Height && length <= Width && width <= Length) ||
             (width <= Height && height <= Width && length <= Length) ||
             (width <= Height && length <= Width && height <= Length) ||
             (length <= Height && height <= Width && width <= Length) ||
             (length <= Height && width <= Width && height <= Length);
    }

    public List<AllowedBox> GetRemainingSpaces(decimal productHeight, decimal productWidth, decimal productLength)
    {
        return new List<AllowedBox>()
        {
            new AllowedBox(
            "Espaço 1 (acima)",
            Height - productHeight,
            Width,
            Length)
            ,new AllowedBox(
            "Espaço 2 (ao lado)",
            productHeight,
            Width - productWidth,
            Length)
            ,new AllowedBox(
            "Espaço 3 (atrás)",
            productHeight,
            productWidth,
            Length - productLength
        )
        };
    }

    public decimal GetNewVolume(decimal productHeight, decimal productWidth, decimal productLength)
    {
        var newHeight = Height - productHeight;
        var newWidth = Width - productWidth;
        var newLength = Length - productLength;

        return newHeight * newWidth * newLength;
    }
}
