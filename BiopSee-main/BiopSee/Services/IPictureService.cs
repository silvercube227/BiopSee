namespace BiopSee.Services;

using System.Drawing;

/// <summary>
/// Takes pictures.
/// </summary>
public interface IPictureService
{
    /// <summary>
    /// Takes a picture
    /// </summary>
    /// <param name="align">Draw alignment markers</param>
    /// <returns></returns>
    string? TakePicture(bool align = false);

    /// <summary>
    /// Removes old unused images.
    /// </summary>
    /// <param name="keep"></param>
    void Prune(ISet<string> keep);
}
