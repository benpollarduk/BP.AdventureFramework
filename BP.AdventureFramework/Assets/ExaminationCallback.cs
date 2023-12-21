namespace BP.AdventureFramework.Assets
{
    /// <summary>
    /// Represents the callback for examinations.
    /// </summary>
    /// <param name="obj">The object to examine.</param>
    /// <returns>A string representing the result of the examination.</returns>
    public delegate ExaminationResult ExaminationCallback(IExaminable obj);
}
