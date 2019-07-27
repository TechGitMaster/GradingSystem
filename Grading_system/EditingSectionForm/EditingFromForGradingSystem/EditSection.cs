using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace EditingFromForGradingSystem
{
    public partial class EditSection : Form
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle = 0x20000;
                return cp;
            }
        }
        public EditSection()
        {
            InitializeComponent();

            foreach (Panel panReport in NavigatorReport.Controls) {
                panReport.Click += new System.EventHandler(ReportFunctionShow);
                foreach (Control label in panReport.Controls) {
                    if (label.GetType() == typeof(Label)) {
                        label.Click += new System.EventHandler(ReportFunctionShow);
                    }
                }
            }
        }

        public void ReportFunctionShow(object controls, EventArgs e) {
            string textCon = "";
            Control Con = (Control)controls;
            Panel pans = null;
            Label label = null;

            if (Con is Panel) {
                textCon = "asd";
                pans = (Panel)Con;
            }
            else if(Con is Label){
                textCon = "as";
                label = (Label)Con;
            }

            foreach (Control pan in PanelShowReports.Controls) {
                if (pan is Panel) {
                    if (pan.AccessibleName != "LineCost")
                    {
                        if ((textCon == "asd" ? pans.Name : label.AccessibleName) == pan.AccessibleName)
                        {
                            pan.Visible = true;
                        }
                        else
                        {
                            pan.Visible = false;
                        }
                    }
                }
            }
        }
    }
}
