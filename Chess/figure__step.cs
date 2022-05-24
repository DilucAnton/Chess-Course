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
  class figure__step
  {
    public void Steps(int X, int Y, int Figure, Form1 f)
    {
      about_figure ab = new about_figure();
      
      int ss;  // для показа шагов пешки
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
        case 6:
          if (ab.Sized(X + 1 * ss, Y))
          {
            if (f.map[X + 1 * ss, Y] == 0)
            {
              f.butts[X + 1 * ss, Y].BackColor = Color.YellowGreen;
              f.butts[X + 1 * ss, Y].Enabled = true;
            }
            if ((X == 6 && ss == -1) || (X == 1 && ss == 1))
            {
              // условие проверки если пешка находится на 7 или 2 горизонтали для двойного хода
              f.butts[X + 2 * ss, Y].BackColor = Color.YellowGreen;
              f.butts[X + 2 * ss, Y].Enabled = true;
            }
          }
          // проверка на нахождение рядом фигуры 
          if (ab.Sized(X + 1 * ss, Y + 1))
          {
            if (f.map[X + 1 * ss, Y + 1] != 0 && f.map[X + 1 * ss, Y + 1] / 10 != f.Player)
            {
              f.butts[X + 1 * ss, Y + 1].BackColor = Color.YellowGreen;
              f.butts[X + 1 * ss, Y + 1].Enabled = true;
            }
          }
          if (ab.Sized(X + 1 * ss, Y - 1))
          {
            if (f.map[X + 1 * ss, Y - 1] != 0 && f.map[X + 1 * ss, Y - 1] / 10 != f.Player)
            {
              f.butts[X + 1 * ss, Y - 1].BackColor = Color.YellowGreen;
              f.butts[X + 1 * ss, Y - 1].Enabled = true;
            }
          }
          break;
        case 5:
          f.VG(X, Y);
          break;
        case 2:
          f.VG(X, Y);
          f.Diagonal(X, Y);
          break;
        case 3:
          f.Diagonal(X, Y);
          break;
        case 4:
          if (ab.Sized(X + 2, Y + 1))
          {
            f.CheckStep(X + 2, Y + 1);
          }
          if (ab.Sized(X + 2, Y - 1))
          {
            f.CheckStep(X + 2, Y - 1);
          }
          if (ab.Sized(X - 2, Y + 1))
          {
            f.CheckStep(X - 2, Y + 1);
          }
          if (ab.Sized(X - 2, Y - 1))
          {
            f.CheckStep(X - 2, Y - 1);
          }
          if (ab.Sized(X + 2, Y + 1))
          {
            f.CheckStep(X + 2, Y + 1);
          }
          if (ab.Sized(X + 1, Y + 2))
          {
            f.CheckStep(X + 1, Y + 2);
          }
          if (ab.Sized(X + 1, Y - 2))
          {
            f.CheckStep(X + 1, Y - 2);
          }
          if (ab.Sized(X - 1, Y + 2))
          {
            f.CheckStep(X - 1, Y + 2);
          }
          if (ab.Sized(X - 1, Y - 2))
          {
            f.CheckStep(X - 1, Y - 2);
          }
          break;
        case 1: 
          f.VG(X, Y, true);
          f.Diagonal(X, Y, true);

          break;
      }

    }
  }
}
