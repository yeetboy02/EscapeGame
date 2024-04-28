using libs;
using System;
using System.Threading;

class Program
{    
    
    static private int numberOfSeconds = 60;

    static void Main(string[] args)
    {
        //Setup
        Console.CursorVisible = false;
        var engine = GameEngine.Instance;
        var inputHandler = InputHandler.Instance;
        
        engine.Setup();

        // INITIAL RENDER
        engine.Render(numberOfSeconds);

        // Main game loop
        while (true)
        {
            // Handle keyboard input
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            inputHandler.Handle(keyInfo);

            engine.Update();

            engine.Render(numberOfSeconds);

            // CHECK WIN CONDITION
            // if (engine.checkWinCond()) {
            //     break;
            // }
        }
    }


}