using Raylib_cs;
using System.CodeDom.Compiler;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Numerics;

Raylib.InitWindow(800, 600, "whaaat");
Raylib.SetTargetFPS(60);

// int x = 0;

Color hotPink = new Color(255, 105, 180, 255);

Vector2 position = new Vector2(0, 0);
Vector2 movement = new Vector2(2, 1);
// Rectangle wall = new Rectangle(20, 300, 400, 20);
Rectangle characterRect = new Rectangle(0, 600-64, 64, 64);
Rectangle doorRect = new Rectangle(700, 10, 60, 60);
Texture2D characterImage = Raylib.LoadTexture("hellokitty.png"); //här valde jag bilden som man spelat som

characterRect.width = characterImage.width;
characterRect.height = characterImage.height;

// Rectangle playerRect = new Rectangle(5,5,10,10);
Rectangle enemyRect = new Rectangle(10,10,10,10);

// bool areOverlapping = Raylib.CheckCollisionRecs(playerRect, enemyRect); // true

(Vector2 pos, Vector2 mov, Texture2D image, Rectangle rect) character;
character.pos = new Vector2(0,0);

// väggarna
List<Rectangle> walls = new();
walls.Add(new Rectangle (-25, 500, 200, 25)); // --
walls.Add(new Rectangle (-25, 300, 400, 25));
walls.Add(new Rectangle (250, 400, 25, 600)); // |
walls.Add(new Rectangle (65, 400, 200, 25));
walls.Add(new Rectangle (350, 300, 25, 200));
walls.Add(new Rectangle (350, 400, 25, 100));
walls.Add(new Rectangle (450, 300, 25, 600));
walls.Add(new Rectangle (75, 200, 500, 25));
walls.Add(new Rectangle (550, 200, 25, 300));
walls.Add(new Rectangle (550, 500, 150, 25));
walls.Add(new Rectangle (550, 400, 150, 25));



string scene = "start";

float speed = 2; //karaktärens fart
int points = 0;

while (!Raylib.WindowShouldClose())
{ 
    // x++;
    // position.X++;

    if (scene == "start")
    {
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
        {
            scene = "game";
        }
    }

    // här så skrev jag coder för att styra figuren man spelar med
    else if (scene == "game")
    {
        movement = Vector2.Zero;
        if (Raylib.IsKeyDown(KeyboardKey.KEY_UP))
        {
            movement.Y = -5;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_DOWN))
        {
            movement.Y = 5;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
        {
            movement.X = 5;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
        {
            movement.X = -5;
        }

        if (movement.Length() > 0)
        {
            movement = Vector2.Normalize(movement) * speed;
        }
        characterRect.x += movement.X;
        characterRect.y += movement.Y;
        bool undoX = false; 
        bool undoY = false;

        // så karaktären inte går utanför mappan
        if (characterRect.x > 800 - characterRect.width || characterRect.x < 0)
        {
            // characterRect.x -= movement.X;
            undoX = true;
        }
        if (characterRect.y > 600 - characterRect.height || characterRect.y < 0)
        {
            // characterRect.y -= movement.Y;
            undoY = true;
        }



        // foreach (Rectangle wall in walls)
        // {
        //     if (Raylib.CheckCollisionRecs(characterRect, wall))
        //     {
        //         undoX = true;
        //         undoY = true;
        //     }
        // }

        if (undoX == true)
        {
            characterRect.x -= movement.X;
        }
        if (undoY == true)
        {
            characterRect.y -= movement.Y;
        }


        // förflytning utanför mappen
        // if (characterRect.x > 800 )
        // {
        //     characterRect.x = -64;
        //     characterRect.x = 64;
        //    }

        // Kolla kollisionerna

        if (Raylib.CheckCollisionRecs(characterRect, doorRect))
        {
            // scene = "finished";
            points++;
        }

        foreach (Rectangle wall in walls)
        {
            if (Raylib.CheckCollisionRecs(characterRect, wall))
            {
                scene = "game over";
            }
        }
        // foreach som går igenom alla väggar
        //   för varje vägg, kolla om den kolliderar med spelaren
        //     Om den gör det, ändras scene till "gameover"

    }
    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.SKYBLUE);

    // Raylib.DrawRectangleRec(characterRect, Color.DARKPURPLE);

    // Raylib.DrawCircle(x, 300, 50, Color.GREEN);
    // Raylib.DrawCircleV(position, 50, hotPink);

    // när karaktären rör sig ska du normalisera den och använda "speed"



    if (scene == "game")
    {
        Raylib.ClearBackground(Color.PINK);
        Raylib.DrawTexture(characterImage, (int)characterRect.x, (int)characterRect.y, Color.WHITE);

        Raylib.DrawRectangleRec(doorRect, Color.GREEN);
        Raylib.DrawText($"points: {points}", 10, 10, 32, Color.WHITE);
        foreach (Rectangle wall in walls)
        {
            Raylib.DrawRectangleRec(wall, Color.BLACK);
        }

    }
    else if (scene == "game over")
    {
        Raylib.ClearBackground(Color.BLACK);
        // Raylib.DrawRectangleRec(doorRect, Color.RED);
        Raylib.DrawText("GAME OVER", 250, 250, 30, Color.RED);
    }


    // startmenyn
    else if (scene == "start")
    {
        Raylib.ClearBackground(Color.BLACK);
        Raylib.DrawText("slå största knappen", 250, 250, 30, Color.LIGHTGRAY);
    }
    // Raylib.DrawRectangle(20, 690, 420, 50, Color.VIOLET);
    Raylib.EndDrawing();


}

