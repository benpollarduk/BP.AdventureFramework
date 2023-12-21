namespace BP.AdventureFramework.Logic
{
    /// <summary>
    /// Callback that invokes a callback for waiting for a key press.
    /// </summary>
    /// <param name="key">The ASCII code of the key to wait for.</param>
    /// <returns>If the key pressed returned the same ASCII character as the key property.</returns>
    internal delegate bool WaitForKeyPressCallback(char key);
}