using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using ClassesOfFirstCome;
using System.Windows.Forms;

namespace FormLoginRegister
{
    public partial class createAccount : Form
    {
        protected List<string> handle_information_create_user = new List<string>();

        protected static register_acc Register_class = new register_acc();
        protected override CreateParams CreateParams {
            get {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle = 0x20000;
                return cp;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (condition_to_line_paint == true)
            {
                line_paint_first(line1, e);
            }
        }

        public createAccount()
        {
            InitializeComponent();

            //Adding Mouse event in the form the will can move.........................
            eventMove.MouseDown += new System.Windows.Forms.MouseEventHandler(mousedown);
            eventMove.MouseUp += new System.Windows.Forms.MouseEventHandler(mouseup);
            eventMove.MouseMove += new System.Windows.Forms.MouseEventHandler(mousemove);
            Email.KeyUp += new System.Windows.Forms.KeyEventHandler(keyup_email);
            Nexts.Click += new System.EventHandler(clickCreateAcc);
            Male.Click += new System.EventHandler(male_clicked);
            Female.Click += new System.EventHandler(female_clicked);
            line1.Paint += new System.Windows.Forms.PaintEventHandler(line_paint_first);
            line2.Paint += new System.Windows.Forms.PaintEventHandler(line_paint_first);
            LineBetweenImageInfo.Paint += new System.Windows.Forms.PaintEventHandler(LineBetweenImageInfoDraw);
            chooseProfile.Click += new System.EventHandler(click_choose_image);
            CreateAccountFinal.Click += new System.EventHandler(CreateAccountFinalBttn);

            foreach (Control drawing_panel_number in drawing_panel.Controls) {
                if (drawing_panel_number is Panel) {
                    if (drawing_panel_number.AccessibleDescription == "number_color") {
                        drawing_panel_number.Paint += new System.Windows.Forms.PaintEventHandler(number_paint_first);
                    }
                }
            }

            panelPicture.Visible = false;
            panelinformation.Visible = false;
            Color_yellow2.Visible = false;
            Color_yellow3.Visible = false;

            //CIRCLE THE IMAGE BOX TO SELECT IMAGE.................................
            using (System.Drawing.Drawing2D.GraphicsPath circle = new System.Drawing.Drawing2D.GraphicsPath()) {
                circle.AddEllipse(0, 0, BoxPictureUser.Width, BoxPictureUser.Height);
                System.Drawing.Region region = new System.Drawing.Region(circle);
                BoxPictureUser.Region = region;
            }

            //THIS IS FOR FINAL IMAGE TO INFORMATION..........................
            using (GraphicsPath grapPath = new GraphicsPath()) {
                grapPath.AddEllipse(0,0, ShowFinalImage.Width, ShowFinalImage.Height);
                System.Drawing.Region region = new System.Drawing.Region(grapPath);
                ShowFinalImage.Region = region;
            }
        }
        private void close_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();

        }

        /// <summary>
        /// WILL THIS FORM IS MOVE THIS IS THE FUNCTIONS....................
        /// </summary>
        protected static Point point = new Point(0, 0);
        public static bool conditions = false;
        public void mousedown(object sender, MouseEventArgs e) {
            point = new Point(e.X, e.Y);
            conditions = true;
        }

        public void mouseup(object sender, MouseEventArgs e)
        {
            conditions = false;
        }

        public void mousemove(object sender, MouseEventArgs e)
        {
            if (conditions) {
                Point pointTo = PointToScreen(e.Location);
                this.Location = new Point(pointTo.X - point.X, pointTo.Y - point.Y);
            }
        }

        public void male_clicked(object sender, EventArgs e) {
            var control_male = (RadioButton)sender;
            control_male.Tag = "Male";
            foreach (Control female_detect_erase_tag in panel3.Controls) {
                if (female_detect_erase_tag.GetType() == typeof(RadioButton)) {
                    if (female_detect_erase_tag.Name == "Female") {
                        female_detect_erase_tag.Tag = "";
                    }
                }
            }
        }

        public void female_clicked(object sender, EventArgs e)
        {
            var control_female = (RadioButton)sender;
            control_female.Tag = "Female";
            foreach (Control male_detect_erase_tag in panel3.Controls)
            {
                if (male_detect_erase_tag.GetType() == typeof(RadioButton))
                {
                    if (male_detect_erase_tag.Name == "Male")
                    {
                        male_detect_erase_tag.Tag = "";
                    }
                }
            }
        }

