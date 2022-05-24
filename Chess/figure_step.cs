using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Chess
{
  public class cl
  {
    public void Steps(int X, int Y, int Figure, Form1 f)
    {
      
      // используется для показа шагов для пешки. Смотря еще какой цвет фигуры
      int ss;
      if (f.Player == 1)
      {
        ss = -1; // первый игрок. Ходы вверх
      }
      else
      {
        ss = 1; // второй игрок. Ходы вниз
      }
      switch (Figure % 10)
      {
        // пешка
        case 6:
          if (f.Sized(X + 1 * ss, Y))
          {
            if (f.map[X + 1 * ss, Y] == 0)
            {
              f.butts[X + 1 * ss, Y].BackColor = Color.YellowGreen;
              f.butts[X + 1 * ss, Y].Enabled = true;
            }
            if ((X == 6 && ss == -1) || (X == 1 && ss == 1))
            {
              // условие проверки если пешка находится на 7(6) или 2(1) горизонтали. И еще смотря какая фигура
              f.butts[X + 2 * ss, Y].BackColor = Color.YellowGreen;
              f.butts[X + 2 * ss, Y].Enabled = true;
            }
          }
          // проверяю есть ли рядом фигура
          if (f.Sized(X + 1 * ss, Y + 1))
          {
            if (f.map[X + 1 * ss, Y + 1] != 0 && f.map[X + 1 * ss, Y + 1] / 10 != f.Player)
            {
              f.butts[X + 1 * ss, Y + 1].BackColor = Color.YellowGreen;
              f.butts[X + 1 * ss, Y + 1].Enabled = true;
            }
          }
          if (f.Sized(X + 1 * ss, Y - 1))
          {
            if (f.map[X + 1 * ss, Y - 1] != 0 && f.map[X + 1 * ss, Y - 1] / 10 != f.Player)
            {
              f.butts[X + 1 * ss, Y - 1].BackColor = Color.YellowGreen;
              f.butts[X + 1 * ss, Y - 1].Enabled = true;
            }
          }
          break;
        case 5: // ладья
          f.VG(X, Y);
          break;
        case 2: // ферзь
          f.VG(X, Y);
          f.Diagonal(X, Y);
          break;
        case 3: // слон
          f.Diagonal(X, Y);
          break;
        case 4: // конь
          if (f.Sized(X + 2, Y + 1))
          {
            f.CheckStep(X + 2, Y + 1);
          }
          if (f.Sized(X + 2, Y - 1))
          {
            f.CheckStep(X + 2, Y - 1);
          }
          if (f.Sized(X - 2, Y + 1))
          {
            f.CheckStep(X - 2, Y + 1);
          }
          if (f.Sized(X - 2, Y - 1))
          {
            f.CheckStep(X - 2, Y - 1);
          }
          if (f.Sized(X + 2, Y + 1))
          {
            f.CheckStep(X + 2, Y + 1);
          }
          if (f.Sized(X + 1, Y + 2))
          {
            f.CheckStep(X + 1, Y + 2);
          }
          if (f.Sized(X + 1, Y - 2))
          {
            f.CheckStep(X + 1, Y - 2);
          }
          if (f.Sized(X - 1, Y + 2))
          {
            f.CheckStep(X - 1, Y + 2);
          }
          if (f.Sized(X - 1, Y - 2))
          {
            f.CheckStep(X - 1, Y - 2);
          }
          break;
        case 1: // король
          f.VG(X, Y, true);
          f.Diagonal(X, Y, true);

          break;
      }

    }

  }
}
