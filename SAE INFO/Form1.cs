﻿using System;
using System.Drawing;
using System.Windows.Forms;

/*************Problèmes**********
 *
 ********************************/
namespace SAE_INFO
{
    public partial class Form1 : Form
    {
        static Case[] plateau = new Case[40] {new Case(Type.DEPART, 0),
                                       new Case.Propriete(new ColorC().MARRON, 1),
                                       new Case(Type.EVENEMENT, 2),
                                       new Case.Propriete(new ColorC().MARRON, 3),
                                       new Case.Taxes(200, 4),
                                       new Case.Gare(5),
                                       new Case.Propriete(new ColorC().CYAN, 6),
                                       new Case(Type.EVENEMENT, 7),
                                       new Case.Propriete(new ColorC().CYAN, 8),
                                       new Case.Propriete(new ColorC().CYAN, 9),
                                       new Case(Type.PRISON, 10),
                                       new Case.Propriete(new ColorC().ROSE, 11),
                                       new Case(Type.COMPAGNIE, 12),
                                       new Case.Propriete(new ColorC().ROSE, 13),
                                       new Case.Propriete(new ColorC().ROSE, 14),
                                       new Case.Gare(15),
                                       new Case.Propriete(new ColorC().ORANGE, 16),
                                       new Case(Type.EVENEMENT, 17),
                                       new Case.Propriete(new ColorC().ORANGE, 18),
                                       new Case.Propriete(new ColorC().ORANGE, 19),
                                       new Case(Type.PARCGRATUIT, 20),
                                       new Case.Propriete(new ColorC().ROUGE, 21),
                                       new Case(Type.EVENEMENT, 22),
                                       new Case.Propriete(new ColorC().ROUGE, 23),
                                       new Case.Propriete(new ColorC().ROUGE, 24),
                                       new Case.Gare(25),
                                       new Case.Propriete(new ColorC().JAUNE, 26),
                                       new Case.Propriete(new ColorC().JAUNE, 27),
                                       new Case(Type.COMPAGNIE, 28),
                                       new Case.Propriete(new ColorC().JAUNE, 29),
                                       new Case(Type.ALLERENPRISON, 30),
                                       new Case.Propriete(new ColorC().VERT, 31),
                                       new Case.Propriete(new ColorC().VERT, 32),
                                       new Case(Type.EVENEMENT, 33),
                                       new Case.Propriete(new ColorC().VERT, 34),
                                       new Case.Gare(35),
                                       new Case(Type.EVENEMENT, 36),
                                       new Case.Propriete(new ColorC().BLEU, 37),
                                       new Case.Taxes(100, 38),
                                       new Case.Propriete(new ColorC().BLEU, 39)};
        static Pion[] joueurs = new Pion[4] {new Pion("joueur 1", Color.Black, plateau[0]),
                                      new Pion("joueur 2", Color.Blue, plateau[0]),
                                      new Pion("balkani", Color.Green, plateau[0]),
                                      new Pion("joueur 4", Color.Red, plateau[0])};
        Afficheur afficheur = new Afficheur(joueurs);
        int resdes;
        int joueur = 0;

        

            

        public Form1()
        {
            for(int i = 0; i < 40; i++)
            {
                plateau[i].setPos(i);
                plateau[i].Click += Case_Click;
                Controls.Add(plateau[i]);
            }
            for(int i = 0; i < 4; i++)
            {
                Controls.Add(joueurs[i]);
                joueurs[i].BringToFront();
            }
            Controls.Add(afficheur);
            InitializeComponent();
        }

        private void Case_Click(object sender, EventArgs e)
        {
            affichageCase(((Case)sender).getPos());
        }

        private void buttonDes_Click(object sender, EventArgs e)
        {
            while (joueurs[joueur].IsLoose())
            {
                joueur ++;
                if (joueur > 3) joueur = 0;
            }
            label6.Text = "tour de joueur " + (joueur + 1) + "\n";
            /*gestion du lancer de dé et avancer sur le plateaux*/
            Random rand = new Random();
            do
            {
                resdes = rand.Next(12);
            } while (resdes < 2 || resdes > 12);
            label1.Text = "Valeur du dés = " + resdes.ToString();
            label1.Text += "\nArgent avant déplacement = " + joueurs[joueur].GetMoney();
            joueurs[joueur].Move(resdes, plateau);
            label1.Text += "\nArgent après déplacement = " + joueurs[joueur].GetMoney();
            label1.Text += '\n' + "Numéro de Case = " + joueurs[joueur].GetPosition();

            affichageCase(joueurs[joueur].GetPosition());
            //achat de la propriété
            if (plateau[joueurs[joueur].GetPosition()].isBuyable() == true)
            {
                joueurs[joueur].Buy(plateau[joueurs[joueur].GetPosition()]);
            }

            label1.Text += "\nArgent en fin de tour = " + joueurs[joueur].GetMoney();
            joueurs[joueur].IsLoose();
            joueur += 1;
            if(joueur>3) joueur = 0;
            label6.Text += "joueur suivant : " + (joueur + 1);
            afficheur.MAJ();
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer13_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer3_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void affichageCase(int pos)
        {
            /*affichage en fonction de la case*/
            //reset des label en vue de l'affichage
            label3.Text = "";
            label3.Font = new Font(label3.Font, FontStyle.Regular);
            label4.Text = "";
            label4.Font = new Font(label4.Font, FontStyle.Regular);
            label5.Text = "";
            label5.Font = new Font(label5.Font, FontStyle.Regular);
            splitContainer1.Panel1.BackColor = Color.Gray;
            //action en fonction de ce qui c'est passer
            switch (plateau[pos].GetTyp())
            {
                case Type.ALLERENPRISON:
                    label2.Text = "Aller en prison";
                    label3.Text = "Aller en prison, ne passer pas par la case départ";
                    break;
                case Type.COMPAGNIE:
                    label2.Text = "Compagnie";
                    break;
                case Type.DEPART:
                    label2.Text = "Départ";
                    break;
                case Type.EVENEMENT:
                    label2.Text = "Evenement";
                    break;
                case Type.GARE:
                    label2.Text = "Gare";
                    splitContainer1.Panel1.BackColor = Color.FromArgb(0, 0, 0);
                    break;
                case Type.PARCGRATUIT:
                    label1.Text = "Parc gratuit";
                    break;
                case Type.PRISON:
                    label1.Text = "Prison";
                    break;
                case Type.PROPRIETE:
                    label2.Text = "Propriété";
                    label3.Text = "Terrain nu     " + plateau[pos].GetTabPrice()[1];
                    label4.Text = "1 maison     " + (plateau[pos].GetTabPrice()[2]);
                    label5.Text = "2 maisons     " + (plateau[pos].GetTabPrice()[3]);
                    label5.Text += "\nIsBuildable = " + ((Case.Propriete)plateau[pos]).IsBuildable();
                    switch (plateau[pos].GetLevel())
                    {
                        case 1:
                            label3.Font = new Font(label3.Font, FontStyle.Bold);
                            break;
                        case 2:
                            label4.Font = new Font(label4.Font, FontStyle.Bold);
                            break;
                        case 3:
                            label5.Font = new Font(label5.Font, FontStyle.Bold);
                            break;
                    }
                    splitContainer1.Panel1.BackColor = plateau[pos].GetColor();
                    break;
                case Type.TAXES:
                    label2.Text = "Taxe";
                    label3.Text = "Prix = " + plateau[pos].GetPrice();
                    break;
            }
        }
    }
}