        /// <summary>
        /// KeyUp the Email for this in array .................................................
        /// </summary>
        private static char[] array_alien_word = new char[] { '~', '`', '!', '@', '#', '$', '%', '^', '&',
        '(',')','-','+','=','{','}','[',']',':', ';', '<', '>', ',', '.', '?', '/'};
        public void keyup_email(object sender, KeyEventArgs e) {
            var control_text = (Control)sender;
            if (control_text.GetType() == typeof(TextBox)) {
                string handle_email = "";
                bool condition_to = false;
                for (int count_number = 0; count_number < control_text.Text.Length; count_number++)
                {
                    bool condition_to_for = true;
                    for (int count_number_form_array = 0; count_number_form_array < array_alien_word.Length; count_number_form_array++) {
                        if (condition_to_for != false) {
                            if (control_text.Text[count_number] != array_alien_word[count_number_form_array])
                            {
                                condition_to = true;
                            }
                            else {
                                condition_to = false;
                                condition_to_for = false;
                            }
                        }
                    }

                    if (condition_to == true) {
                        handle_email += control_text.Text[count_number].ToString();
                    }
                }
                control_text.Text = handle_email;
            }
        }

        /// <summary>
        /// LINE UP AND SOLIDBRUSH UP FIRST LOAD............................................
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected static bool condition_to_line_paint = false;
        protected static bool condition_to_line_paint2 = false;
        protected static bool condition_to_line_paint3 = false;
        public void line_paint_first(object sender, PaintEventArgs e)
        {
            Control line1 = (Panel)sender;
                if (line1.Name == "line1")
                {
                    Graphics drawLine = e.Graphics;
                Pen pen_for_line = new Pen(ColorTranslator.FromHtml("#F1C40F"), 5);
                Pen pen_for_color = new Pen(ColorTranslator.FromHtml("#273746"), 5);
                drawLine.DrawLine(pen_for_color, 0, 0, 200, 0);
                drawLine.DrawLine(pen_for_line, 0, 0, 10, 0);
                    if (condition_to_line_paint != false)
                    {
                    Pen pen_for_lines = new Pen(ColorTranslator.FromHtml("#F1C40F"), 5);
                    drawLine.DrawLine(pen_for_lines, 0, 0, 200, 0);
                    }
                   drawLine.Dispose();
                }
                else
                {
                Graphics line2_draw = e.Graphics;
                Pen pen_line2 = new Pen(ColorTranslator.FromHtml("#273746"), 5);
                    line2_draw.DrawLine(pen_line2, 0, 0, 200, 0);
                    if  (condition_to_line_paint2 == true) {
                        Pen pen_lines2 = new Pen(ColorTranslator.FromHtml("#F1C40F"), 5);
                        line2_draw.DrawLine(pen_lines2, 0, 0, 10, 0);
                        if (condition_to_line_paint3 != false) {
                            Pen pen_lines3 = new Pen(ColorTranslator.FromHtml("#F1C40F"), 5);
                            line2_draw.DrawLine(pen_lines3, 0, 0, 200, 0);
                        }
                    }
                    line2_draw.Dispose();
                }
        }

        public void number_paint_first(object sender, PaintEventArgs e)
        {
            Control number_1 = (Control)sender;
            if (number_1.GetType() == typeof(Panel))
            {
                if (number_1.Name == "number1")
                {
                    SolidBrush brush = new SolidBrush(ColorTranslator.FromHtml("#F4D03F"));
                    Graphics grap_brush = e.Graphics;
                    grap_brush.FillEllipse(brush, 3, 0, 15, 15);
                }
                else if (number_1.Name == "number2") {
                    if (condition_to_line_paint2 != true)
                    {
                        SolidBrush solidbrush_2_3 = new SolidBrush(ColorTranslator.FromHtml("#273746"));
                        Graphics solid_graph = e.Graphics;
                        solid_graph.FillEllipse(solidbrush_2_3, 3, 0, 15, 15);
                    }
                } else if (number_1.Name == "Color_yellow2") {
                    SolidBrush brush = new SolidBrush(ColorTranslator.FromHtml("#F4D03F"));
                    Graphics grap_brush = e.Graphics;
                    grap_brush.FillEllipse(brush, 3, 0, 15, 15);
                } else if (number_1.Name == "Color_yellow3") {
                    SolidBrush brush = new SolidBrush(ColorTranslator.FromHtml("#F4D03F"));
                    Graphics grap_brush = e.Graphics;
                    grap_brush.FillEllipse(brush, 3, 0, 15, 15);
                }
                else
                {
                    SolidBrush solidbrush_2_3 = new SolidBrush(ColorTranslator.FromHtml("#273746"));
                    Graphics solid_graph = e.Graphics;
                    solid_graph.FillEllipse(solidbrush_2_3, 3, 0, 15, 15);
                }
            }
        }


