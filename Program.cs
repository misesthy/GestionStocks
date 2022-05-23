using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

//enummération des états du stock
enum etatStock
{
    disponible,
    reaprovisionnement,
    rupture,
    RAS,
}


//impléùentation de la classe produit
class Produit
{
    private static int size = 1;
    private int id = 0;
    private String name { get; set; }
    private int stock { get; set; }
    private etatStock etat { get; set; }

    //définition des états du stock
    public Produit(string name, int? stock)
    {
        this.id = Produit.size;
        this.name = name;
        this.stock = stock ?? 0;

        if (stock == 0)
        {
            this.etat = etatStock.rupture;
        }
        else if (stock <= 5)
        {
            this.etat = etatStock.reaprovisionnement;
        }
        else if (stock > 5)
        {
            this.etat = etatStock.disponible;
        }
        else
        {
            this.etat = etatStock.RAS;
        }

        Produit.size++;
    }

    // affichage du stock
    public void showStock()
    {
        Console.BackgroundColor = Program.getColorLine(etat);
        int spaceId = Produit.intLength(id);
        int spaceName = name.Length;
        int spaceStock = Produit.intLength(stock);
        int spaceEtat = etat.ToString().Length;

        Console.Write($"| {id}", Console.BackgroundColor);

        for (int i = 0; i < (3 - spaceId); i++)
        {
            Console.Write(' ');
        }

        Console.Write($"| {name}");

        for (int i = 0; i < (20 - spaceName); i++)
        {
            Console.Write(' ');
        }

        Console.Write($"| {stock}");


        for (int i = 0; i < (7 - spaceStock); i++)
        {
            Console.Write(' ');
        }

        Console.Write($"| {etat}");

        for (int i = 0; i < (19 - spaceEtat); i++)
        {
            Console.Write(' ');
        }

        Console.Write('|');
        Console.BackgroundColor = Program.getColorLine(0);
        Console.Write("\n");
    }

    public void decrement()
    {
        if (stock > 0) stock--;
        maj();
    }

    public void maj()
    {
        if (stock == 0)
        {
            etat = etatStock.rupture;
        }
        else if (stock <= 5)
        {
            etat = etatStock.reaprovisionnement;
        }
        else if (stock > 5)
        {
            etat = etatStock.disponible;
        }
        else
        {
            etat = etatStock.rupture;
        }

    }


    public static int intLength(int i)
    {
        if (i < 0)
            throw new ArgumentOutOfRangeException();
        if (i == 0)
            return 1;
        return (int)Math.Floor(Math.Log10(i)) + 1;
    }

}
class Program
{
    static void Main(string[] args)
    {
        Console.Clear();

        //instanciation de la classe produit
        Produit tongs = new Produit("Tongs", 50);
        Produit ballon = new Produit("Ballon", 40);
        Produit maillot = new Produit("Maillot", 3);
        Produit seau = new Produit("Seau", 9);
        Produit pelle = new Produit("Pelle", 35);
        Produit sac = new Produit("Sac", 0);
        Produit viande = new Produit("Viande", 50);
        Produit avocat = new Produit("Avocat", 5);
        Produit sel = new Produit("Sel", 20);

        Produit[] produits = new Produit[] { tongs, ballon, maillot, seau, pelle, sac, viande, avocat, sel };

        Program.showStocks(produits);
        int numericValue;

          //fonction de décrementation
        do
        {
            Console.WriteLine("Quel produit voulez vous !? ( Veillez insérer son numéro et 0 pour sortir)");
            ConsoleKeyInfo cki = Console.ReadKey();
            string key = cki.Key.ToString();


            numericValue = Program.getNumberCKI(key);

            if (numericValue == 0)
            {
                return;
            }
            else
            {
                try
                {
                    Console.SetCursorPosition(0, 0);
                    produits[numericValue - 1].decrement();
                    Program.showStocks(produits);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                }

            }
        } while (numericValue != 0);
        return;
    }

    //selectiondes touches du clavier
    public static int getNumberCKI(String cki)
    {
        switch (cki)
        {
            case "NumPad1":
                return 1;
            case "NumPad2":
                return 2;
            case "NumPad3":
                return 3;
            case "NumPad4":
                return 4;
            case "NumPad5":
                return 5;
            case "NumPad6":
                return 6;
            case "NumPad7":
                return 7;
            case "NumPad8":
                return 8;
            case "NumPad9":
                return 9;
            case "NumPad0":
                return 0;
            case "D1":
                return 1;
            case "D2":
                return 2;
            case "D3":
                return 3;
            case "D4":
                return 4;
            case "D5":
                return 5;
            case "D6":
                return 6;
            case "D7":
                return 7;
            case "D8":
                return 8;
            case "D9":
                return 9;
            case "D0":
                return 0;
            default:
                Console.WriteLine("\n The caracter is not a number.\n");
                return 0;
        }

    }

