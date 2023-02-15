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
        private int m_type;
        private int m_price;
        private int m_level;
        private bool m_isBought;
        private unsafe Pion m_proprio;
        private Color m_color;
        private int m_pos;
        


        /*----------constructeur-------*/
        public Case()
        {
            m_level = 0;
            m_type = 1;
            m_isBought = false;
            m_price = 0;
        }
        public Case(int type, int nbCase)
        {
            m_type = type;
            switch (m_type)
            {
                case Type.COMPAGNIE:
                    m_color = Color.AliceBlue;
                    m_price = 150;
                    break;
                case Type.EVENEMENT:
                    m_color = Color.Violet;
                    break;
                default:
                    m_color = Color.Gray;
                    break;
            }
            m_isBought = false;
            Location = Position(nbCase);
            Size = new Size(70, 70);
            Enabled = true;
            Visible = true;
            Panel1.BackColor = m_color;
        }
        /*-----------get/set----------*/
        public int GetTyp() { return m_type; }
        public int GetPrice() { return m_price; }
        public int GetLevel() { return m_level; }
        public bool GetIsBought(){ return m_isBought; }
        public void SetType(int typ) { m_type = typ; }
        public void SetPrice(int pric) { m_price = pric; }
        public void SetLevel(int lvl) { m_level = lvl; }
        public void setProprio(Pion acheteur)
        {
            m_proprio = acheteur;
            m_isBought = true;
        }
        public Pion getProprio()
        {
            return m_proprio;
        }
        public Color GetColor() { return m_color; }
        public int getPos() { return m_pos; }
        public void setPos(int pos) { m_pos = pos; }
        /*-----------méthode----------*/


        public bool isBuyable()
        {
            if (m_level != 0) return false;
            else if (m_type == Type.COMPAGNIE || m_type == Type.GARE || m_type == Type.PROPRIETE) return true;
            else return false;
        }

        public string Afficher()
        {
            string str = "Type = ";
            switch (m_type)
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
            str += "\nPrix = " + m_price;
            str += "\nlevel = " + m_level;
            str += "\nis bought = " + m_isBought;
            str += "\nProprio :";
            if (m_proprio != null)
            {
                str += "\n Nom = " + m_proprio.GetName();
                str += "\n Argent = " + m_proprio.GetMoney();
                str += "\n position = " + m_proprio.GetPosition();
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
            /**----------constructeur-------**/
            public Propriete(int price, Color color, int nbCase)
            {
                this.m_level = 0;
                this.m_type = Type.PROPRIETE;
                this.m_isBought = false;
                this.m_price = price;
                this.m_color = color;
                Location = Position(nbCase);
                Size = new Size(70, 70);
                Enabled = true;
                Visible = true;
                Panel1.BackColor = color;
            }
            /**-----------fonctions------------**/
            public bool IsBuildable()
            {
                if (m_proprio == null) { return false; }
                else
                {
                    if (m_color == new ColorC().MARRON || m_color == new ColorC().BLEU)
                    {
                        for(int i = 0; i<m_proprio.GetPossedees().Length; i++)
                        {
                            if(m_proprio.GetPossedees()[i] != this && m_proprio.GetPossedees()[i] != null && GetColor() == m_proprio.GetPossedees()[i].GetColor())
                            {
                                return true;
                            }
                        }
                        return false;
                    }
                    else
                    {
                        for (int i = 0; i < m_proprio.GetPossedees().Length; i++)
                        {
                            if (m_proprio.GetPossedees()[i] != this && m_proprio.GetPossedees()[i] != null && GetColor() == m_proprio.GetPossedees()[i].GetColor())
                            {
                                for (int j = i+1; j < m_proprio.GetPossedees().Length; j++)
                                {
                                    if (m_proprio.GetPossedees()[j] != this && m_proprio.GetPossedees()[j] != null && GetColor() == m_proprio.GetPossedees()[j].GetColor())
                                    {
                                        return true;
                                    }
                                }
                                return false;
                            }
                        }
                        return false;
                    }
                }
            }
        }

        public class Gare : Case
        {
            /**----------constructeur-------**/
            public Gare(int nbCase)
            {
                this.m_level = 0;
                this.m_type = Type.GARE;
                this.m_price = 200;
                this.m_isBought = false;
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
                this.m_type = Type.TAXES;
                this.m_price = price;
                m_isBought = false;
                m_level = 0;
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