        /// <summary>
        /// THIS WILL DRAW A LINE BEETWEN IMAGE AND INFORMATIONOF USER TO THE LAST PART
        /// OF CREATE ACCOUNT............................
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void LineBetweenImageInfoDraw(object sender, PaintEventArgs e) {
            Control con_panel = (Control)sender;
            if (con_panel.GetType() == typeof(Panel)) {
                if (con_panel.Name == "LineBetweenImageInfo") {
                    Graphics grap_draw_line = e.Graphics;
                    Pen draw_pen = new Pen(ColorTranslator.FromHtml("#979A9A"), 2);
                    grap_draw_line.DrawLine(draw_pen, 0, 0, 0, con_panel.Height);
                    grap_draw_line.Dispose(); 
                }
            }
        }










        public void clickCreateAcc(object sender, EventArgs e) {
            protected_submit_bttn_create_account();
        }

        public static string image_handle;
        public string[] list_Array_in_Ceate = new string[] { "Firstname", "Lastname",
                "Username", "Email", "Password", "Conpassword", "Gender"};
        protected void protected_submit_bttn_create_account()
        {
            bool[] condition_to_send_the_datas_user = new bool[] { false, false, false, false, false,
            false, false};
            int count_the_list_array_create = 0;
            while (count_the_list_array_create < list_Array_in_Ceate.Length)
            {
                if (list_Array_in_Ceate[count_the_list_array_create] != "Gender")
                {
                    foreach (Control control_handles in panelCreateAcc.Controls)
                    {
                        if (control_handles is Panel)
                        {
                            foreach (Control control_handle in control_handles.Controls)
                            {
                                if (control_handle.GetType() == typeof(TextBox))
                                {
                                    if (control_handle.Name == list_Array_in_Ceate[count_the_list_array_create])
                                    {
                                        switch (control_handle.Name.ToString())
                                        {
                                            case "Firstname":
                                                if (String.IsNullOrEmpty(control_handle.Text) != true)
                                                {
                                                    condition_to_send_the_datas_user[count_the_list_array_create] = true;
                                                }
                                                break;
                                            case "Lastname":
                                                if (String.IsNullOrEmpty(control_handle.Text) != true)
                                                {
                                                    condition_to_send_the_datas_user[count_the_list_array_create] = true;
                                                }
                                                break;
                                            case "Username":

                                                if (String.IsNullOrEmpty(control_handle.Text) != true)
                                                {
                                                    Register_class.check_if_username_have_database = control_handle.Text;
                                                    if (Register_class.check_if_username_have_database == "false")
                                                    {
                                                        condition_to_send_the_datas_user[count_the_list_array_create] = true;

                                                    }

                                                }
                                                break;
                                            case "Email":
                                                if (control_handle.Text.Length != 0)
                                                {
                                                    condition_to_send_the_datas_user[count_the_list_array_create] = true;
                                                }
                                                break;
                                            case "Password":
                                                if (control_handle.Text.ToString() != "")
                                                {
                                                    condition_to_send_the_datas_user[count_the_list_array_create] = true;
                                                }
                                                break;
                                            case "Conpassword":
                                                if (String.IsNullOrEmpty(control_handle.Text) != true)
                                                {
                                                    if (control_handle.Text == Password.Text)
                                                    {
                                                        condition_to_send_the_datas_user[count_the_list_array_create] = true;
                                                    }
                                                }
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    foreach (Control Gender in panel3.Controls)
                    {
                        if (Gender.GetType() == typeof(RadioButton))
                        {
                            if (String.IsNullOrEmpty(Gender.Tag.ToString()) != true)
                            {
                                condition_to_send_the_datas_user[count_the_list_array_create] = true;
                            }
                        }
                    }
                }
                count_the_list_array_create++;
                if (list_Array_in_Ceate.Length <= count_the_list_array_create)
                {
                    int count_number_of_bool = 0;
                    bool checking_if_have_false = true;
                    do
                    {
                        if (condition_to_send_the_datas_user[count_number_of_bool] == false)
                        {
                            checking_if_have_false = false;
                            if (list_Array_in_Ceate[count_number_of_bool] != "Gender")
                            {
                                foreach (Control control_detect_null in panel2.Controls)
                                {
                                    if (control_detect_null is TextBox)
                                    {
                                        if (control_detect_null.Name == list_Array_in_Ceate[count_number_of_bool])
                                        {
                                            control_detect_null.BackColor = System.Drawing.Color.Red;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (list_Array_in_Ceate[count_number_of_bool] != "Gender")
                            {
                                var textbox = (TextBox)(panel2.Controls[list_Array_in_Ceate[count_number_of_bool]]);
                                textbox.BackColor = System.Drawing.ColorTranslator.FromHtml("#566573");
                            }
                        }

                        count_number_of_bool++;
                        if (count_number_of_bool >= condition_to_send_the_datas_user.Length)
                        {
                            if (checking_if_have_false != false)
                            {
                                // MessageBox.Show("Have true");

                                string[] string_data_create = new string[] { Firstname.Text,
                                Lastname.Text, Username.Text, Email.Text, Password.Text, Conpassword.Text,
                                    (String.IsNullOrEmpty(Male.Tag.ToString()) ? Female.Tag.ToString():Male.Tag.ToString())};

                                //DEFINING IF MELE OR FEMALE AND CHANGE THE IMAGE FROM IT;
                                if (string_data_create[6] != "Female")
                                {
                                    image_handle = "C:/Users/Kyle_velarde/Desktop/Microsoft c# system/icons/219953-people/png/user-16.png";
                                    BoxPictureUser.Image = Image.FromFile(image_handle);
                                }
                                else {
                                    image_handle = "C:/Users/Kyle_velarde/Desktop/Microsoft c# system/icons/219953-people/png/user-17.png";
                                    BoxPictureUser.Image = Image.FromFile(image_handle);
                                }

                                for (int count_data_string = 0; count_data_string < string_data_create.Length; count_data_string++)
                                {
                                    handle_information_create_user.Add(string_data_create[count_data_string]);
                                }

                                condition_to_line_paint = true;
                                condition_to_line_paint2 = true;
                                Color_yellow2.Visible = true;
                                number2.Visible = false;
                                panelPicture.Visible = true;
                                panelCreateAcc.Visible = false;
                                drawing_panel.Refresh();
                                drawing_panel.Invalidate();
                            }
                            else
                            {
                                if (condition_to_send_the_datas_user[condition_to_send_the_datas_user.Length - 1] == false)
                                {
                                    MessageBox.Show("Please select your Gender.");
                                }
                                if (condition_to_send_the_datas_user[2] != true)
                                {
                                    if (String.IsNullOrEmpty(Email.Text) != true)
                                    {
                                        MessageBox.Show("The Username you type had value into database please change.");
                                    }
                                }
                            }
                        }
                    } while (count_number_of_bool < condition_to_send_the_datas_user.Length);
                }
            }
        }

        //IMAGE CHOOSE OPENDIALOG............................................
        public void click_choose_image(object sender, EventArgs e) {
            System.Windows.Forms.OpenFileDialog file = new System.Windows.Forms.OpenFileDialog();
            file.Filter = "Choose Image(*.gif;*.jpg;*.png)|*.gif;*.jpg;*.png";
            if (file.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                image_handle = file.FileName;
                BoxPictureUser.Image = Image.FromFile(image_handle);
            }
        }

        //NEXT TO SEE THE INFORMATION OF USER..............................................
        private void Nexts2_Click(object sender, EventArgs e)
        {
            handle_information_create_user.Add(image_handle);
            condition_to_line_paint3 = true;
            Color_yellow3.Visible = true;
            numbe3.Visible = false;
            panelPicture.Visible = false;
            panelinformation.Visible = true;
            drawing_panel.Invalidate(true);
            information_see(list_Array_in_Ceate, handle_information_create_user.ToArray());
        }

        //SHOW INFORMATION IN CREATE ACCOUNT....................................................
        protected void information_see(string[] list_Array_in_Ceates, string[] handle_information_create_users) {
            int number_count_to_detect_the_last = 0;
            foreach (string handle_info in list_Array_in_Ceates) {
                if (handle_info != "Conpassword") {
                    number_count_to_detect_the_last++;
                    if (handle_info != "Password")
                    {
                        Informations.Text += handle_info + ": " + handle_information_create_users[number_count_to_detect_the_last] + "\n\n";
                    }
                    else {
                        Informations.Text += handle_info + ": Your password is SECURED.\n\n";
                    }
                }

                if (number_count_to_detect_the_last >= list_Array_in_Ceates.Length-1) {
                    number_count_to_detect_the_last += 1;
                    ShowFinalImage.Image = Image.FromFile(handle_information_create_users[number_count_to_detect_the_last]);
                }
            }
        }


        /// <summary>
        ///         CREATE ACCOUNT AFTER THE USER SEE HIS/HER INFORMATION......................
        /// </summary>
        public void CreateAccountFinalBttn(object sender, EventArgs e) {
            var registerAccClass = new register_acc();
            registerAccClass.dataInformationUser = handle_information_create_user.ToArray();
            if (registerAccClass.dataInformationUser[0] == "Succesful")
            {
                this.Hide();
                Form1 formLogin = new Form1();
                formLogin.Show();
            }
            else {
                MessageBox.Show(registerAccClass.dataInformationUser[0]);
            }
       }
    }
}
