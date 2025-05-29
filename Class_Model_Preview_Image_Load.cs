namespace XstreaMonNET8
{
    public class Class_Model_Preview_Image_Load
    {
        public static async Task<Image> Preview_Image(Class_Model Model_Class, Size Image_Size)
        {
            await Task.CompletedTask;
            Image image = null!;

            if (Model_Class?.Timer_Online_Change != null)
            {
                try
                {
                    Image original;

                    if (Model_Class.Timer_Online_Change.BGW_Result == 1)
                    {
                        original = await Sites.ImageFromWeb(Model_Class);//TODO agregar metodo para obtener imagen de la web
                        if (original == null)
                        {
                            string str = Model_Class.Pro_Model_Directory + "\\Thumbnail.jpg";
                            if (File.Exists(str) && File.ReadAllBytes(str).Length > 0)
                            {
                                original = new Bitmap(str).Clone() as Image;
                            }
                        }
                    }
                    else
                    {
                        original = Model_Class.Timer_Online_Change.BGW_Result switch
                        {
                            2 => Resources.Status_Private,
                            3 => Resources.Status_hidden,
                            4 => Resources.Status_away,
                            5 => Resources.Status_group,
                            6 => Resources.Status_Pass,
                            _ => null
                        };
                    }

                    if (original != null && original.Size != Size.Empty)
                    {
                        try
                        {
                            int newWidth = (int)Math.Round((double)Image_Size.Height / ValueBack.Get_CInteger(original.Height) * ValueBack.Get_CInteger(original.Width) - 4.0);
                            int newHeight = Image_Size.Height - 4;

                            Bitmap bitmap = new(original, new Size(newWidth, newHeight));

                            if (Model_Class.Pro_Model_Stream_Record != null)
                            {
                                try
                                {
                                    using Graphics graphics = Graphics.FromImage(bitmap);
                                    using Pen pen = new Pen(new SolidBrush(Color.IndianRed), 2f);
                                    graphics.DrawRectangle(pen, 0, 0, bitmap.Width - 1, bitmap.Height - 1);
                                }
                                catch (Exception ex)
                                {
                                    Parameter.Error_Message(ex, "Class_Model_Preview_Image_Load.Streamshow.Preview_Image");
                                }
                            }

                            image = bitmap;
                        }
                        catch (Exception ex)
                        {
                            Parameter.Error_Message(ex, "Class_Model_Preview_Image_Load.Streamshow.Preview_Image");
                        }
                    }

                    original?.Dispose();
                }
                catch (Exception ex)
                {
                    Parameter.Error_Message(ex, "Streamshow.Image");
                }
            }

            return image;
        }
    }
}
