using BCProject.Domain;

namespace BCProject
{
    class Program
    {
        //launch an async server , this program needs to be manually closed 
        static void Main(string[] args)
        {
            AsyncServer server = new AsyncServer();
            server.Start();
            while (true){}
        }
    }
}
