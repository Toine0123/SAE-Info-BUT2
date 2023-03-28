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
        private int[] m_price;
        private int m_level;
        private unsafe Pion m_proprio;
        private Color m_color;
        private int m_pos;



        /*----------constructeur-------*/
        public Case()
        {
            m_price = new int[1];
            m_level = 0;
            m_type = 1;
            m_price[0] = 0;
        }
        public Case(int type, int nbCase)
        {
            m_type = type;
            m_price = new int[1];
            switch (m_type)
            {
                case Type.COMPAGNIE:
                    m_price = new int[2];
                    m_color = Color.AliceBlue;
                    m_price[0] = 150;
                    break;
                case Type.EVENEMENT:
                    m_color = Color.Violet;
                    m_price[0] = 0;
                    break;
                default:
                    m_color = Color.Gray;
                    m_price[0] = 0;
                    break;
            }
            Location = Position(nbCase);
            Size = new Size(70, 70);
            Enabled = true;
            Visible = true;
            Panel1.BackColor = m_color;
        }
        /*-----------get/set----------*/
        public int GetTyp() { return m_type; }
        public int GetPrice() { return m_price[m_level]; }
        public int[] GetTabPrice() { return m_price; }
        public int GetLevel() { return m_level; }
        public void SetType(int typ) { m_type = typ; }
        public void SetPrice(int[] pric) { m_price = pric; }
        public void SetLevel(int lvl) { m_level = lvl; }
        public void setProprio(Pion acheteur)
        {
            m_proprio = acheteur;
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
            str += "\nis buyable = " + isBuyable();
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
            int lvl = 0;
            if (m_type == Type.GARE)
            {
                Case[] temp = new Case[4];
                for (int i = 0; i < m_proprio.GetPossedees().Length; i++)
                {
                    if (m_proprio.GetPossedees()[i] != null && m_proprio.GetPossedees()[i].m_type == Type.GARE)
                    {
                        temp[lvl] = m_proprio.GetPossedees()[i];
                        lvl++;
                    }
                }
                for(int i =0; i < lvl; i++)
                {
                    temp[i].m_level = lvl;
                }
            }
            else { m_level = 1; }
            Panel2.BackColor = owner.BackColor;
        }

        public void Loose()
        {
            m_proprio = null;
            SetLevel(0);
            Panel2.BackColor = Color.White;
        }

        /*------------héritage----------*/
        public class Propriete : Case
        {
            /**----------constructeur-------**/
            public Propriete(Color color, int nbCase)
            {
                m_level = 0;
                m_type = Type.PROPRIETE;
                m_price = new Prix().Give(nbCase);
                m_color = color;
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
                m_level = 0;
                m_type = Type.GARE;
                m_price = new int[] { 200, 25, 50, 100, 200 };
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
                m_price = new int[1];
                m_type = Type.TAXES;
                m_price[0] = price;
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

    class Prix
    {
        public int[] Give(int pos)
        {
            switch (pos)
            {
                case 1:
                    return new int[] { 60, 2, 10, 30 };
                case 3:
                    return new int[] { 60, 4, 20, 60 };
                case 6:
                    return new int[] { 100, 6, 30, 90 };
                case 8:
                    return new int[] { 100, 6, 30, 90 };
                case 9:
                    return new int[] { 120, 8, 40, 100 };
                case 11:
                    return new int[] { 140, 10, 50, 150 };
                case 13:
                    return new int[] { 140, 10, 50, 150 };
                case 14:
                    return new int[] { 160, 12, 60, 180 };
                case 16:
                    return new int[] { 180, 14, 70, 200 };
                case 18:
                    return new int[] { 180, 14, 70, 200 };
                case 19:
                    return new int[] { 200, 16, 80, 220 };
                case 21:
                    return new int[] { 220, 18, 90, 250 };
                case 23:
                    return new int[] { 220, 18, 90, 250 };
                case 24:
                    return new int[] { 240, 20, 10, 300 };
                case 26:
                    return new int[] { 260, 22, 110, 330 };
                case 27:
                    return new int[] { 260, 22, 110, 330 };
                case 29:
                    return new int[] { 280, 24, 120, 360 };
                case 31:
                    return new int[] { 300, 26, 130, 390 };
                case 32:
                    return new int[] { 300, 26, 130, 3900 };
                case 34:
                    return new int[] { 320, 28, 150, 450 };
                case 37:
                    return new int[] { 350, 35, 175, 500 };
                case 39:
                    return new int[] { 400, 50, 200, 600 };
                default:
                    return new int[] { 0, 0, 0, 0 };

            }
        }
        /*,
        ,
        */
    }
}