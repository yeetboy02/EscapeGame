using libs;
using System;
using System.Threading;
 
class Program
{
    private static int numberOfSeconds = 120;
    
    private static GameEngine engine = GameEngine.Instance;
    private static InputHandler inputHandler = InputHandler.Instance;
    private static System.Threading.Timer countdownTimer;
    private static bool shouldRender = false;

    static void Main(string[] args)
    {
        //Setup
        Console.CursorVisible = false;

        engine.Setup();
        engine.SetNumberOfSeconds(numberOfSeconds);

        // Schedule countdown timer
        countdownTimer = new System.Threading.Timer(UpdateCountdown, null, 1000, 1000);

        // Initial render
        engine.Render();

        // Main game loop
        while (true)
        {
            // Handle keyboard input
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                inputHandler.Handle(keyInfo);
                engine.Update();
                shouldRender = true;

            }

            // engine.Update();

            if (shouldRender) {
                // Render game state
                engine.Render();
                shouldRender = false;
            }
            

            //     break;
            // }
        }
    }

    static void UpdateCountdown(object state)
    {
        numberOfSeconds--;
        shouldRender = true;

        if (numberOfSeconds <= 0)
        {
            countdownTimer.Change(Timeout.Infinite, Timeout.Infinite);
            lostGame();
        }
        engine.SetNumberOfSeconds(numberOfSeconds);
    }
    static void lostGame()
    {
        Console.Clear();
        Console.WriteLine("You lost!");
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
        Environment.Exit(0);
    }
 
}
