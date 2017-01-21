using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

public class ThumbnailHelper
{
    internal string _sourceimagepath;
    internal int _thumbnailImageHeight;
    internal string _thumbnailimagepath;
    internal int _thumbnailimagewidth;
    internal readonly string AllowExt = ".jpe|.jpeg|.jpg|.gif|.png|.tif|.tiff|.bmp";
    internal int diaphaneity;
    private string filePath = "";
    private static Hashtable htmimes = new Hashtable();
    internal float imageDeaphaneity;
    private string message = "";

    internal bool CheckValidExt(string sExt)
    {
        string[] strArray = this.AllowExt.Split(new char[] { '|' });
        foreach (string str in strArray)
        {
            if (str.ToLower() == sExt)
            {
                return true;
            }
        }
        return false;
    }

    public bool ToThumbnailImage()
    {
        if (this.SourceImagePath.ToString() == string.Empty)
        {
            throw new NullReferenceException("SourceImagePath is null!");
        }
        string sExt = this.SourceImagePath.Substring(this.SourceImagePath.LastIndexOf(".")).ToLower();
        if (!this.CheckValidExt(sExt))
        {
            this.message = "文件格式不正确,支持的格式有[ " + this.AllowExt + " ]";
            return false;
        }
        Image image = Image.FromFile(this.SourceImagePath, false);
        int width = image.Width;
        int height = image.Height;
        int target_width = ThumbnailImageWidth;
        int target_height = ThumbnailImageHeight;
        int new_width, new_height;
        System.Drawing.Image final_image;
        System.Drawing.Graphics graphics;
        if (target_height != 0 && target_width != 0)
        {
            float target_ratio = (float)target_width / (float)target_height;
            float image_ratio = (float)width / (float)height;

            if (target_ratio > image_ratio)
            {
                new_height = target_height;
                new_width = (int)Math.Floor(image_ratio * (float)target_height);
            }
            else
            {
                new_height = (int)Math.Floor((float)target_width / image_ratio);
                new_width = target_width;
            }

            new_width = new_width > target_width ? target_width : new_width;
            new_height = new_height > target_height ? target_height : new_height;


            final_image = new System.Drawing.Bitmap(target_width, target_height);
            graphics = System.Drawing.Graphics.FromImage(final_image);

            graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.White), new System.Drawing.Rectangle(0, 0, target_width, target_height));

            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            int paste_x = (target_width - new_width) / 2;
            int paste_y = (target_height - new_height) / 2;
            graphics.DrawImage(image, paste_x, paste_y, new_width, new_height);

        }
        else
        {
            final_image = new System.Drawing.Bitmap(width, height);
            graphics = System.Drawing.Graphics.FromImage(final_image);
            graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.White), new System.Drawing.Rectangle(0, 0, target_width, target_height));
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            int paste_x = 0;
            int paste_y = 0;
            graphics.DrawImage(image, paste_x, paste_y, width, height);

        }

        try
        {
            string str2 = (this.ThumbnailImagePath == null) ? this.SourceImagePath : this.ThumbnailImagePath;
            EncoderParameters ep = new EncoderParameters();
            ep.Param[0] = new EncoderParameter(Encoder.Quality, (long)90);

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            ImageCodecInfo ici = null;

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.MimeType == "image/jpeg")
                    ici = codec;
            }

            final_image.Save(str2, ici, ep);
            return true;
        }
        catch (Exception exception)
        {
            this.message = exception.Message;
        }
        finally
        {
            image.Dispose();
            final_image.Dispose();
            graphics.Dispose();
        }
        return false;
    }

    public int Diaphaneity
    {
        get
        {
            return this.diaphaneity;
        }
        set
        {
            this.diaphaneity = value;
        }
    }

    public string FilePath
    {
        get
        {
            return this.filePath;
        }
        set
        {
            this.filePath = value;
        }
    }

    public float ImageDeaphaneity
    {
        get
        {
            return this.imageDeaphaneity;
        }
        set
        {
            this.imageDeaphaneity = value;
        }
    }

    public string Message
    {
        get
        {
            return this.message;
        }
    }

    public string SourceImagePath
    {
        get
        {
            return this._sourceimagepath;
        }
        set
        {
            this._sourceimagepath = value;
        }
    }

    public int ThumbnailImageHeight
    {
        get
        {
            return this._thumbnailImageHeight;
        }
        set
        {
            this._thumbnailImageHeight = value;
        }
    }

    public string ThumbnailImagePath
    {
        get
        {
            return this._thumbnailimagepath;
        }
        set
        {
            this._thumbnailimagepath = value;
        }
    }

    public int ThumbnailImageWidth
    {
        get
        {
            return this._thumbnailimagewidth;
        }
        set
        {
            this._thumbnailimagewidth = value;
        }
    }
}


