using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAE_INFO
{
    class Pion : PictureBox
    {   /*variables*/
        private int m_argent;
        private string m_name;
        private int m_class;
        private bool m_Isloose;
        private int m_position;
        private int m_couldown;
        private Color m_color;
        private bool m_speIsUsable;
        private Case[] m_poseder;

        /*méthodes de pion*/
        //constructeur 
        public Pion()
        {
            m_argent = 1500;
            m_class = 0;
            m_Isloose = false;
            m_position = 0;
            m_couldown = 0;
            m_name = "";
            m_color = Color.Black;
        }
        public Pion(string name, Color color, Case depart)
        {
            m_argent = 1500;
            m_class = 0;
            m_Isloose = false;
            m_position = 0;
            m_couldown = 0;
            m_name = name;
            m_color = color;
            Location = depart.Location;
            Image = Image.FromFile("Pion.png");
            Size = new Size(depart.Size.Width - 10, depart.Size.Height);
            BackColor = color;
            SizeMode = PictureBoxSizeMode.Zoom;
            m_poseder = new Case[28];
        }

        //méthode loose verifie si un pions est perdue ou non 
        public void loose()
        {
            if (m_argent <= 0)
            {
                m_Isloose = true;
            }
            else
            {
                m_Isloose = false;
            }

        }
        public bool getIsLoose()
        {
            return m_Isloose;
        }
        //méthode usable verifie si on peux utilisé la compétence 
        public void Usable()
        {
            if (m_couldown == 0)
            {
                m_speIsUsable = true;
            }
            else if (m_couldown == 1)
            {
                m_speIsUsable = false;
            }
        }



        /*--méthodes pour m_argent--*/
        //methode ajouter argent 
        public void AddMoney(int money)
        {
            m_argent = m_argent + money;
        }
        //méthode enlevé argent 
        public void RemoveMoney(int money)
        {
            m_argent = m_argent - money;
        }
        //méthode get money
        public int GetMoney()
        {
            return m_argent;
        }
        //Méthode d'achat de case
        public bool Buy(Case caseIn)
        {
            string temp;
            switch (caseIn.GetTyp())
            {
                case Type.COMPAGNIE:
                    temp = " compagnie ";
                    break;
                case Type.GARE:
                    temp = " gare ";
                    break;
                case Type.PROPRIETE:
                    temp = " propriété ";
                    break;
                default:
                    return false;
            }

            if (MessageBox.Show("Voulez vous acheter cette" + temp + "pour " + caseIn.GetPrice(),
                            "Achat de ", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                AddMoney(-caseIn.GetPrice());
                caseIn.Buy(this);
                int i = 0;
                while(m_poseder[i] != null) { i++; }
                m_poseder[i] = caseIn;
                return true;
            }
            else return false;
        }



        /*--méthodes pour m_position--*/
        //méthode move
        public void Move(int nb_case, Case[] tabCase)
        {
            //déplacement
            if ((m_position + nb_case) > 39)
            {
                nb_case -= 39 - m_position;
                m_position = 0;
                AddMoney(200);
            }
            m_position += nb_case;

            Location = tabCase[m_position].Location;
            //Paye si il faut
            if (tabCase[m_position].isBuyable() == false)
            {
                AddMoney(-tabCase[m_position].GetPrice());
                if (tabCase[m_position].GetTyp() == Type.PROPRIETE)
                {
                    tabCase[m_position].getProprio().AddMoney(tabCase[m_position].GetPrice());
                }
            }
        }
        //méthode back
        public void Back(int nb_case)
        {
            m_position = m_position - nb_case;
        }
        //méthode get position 
        public int GetPosition()
        {
            return m_position;
        }



        /*--méthode pour m_name--*/
        //méthode set Name 
        public void SetName(string input_name)
        {
            m_name = input_name;
        }
        //methode get name 
        public string GetName()
        {
            return m_name;
        }



        /*--méthode pour m_classe--*/
        //méthode
        public int GetClass()
        {
            return m_class;
        }



        /*--méthode pour m_couldown--*/
        //méthode addCouldown 
        public void AddCouldown(int input_a)
        {
            m_couldown = m_couldown + input_a;
        }
        //méthode removeCouldown
        public void RemoveCouldown(int input_a)
        {
            m_couldown = m_couldown - input_a;
        }
        //méthode getCouldown 
        public int GetCouldown()
        {
            return m_couldown;
        }


        /*************/
        /* fonctions */
        /*************/

        public Case[] GetPossedees()
        {
            return m_poseder;
        }

        public Color GetColor() { return m_color; }


    }
}
