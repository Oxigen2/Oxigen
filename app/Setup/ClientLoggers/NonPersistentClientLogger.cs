namespace Setup.ClientLoggers
{
  public class NonPersistentClientLogger : ClientLogger
  {
    public NonPersistentClientLogger()
    {
      _userRef = System.Guid.NewGuid().ToString();
    }
  }
}
