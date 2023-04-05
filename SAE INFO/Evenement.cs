using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE_INFO
{   
/// <summary>
/// class évènement permet de 
/// gerer nos évènements
/// </summary>

    class Evenement
    {
        // déclaration des variables 
        
        private int m_id_carte;
        private string m_text;
        private char m_type;


        //constructeur 1
        public Evenement() 
        {

            m_id_carte = 0;
            m_text = "texte par défaut";
            m_type = 'u';

        }

        //constructeur 2
        public Evenement(string text,char type, int id) 
        { 
        m_id_carte = id;
        m_text = text;
        m_type = type;
        
        }

        //méthode getteur 
        public string GetText() { return m_text; }
        public int GetId() { return m_id_carte; }
        public char GetType() { return m_type; }


        //méthode setteur
        public void SetText(string text) { m_text = text; }
        public void SetId(int id) { m_id_carte = id; }
        public void Settype(char type) { m_type = type; }


        //méthode pour connaitre la class 
        public string affichertype()
        {
            if(m_type == 'u') { return "valeur indéfinie"; } // class de base du constructeur 
            if(m_type == 'd') { return "carte déplacement"; } // class des cartes permettant d'avancer 
            if(m_type == 't') { return "carte téléportation"; } // carte permettant d'aller à un endroit spécifique 
            if (m_type == 'v') { return "carte argent"; } // class des cartes ou l'on doit dépenses de l'argent 
            else return "erreur de class";

        }




        //héritage pour le type téleportation
        public class teleportation :Evenement // class des évènement qui se téléporte

        {
            //variable 
            private int m_position;
            
            //constructeur 1
             public teleportation()
            {

                m_id_carte = 0;
                m_text = "texte par défaut";
                m_type = 't';
                m_position = 0;

            }

            //constructeur 2
            public teleportation(string text, int id, int position)
            {
                m_id_carte = id;
                m_text = text;
                m_position=position;
                m_type = 't';

            }

            //méthode get / set 
            public int getposition() { return m_position; }
            public void setposition(int position) { m_position = position; }

            //méthode goto
            public void AllerVers(Pion joueur, Case[] tabcase)
            {
                int temp = m_position - joueur.GetPosition();
                joueur.Move(temp, tabcase);
            }
            }




        //héritage pour le type déplacement
        public class deplacement :Evenement
        {
            //variable
            private int m_move;

            //constructeur 1
            public deplacement()
            {

                m_id_carte = 0;
                m_text = "texte par défaut";
                m_type = 't';
                m_move = 0;

            }

            // constructeur 2
            public deplacement(string text, int id, int move)
            {
                m_id_carte = id;
                m_text = text;
                m_move = move;
                m_type = 'd';

            }
            //méthode get / set     
            public int GetMove() { return m_move; }
            public void SetMove(int move) { m_move = move; }

        }

        //héritage pour le type argent
        public class valeur : Evenement
        {
            //variable
            private int m_valeur;

            //constructeur 1
            public valeur()
            {

                m_id_carte = 0;
                m_text = "texte par défaut";
                m_type = 'v';
                m_valeur = 0;

            }

            // constructeur 2
            public valeur(string text, int id, int valeur)
            {
                m_id_carte = id;
                m_text = text;
                m_valeur = valeur;
                m_type = 'v';

            }
            //méthode get / set     
            public int GetVal() { return m_valeur; }
            public void SetVal(int valeur) { m_valeur = valeur; }

            //méthode spécifique 
            //méthode de gestion de l'évènement anniversaire
            public void anniversaire(Pion[] tabj, int no_joueur) 
            {
                for (int i = 0; i < 4; i++)
                {
                    if (i == no_joueur)
                    {
                        tabj[i].AddMoney(30);
                    }
                    else
                    {
                        tabj[i].AddMoney(-10);
                    }
                }

            }
            //méthode de gestion de l'évènement élection du président
            public  void president(Pion[] tabj,int no_joueur) 
            {
                for (int i = 0; i < 4; i++)
                {
                    if (i == no_joueur)
                    {
                        tabj[i].AddMoney(-150);
                    }
                    else
                    {
                        tabj[i].AddMoney(50);
                    }
                }
                
            } 

        }

    }
}
