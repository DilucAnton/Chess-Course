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
    
    public partial class Form1 : Form
    {
        
    public Image Sprite;
    public int Player = 1;
    public Button[,] butts = new Button[8, 8]; //массив кнопок, чтобы их использовать как фигуры
    public Form1()
    {
        InitializeComponent();
            
        Sprite = new Bitmap(@"Sprites\\chess.png");
        Image figure = new Bitmap(50, 50);
        Graphics gr = Graphics.FromImage(figure);
        Start();
    }
    public void CreateMap()
    {
      map = new int[8, 8] 
      {
                {25,24,23,22,21,23,24,25 },
                {26,26,26,26,26,26,26,26 },
                {0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0 },
                {16,16,16,16,16,16,16,16 },
                {15,14,13,12,11,13,14,15 },
      };

      // создание поля с клеточками цветными
      int fl = 0;
      for (int i = 0; i < 8; i++)
      {
        for (int j = 0; j < 8; j++)
        {
          butts[i, j] = new Button();
          Button cell = new Button();
          cell.Size = new Size(50, 50);
          cell.Location = new Point(j * 50, i * 50);
          if (fl == 0)
          {
            cell.BackColor = Color.White;
            if (j != 7)
            {
              fl = 1;
            }
          }
          else
          {
            cell.BackColor = Color.Brown;
            if (j != 7)
            {
              fl = 0;
            }
          }
          

          //рисуем фигуры
          switch (map[i, j] / 10)
          {
            case 1:
              Image figure = new Bitmap(50, 50);
              Graphics gr = Graphics.FromImage(figure);
              gr.DrawImage(Sprite, new Rectangle(0, 0, 50, 50), 0 + 150 * (map[i, j] % 10 - 1), 0, 150, 150, GraphicsUnit.Pixel);
              cell.BackgroundImage = figure;
              break;
            case 2:
              Image figure1 = new Bitmap(50, 50);
              Graphics gr1 = Graphics.FromImage(figure1);
              gr1.DrawImage(Sprite, new Rectangle(0, 0, 50, 50), 0 + 150 * (map[i, j] % 10 - 1), 150, 150, 150, GraphicsUnit.Pixel);
              cell.BackgroundImage = figure1;
              break;
          }
          // при нажатии на клетку происходит событие PressFigure  
          cell.Click += new EventHandler(PressFigure);
          this.Controls.Add(cell);

          // запоминаем созданную кнопку, чтобы использовать в будущем 
          butts[i, j] = cell;
        }
      }

    }
    public void Start()
        {
         map = new int[8, 8] 
         {
             {25,24,23,22,21,23,24,25 },
             {26,26,26,26,26,26,26,26 },
             {0,0,0,0,0,0,0,0 },
             {0,0,0,0,0,0,0,0 },
             {0,0,0,0,0,0,0,0 },
             {0,0,0,0,0,0,0,0 },
             {16,16,16,16,16,16,16,16 },
             {15,14,13,12,11,13,14,15 },
         };
         Player = 1;
         CreateMap();
        }
    public int[,] map = new int[8, 8] 
    {
            {25,24,23,22,21,23,24,25 },
            {26,26,26,26,26,26,26,26 },
            {0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0 },
            {16,16,16,16,16,16,16,16 },
            {15,14,13,12,11,13,14,15 },
    };

        
    public bool move = false;
    public Button prevFigure; 
    public Button Fig;
    public void PressFigure(object sender, EventArgs e) //обработчик событий 
    {
     figure__step st = new figure__step();
     about_figure ab = new about_figure();
     Button Fig = sender as Button;
            
      if (map[Fig.Location.Y / 50, Fig.Location.X / 50] != 0 && map[Fig.Location.Y / 50, Fig.Location.X / 50] / 10 == Player)
      {
         Fig.BackColor = Color.Red;
         ab.OffButtons(this);
         Fig.Enabled = true;
         st.Steps(Fig.Location.Y / 50, Fig.Location.X / 50, map[Fig.Location.Y / 50, Fig.Location.X / 50],this);

               // если передумали ходить выбранной фигурой
         if (move)
         {
             RestartCell();
             ab.OnButtons(this);
             move = false;
         }
         else
         {
            move = true;
         }

      }
      else
      {
        if (move)
        {
            //делаем ход заменяя фигурой свободную клетку присваивая ей свойства фигуры через временную переменную и на место, где была фигура ставим пустую клетку
            int temp = map[Fig.Location.Y / 50, Fig.Location.X / 50]; 
            map[Fig.Location.Y / 50, Fig.Location.X / 50] = map[prevFigure.Location.Y / 50, prevFigure.Location.X / 50]; 
            map[prevFigure.Location.Y / 50, prevFigure.Location.X / 50] = temp; 
            Fig.BackgroundImage = prevFigure.BackgroundImage;
            map[prevFigure.Location.Y / 50, prevFigure.Location.X / 50] = 0;
            prevFigure.BackgroundImage = null;
            move = false;
            RestartCell();
            ab.OnButtons(this); //включаем кнопки 
                    
            switchPlayer();
            ab.OnButtons(this); //меняем игрока и также включаем кнопки

        }
      }


      prevFigure = Fig; //запоминаем предыдущую фигуру
    }

        
        // ходы вертикаль и горизонталь X и Y координаты  step - сколько нужно шагов false чтобы показать на все поле
    public void VG(int X, int Y, bool step = false)
    {
      about_figure ab = new about_figure();
    
      for (int i = X - 1; i >= 0; i--)
         {
          if (ab.Sized(i, Y)) //в пределах i и Y
          {
              if (!CheckStep(i, Y)) //проверяем на возможность хода
              {
                   break;
              }
          }
          if (step)
              {
               break;
              }
          }

      for (int i = X + 1; i < 8; i++)
          {
          if (ab.Sized(i, Y))
          {
             if (!CheckStep(i, Y))
             {
                  break;
             }
          }
          if (step)
            {
               break;
            }
          }
  
      for (int j = Y + 1; j < 8; j++)
      {
         if (ab.Sized(X, j))
         {
            if (!CheckStep(X, j))
            {
               break;
            }
         }
         if (step)
            {
               break;
            }
      }

      for (int j = Y - 1; j >= 0; j--)
         {
           if (ab.Sized(X, j))
           {
              if (!CheckStep(X, j))
              {
                    break;
              }
           }
           if (step)
           {
              break;
           }
      }
    }


        public void Diagonal(int X, int Y, bool step = false)
        {
            about_figure ab = new about_figure();
            // вверх вправо
            int j = Y + 1;
            for (int i = X - 1; i >= 0; i--)
            {
                if (ab.Sized(i, j))
                {
                if (!CheckStep(i, j))
                    {
                      break;
                    }
                }
                if (j < 7)
                {
                    j++;
                }
                else break;
                if (step) break;
            }


            // вверх влево
            j = Y - 1;
            for (int i = X - 1; i >= 0; i--)
            {
                if (ab.Sized(i, j))
                {
               if (!CheckStep(i, j))
                  {
                     break;
                  }
                }
                if (j > 0)
                {
                    j--;
                }
                else break;
                if (step) break;
            }


             // вниз вправо
            j = Y + 1;
            for (int i = X + 1; i < 8; i++)
            {
              if (ab.Sized(i, j))
                 {
                if (!CheckStep(i, j))
                    {
                        break;
                    }
                 }
             if (j < 7)
              {
                j++;
              }
             else break;
             if (step) break;
            }


           // вниз влево
            j = Y - 1;
                 for (int i = X + 1; i < 8; i++)
                 {
                     if (ab.Sized(i, j))
                     {
                       if (!CheckStep(i, j))
                          {
                             break;
                          }
                     }
                      if (j > 0)
                      {
                         j--;
                      }
                     else break;
                     if (step) break;
                  }
                 
             }

    // проверка на возможность хода
    public bool CheckStep(int X, int Y)
    {
      if (map[X, Y] == 0)
      {
        butts[X, Y].BackColor = Color.YellowGreen; // показывает куда я могу сходить
        butts[X, Y].Enabled = true; // это для пустой клетки
      }
      else
      {
        if (map[X, Y] / 10 != Player)
        {
          butts[X, Y].BackColor = Color.YellowGreen;
          butts[X, Y].Enabled = true; // если противник стоит на этой клетке
        }
        return false;
      }
      return true;
    }


    // смена хода у игроков
    public void switchPlayer()
        {
            if (Player == 1)
            {
                Player = 2;
                lb14.Text = "★ Черные";
                lb15.Text = "Белые";
                lb14.Text = "★ Черные";
                lb15.Text = "Белые";
            }
            else
            {
                Player = 1;
                lb14.Text = "Черные";
                lb15.Text = "★ Белые";
                lb14.Text = "Черные";
                lb15.Text = "★ Белые";
            }
        }
        // перекрасить клетки обратно
        public void RestartCell()
        {
            int fl = 0;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (fl == 0)
                    {
                        butts[i, j].BackColor = Color.White;
                        if (j != 7)
                        {
                            fl = 1;
                        }
                    }
                    else
                    {
                        butts[i, j].BackColor = Color.Brown;
                        if (j != 7)
                        {
                            fl = 0;
                        }
                    }
                }

            }
        }
    //Ключевое слово this ссылается на текущий экземпляр класса, а также используется в качестве модификатора первого параметра метода расширения.
        private void button4_Click(object sender, EventArgs e)
        {
          this.Hide();
          Form1 form = new Form1();
          form.Show();
        }
    
    }
    

}