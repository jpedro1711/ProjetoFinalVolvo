namespace ConcessionariaAPI.Services
{
    public class LogService
    {
        public void SaveLog(string msg)
        {
            try
            {
                StreamWriter sw = new StreamWriter(@"log.txt", true);

                sw.WriteLine(msg);
                
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }
    }
}
