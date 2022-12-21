﻿using SAE_INFO;
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

    class Case
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
        public Case(int type)
            {
                this.type = type;
            this.isBought = false;
        }
        /*-----------get/set----------*/
        public int GetTyp() { return type; }
        public int GetPrice() { return price; }
        public int GetLevel() { return level; }
        public bool GetIsBought() 
            {
                return isBought; 
            }
        public void SetType(int typ) { type = typ; }
        public void SetPrice(int pric) { price = pric; }
        public void SetLevel(int lvl) { level = lvl; }
        public void setProprio(Pion acheteur)
        {
            proprio = acheteur;
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



        /*------------héritage----------*/
        public class Propriete : Case
        {
            /**-----------variables--------**/
            public Color color;
            /**----------constructeur-------**/
            public Propriete(int price, Color color)
            {
                this.level = 0;
                this.type = Type.PROPRIETE;
                this.isBought = false;
                this.price = price;
                this.color = color;
            }
            
        }

        public class Gare : Case
        {
            /**----------constructeur-------**/
            public Gare()
            {
                this.level = 0;
                this.type = Type.GARE;
                this.price = 200;
                this.isBought = false;
            }
        }

        public class Taxes : Case
        {
            /**----------constructeur-------**/
            public Taxes(int price)
            {
                this.type = Type.TAXES;
                this.price = price;
            }
        }
    }

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

    class Pion
    {   /*variables*/
        private
            int m_argent;
            string m_name;
            int m_class;
            bool m_Isloose;
            int m_position;
            int m_couldown;
            Color m_color;
            bool m_IsUsable;

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

        //méthode loose verifie si un pions est perdue ou non 
        public void loose()
        {
            if (m_argent == 0)
            {
                m_Isloose = true;
            }
            else
            {
                m_Isloose = false;
            }

        }
        //méthode usable verifie si on peux utilisé la compétence 
        public void Usable()
        {
            if (m_couldown == 0)
            {
                m_IsUsable = true;
            }
            else if (m_couldown == 1)
            {
                m_IsUsable = false;
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
                    break ;
                default:
                    return false;
            }

            if (MessageBox.Show("Voulez vous acheter cette" + temp + "pour " + caseIn.GetPrice(),
                            "Achat de ", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                AddMoney(-caseIn.GetPrice());
                caseIn.setProprio(this);
                caseIn.SetLevel(1);
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
                nb_case -= 40 - m_position;
                m_position = 0;
                AddMoney(200);
            }
            m_position += nb_case;

            //Paye si il faut
            if(tabCase[m_position].isBuyable() == false)
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
    }
}