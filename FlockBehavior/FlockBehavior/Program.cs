using System;

namespace FlockBehavior
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (FlockBehavior game = new FlockBehavior())
            {
                game.Run();
            }
        }
    }
#endif
}

