using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
  class about_figure
  {
    public void OnButtons(Form1 f)
    {
      for (int i = 0; i < 8; i++)
      {
        for (int j = 0; j < 8; j++)
        {
          f.butts[i, j].Enabled = true;
        }
      }
    }

    public void OffButtons(Form1 f)
    {
      for (int i = 0; i < 8; i++)
      {
        for (int j = 0; j < 8; j++)
        {
          f.butts[i, j].Enabled = false;
        }
      }
    }


    public bool Sized(int i, int j)
    {
      if (i < 0 || j < 0 || i >= 8 || j >= 8)
      {
        return false;
      }
      else
      {
        return true;
      }
    }
  }
}
