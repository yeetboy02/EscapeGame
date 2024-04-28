using libs;
using System;
using System.Threading;
 
class Program
{    
   
    static private int numberOfSeconds = 60;
    static private bool isCountdownRunning = false;
 
    static void Main(string[] args)
    {
        //Setup
        Console.CursorVisible = false;
        var engine = GameEngine.Instance;
        var inputHandler = InputHandler.Instance;
       
        engine.Setup();
        engine.SetNumberOfSeconds(numberOfSeconds);
        // INITIAL RENDER
        engine.Render();
 
        if (!isCountdownRunning)
            {
                isCountdownRunning = true;
                Thread countdownThread = new Thread(() =>
                {
                   for (int i = numberOfSeconds; i > 0; i--)
                    {
                    engine.SetNumberOfSeconds(i);
                    engine.Render();
                    Thread.Sleep(1000);
                    }
 
                });
 
                countdownThread.Start();
            }
 
 
        // Main game loop
        while (true)
        {
            // Handle keyboard input
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            inputHandler.Handle(keyInfo);
 
            engine.Update();
 
            engine.Render();
 
 
            // CHECK WIN CONDITION
            // if (engine.checkWinCond()) {
            //     break;
            // }
        }
    }
 
 
}