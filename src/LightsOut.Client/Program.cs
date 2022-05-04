// See https://aka.ms/new-console-template for more information
using LightsOut.Client;
using LightsOut.Client.Enumerations;
using LightsOut.Client.Rest;
using LightsOut.Client.Rest.Models.Binding;
using LightsOut.Client.Rest.Models.View;
using LightsOut.Utility;

Console.WriteLine("Welcome to Lights Out!");
Console.WriteLine();

var game = DrawMenu();

while (!game.IsSolved)
{
    

    var position = Console.GetCursorPosition();

    LightsOutBoard Board = new LightsOutBoard(game.Board.ToTwoDimensionalArray());
    Board.Draw();

    Console.Write("[A1..I9] Toggle: ");
    Console.Write("  ");
    Console.CursorLeft = Console.CursorLeft - 2;
    var cell = Console.ReadLine();

    Console.SetCursorPosition(position.Left, position.Top);

    var response = (LightsOutApiConnector.ToggleAsync(new ToggleBindingModel
    {
        Cell = cell,
        Id = game.Id
    })).GetAwaiter()
    .GetResult();

    if(response != null)
    {
        game = response;
    }
}

Console.WriteLine($"Final Score: {game.Score}");

Console.Read();


static LightsOutViewModel DrawMenu()
{
    Menu menu = Menu.Invalid;

    do
    {
        Console.WriteLine("Please Select an Option:");
        Console.WriteLine("1. New Game");
        Console.WriteLine("2. Load Game");

        Console.Write("Option: ");
        var option = Console.ReadLine();
        if (option == "1")
            menu = Menu.NewGame;
        else if (option == "2")
            menu = Menu.LoadGame;
        else
            Console.WriteLine("Invalid Selection!");

    }
    while (menu == Menu.Invalid);

    if (menu == Menu.NewGame)
    {
        int columns;
        do
        {
            Console.Write("[1..9] Columns: ");
            int.TryParse(Console.ReadLine(), out columns);
        }
        while (columns == 0 || columns > 9);

        int rows;
        do
        {
            Console.Write("[1..9] Rows: ");
            int.TryParse(Console.ReadLine(), out rows);
        }
        while (columns == 0 || columns > 9);

        int difficulty;
        do
        {
            Console.Write($"[1..{rows * columns}] Difficulty: ");
            int.TryParse(Console.ReadLine(), out difficulty);
        }
        while (difficulty == 0 || difficulty > (rows * columns));

        var game = (LightsOutApiConnector.IntializeAsync(new InitializeBindingModel
        {
            ColumnLegth = columns,
            RowLegth = rows,
            ActiveCells = difficulty
        })).GetAwaiter()
        .GetResult();

        Console.WriteLine();
        Console.WriteLine($"Starting Game {game.Id}");
        Console.WriteLine();

        return game;
    }
    else
    {
        LightsOutViewModel game = null;
        Guid id;
        do
        {
            Console.Write("Game: ");
            if (Guid.TryParse(Console.ReadLine(), out id))
            {
                game = (LightsOutApiConnector.GetAsync(id)).GetAwaiter()
                .GetResult();
            }
        }
        while (game == null);

        Console.WriteLine();
        Console.WriteLine($"Loaded Game {game.Id}");
        Console.WriteLine();

        return game;
    }
}
