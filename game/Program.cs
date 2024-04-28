using libs;
using System;
using System.Threading;
 
class Program
{    
   
    static private int numberOfSeconds = 2;
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
                   for (int i = numberOfSeconds; i >= 0; i--)
                    {
                        engine.SetNumberOfSeconds(i);
                        if(i <= 0) {
                            isCountdownRunning = false;
                            // lostGame();
                        }
                        engine.Render();
                        Thread.Sleep(1000);
                        
                    }
 
                });
 
                countdownThread.Start();
            }
 
 
        // Main game loop
        while (true)
        {

            engine.Render();

            if(!isCountdownRunning)  {
                lostGame();
            }

            // Handle keyboard input
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            inputHandler.Handle(keyInfo);
 
            engine.Update();
 
           
 
 
            // CHECK WIN CONDITION
            // if (engine.checkWinCond()) {
            //     break;
            // }
        }
    }
    
    
    static private void lostGame() {
        Console.Clear();
        Console.WriteLine("You suck!");
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
        Environment.Exit(0);
    }
 
}
