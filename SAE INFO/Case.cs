using SAE_INFO;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAE_INFO
{

    class Case : SplitContainer
    {
        /*-----------variables--------*/
        private
            int type;
        int price;
        int level;
        bool isBought;
        unsafe Pion proprio;


        /*----------constructeur-------*/
        public Case()
        {
            this.level = 0;
            this.type = 1;
            this.isBought = false;
            this.price = 0;
        }
        public Case(int type, int nbCase)
        {
            this.type = type;
            this.isBought = false;
            if (type == Type.COMPAGNIE)
            {
                this.price = 150;
            }
            Location = Position(nbCase);
            Size = new Size(70, 70);
            Enabled = true;
            Visible = true;
            Panel1.BackColor = Color.Gray;
        }
        /*-----------get/set----------*/
        public int GetTyp() { return type; }
        public int GetPrice() { return price; }
        public int GetLevel() { return level; }
        public bool GetIsBought(){ return isBought; }
        public void SetType(int typ) { type = typ; }
        public void SetPrice(int pric) { price = pric; }
        public void SetLevel(int lvl) { level = lvl; }
        public void setProprio(Pion acheteur)
        {
            proprio = acheteur;
            isBought = true;
        }
        public Pion getProprio()
        {
            return proprio;
        }
        /*-----------méthode----------*/


        public bool isBuyable()
        {
            if (level != 0) return false;
            else if (type == Type.COMPAGNIE || type == Type.GARE || type == Type.PROPRIETE) return true;
            else return false;
        }

        public string Afficher()
        {
            string str = "Type = ";
            switch (type)
            {
                case Type.COMPAGNIE:
                    str += "compagnie";
                    break;
                case Type.GARE:
                    str += "gare";
                    break;
                case Type.PROPRIETE:
                    str += "propriete";
                    break;
            }
            str += "\nPrix = " + price;
            str += "\nlevel = " + level;
            str += "\nis bought = " + isBought;
            str += "\nProprio :";
            if (proprio != null)
            {
                str += "\n Nom = " + proprio.GetName();
                str += "\n Argent = " + proprio.GetMoney();
                str += "\n position = " + proprio.GetPosition();
            }
            else str += "pas encore défini";
            return str;
        }

        private Point Position(int nbCase) {
            int width = 80;
            int height = 80;
            int x_max = width * 10 + 20;
            int y_max = height * 10 + 20;
            int x_min = x_max - 10 * width;
            int y_min = y_max - 10 * height;
            if (nbCase < 11)
            {
                Orientation = Orientation.Horizontal;
                return new Point(x_max - nbCase * width, y_max);
            }
            if (nbCase < 21)
            {
                nbCase -= 10;
                return new Point(x_min, y_max - nbCase * height);
            }
            if (nbCase < 31)
            {
                nbCase -= 20;
                Orientation = Orientation.Horizontal;
                return new Point( x_min + nbCase * width, y_min);
            }
            if (nbCase < 41)
            {
                nbCase -= 30;
                return new Point(x_max, y_min + nbCase * height);
            }
            else { return new Point(0, 0); }
        }

        public void Buy(Pion owner)
        {
            setProprio(owner);
            SetLevel(1);
            Panel2.BackColor = owner.BackColor;
        }
        /*------------héritage----------*/
        public class Propriete : Case
        {
            /**-----------variables--------**/
            public Color color;
            /**----------constructeur-------**/
            public Propriete(int price, Color color, int nbCase)
            {
                this.level = 0;
                this.type = Type.PROPRIETE;
                this.isBought = false;
                this.price = price;
                this.color = color;
                Location = Position(nbCase);
                Size = new Size(70, 70);
                Enabled = true;
                Visible = true;
                Panel1.BackColor = color;
            }

        }

        public class Gare : Case
        {
            /**----------constructeur-------**/
            public Gare(int nbCase)
            {
                this.level = 0;
                this.type = Type.GARE;
                this.price = 200;
                this.isBought = false;
                Location = Position(nbCase);
                Size = new Size(70, 70);
                Enabled = true;
                Visible = true;
                Panel1.BackColor = Color.Black;
            }
        }

        public class Taxes : Case
        {
            /**----------constructeur-------**/
            public Taxes(int price, int nbCase)
            {
                this.type = Type.TAXES;
                this.price = price;
                isBought = false;
                level = 0;
                Location = Position(nbCase);
                Size = new Size(70, 70);
                Enabled = true;
                Visible = true;
                Panel1.BackColor = Color.BlueViolet;
            }
        }
    }



    //********************************************************************************************************//




    class Type
    {
        public const int GARE = 1;
        public const int EVENEMENT = 2;
        public const int PARCGRATUIT = 3;
        public const int PRISON = 4;
        public const int ALLERENPRISON = 5;
        public const int DEPART = 6;
        public const int COMPAGNIE = 7;
        public const int TAXES = 8;
        public const int PROPRIETE = 0;
    }

    class ColorC
    {
        public Color MARRON = Color.FromArgb(150, 83, 56);
        public Color CYAN = Color.FromArgb(168, 225, 254);
        public Color ROSE = Color.FromArgb(217, 58, 250);
        public Color ORANGE = Color.FromArgb(248, 147, 31);
        public Color ROUGE = Color.FromArgb(235, 29, 33);
        public Color JAUNE = Color.FromArgb(255, 240, 0);
        public Color VERT = Color.FromArgb(33, 177, 92);
        public Color BLEU = Color.FromArgb(0, 114, 184);
    }
}