    //etat du stock en couleur
    public static ConsoleColor getColorLine(dynamic etat)
    {
        switch (etat)
        {
            case etatStock.disponible:
                return ConsoleColor.Black;
            case etatStock.rupture:
                return ConsoleColor.Red;
            case etatStock.reaprovisionnement:
                return ConsoleColor.DarkGray;
            case etatStock.RAS:
                return ConsoleColor.Black;
            case 0:
                return ConsoleColor.Black;
            default:
                return ConsoleColor.Black;
        }
    }

    //Affichage du programme
    public static void showStocks(Produit[] produits)
    {
        Console.WriteLine("Gestion des Stocks en C#\r");
        Console.WriteLine("------------------------\n");

        Program.showHorizontalLine(1);
        Program.showHeaderText();
        Program.showHorizontalLine(2);
        foreach (Produit produit in produits)
        {
            produit.showStock();
        }
        Console.BackgroundColor = Program.getColorLine(0);
        Program.showHorizontalLine(0);
    }
    public static void showHorizontalLine(int rang)
    {
        if (rang == 1)
        {
            Console.Write('\r');
        }

        Console.Write('+');
        for (int i = 0; i < 4; i++)
        {
            Console.Write('-');
        }

        Console.Write('+');
        for (int i = 0; i < 21; i++)
        {
            Console.Write('-');
        }
        Console.Write('+');

        for (int i = 0; i < 8; i++)
        {
            Console.Write('-');
        }
        Console.Write('+');

        for (int i = 0; i < 20; i++)
        {
            Console.Write('-');
        }
        Console.Write('+');
        Console.Write("\n");
    }
    public static void showHeaderText()
    {

        Console.Write($"| #");

        for (int i = 0; i < (3 - 1); i++)
        {
            Console.Write(' ');
        }

        Console.Write($"| Produits");

        for (int i = 0; i < (12); i++)
        {
            Console.Write(' ');
        }

        Console.Write($"| Stock");


        for (int i = 0; i < (2); i++)
        {
            Console.Write(' ');
        }

        Console.Write($"| Etat");

        for (int i = 0; i < (15); i++)
        {
            Console.Write(' ');
        }

        Console.Write('|');
        Console.Write("\n");
    }

}
public struct POINT
{
    public int X;
    public int Y;
}

static class Mouse
{
    [DllImport("user32.dll")]
    static extern bool GetCursorPos(out POINT lpPoint);
    static int _x, _y;

    public static void showMousePosition()
    {
        POINT point;
        if (GetCursorPos(out point) && point.X != _x && point.Y != _y)
        {
            Console.Clear();
            Console.WriteLine("({0},{1})", point.X, point.Y);
            _x = point.X;
            _y = point.Y;
        }
    }
}


public class MouseOperations
{
    [Flags]
    public enum MouseEventFlags
    {
        LeftDown = 0x00000002,
        LeftUp = 0x00000004,
        MiddleDown = 0x00000020,
        MiddleUp = 0x00000040,
        Move = 0x00000001,
        Absolute = 0x00008000,
        RightDown = 0x00000008,
        RightUp = 0x00000010
    }

    [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool SetCursorPos(int x, int y);      

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetCursorPos(out MousePoint lpMousePoint);

    [DllImport("user32.dll")]
    private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

    public static void SetCursorPosition(int x, int y) 
    {
        SetCursorPos(x, y);
    }

    public static void SetCursorPosition(MousePoint point)
    {
        SetCursorPos(point.X, point.Y);
    }

    public static MousePoint GetCursorPosition()
    {
        MousePoint currentMousePoint;
        var gotPoint = GetCursorPos(out currentMousePoint);
        if (!gotPoint) { currentMousePoint = new MousePoint(0, 0); }
        return currentMousePoint;
    }

    public static void MouseEvent(MouseEventFlags value)
    {
        MousePoint position = GetCursorPosition();

        mouse_event
            ((int)value,
             position.X,
             position.Y,
             0,
             0)
            ;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MousePoint
    {
        public int X;
        public int Y;

        public MousePoint(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}

